using CrowdTouring_Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class DetalhesDesafio
    {
        public int DesafioId { get; set; }
        public string TipoTrabalho { get; set; }
        public string Descricao { get; set; }
        public int ValorMonetario { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string FilePath { get; set; }
        public int Pontuacao { get; set; }
        public string Solucao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string FileName { get; set; }
        public int FileId { get; set; }
        public ICollection<Models.Solucao> Solucoes { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public string idUtilizador { get; set; }
        public string nomeUtilizador { get; set; }
        public int diasAvaliacao { get; set; }
        public int diasVotacao { get; set; }
        public DateTime dataFinal { get; set; }
        public string TipoAvaliacao { get; set; }
        public DateTime DataFinalAceitacao { get; set; }
    }
}