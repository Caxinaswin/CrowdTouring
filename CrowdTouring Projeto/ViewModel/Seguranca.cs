using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class Seguranca
    {
        
        [DataType(DataType.Password)]
        [Display(Name = "Password Antiga")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "A {0} Tem que ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password")]
        public string NovaPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Precisa de inserir uma confirmação de password")]
        [Display(Name = "Confirmar nova password")]
        [Compare("NovaPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}