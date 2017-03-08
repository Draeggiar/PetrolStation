using DomainModel;
using PetrolStationDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler
{
    public class CarWashHandler
    {
        private PSDbContext db = new PSDbContext();

        public void Create(CarWash carWash)
        {
            db.Car_Wash.Add(carWash);
            db.SaveChanges();
        }

        public CarWash GetCarWashById(int id)
        {
            var result = db.Car_Wash.First(x => x.Id == id);
            return result;
        }

        public List<CarWash> GetAllCarWashes()
        {
            var result = db.Car_Wash.ToList();
            return result;
        }

        public void Edit(CarWash carWash)
        {
            db.Entry(carWash).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            CarWash result = GetCarWashById(id);
            db.Car_Wash.Remove(result);
            db.SaveChanges();
        }
    }
}
