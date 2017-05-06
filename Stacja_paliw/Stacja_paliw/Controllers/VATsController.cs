using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DomainModel;
using PetrolStationDB;
using Stacja_paliw.Models;
using System;
using Microsoft.AspNet.Identity;
using Stacja_paliw.Areas.Worker.Models;
using System.Collections.Generic;

namespace Stacja_paliw.Controllers
{
    public class VATsController : Controller
    {
        private PSDbContext db = new PSDbContext();
        private ApplicationDbContext identityDb = new ApplicationDbContext();

        // GET: VATs
        public ActionResult Index()
        {
            var _vats = new List<VAT>();

            try
            {
                _vats = db.Vats.ToList();
            }
            catch (Exception e) { }

            return View(_vats);
        }

        public ActionResult Landing(double volume, string fuelType)
        {
            ViewBag.Vol = volume;
            ViewBag.Fuel = fuelType;

            return View("LandingPage");
        }

        public ActionResult ExistingClient()
        {
            ViewBag.Existing = true;

            return View("LandingPage");
        }

        [HttpPost]
        public ActionResult SearchClient(string q)
        {
            var client = from p in identityDb.MyUserInfo select p;
            var vat = from v in db.Vats select v;

            decimal volume = Convert.ToDecimal(TempData["Volume"]);
            string fuel = Convert.ToString(TempData["Fuel"]);
            decimal u_price = GetPrice(fuel);

            if (!string.IsNullOrEmpty(q))
            {
                client = client.Where(x => x.NIP_Regon.ToString() == q);
            }

            if (client.Any())
            {
                var test = client.Any();

                var currVat = CreateAndPopulate(q, volume, u_price);
                AddPoints(fuel, volume);

                ViewBag.NoVat = currVat.NoVAT;
                ViewBag.Amount = currVat.ProductsAmountOrServices;
                ViewBag.UPrice = currVat.UnitPrice;
                ViewBag.Discount = currVat.Discount;
                ViewBag.PriceNet = Math.Round(currVat.TotalPriceNet, 2);
                ViewBag.Tax = currVat.TaxRate;
                ViewBag.Total = Math.Round(currVat.TotalPrice, 2);
            }


            return View("VatPreview", client.ToList());
        }

        // GET: VATs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAT vAT = db.Vats.Find(id);
            if (vAT == null)
            {
                return HttpNotFound();
            }
            return View(vAT);
        }

        // GET: VATs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VATs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientFirstName,ClientLastName,Address,NIP,ProductsAmountOrServices,UnitPrice,Discount,TotalPriceNet,TaxRate,TotalPrice")] VAT Vat)
        {
            if (ModelState.IsValid)
            {
                Vat.Date = DateTime.Today;

                //FIX ME: Algorytm do generowania numeru faktury
                Vat.NoVAT = 1;

                db.Vats.Add(Vat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Vat);
        }

        public VAT CreateAndPopulate(string NIP, decimal amount, decimal u_price)
        {
            var client = from p in identityDb.MyUserInfo select p;
            client = client.Where(x => x.NIP_Regon.ToString() == NIP);

            VAT Vat = new VAT();

            //FIX ME: Algorytm do generowania numeru faktury
            Vat.NoVAT = 1;

            Vat.Date = DateTime.Now;
            Vat.ClientFirstName = client.First().FirstName;
            Vat.ClientLastName = client.First().LastName;
            Vat.Address = client.First().Address;
            Vat.NIP = Convert.ToInt64(NIP);
            Vat.ProductsAmountOrServices = amount.ToString();
            Vat.UnitPrice = u_price;
            Vat.Discount = 0;
            Vat.TotalPriceNet = amount * u_price;
            Vat.TaxRate = 30;
            Vat.TotalPrice = (amount * u_price) * 1.3m;

            db.Vats.Add(Vat);
            db.SaveChanges();

            return Vat;
        }

        // GET: VATs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAT vAT = db.Vats.Find(id);
            if (vAT == null)
            {
                return HttpNotFound();
            }
            return View(vAT);
        }

        // POST: VATs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NoVAT,Date,ClientFirstName,ClientLastName,Address,NIP,ProductsAmountOrServices,UnitPrice,Discount,TotalPriceNet,TaxRate,TotalPrice")] VAT vAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vAT);
        }

        // GET: VATs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VAT vAT = db.Vats.Find(id);
            if (vAT == null)
            {
                return HttpNotFound();
            }
            return View(vAT);
        }

        // POST: VATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VAT vAT = db.Vats.Find(id);
            db.Vats.Remove(vAT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult VatsHistory()
        {
            var userId = User.Identity.GetUserId();
            var user = identityDb.Users.FirstOrDefault(x => x.Id.Equals(userId));

            var vats = from p in db.Vats select p;
            var _vats = new List<VAT>();

            vats = vats.Where(x => x.NIP == user.MyUserInfo.NIP_Regon);

            try
            {
                _vats = vats.ToList();
            }
            catch(Exception e)
            {
                return View(e.Message.ToString());
            }

            return View("VatsHistory", _vats);
        }

        public PartialViewResult PartialHistory()
        {          
            var userId = User.Identity.GetUserId();
            var user = identityDb.Users.FirstOrDefault(x => x.Id.Equals(userId));

            var vats = from p in db.Vats select p;
            var _list = new List<VAT>();

            try
            {
                vats = vats.Where(x => x.NIP == user.MyUserInfo.NIP_Regon);
                _list = vats.ToList();
            }
            catch (Exception e) { }

            return PartialView("_HistoryPartial", _list);
        }

        public decimal GetPrice(string fuel)
        {
            decimal u_price;
            var prices = from p in db.Prices select p;

            switch (fuel)
            {
                case "E95":
                    u_price = prices.FirstOrDefault().Pb95;
                    break;
                case "E98":
                    u_price = prices.FirstOrDefault().Pb98;
                    break;
                case "ON":
                    u_price = prices.FirstOrDefault().On;
                    break;
                case "LPG":
                    u_price = prices.FirstOrDefault().Lpg;
                    break;
                default:
                    u_price = 10; break;
            }

            return u_price;
        }

        public void AddPoints(string fuel, decimal volume)
        {
            var context = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(x => x.Id.Equals(userId));

            var curr = context.LoyalStatus.FirstOrDefault(x => x.ApplicationUser.Id.Equals(userId));
            var _points = from p in db.Loyality select p;

            try
            {
                int points;
                int multiply = Convert.ToInt32(Math.Floor(volume));

                switch (fuel)
                {
                    case "E95":
                        points = _points.FirstOrDefault().PbOnPtsReciv * multiply;
                        break;
                    case "E98":
                        points = _points.FirstOrDefault().PbOnPtsReciv * multiply;
                        break;
                    case "ON":
                        points = _points.FirstOrDefault().PbOnPtsReciv * multiply;
                        break;
                    case "LPG":
                        points = _points.FirstOrDefault().LPGPtsReciv * multiply;
                        break;
                    default:
                        points = 0; break;
                }

                if (curr != null)
                {
                    curr.CurrPts += points;
                    curr.LifetimePts += points;

                    UpdateModel(curr, "model");
                }
                else
                {
                    curr = new LoyalityStatus();

                    curr.CurrPts = points;
                    curr.LifetimePts = points;
                    curr.ApplicationUser = user;

                    context.LoyalStatus.Add(curr);
                }

                context.SaveChanges();
            }
            catch (Exception e) { }
        }
    }
}
