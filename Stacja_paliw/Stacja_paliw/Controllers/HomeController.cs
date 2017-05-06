using Microsoft.AspNet.Identity;
using PetrolStationDB;
using Stacja_paliw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Stacja_paliw.Controllers
{
    public class HomeController : Controller
    {
        private PSDbContext db = new PSDbContext();
        private ApplicationDbContext identityDb = new ApplicationDbContext();

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
            catch (Exception e) {

                return View(e.Message.ToString());
            }

            return View();
        }

        public PartialViewResult LoyalityPartial()
        {
            var userId = User.Identity.GetUserId();
            var user = identityDb.Users.FirstOrDefault(x => x.Id.Equals(userId));

            var points = from p in identityDb.LoyalStatus select p;

            try
            {
                points = points.Where(x => x.ApplicationUser.Id == user.Id);
                ViewBag.Curr = points.FirstOrDefault().CurrPts;
                ViewBag.Total = points.FirstOrDefault().LifetimePts;
            }
            catch (Exception e) {
                return PartialView(e.Message.ToString());
            }

            return PartialView("_PointsPartial");
        }

        public ActionResult Terms()
        {
            return View();
        }
    }
}