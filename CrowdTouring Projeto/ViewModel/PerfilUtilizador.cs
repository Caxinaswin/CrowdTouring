using CrowdTouring_Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.ViewModel
{
    public class PerfilUtilizador
    {
        public string path { get; set; }

        public int pontos { get; set; }

        public ICollection<Tag> tags { get; set; }
    }
}