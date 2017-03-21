using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using FuelDistributors;

namespace Stacja_paliw.Areas.Worker
{
    public partial class WorkerPanel : System.Web.UI.Page
    {
        private static List<DistributorHandler> _distributors;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Thread t = new Thread(() =>
                {                    
                    MainWindow _mainWindow = new MainWindow(GetDistributorsData);
                    _mainWindow.Show();
                    System.Windows.Threading.Dispatcher.Run();
                });
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            rptTransactions.DataSource = _distributors;
            rptTransactions.DataBind();
        }

        protected void btnAcceptTransaction_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GetDistributorsData(List<FuelDistributors.DistributorHandler> distributors)
        {
            _distributors = distributors;
        }
    }
}