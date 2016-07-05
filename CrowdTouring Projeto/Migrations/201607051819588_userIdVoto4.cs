namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userIdVoto4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estrelas", "EstrelaValor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Estrelas", "EstrelaValor");
        }
    }
}
