namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Solucaos",
                c => new
                    {
                        SolucaoId = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        NumeroVisualizacoes = c.Int(nullable: false),
                        NumeroVotos = c.Int(nullable: false),
                        DesafioId = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SolucaoId)
                .ForeignKey("dbo.Desafios", t => t.DesafioId, cascadeDelete: true)
                .Index(t => t.DesafioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solucaos", "DesafioId", "dbo.Desafios");
            DropIndex("dbo.Solucaos", new[] { "DesafioId" });
            DropTable("dbo.Solucaos");
        }
    }
}
