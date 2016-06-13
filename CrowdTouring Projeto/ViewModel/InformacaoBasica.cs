using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class InformacaoBasica
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }

        [StringLength(12, MinimumLength = 9, ErrorMessage = "Verifique a quantidade de números introduzidos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Número de telémovel tem que ser númerico")]
        public string Telemóvel { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
    }
}