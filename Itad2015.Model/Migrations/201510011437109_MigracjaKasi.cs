namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracjaKasi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guest", "Info", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guest", "Info");
        }
    }
}
