using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string NomeTag { get; set; }
        public string cor { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Desafio> Desafios { get; set; }
    }
}