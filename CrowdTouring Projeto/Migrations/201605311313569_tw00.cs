namespace CrowdTouring_Projeto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tw00 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Desafios", "valor", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Desafios", "valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
