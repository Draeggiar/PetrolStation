namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersinfoupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "Address", c => c.String());
            AddColumn("dbo.UserInfoes", "NIP_Regon", c => c.Long(nullable: false));
            AddColumn("dbo.UserInfoes", "UserId_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserInfoes", "UserId_Id");
            AddForeignKey("dbo.UserInfoes", "UserId_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserInfoes", new[] { "UserId_Id" });
            DropColumn("dbo.UserInfoes", "UserId_Id");
            DropColumn("dbo.UserInfoes", "NIP_Regon");
            DropColumn("dbo.UserInfoes", "Address");
        }
    }
}
