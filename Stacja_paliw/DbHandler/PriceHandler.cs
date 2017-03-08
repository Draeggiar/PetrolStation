using DomainModel;
using PetrolStationDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DbHandler
{
    public class PriceHandler
    {
        private PSDbContext db = new PSDbContext();

        public Price GetPricesByDate(DateTime date)
        {
            var price = db.Prices.First(x => x.Date == date);
            return price;
        }

        public List<Price> GetAllPricesHistorical()
        {
            var prices = db.Prices.ToList();
            return prices;
        }

        public void EditPrice(Price price)
        {
            db.Entry(price).State = EntityState.Modified;
            db.SaveChanges();
        }
    }    
}
