namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class size : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guest", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guest", "Size");
        }
    }
}
