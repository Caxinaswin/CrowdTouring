namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class masdas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anexoes",
                c => new
                    {
                        AnexoId = c.Int(nullable: false, identity: true),
                        Caminho = c.String(),
                        DesafioId = c.Int(nullable: false),
                        NomeFicheiro = c.String(),
                    })
                .PrimaryKey(t => t.AnexoId);
            
            CreateTable(
                "dbo.Desafios",
                c => new
                    {
                        DesafioId = c.Int(nullable: false, identity: true),
                        TipoTrabalho = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        Descricao = c.String(),
                        TipoAvaliacaoId = c.Int(nullable: false),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Visualizacoes = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        lat = c.Double(nullable: false),
                        lon = c.Double(nullable: false),
                        IdSolucaoVencedora = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DesafioId)
                .ForeignKey("dbo.TipoAvaliacaos", t => t.TipoAvaliacaoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.TipoAvaliacaoId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeTag = c.String(),
                        cor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(nullable: false),
                        Telemóvel = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Sobre = c.String(),
                        TipoUtilizador = c.String(nullable: false),
                        ConfirmacaoConta = c.Boolean(nullable: false),
                        Notificacao = c.Boolean(nullable: false),
                        empresa = c.String(),
                        website = c.String(),
                        Iban = c.String(),
                        ImageId = c.Int(nullable: false),
                        ImagePath = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TipoAvaliacaos",
                c => new
                    {
                        TipoAvaliacaoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.TipoAvaliacaoId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sugestaos",
                c => new
                    {
                        SugestaoId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        Titulo = c.String(),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.SugestaoId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.TipoPagamentoes",
                c => new
                    {
                        TipoPagamentoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.TipoPagamentoId);
            
            CreateTable(
                "dbo.TagDesafios",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Desafio_DesafioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Desafio_DesafioId })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Desafios", t => t.Desafio_DesafioId, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Desafio_DesafioId);
            
            CreateTable(
                "dbo.ApplicationUserTags",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Tag_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sugestaos", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Desafios", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Desafios", "TipoAvaliacaoId", "dbo.TipoAvaliacaos");
            DropForeignKey("dbo.ApplicationUserTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.ApplicationUserTags", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagDesafios", "Desafio_DesafioId", "dbo.Desafios");
            DropForeignKey("dbo.TagDesafios", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.ApplicationUserTags", new[] { "Tag_Id" });
            DropIndex("dbo.ApplicationUserTags", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TagDesafios", new[] { "Desafio_DesafioId" });
            DropIndex("dbo.TagDesafios", new[] { "Tag_Id" });
            DropIndex("dbo.Sugestaos", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Desafios", new[] { "TipoAvaliacaoId" });
            DropIndex("dbo.Desafios", new[] { "ApplicationUserId" });
            DropTable("dbo.ApplicationUserTags");
            DropTable("dbo.TagDesafios");
            DropTable("dbo.TipoPagamentoes");
            DropTable("dbo.Sugestaos");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TipoAvaliacaos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tags");
            DropTable("dbo.Desafios");
            DropTable("dbo.Anexoes");
        }
    }
}
