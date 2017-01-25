namespace EventManager.Migrations.InvitationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invitations", "InvitationCode", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invitations", "InvitationCode", c => c.String());
        }
    }
}
