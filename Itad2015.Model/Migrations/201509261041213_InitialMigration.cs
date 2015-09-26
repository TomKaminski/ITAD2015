namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        SchoolName = c.String(),
                        CheckInDate = c.DateTime(),
                        ConfirmationHash = c.String(),
                        CancelationHash = c.String(),
                        RegistrationTime = c.DateTime(nullable: false),
                        ConfirmationTime = c.DateTime(),
                        Cancelled = c.Boolean(nullable: false),
                        WorkshopId = c.Int(),
                        WorkshopGuestId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workshop", t => t.WorkshopId)
                .ForeignKey("dbo.WorkshopGuest", t => t.WorkshopGuestId)
                .Index(t => t.WorkshopId)
                .Index(t => t.WorkshopGuestId);
            
            CreateTable(
                "dbo.Workshop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        MaxParticipants = c.Int(nullable: false),
                        TutorName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ImgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkshopGuest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConfirmationHash = c.String(),
                        CancelationHash = c.String(),
                        RegistrationTime = c.DateTime(nullable: false),
                        ConfirmationTime = c.DateTime(),
                        Cancelled = c.Boolean(nullable: false),
                        GuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guest", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.Prize",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guest", "WorkshopGuestId", "dbo.WorkshopGuest");
            DropForeignKey("dbo.WorkshopGuest", "GuestId", "dbo.Guest");
            DropForeignKey("dbo.Guest", "WorkshopId", "dbo.Workshop");
            DropIndex("dbo.WorkshopGuest", new[] { "GuestId" });
            DropIndex("dbo.Guest", new[] { "WorkshopGuestId" });
            DropIndex("dbo.Guest", new[] { "WorkshopId" });
            DropTable("dbo.Prize");
            DropTable("dbo.WorkshopGuest");
            DropTable("dbo.Workshop");
            DropTable("dbo.Guest");
        }
    }
}
