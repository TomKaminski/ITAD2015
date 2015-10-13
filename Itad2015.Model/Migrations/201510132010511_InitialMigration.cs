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
                        CheckInDate = c.DateTime(),
                        Size = c.Int(nullable: false),
                        Info = c.String(),
                        ConfirmationHash = c.String(),
                        CancelationHash = c.String(),
                        RegistrationTime = c.DateTime(nullable: false),
                        ConfirmationTime = c.DateTime(),
                        Cancelled = c.Boolean(nullable: false),
                        WorkshopGuestId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkshopGuest", t => t.WorkshopGuestId)
                .Index(t => t.WorkshopGuestId);
            
            CreateTable(
                "dbo.WorkshopGuest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        GuestId = c.Int(nullable: false),
                        WorkshopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guest", t => t.GuestId, cascadeDelete: true)
                .ForeignKey("dbo.Workshop", t => t.WorkshopId, cascadeDelete: true)
                .Index(t => t.GuestId)
                .Index(t => t.WorkshopId);
            
            CreateTable(
                "dbo.Workshop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        MaxParticipants = c.Int(nullable: false),
                        TutorName = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        ImgPath = c.String(),
                        Room = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prize",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        PasswordSalt = c.String(),
                        SuperAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guest", "WorkshopGuestId", "dbo.WorkshopGuest");
            DropForeignKey("dbo.WorkshopGuest", "WorkshopId", "dbo.Workshop");
            DropForeignKey("dbo.WorkshopGuest", "GuestId", "dbo.Guest");
            DropIndex("dbo.WorkshopGuest", new[] { "WorkshopId" });
            DropIndex("dbo.WorkshopGuest", new[] { "GuestId" });
            DropIndex("dbo.Guest", new[] { "WorkshopGuestId" });
            DropTable("dbo.User");
            DropTable("dbo.Prize");
            DropTable("dbo.Workshop");
            DropTable("dbo.WorkshopGuest");
            DropTable("dbo.Guest");
        }
    }
}
