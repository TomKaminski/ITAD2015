namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgendaEmailSent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guest", "AgendaEmailSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guest", "AgendaEmailSent");
        }
    }
}
