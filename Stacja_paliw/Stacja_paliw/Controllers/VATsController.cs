using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DomainModel;
using PetrolStationDB;
using Stacja_paliw.Models;
using System;

namespace Stacja_paliw.Controllers
{
    public class VATsController : Controller
    {
        private PSDbContext db = new PSDbContext();
        private ApplicationDbContext identityDb = new ApplicationDbContext();

        // GET: VATs
        public ActionResult Index()
        {
            return View(db.Vats.ToList());
        }

        public ActionResult Landing()
        {
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

            if (!string.IsNullOrEmpty(q))
            {
                client = client.Where(x => x.NIP_Regon.ToString() == q);
            }

            ViewBag.NoVat = vat.FirstOrDefault().NoVAT;
            ViewBag.Amount = vat.FirstOrDefault().ProductsAmountOrServices;
            ViewBag.UPrice = vat.FirstOrDefault().UnitPrice;
            ViewBag.Discount = vat.FirstOrDefault().Discount;
            ViewBag.PriceNet = vat.FirstOrDefault().TotalPriceNet;
            ViewBag.Tax = vat.FirstOrDefault().TaxRate;
            ViewBag.Total = vat.FirstOrDefault().TotalPrice;

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
    }
}
