using DomainModel;
using System.Data.Entity;

namespace PetrolStationDB
{
    public class PSDbContext : DbContext
    {

        public PSDbContext() : base("PerolStationDB")
        {

        }

        public DbSet<Price> Prices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarWash> Car_Wash { get; set; }
        public DbSet<VAT> Vats { get; set; }
        public DbSet<LoyalityProg> Loyality { get; set; }
    }
}
