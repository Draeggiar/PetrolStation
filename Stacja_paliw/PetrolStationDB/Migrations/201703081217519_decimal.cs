namespace PetrolStationDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prices", "Pb95", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prices", "Pb98", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prices", "Lpg", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prices", "On", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prices", "Wash", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prices", "Waxing", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prices", "Waxing", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Wash", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "On", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Lpg", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Pb98", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Pb95", c => c.Double(nullable: false));
        }
    }
}
