using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class Sugestao
    {
        public int SugestaoId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }

        public string ApplicationUserId { get; set; }
        public string Titulo { get; set; }
        public string Comentario { get; set; }
    }
}