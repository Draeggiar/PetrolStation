using System;
using System.Web.Mvc;
using Stacja_paliw.MonitoringService;

namespace Stacja_paliw.Areas.Worker.Controllers
{
    public class MonitoringController : Controller
    {
        private static readonly MonitoringService.MonitoringServiceClient ServiceClient = new MonitoringServiceClient();
        // GET: Monitoring
        public ActionResult Index()
        {
            try
            {
                ServiceClient.Open();
                ViewBag.Authenticate = "Pomyślnie uwierzytelniono.";
                ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            }            
            catch (Exception exception)
            {
                //ViewBag.Authenticate = "Błąd autentykacji!" + exception.Message;
            }

            return View();
        }

        public ActionResult View(int cameraID)
        {
            ViewBag.Message = ServiceClient.ViewCamera(cameraID);
            ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }

        public ActionResult Start()
        {
            ViewBag.Message = ServiceClient.StartMonitoring();
            ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();           
            return View("Index");
        }

        public ActionResult Stop()
        {
            ViewBag.Message = ServiceClient.StopMonitoring();
            ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }
    }
}