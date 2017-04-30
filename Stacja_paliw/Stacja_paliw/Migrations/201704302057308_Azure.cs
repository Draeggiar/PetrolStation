namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Azure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserID = c.Int(nullable: false),
                    FirstName = c.String(),
                    LastName = c.String(),
                    Address = c.String(),
                    NIP_Regon = c.Long(nullable: false),
                    MyUserInfo_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MyUserInfo_Id)
                .Index(t => t.MyUserInfo_Id);
        }

        public override void Down()
        {
        }
    }
}

