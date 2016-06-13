using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrowdTouring_Projeto.Models
{
    public class Anexo
    {
        public int AnexoId { get; set; }
        public string Caminho { get; set; }
        public int DesafioId { get; set; }
        public int SolucaoId { get; set; }
        public string NomeFicheiro { get; set; }
    }
}