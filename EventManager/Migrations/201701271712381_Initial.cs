namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        Guest1 = c.String(nullable: false),
                        Guest2 = c.String(nullable: false),
                        Guest3 = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
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
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false),
                        InvitationCode = c.String(nullable: false, maxLength: 12),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "EventId", "dbo.Events");
            DropIndex("dbo.Bookings", new[] { "EventId" });
            DropTable("dbo.Invitations");
            DropTable("dbo.Events");
            DropTable("dbo.Bookings");
        }
    }
}
