namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao400 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserTags", newName: "TagApplicationUsers");
            DropPrimaryKey("dbo.TagApplicationUsers");
            AddColumn("dbo.Solucaos", "ApplicationUserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.TagApplicationUsers", new[] { "Tag_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.Solucaos", "ApplicationUserId");
            AddForeignKey("dbo.Solucaos", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solucaos", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Solucaos", new[] { "ApplicationUserId" });
            DropPrimaryKey("dbo.TagApplicationUsers");
            DropColumn("dbo.Solucaos", "ApplicationUserId");
            AddPrimaryKey("dbo.TagApplicationUsers", new[] { "ApplicationUser_Id", "Tag_Id" });
            RenameTable(name: "dbo.TagApplicationUsers", newName: "ApplicationUserTags");
        }
    }
}
