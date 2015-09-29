namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workshopguestworkshop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guest", "WorkshopId", "dbo.Workshop");
            DropIndex("dbo.Guest", new[] { "WorkshopId" });
            AddColumn("dbo.WorkshopGuest", "WorkshopId", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkshopGuest", "WorkshopId");
            AddForeignKey("dbo.WorkshopGuest", "WorkshopId", "dbo.Workshop", "Id", cascadeDelete: true);
            DropColumn("dbo.Guest", "WorkshopId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guest", "WorkshopId", c => c.Int());
            DropForeignKey("dbo.WorkshopGuest", "WorkshopId", "dbo.Workshop");
            DropIndex("dbo.WorkshopGuest", new[] { "WorkshopId" });
            DropColumn("dbo.WorkshopGuest", "WorkshopId");
            CreateIndex("dbo.Guest", "WorkshopId");
            AddForeignKey("dbo.Guest", "WorkshopId", "dbo.Workshop", "Id");
        }
    }
}
