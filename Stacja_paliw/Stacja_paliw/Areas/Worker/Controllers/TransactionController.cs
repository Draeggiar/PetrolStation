
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stacja_paliw.Areas.Worker.Models;

namespace Stacja_paliw.Areas.Worker.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Worker/Transaction
        public ActionResult Faktura(double volume, double totalPrice)
        {
            TransactionData td = new TransactionData(volume, totalPrice);
            RedirectToAction("Landing", "VATs", td);
            return View();
        }

        public ActionResult Rachunek(double volume, double totalPrice)
        {
            TransactionData td = new TransactionData(volume, totalPrice);
            
            return View(td);
        }
    }
}