namespace TicketTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class App : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "App", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "App");
        }
    }
}
