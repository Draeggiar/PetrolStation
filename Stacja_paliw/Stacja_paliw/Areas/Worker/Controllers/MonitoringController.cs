using System.Web.Mvc;
using MonitoringCompany;

namespace Stacja_paliw.Areas.Worker.Controllers
{
    public class MonitoringController : Controller
    {
        private MonitoringCompany.MonitoringService _service = new MonitoringService();
        // GET: Monitoring
        public ActionResult Index()
        {
            ViewBag.ServiceStatus = _service.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View();
        }

        public ActionResult View(int cameraID)
        {
            ViewBag.Message = _service.ViewCamera(cameraID);
            ViewBag.ServiceStatus = _service.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }

        public ActionResult Start()
        {
            ViewBag.Message = _service.StartMonitoring();
            ViewBag.ServiceStatus = _service.GetServiceStatus().MonitoringStarted.ToString().ToLower();           
            return View("Index");
        }

        public ActionResult Stop()
        {
            ViewBag.Message = _service.StopMonitoring();
            ViewBag.ServiceStatus = _service.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }
    }
}