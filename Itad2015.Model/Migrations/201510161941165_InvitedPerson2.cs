namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvitedPerson2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvitedPerson", "Name", c => c.String());
            AddColumn("dbo.InvitedPerson", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvitedPerson", "LastName");
            DropColumn("dbo.InvitedPerson", "Name");
        }
    }
}
