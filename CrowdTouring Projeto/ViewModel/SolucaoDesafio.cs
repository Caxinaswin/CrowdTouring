using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class SolucaoDesafio
    {
        public string NomeDesafio { get; set; }
        public int IdDesafio { get; set; }
        public string DescricaoSolucao { get; set; }
        public string NomeSolucao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string DiferencaDatas { get; set; }
        public string FileName { get; set; }
        public int FileId { get; set; }
        public string FilePath { get; set; }
    }
}