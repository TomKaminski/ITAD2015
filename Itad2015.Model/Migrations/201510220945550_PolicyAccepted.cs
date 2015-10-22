namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PolicyAccepted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guest", "PolicyAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guest", "PolicyAccepted");
        }
    }
}
