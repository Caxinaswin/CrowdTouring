using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class TipoPagamento
    {
        public int TipoPagamentoId { get; set; }
        public string Descricao { get; set; }
        ICollection<Desafio> Desafios { get; set; }
    }
}