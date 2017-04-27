using System;
using System.Drawing;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Windows;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Month;
using Stacja_paliw.DAL;
using InitArgs = DayPilot.Web.Mvc.Events.Month.InitArgs;

namespace Stacja_paliw.Controllers
{
    public class CarWashController : Controller
    {
        // GET: CarWash
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Backend()
        {
            return new Dpm().CallBack(this);
        }

        class Dpm : DayPilotMonth
        {
            private string DataUserField { get; set; }
            private CalendarEventsDataContext _db;

            protected override void OnInit(InitArgs initArgs)
            {
                _db = new CalendarEventsDataContext();
                //Events = from ev in _db.CarWashMonthlyEvents select ev;

                //DataIdField = "Id";
                //DataTextField = "Text";
                //DataStartField = "EventStart";
                //DataEndField = "EventEnd";
                //DataUserField = "UserName";

                Update();
            }

            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = from ev in _db.CarWashMonthlyEvents select ev;

                DataIdField = "Id";
                DataTextField = "Text";
                DataStartField = "EventStart";
                DataEndField = "EventEnd";
                DataUserField = "UserName";
            }

            #region ---Events---

            protected override void OnEventMove(EventMoveArgs e)
            {
                try
                {
                    var toBeResized =
                        (from ev in _db.CarWashMonthlyEvents where ev.Id == Convert.ToInt32(e.Id) select ev).First();

                    if (toBeResized.UserName == Controller.User.Identity.Name)
                    {
                        toBeResized.EventStart = e.NewStart;
                        toBeResized.EventEnd = e.NewEnd;
                        _db.SubmitChanges();
                        Update();                       
                    }
                    else
                    {
                       throw new AuthenticationException("Nie masz uprawnień do zmiany tego obiektu");
                    }
                }
                catch (Exception ex)
                {
                    Controller.ModelState.AddModelError("ErrorMessage", ex);
                }
            }

            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                try
                {
                    if (Controller.User.Identity.Name != null)
                    {
                        var toBeCreated = new CarWashMonthlyEvent
                        {
                            EventStart = e.Start,
                            EventEnd = e.End,
                            Text = (string) e.Data["eventName"],
                            UserName = Controller.User.Identity.Name
                        };
                        _db.CarWashMonthlyEvents.InsertOnSubmit(toBeCreated);
                        _db.SubmitChanges();
                        Update(Events);
                    }
                    else
                    {
                        throw new AuthenticationException("Musisz być zalogowany aby zarezerwować termin");
                    }
                }
                catch (Exception ex)
                {
                   Controller.ModelState.AddModelError("ErrorMessage", ex);
                }
            }

            protected override void OnBeforeEventRender(BeforeEventRenderArgs e)
            {
                base.OnBeforeEventRender(e);
                e.BackgroundColor = UserNameToColor(e.DataItem["UserName"].ToString());
                e.FontColor = ContrastColor(e.BackgroundColor);
            }

            #endregion

            #region ---Methods---

            private string UserNameToColor(string userName)
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(userName));

                return ColorTranslator.ToHtml(Color.FromArgb(hash[0] + hash[1] + hash[2]));
            }

            private string ContrastColor(string htmlColor)
            {
                var color = ColorTranslator.FromHtml(htmlColor);

                double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

                int d = a < 0.5 ? 0 : 255;

                return ColorTranslator.ToHtml(Color.FromArgb(d, d, d));
            }

            #endregion
        }
    }
}