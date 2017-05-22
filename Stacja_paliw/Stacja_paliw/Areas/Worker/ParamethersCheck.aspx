<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/_WebFormsMasterPage.master" AutoEventWireup="true" CodeBehind="ParamethersCheck.aspx.cs" Inherits="Stacja_paliw.Areas.Worker.ParamethersCheck" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:ScriptManager runat="server" />

    <div style="margin-left:20%">
        <h2><b>Stan parametrów w zbiornikach</b></h2>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Repeater runat="server" DataSourceID="srcParamethers">
                <HeaderTemplate>
                    <table style="width: 80%; margin-left: 5%">
                        <tr>
                            <th>Zbiornik</th>
                            <th>Typ paliwa</th>
                            <th>Poziom paliwa</th>
                            <th>Temperatura</th>
                            <th>Ciśnienie</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text='<%# Eval("FuelLevel") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text='<%# Eval("Temperature") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" Text='<%# Eval("Pressure") %>'></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="srcParamethers" runat="server" ConnectionString="<%$ ConnectionStrings:PerolStationDB %>" SelectCommand="SELECT * FROM [FuelTanksParamethers] ORDER BY [Id]"></asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
