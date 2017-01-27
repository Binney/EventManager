namespace EventManager.Migrations.EventDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Events");
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

            DropColumn("dbo.Events", "ID");
            AddColumn("dbo.Events", "EventId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Events", "EventId"); 
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Bookings", "BookingId", "dbo.Events");
            DropIndex("dbo.Bookings", new[] { "BookingId" });
            DropPrimaryKey("dbo.Events");
            DropColumn("dbo.Events", "EventId");
            DropTable("dbo.Bookings");
            AddPrimaryKey("dbo.Events", "ID");
        }
    }
}
