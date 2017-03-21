namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userchanges4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInfoes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "UserId", c => c.String());
        }
    }
}
