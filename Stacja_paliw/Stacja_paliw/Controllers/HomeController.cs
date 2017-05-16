using Microsoft.AspNet.Identity;
using PetrolStationDB;
using Stacja_paliw.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DomainModel;

namespace Stacja_paliw.Controllers
{
    public class HomeController : Controller
    {
        private PSDbContext _db = new PSDbContext();
        private ApplicationDbContext _identityDb = new ApplicationDbContext();

        public ActionResult Index()
        {
            try
            {
                var _prices = from p in _db.Prices select p;
                ViewBag.pb95 = _prices.FirstOrDefault().Pb95;
                ViewBag.pb98 = _prices.FirstOrDefault().Pb98;
                ViewBag.lpg = _prices.FirstOrDefault().Lpg;
                ViewBag.diesel = _prices.FirstOrDefault().On;
            }
            catch (Exception e)
            {

                return View(e.Message.ToString());
            }

            return View();
        }

        public PartialViewResult LoyalityPartial()
        {
            var userId = User.Identity.GetUserId();
            var user = _identityDb.Users.FirstOrDefault(x => x.Id.Equals(userId));

            var points = from p in _identityDb.LoyalStatus select p;

            try
            {
                points = points.Where(x => x.ApplicationUser.Id == user.Id);
                ViewBag.Curr = points.FirstOrDefault().CurrPts;
                ViewBag.Total = points.FirstOrDefault().LifetimePts;
            }
            catch (Exception e)
            {
                return PartialView(e.Message.ToString());
            }

            return PartialView("_PointsPartial");
        }

        public PartialViewResult CarWashPartial()
        {
            try
            {
                var prices = from s in _db.Prices select s;

                List<Tuple<string, string>> services = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("Pełne mycie", prices.FirstOrDefault().Wash.ToString()),
                    new Tuple<string, string>("Mycie z woskowaniem", prices.FirstOrDefault().Waxing.ToString())
                };

                return PartialView("~/Views/CarWash/_Prices.cshtml", services);

            }
            catch (Exception e)
            {
                return PartialView(e.Message.ToString());
            }
        }

        public ActionResult Terms()
        {
            return View();
        }
    }
}