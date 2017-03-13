using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonitoringCompany;

namespace Stacja_paliw.Controllers
{
    public class MonitoringController : Controller
    {
        public MonitoringCompany.MonitoringService service = new MonitoringService();
        // GET: Monitoring
        public ActionResult Index()
        {
            ViewBag.ServiceStatus = service.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View();
        }

        public ActionResult View(int cameraID)
        {
            ViewBag.Message = service.ViewCamera(cameraID);
            ViewBag.ServiceStatus = service.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }

        public ActionResult Start()
        {
            ViewBag.Message = service.StartMonitoring();
            ViewBag.ServiceStatus = service.GetServiceStatus().MonitoringStarted.ToString().ToLower();           
            return View("Index");
        }

        public ActionResult Stop()
        {
            ViewBag.Message = service.StopMonitoring();
            ViewBag.ServiceStatus = service.GetServiceStatus().MonitoringStarted.ToString().ToLower();
            return View("Index");
        }
    }
}