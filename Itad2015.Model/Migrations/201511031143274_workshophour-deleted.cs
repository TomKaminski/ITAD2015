namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workshophourdeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkshopGuest", "WorkshopDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkshopGuest", "WorkshopDateTime", c => c.DateTime(nullable: false));
        }
    }
}
