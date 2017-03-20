using System.Web.Mvc;

namespace Stacja_paliw.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}