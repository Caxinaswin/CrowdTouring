using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdTouring_Projeto.Models
{
    public class Estrela
    {
        public int EstrelaId { get; set; }
        public int EstrelaValor { get; set; }
        public int SolucaoId { get; set; }
        public Solucao solucao { get; set; }
        public string User { get; set; }
    }
}
