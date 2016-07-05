namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votoes",
                c => new
                    {
                        VotoId = c.Int(nullable: false, identity: true),
                        solucao_SolucaoId = c.Int(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VotoId)
                .ForeignKey("dbo.Solucaos", t => t.solucao_SolucaoId)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.solucao_SolucaoId)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votoes", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votoes", "solucao_SolucaoId", "dbo.Solucaos");
            DropIndex("dbo.Votoes", new[] { "user_Id" });
            DropIndex("dbo.Votoes", new[] { "solucao_SolucaoId" });
            DropTable("dbo.Votoes");
        }
    }
}
