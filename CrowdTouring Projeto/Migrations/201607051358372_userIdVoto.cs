namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userIdVoto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votoes", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Votoes", new[] { "user_Id" });
            AddColumn("dbo.Votoes", "userId", c => c.String());
            DropColumn("dbo.Votoes", "user_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Votoes", "user_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Votoes", "userId");
            CreateIndex("dbo.Votoes", "user_Id");
            AddForeignKey("dbo.Votoes", "user_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
