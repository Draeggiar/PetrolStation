namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserInfoes", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserInfoes", new[] { "UserId_Id" });
            AddColumn("dbo.UserInfoes", "UserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "MyUserInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "MyUserInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "MyUserInfo_Id", "dbo.UserInfoes", "Id");
            DropColumn("dbo.UserInfoes", "UserId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "UserId_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "MyUserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "MyUserInfo_Id" });
            DropColumn("dbo.AspNetUsers", "MyUserInfo_Id");
            DropColumn("dbo.UserInfoes", "UserId");
            CreateIndex("dbo.UserInfoes", "UserId_Id");
            AddForeignKey("dbo.UserInfoes", "UserId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
