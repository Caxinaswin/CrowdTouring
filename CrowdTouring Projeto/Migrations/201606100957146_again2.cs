namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class again2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solucaos", "SolucaoTitulo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solucaos", "SolucaoTitulo");
        }
    }
}
