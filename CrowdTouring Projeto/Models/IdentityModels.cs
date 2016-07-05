using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CrowdTouring_Projeto.Models;
using System.Web.Mvc;
using System;

namespace CrowdTouring_Projeto.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public ApplicationUser()
        {
            Tags = new List<Tag>();
            ConfirmacaoConta = false;
        }
        [Required]
        public string Nome { get; set; }
        public string Telemóvel { get; set; }
        [Required]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MMMM/d/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }
        public string Sobre { get; set; }
        [Required]
        public string TipoUtilizador { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public bool ConfirmacaoConta { get; set; }
        public bool Notificacao { get; set; }
        public string empresa { get; set; }
        public string website { get; set; }
        public string Iban { get; set; }
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public int pontos { get; set; }
        public int UltimaSessao { get; set; }
        public virtual ICollection<Desafio> Desafios { get; set; }
        public virtual ICollection<Solucao> Solucoes { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Sugestao> Sugestoes { get; set; }
        public DbSet<TipoPagamento> TiposPagamento { get; set; }
        public DbSet<TipoAvaliacao> TiposAvaliacao { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<Solucao> Solucoes { get; set; }
        public DbSet<Estrela> Estrelas { get; set; }
        public DbSet<Voto> Votos { get; set; }
        public object UserRoles { get; internal set; }
        public System.Data.Entity.DbSet<CrowdTouring_Projeto.Models.Desafio> Desafios { get; set; }

      
    }

}