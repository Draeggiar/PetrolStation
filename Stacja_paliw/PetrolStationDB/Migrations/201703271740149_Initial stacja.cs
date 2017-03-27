namespace PetrolStationDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialstacja : DbMigration
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
                "dbo.LoyalityProgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequiredPoints = c.Int(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        Pb95 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pb98 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lpg = c.Decimal(nullable: false, precision: 18, scale: 2),
                        On = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Wash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Waxing = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VATs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoVAT = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ClientFirstName = c.String(),
                        ClientLastName = c.String(),
                        Address = c.String(),
                        NIP = c.Long(nullable: false),
                        ProductsAmountOrServices = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPriceNet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VATs");
            DropTable("dbo.Prices");
            DropTable("dbo.Orders");
            DropTable("dbo.LoyalityProgs");
            DropTable("dbo.CarWashes");
        }
    }
}
