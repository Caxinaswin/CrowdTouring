using CrowdTouring_Projeto.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrowdTouring_Projeto.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //var primeiroLogin = db.Users.Where(d => d.Id == User.Identity.GetUserId()).Select(d => d.UltimaSessao);
           //ViewBag.primeiroLogin = primeiroLogin;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Sugestao()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sugestao([Bind(Include = "SugestaoId,ApplicationUserId,Titulo,Comentario")] Sugestao sugestao)
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Register", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    sugestao.ApplicationUserId = User.Identity.GetUserId();
                    db.Sugestoes.Add(sugestao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
    }
}