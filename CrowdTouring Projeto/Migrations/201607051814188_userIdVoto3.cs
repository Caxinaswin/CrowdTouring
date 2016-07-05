namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userIdVoto3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Estrelas", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Estrelas", new[] { "User_Id" });
            AddColumn("dbo.Estrelas", "User", c => c.String());
            DropColumn("dbo.Estrelas", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Estrelas", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Estrelas", "User");
            CreateIndex("dbo.Estrelas", "User_Id");
            AddForeignKey("dbo.Estrelas", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
