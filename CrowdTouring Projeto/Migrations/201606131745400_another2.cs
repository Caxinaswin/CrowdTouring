namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class another2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Desafios", "TipoTrabalho", c => c.String(nullable: false));
            AlterColumn("dbo.Desafios", "Descricao", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Desafios", "Descricao", c => c.String());
            AlterColumn("dbo.Desafios", "TipoTrabalho", c => c.String());
        }
    }
}
