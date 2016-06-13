using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class TipoAvaliacao
    {
        public int TipoAvaliacaoId { get; set; }

        public string Descricao { get; set; }

        ICollection<Desafio> Desafios { get; set; }
    }
}