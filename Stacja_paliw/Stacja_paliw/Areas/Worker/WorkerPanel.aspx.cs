using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using FuelDistributors;

namespace Stacja_paliw.Areas.Worker
{
    public partial class WorkerPanel : System.Web.UI.Page
    {
        #region ---Fields---

        private static List<DistributorHandler> _distributors;

        #endregion

        #region ---PageLoad---

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Thread t = new Thread(() =>
                {                    
                    MainWindow mainWindow = new MainWindow(GetDistributorsData);
                    mainWindow.Show();
                    System.Windows.Threading.Dispatcher.Run();
                });
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
            }
        }

        #endregion

        #region ---PreRender---

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            rptTransactions.DataSource = _distributors;
            rptTransactions.DataBind();
            DispatchView();
        }

        #endregion

        #region ---Events---

        protected void btnAcceptTransaction_OnClick(object sender, EventArgs e)
        {
            //TODO rozpoznawanie konkretnego dystrybutora
            foreach (DistributorHandler distributor in _distributors)
            {
                distributor.ResetDistributor();
                //TODO drukowanie faktury
            }
        }

        #endregion

        #region ---Methods---

        private void GetDistributorsData(List<DistributorHandler> distributors)
        {
            _distributors = distributors;
        }

        private void DispatchView()
        {
            foreach (RepeaterItem item in rptTransactions.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var lblDistName = (Label) item.FindControl("lblDistName");
                    var txtNip = (TextBox) item.FindControl("txtNIP");
                    var btnAcceptTransaction = (Button) item.FindControl("btnAcceptTransaction");

                    #region --PriceLessThanZero--

                    if (
                        _distributors.First(d => d.DistributorName == lblDistName.Text)
                            .IsBusy
                        || ((Label) item.FindControl("lblTotalPrice")).Text == @"0")
                    {
                        txtNip.Attributes.Add("disabled", "disabled");
                        btnAcceptTransaction.Attributes.Add("hidden", "hidden");
                    }
                    else
                    {
                        txtNip.Attributes.Add("enabled", "enabled");
                        btnAcceptTransaction.Attributes.Add("visible", "visible");
                    }

                    #endregion

                    #region --ParamethersWarning--

                    //TODO różne rodzaje ostrzeżeń
                    if (!_distributors.First(d => d.DistributorName == lblDistName.Text).FuelTank.IsSafe())
                    {
                        lblDistName.BackColor = Color.Coral;
                    }
                    else
                    {
                        lblDistName.BackColor = Color.White;
                    }

                    #endregion
                }
            }
        }

        #endregion
    }
}