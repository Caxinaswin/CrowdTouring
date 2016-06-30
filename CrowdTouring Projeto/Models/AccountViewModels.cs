using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrowdTouring_Projeto.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Precisa de inserir um nome de utilizador")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Precisa de inserir uma password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Precisa de inserir uma confirmação de password")]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Precisa de inserir um email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Verifique a quantidade de números introduzidos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Número de telémovel tem que ser númerico")]
        public string Telemóvel { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Precisa de inserir a sua data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string Sobre { get; set; }
        [Required(ErrorMessage = "Precisa de escolher o tipo de utilizador que pretende ser na plataforma")]
        public string TipoUtilizador { get; set; }

        public bool Notificacao { get; set; }

    }

    public class TagUtilizador
    {
        public int TagId { get; set; }
        public string Nome { get; set; }
        public bool Seleccionado { get; set; }
    }

    public class EditarUtilizadorViewModel
    {
        public string Utilizador { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Verifique a quantidade de números introduzidos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Número de telémovel tem que ser númerico")]
        public string Telemóvel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
        public string Website { get; set; }
        public string Empresa { get; set; }
        public string Iban { get; set; }
        public int Idade { get; set; }
        public int Pontos { get; set; }

        public virtual ICollection<AssignedTagsData> Tags { get; set; }

        [DataType(DataType.MultilineText)]
        public string Sobre { get; set; }
        public virtual string TipoUtilizador { get; set; }

        public bool Notificacao { get; set; }

        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public bool ApagarFoto { get; set; }
        public EditarUtilizadorViewModel()
        {
            ApagarFoto = false;
        }



    }

    public class TagViewModel
    {
        public int Id { get; set; }
        public string DescricaoTag { get; set; }
        public string Cor { get; set; }
        public virtual ICollection<EditarUtilizadorViewModel> Utilizador { get; set; }
    }

    public class AssignedTagsData
    {
        public int Tag_Id { get; set; }
        public string DescricaoTag { get; set; }
        public string cor { get; set; }
        public bool Seleccionado { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
