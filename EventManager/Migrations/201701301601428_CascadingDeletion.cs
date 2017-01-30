namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadingDeletion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "EventId", "dbo.Events");
            AddForeignKey("dbo.Bookings", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "EventId", "dbo.Events");
            AddForeignKey("dbo.Bookings", "EventId", "dbo.Events", "EventId");
        }
    }
}
