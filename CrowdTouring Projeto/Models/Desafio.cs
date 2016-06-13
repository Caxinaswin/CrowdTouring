using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class Desafio
    {
        public int DesafioId { get; set; }

        public string TipoTrabalho { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }

        public string Descricao { get; set; }
        public int TipoAvaliacaoId { get; set; }
        public virtual TipoAvaliacao TipoAvaliacao { get; set; }       
        public int valor { get; set; }
        public int Visualizacoes { get; set; }
        public DateTime DataCriacao { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int IdSolucaoVencedora { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Solucao> Solucao { get; set; }

    }
}