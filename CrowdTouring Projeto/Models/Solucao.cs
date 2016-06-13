using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class Solucao
    {
        public int SolucaoId { get; set; }
        public string SolucaoTitulo { get; set; }
        public string Descricao { get; set; }
        public int NumeroVisualizacoes { get; set; }
        public int NumeroVotos { get; set; }
        public int DesafioId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DataCriacao { get; set; }
        public Desafio Desafio { get; set; }
    }
}