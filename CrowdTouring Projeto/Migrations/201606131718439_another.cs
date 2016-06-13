namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class another : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Desafios", "DataFinalSolucoes", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Desafios", "DataFinalSolucoes");
        }
    }
}
