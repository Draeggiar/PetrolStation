﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using FuelDistributors;

namespace Stacja_paliw.Areas.Worker
{
    //TODO podgląd aktualnego stanu parametrów
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
            var senderButton = sender as Button;

            foreach (RepeaterItem item in rptTransactions.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var btnAcceptTransaction1 = (Button)item.FindControl("btnAcceptTransaction1");
                    var btnAcceptTransaction2 = (Button)item.FindControl("btnAcceptTransaction1");
                    if (senderButton.GetHashCode() == btnAcceptTransaction1.GetHashCode())
                    {
                        var lblDistName = (Label)item.FindControl("lblDistName");
                        var lblVolume = (Label)item.FindControl("lblFuelVolume");
                        var lblTotalPrice = (Label)item.FindControl("lblTotalPrice");

                        Response.Redirect("/Transaction/Faktura/?Vlolume=" + lblVolume.Text +
                                         "&Total_price=" + lblTotalPrice.Text);
                        _distributors.First(d => d.DistributorName == lblDistName.Text).ResetDistributor();
                    }
                    else if (senderButton.GetHashCode() == btnAcceptTransaction2.GetHashCode())
                    {
                        var lblDistName = (Label)item.FindControl("lblDistName");
                        var lblVolume = (Label)item.FindControl("lblFuelVolume");
                        var lblTotalPrice = (Label)item.FindControl("lblTotalPrice");

                        Response.Redirect("/Transaction/Rachunek/?Vlolume=" + lblVolume.Text +
                                         "&Total_price=" + lblTotalPrice.Text);
                        _distributors.First(d => d.DistributorName == lblDistName.Text).ResetDistributor();
                    }
                }
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
                    var lblDistName = (Label)item.FindControl("lblDistName");
                    var btnAcceptTransaction1 = (Button)item.FindControl("btnAcceptTransaction1");
                    var btnAcceptTransaction2 = (Button)item.FindControl("btnAcceptTransaction2");

                    #region --PriceLessThanZeroOrBusy--

                    if (
                        _distributors.First(d => d.DistributorName == lblDistName.Text)
                            .IsBusy
                        || ((Label)item.FindControl("lblTotalPrice")).Text == @"0")
                    {
                        btnAcceptTransaction1.Attributes.Add("hidden", "hidden");
                        btnAcceptTransaction2.Attributes.Add("hidden", "hidden");
                    }
                    else
                    {
                        btnAcceptTransaction1.Attributes.Add("visible", "visible");
                        btnAcceptTransaction2.Attributes.Add("visible", "visible");
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