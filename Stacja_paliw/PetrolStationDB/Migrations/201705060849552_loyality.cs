namespace PetrolStationDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loyality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoyalityProgs", "PbOnPtsReciv", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "LPGPtsReciv", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "WashPtsReciv", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "WaxPtsReciv", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "PbOnPtsReq", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "LPGPtsReq", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "WashPtsReq", c => c.Int(nullable: false));
            AddColumn("dbo.LoyalityProgs", "WaxPtsReq", c => c.Int(nullable: false));
            DropColumn("dbo.LoyalityProgs", "RequiredPoints");
            DropColumn("dbo.LoyalityProgs", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoyalityProgs", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LoyalityProgs", "RequiredPoints", c => c.Int(nullable: false));
            DropColumn("dbo.LoyalityProgs", "WaxPtsReq");
            DropColumn("dbo.LoyalityProgs", "WashPtsReq");
            DropColumn("dbo.LoyalityProgs", "LPGPtsReq");
            DropColumn("dbo.LoyalityProgs", "PbOnPtsReq");
            DropColumn("dbo.LoyalityProgs", "WaxPtsReciv");
            DropColumn("dbo.LoyalityProgs", "WashPtsReciv");
            DropColumn("dbo.LoyalityProgs", "LPGPtsReciv");
            DropColumn("dbo.LoyalityProgs", "PbOnPtsReciv");
        }
    }
}
