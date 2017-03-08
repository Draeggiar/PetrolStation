namespace PetrolStationDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarWashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Position = c.Int(nullable: false),
                        ServiceType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fuel = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Pb95 = c.Int(nullable: false),
                        Pb98 = c.Int(nullable: false),
                        Lpg = c.Int(nullable: false),
                        On = c.Int(nullable: false),
                        Wash = c.Int(nullable: false),
                        Waxing = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Prices");
            DropTable("dbo.Orders");
            DropTable("dbo.CarWashes");
        }
    }
}
