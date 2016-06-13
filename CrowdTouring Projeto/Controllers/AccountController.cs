using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CrowdTouring_Projeto.Models;
using System.Web.Security;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.EntityFramework;
using CrowdTouring_Projeto.ViewModel;

namespace CrowdTouring_Projeto.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager; 
  
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var user = model.Nome;
            var utilizador = db.Users.Where(d => model.Nome == d.Nome).First();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Nome, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    utilizador.UltimaSessao++;
                    db.SaveChanges();
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var Roles = db.Roles.Where(d => d.Name != "Admin").ToList();
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");
            ViewBag.Tags = db.Tags.ToList();
            return View();
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string Nome)
        {

            var user = Membership.GetUser(Nome);

            return Json(user == null);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var Roles = db.Roles.Where(d => d.Name != "Admin").ToList();
            ViewBag.Roles = new SelectList(Roles, "Id", "Name");
                if (ModelState.IsValid)
            {               
                var user = new ApplicationUser { UserName = model.Nome, Email = model.Email, Sobre = model.Sobre,DataNascimento = model.DataNascimento, Telemóvel = model.Telemóvel, Nome = model.Nome, TipoUtilizador = model.TipoUtilizador, Notificacao = model.Notificacao};
                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {

                    //Comment the following line to prevent log in until the user is confirmed:
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Account confirmation");

                    // Uncomment to debug locally 
                    // TempData["ViewBagLink"] = callbackUrl;

                    ViewBag.errorMessage = "Please confirm the email was sent to you.";
                    return View("MostrarMensagem");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult IsEmailAvailable(string email, string initialEmail)
        {
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditarUtilizador()
        {
            var id = User.Identity.GetUserId();

            // Fetch the userprofile
            ApplicationUser user = db.Users.Include(u => u.Tags).FirstOrDefault(u => u.Id.Equals(id));
            preencherTagUtilizador(user);
            var role = (from s in db.Roles where s.Id == user.TipoUtilizador select s.Name).First();

            // Construct the viewmodel
            EditarUtilizadorViewModel model = new EditarUtilizadorViewModel();
            model.Utilizador = user.Nome;
            model.Email = user.Email;
            model.TipoUtilizador = role;
            model.DataNascimento = user.DataNascimento;
            model.Empresa = user.empresa;
            model.Sobre = user.Sobre;
            model.Iban = user.Iban;
            model.Telemóvel = user.Telemóvel;
            model.Website = user.website;
            model.ImagePath = user.ImagePath ;

            return View(model);
        }

        private void preencherTagUtilizador(ApplicationUser user)
        {
            var tags = db.Tags;
            var tagsUsers = new HashSet<int>(user.Tags.Select(a => a.Id));
            var viewModel = new List<TagUtilizador>();
            foreach (var tag in tags)
            {
                viewModel.Add(new TagUtilizador
                {
                    TagId= tag.Id,
                    Nome = tag.NomeTag,
                    Seleccionado = tagsUsers.Contains(tag.Id)
                });
            }
            ViewBag.Tags = viewModel;
        }


        public PartialViewResult _InformacaoBasica()
        {
            var id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include(u => u.Tags).FirstOrDefault(u => u.Id.Equals(id));
            preencherTagUtilizador(user);
            var role = (from s in db.Roles where s.Id == user.TipoUtilizador select s.Name).First();

            EditarUtilizadorViewModel model = new EditarUtilizadorViewModel();
            model.Utilizador = user.Nome;
            model.Email = user.Email;
            model.ImagePath = user.ImagePath;
            model.DataNascimento = user.DataNascimento;
            model.Telemóvel = user.Telemóvel;
            model.Empresa = user.empresa;
            model.Iban = user.Iban;
            model.Website = user.website;
            model.TipoUtilizador = role;
      


            return PartialView("~/Views/PartialViews/InformacaoBasica.cshtml",model);
        }

        [HttpPost]
        public ActionResult _InformacaoBasica(EditarUtilizadorViewModel model, HttpPostedFileBase file)
        {
            var user = User.Identity.GetUserId();
            var utilizador = db.Users.Where(d => d.Id == user).First();
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    
                    var fileName = utilizador.Id + "_" + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + fileName);
                    model.ImagePath = fileName;
                }
                else
                {
                    var filename = utilizador.ImagePath;
                    model.ImagePath = filename;
                }
                utilizador.ImagePath = model.ImagePath;

            }
 

            utilizador.Telemóvel = model.Telemóvel;
            db.SaveChanges();
            return View("~/Views/Account/EditarUtilizador.cshtml");
        }

        public PartialViewResult _OutrasInformacoes()
        {
            var id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include(u => u.Tags).FirstOrDefault(u => u.Id.Equals(id));

            EditarUtilizadorViewModel model = new EditarUtilizadorViewModel();
            model.Empresa = user.empresa;
            model.Iban = user.Iban;
            model.Website = user.website;
            model.Sobre = user.Sobre;
            model.Notificacao = user.Notificacao;

            return PartialView("~/Views/PartialViews/OutrasInformacoes.cshtml", model);
        }



        public PartialViewResult _Seguranca()
        {
            return PartialView("~/Views/PartialViews/Seguranca.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> _Seguranca(Seguranca seguranca)
        {
            var userId = User.Identity.GetUserId();

            var utilizador = db.Users.Where(a => a.Id == userId).First();

            var user = await UserManager.FindAsync(utilizador.Nome, seguranca.Password);

            if (user == null)
            {
                ModelState.AddModelError("error", "Password Incorreta, Insira a sua antiga password");
            }
            if (seguranca.Password == seguranca.NovaPassword)
            {
                ModelState.AddModelError("error2", "Tem que adicionar uma password diferente da atual");
            }
            if (!ModelState.IsValid)
            {
     
                return View("~/Views/PartialViews/Seguranca.cshtml", seguranca);
            }

            var novaPassword = seguranca.NovaPassword;
            ApplicationUser cUser = UserManager.FindById(userId);
            String hashedNewPassword = UserManager.PasswordHasher.HashPassword(novaPassword);
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
            await store.SetPasswordHashAsync(cUser, hashedNewPassword);
            return View("~/Views/PartialViews/Seguranca.cshtml");


        }



        public PartialViewResult _Tags()
        {
            var id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Include(u => u.Tags).FirstOrDefault(u => u.Id.Equals(id));
            preencherTagUtilizador(user);

            return PartialView("~/Views/PartialViews/Tags.cshtml");
        }
        // POST: Desafios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> EditarUtilizador(EditarUtilizadorViewModel model, string[] selectedTag, HttpPostedFileBase file)
        {
            var id = User.Identity.GetUserId();
            // Get the userprofile
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id.Equals(id));

            if (ModelState.IsValid)
            {
               
                if (file != null)
                {
                    var fileName = id + "_" + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + fileName);
                    model.ImagePath = fileName;
                }
                else
                {
                    var filename = user.ImagePath;
                    model.ImagePath = filename;
                }
                user.ImagePath = model.ImagePath;


                atualizarUtilizadorTag(user, selectedTag);
                // Update fields
                user.DataNascimento = model.DataNascimento;
                user.empresa = model.Empresa;
                user.Sobre = model.Sobre;
                user.Iban = model.Iban;
                user.Telemóvel = model.Telemóvel;
                user.website = model.Website;

                var result =  UserManager.CheckPassword(user, model.Password);

                if (result)
                {
                    await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.Password, model.NovaPassword);
                }
                else
                {
                    TempData["CustomError"] = "A sua password antiga não está correta";
                }

                db.Entry(user).State = EntityState.Modified;

                db.SaveChanges();
                preencherTagUtilizador(user);
                return RedirectToAction("Index", "Home"); // or whatever
            }
            preencherTagUtilizador(user);
            var caminho = user.ImagePath;
            model.ImagePath = caminho;
            model.Utilizador = user.Nome;
            model.Email = user.Email;
            return View(model);
        }

        public ActionResult VisualizarPerfilUtilizador(string id)
        {
            var utilizador = db.Users.Where(u => u.UserName == id).First();
            var role = (from s in db.Roles where s.Id == utilizador.TipoUtilizador select s.Name).First();
            EditarUtilizadorViewModel PerfilUtilizador = new EditarUtilizadorViewModel();  // reaproveitar o modelview
            PerfilUtilizador.Utilizador = utilizador.UserName;
            PerfilUtilizador.Email = utilizador.Email;
            PerfilUtilizador.Telemóvel = utilizador.Telemóvel;
            DateTime reference = DateTime.Now;
            PerfilUtilizador.Idade = CalculaIdade(reference,utilizador.DataNascimento);
            PerfilUtilizador.TipoUtilizador = role;
            preencherTagUtilizador(utilizador);
            PerfilUtilizador.Sobre = utilizador.Sobre;
            PerfilUtilizador.Empresa = utilizador.empresa;
            PerfilUtilizador.Website = utilizador.website;
            PerfilUtilizador.Pontos = utilizador.pontos;
            PerfilUtilizador.Iban = utilizador.Iban;
            return View(PerfilUtilizador);
        }

        private int CalculaIdade(DateTime reference,DateTime dataNascimento)
        {
            int age = reference.Year - dataNascimento.Year;
            if (reference < dataNascimento.AddYears(age)) age--;

            return age;
        }
        private void atualizarUtilizadorTag(ApplicationUser utilizadorTag,
    string[] selectedTag)
        {
            if (selectedTag == null)
            {
                utilizadorTag.Tags = new List<Tag>();
                return;
            }
            var selectedAutoresHS = new HashSet<string>(selectedTag);
            var UserTag = new HashSet<int>(
             utilizadorTag.Tags.Select(a => a.Id));
            var Tags = db.Tags;
            foreach (var tag in Tags)
            {
                if (selectedAutoresHS.Contains(tag.Id.ToString()))
                {
                    if (!UserTag.Contains(tag.Id))
                    {
                        utilizadorTag.Tags.Add(tag);
                    }
                }
                else {

                    if (UserTag.Contains(tag.Id))
                    {
                        utilizadorTag.Tags.Remove(tag);
                    }
                }
            }
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            System.Diagnostics.Debug.WriteLine("This will be displayed in output window");
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }



        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        {
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link:
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(userID, subject, "Obrigado pela preferência, queremos ser o número 1 nas suas escolhas venha e usufrua da nossa plataforma, <a href=\"" + callbackUrl + "\">clique aqui para confirmar a sua conta</a>");


            return callbackUrl;
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}