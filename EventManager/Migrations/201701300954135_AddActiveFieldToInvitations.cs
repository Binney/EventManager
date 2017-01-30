namespace EventManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveFieldToInvitations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invitations", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invitations", "Active");
        }
    }
}
