using DomainModel;
using PetrolStationDB;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DbHandler
{
    public class OrderHandler
    {
        private PSDbContext db = new PSDbContext();

        public void Create(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            var result = db.Orders.First(x => x.Id == id);
            return result;
        }

        public List<Order> GetAllOrders()
        {
            var orders = db.Orders.ToList();
            return orders;
        }

        public void Edit(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Order product = GetOrderById(id);
            db.Orders.Remove(product);
            db.SaveChanges();
        }
    }
}
