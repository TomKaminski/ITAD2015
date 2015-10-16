namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvitedPerson : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvitedPerson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvitedPerson");
        }
    }
}
