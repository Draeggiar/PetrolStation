namespace PetrolStationDB.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class vatlayality : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VATs", "NIP", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VATs", "NIP", c => c.Int(nullable: false));
        }
    }
}
