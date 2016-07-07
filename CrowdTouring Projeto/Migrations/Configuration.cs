namespace CrowdTouring_Projeto.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrowdTouring_Projeto.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CrowdTouring_Projeto.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Cliente"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Cliente" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Resolvedor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Resolvedor" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Avaliador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Avaliador" };

                manager.Create(role);
            }

            context.Tags.AddOrUpdate(x => x.Id,
        new Tag() { Id = 1, NomeTag = "Restauração", cor = "blue" },
        new Tag() { Id = 2, NomeTag = "Hotelaria", cor = "green" },
        new Tag() { Id = 3, NomeTag = "Lazer", cor = "red" },
        new Tag() { Id = 4, NomeTag = "Trilhos", cor = "yellow" },
        new Tag() { Id = 5, NomeTag = "Transportes", cor = "grey" },
        new Tag() { Id = 6, NomeTag = "Praias", cor = "orange" },
        new Tag() { Id = 7, NomeTag = "Lembranças", cor = "Pink" },
        new Tag() { Id = 7, NomeTag = "Tecnologia", cor = "black" },
        new Tag() { Id = 7, NomeTag = "Outros", cor = "DarkGray" }
        );

            context.TiposAvaliacao.AddOrUpdate(x => x.TipoAvaliacaoId,
        new TipoAvaliacao() { TipoAvaliacaoId = 1, Descricao = "AceitarSolucoes" },
        new TipoAvaliacao() { TipoAvaliacaoId = 2, Descricao = "Votacao" },
        new TipoAvaliacao() { TipoAvaliacaoId = 3, Descricao = "Avaliacao" },
        new TipoAvaliacao() { TipoAvaliacaoId = 4, Descricao = "Fechado" }
        );
        }
    }
}
