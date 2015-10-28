namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workshophour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkshopGuest", "WorkshopDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkshopGuest", "WorkshopDateTime");
        }
    }
}
