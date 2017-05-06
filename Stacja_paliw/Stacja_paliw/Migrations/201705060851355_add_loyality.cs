namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_loyality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoyalityStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LifetimePts = c.Int(nullable: false),
                        CurrPts = c.Int(nullable: false),
                        AspUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspUser_Id)
                .Index(t => t.AspUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoyalityStatus", "AspUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LoyalityStatus", new[] { "AspUser_Id" });
            DropTable("dbo.LoyalityStatus");
        }
    }
}
