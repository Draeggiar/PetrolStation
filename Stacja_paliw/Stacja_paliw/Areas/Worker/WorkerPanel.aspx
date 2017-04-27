<%@ Page Title="Panel pracownika" Language="C#" CodeBehind="WorkerPanel.aspx.cs" MasterPageFile="~/Views/Shared/_WebFormsMasterPage.master" Inherits="Stacja_paliw.Areas.Worker.WorkerPanel" %>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <asp:ScriptManager runat="server" />
    <asp:Timer runat="server" ID="timUpdater" Interval="1000" />

    <a href='http://<%:HttpContext.Current.Request.Url.Authority %>/Worker/Monitoring' class="btn-link">Centrum monitoringu</a>
    <br />

    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="timUpdater" EventName="Tick" />
        </Triggers>
        <ContentTemplate>
            <asp:Repeater ID="rptTransactions" runat="server" EnableViewState="True">
                <HeaderTemplate>
                    <table width="80%">
                        <tr>
                            <th>Dystrybutor</th>
                            <th>Ilość paliwa</th>
                            <th>Do zapłaty</th>
                            <th>Akcje</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblDistName" runat="server" Text='<%#Eval("DistributorName") %>'/>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelVolume" runat="server" Text='<%# $"{Eval("Volume"):0.###}" %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPrice" runat="server" Text='<%# $"{Eval("TotalPrice"):0.##}" %>' />
                        </td>
                        <td>
                            <asp:Button ID="btnAcceptTransaction1" runat="server" OnClick="btnAcceptTransaction_OnClick" Text="Faktura" />
                        </td>
                        <td>
                            <asp:Button ID="btnAcceptTransaction2" runat="server" OnClick="btnAcceptTransaction_OnClick" Text="Rachunek" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
