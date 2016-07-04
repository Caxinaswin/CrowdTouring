using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class SolucaoDesafio
    {
        public string NomeDesafio { get; set; }
        public int IdDesafio { get; set; }
        [Required(ErrorMessage = "é necessário inserir um descrição para a proposta de solução")]
        public string DescricaoSolucao { get; set; }
        [Required(ErrorMessage = "é necessário inserir um título para a proposta de solução")]
        public string NomeSolucao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string DiferencaDatas { get; set; }
        public string FileName { get; set; }
        public int FileId { get; set; }
        public string FilePath { get; set; }
    }
}