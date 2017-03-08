﻿using DomainModel;
using System.Data.Entity;

namespace PetrolStationDB
{
    public class PSDbContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarWash> Car_Wash { get; set; }
    }
}
