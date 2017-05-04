using PetrolStationDB;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Stacja_paliw.Controllers
{
    public class HomeController : Controller
    {
        private PSDbContext db = new PSDbContext();

        public ActionResult Index()
        {
            try
            {
                var _prices = from p in db.Prices select p;
                ViewBag.pb95 = _prices.FirstOrDefault().Pb95;
                ViewBag.pb98 = _prices.FirstOrDefault().Pb98;
                ViewBag.lpg = _prices.FirstOrDefault().Lpg;
                ViewBag.diesel = _prices.FirstOrDefault().On;
            }
            catch (Exception e) { }

            return View();
        }
    }
}