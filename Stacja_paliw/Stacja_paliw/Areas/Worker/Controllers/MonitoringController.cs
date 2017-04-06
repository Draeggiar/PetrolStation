using System;
using System.Web.Mvc;
using Stacja_paliw.MonitoringService;

namespace Stacja_paliw.Areas.Worker.Controllers
{
    public class MonitoringController : Controller
    {
        private MonitoringServiceClient ServiceClient { get; set; }

        [Route("MonitoringPanel")]
        public ActionResult Index()
        {
            try
            {
                if (ServiceClient == null)
                    ServiceClient = new MonitoringServiceClient();
                ServiceClient.Open();
                ViewBag.Authenticate = "Pomyślnie uwierzytelniono. Witaj " + User.Identity.Name;
                ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            }
            catch (System.ServiceModel.EndpointNotFoundException e)
            {
                ViewBag.Authenticate = "Nie znaleziono serwisu, sprawdź czy jest włączony";
            }           
            catch (InvalidOperationException e)
            {
                ViewBag.Authenticate = "Błąd autentykacji!";
            }

            return View();
        }

        protected ActionResult View(int cameraID)
        {
            ViewBag.Message = ServiceClient.ViewCamera(cameraID);
            ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }

        protected ActionResult Start()
        {
            ViewBag.Message = ServiceClient.StartMonitoring();
            ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();           
            return View("Index");
        }

        protected ActionResult Stop()
        {
            ViewBag.Message = ServiceClient.StopMonitoring();
            ViewBag.ServiceStatus = ServiceClient.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }
    }
}