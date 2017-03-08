namespace PetrolStationDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prices", "Pb95", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Pb98", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Lpg", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "On", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Wash", c => c.Double(nullable: false));
            AlterColumn("dbo.Prices", "Waxing", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prices", "Waxing", c => c.Int(nullable: false));
            AlterColumn("dbo.Prices", "Wash", c => c.Int(nullable: false));
            AlterColumn("dbo.Prices", "On", c => c.Int(nullable: false));
            AlterColumn("dbo.Prices", "Lpg", c => c.Int(nullable: false));
            AlterColumn("dbo.Prices", "Pb98", c => c.Int(nullable: false));
            AlterColumn("dbo.Prices", "Pb95", c => c.Int(nullable: false));
        }
    }
}
