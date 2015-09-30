namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkshopRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkshopGuest", "SchoolName", c => c.String());
            AddColumn("dbo.Workshop", "Room", c => c.String());
            DropColumn("dbo.Guest", "SchoolName");
            DropColumn("dbo.WorkshopGuest", "ConfirmationHash");
            DropColumn("dbo.WorkshopGuest", "CancelationHash");
            DropColumn("dbo.WorkshopGuest", "RegistrationTime");
            DropColumn("dbo.WorkshopGuest", "ConfirmationTime");
            DropColumn("dbo.WorkshopGuest", "Cancelled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkshopGuest", "Cancelled", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkshopGuest", "ConfirmationTime", c => c.DateTime());
            AddColumn("dbo.WorkshopGuest", "RegistrationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkshopGuest", "CancelationHash", c => c.String());
            AddColumn("dbo.WorkshopGuest", "ConfirmationHash", c => c.String());
            AddColumn("dbo.Guest", "SchoolName", c => c.String());
            DropColumn("dbo.Workshop", "Room");
            DropColumn("dbo.WorkshopGuest", "SchoolName");
        }
    }
}
