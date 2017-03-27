namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2identity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInfoes", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "UserID", c => c.Int(nullable: false));
        }
    }
}
