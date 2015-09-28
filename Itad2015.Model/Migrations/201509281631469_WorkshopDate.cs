namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkshopDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workshop", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Workshop", "StartDate");
            DropColumn("dbo.Workshop", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workshop", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Workshop", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Workshop", "Date");
        }
    }
}
