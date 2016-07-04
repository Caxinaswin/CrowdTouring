namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class days6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Desafios", "DiasAvaliacao", c => c.Int(nullable: false));
            AddColumn("dbo.Desafios", "DiasVotacao", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Desafios", "DiasVotacao");
            DropColumn("dbo.Desafios", "DiasAvaliacao");
        }
    }
}
