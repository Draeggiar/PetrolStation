using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DomainModel;
using PetrolStationDB;

namespace Stacja_paliw.Controllers
{
    public class VATsController : Controller
    {
        private PSDbContext db = new PSDbContext();

        // GET: VATs
        public ActionResult Index()
        {
            return View(db.Vats.ToList());
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
        public ActionResult Create([Bind(Include = "Id,NoVAT,Date,ClientFirstName,ClientLastName,Address,NIP,ProductsAmountOrServices,UnitPrice,Discount,TotalPriceNet,TaxRate,TotalPrice")] VAT vAT)
        {
            if (ModelState.IsValid)
            {
                db.Vats.Add(vAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vAT);
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
