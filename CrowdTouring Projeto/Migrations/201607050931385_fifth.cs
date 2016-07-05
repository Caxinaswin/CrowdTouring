namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estrelas",
                c => new
                    {
                        EstrelaId = c.Int(nullable: false, identity: true),
                        SolucaoId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EstrelaId)
                .ForeignKey("dbo.Solucaos", t => t.SolucaoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.SolucaoId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estrelas", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Estrelas", "SolucaoId", "dbo.Solucaos");
            DropIndex("dbo.Estrelas", new[] { "User_Id" });
            DropIndex("dbo.Estrelas", new[] { "SolucaoId" });
            DropTable("dbo.Estrelas");
        }
    }
}
