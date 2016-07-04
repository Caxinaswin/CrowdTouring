namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class days7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Solucaos", "SolucaoTitulo", c => c.String(nullable: false));
            AlterColumn("dbo.Solucaos", "Descricao", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Solucaos", "Descricao", c => c.String());
            AlterColumn("dbo.Solucaos", "SolucaoTitulo", c => c.String());
        }
    }
}
