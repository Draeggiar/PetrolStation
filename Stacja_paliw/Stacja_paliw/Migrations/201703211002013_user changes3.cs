namespace Stacja_paliw.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userchanges3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ManageUserInfoModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ManageUserInfoModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        NIP_Regon = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
