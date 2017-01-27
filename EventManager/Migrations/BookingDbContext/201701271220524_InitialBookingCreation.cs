namespace EventManager.Migrations.BookingDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialBookingCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false),
                        Guest1 = c.String(nullable: false),
                        Guest2 = c.String(nullable: false),
                        Guest3 = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Events", t => t.BookingId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "BookingId", "dbo.Events");
            DropIndex("dbo.Bookings", new[] { "BookingId" });
            DropTable("dbo.Events");
            DropTable("dbo.Bookings");
        }
    }
}
