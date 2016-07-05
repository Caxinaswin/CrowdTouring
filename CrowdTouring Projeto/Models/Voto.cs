using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdTouring_Projeto.Models
{
    public class Voto
    {
        public int VotoId { get; set; }
        public string userId { get; set; }
        public Solucao solucao { get; set; }
    }
}
