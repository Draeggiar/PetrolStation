using PetrolStationDB;
using System.Linq;
using System.Web.Mvc;

namespace Stacja_paliw.Controllers
{
    public class HomeController : Controller
    {
        private PSDbContext db = new PSDbContext();

        public ActionResult Index()
        {
            var items = from p in db.Prices select p;

            return View(db.Prices.ToList().Last());
        }
    }
}