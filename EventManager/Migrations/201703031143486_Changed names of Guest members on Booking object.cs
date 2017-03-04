namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangednamesofGuestmembersonBookingobject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "PrimaryGuest", c => c.String());
            DropColumn("dbo.Bookings", "Guest3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Guest3", c => c.String(nullable: false));
            DropColumn("dbo.Bookings", "PrimaryGuest");
        }
    }
}
