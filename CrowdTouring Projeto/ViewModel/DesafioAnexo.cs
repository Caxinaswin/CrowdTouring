using CrowdTouring_Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class DesafioCreate
    {
        [Display(Name = "Desafio")]
        [StringLength(50)]
        public string TipoTrabalho { get; set; }
        public string Descricao { get; set; }
        [Range(0, 500)]
        public int ValorMonetario { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public ICollection<TagDesafio> TagDesafio { get; set; }
       
    }
    public class TagDesafio
    {
        public int TagId { get; set; }
        public string Nome { get; set; }
        public bool Seleccionado { get; set; }
    }
}