using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class ProjetoUtilizador
    {
        public int ProjetoUtilizadorId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
        public HttpPostedFileBase File { get; set; }


    }
}