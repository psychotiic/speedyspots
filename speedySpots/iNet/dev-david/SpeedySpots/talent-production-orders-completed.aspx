<%@ Page Title="Speedy Spots :: Completed POs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="talent-production-orders-completed.aspx.cs" Inherits="SpeedySpots.talent_production_orders_completed" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/Tabs/Talent-Tabs.ascx" TagName="TalentTabs" TagPrefix="SS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
.group label.label 
{
    font-size: 14px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="group">
    <div class="full">
        <h2>Completed Production Orders</h2>
        <SS:TalentTabs id="ucTalentTabs" SelectedTab="Completed" runat="server" />
    </div>
</div>
<div class="main">
    <div class="form-holder filter">
        <fieldset>
            <legend>Completed by QC Filter:</legend>
            <div class="group">
                <asp:Label ID="m_lblFromDate" runat="server" Text="From Date/Time:" AssociatedControlID="m_dtFrom"></asp:Label>
                <telerik:RadDateTimePicker runat="server" ID="m_dtFrom" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                    <Calendar>
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                            <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                        </SpecialDays>
                    </Calendar>
                </telerik:RadDateTimePicker>
            </div>
            <div classs="group">
                <asp:Label ID="m_lblToDate" runat="server" Text="To Date/Time:" AssociatedControlID="m_dtTo" CssClass="label"></asp:Label>
                <telerik:RadDateTimePicker runat="server" ID="m_dtTo" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                    <Calendar>
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                            <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                        </SpecialDays>
                    </Calendar>
                </telerik:RadDateTimePicker>
            </div>
        </fieldset>
        <div class="button-row">
            <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter" CssClass="button primaryAction" OnClick="OnFilter" />
        </div>
    </div>
    
    <h3>Completed Production Orders from <%=string.Format("{0:d}", m_dtFrom.SelectedDate) %> to <%=string.Format("{0:d}", m_dtTo.SelectedDate) %></h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoMasterRecordsText="No Completed Production Orders"></MasterTableView>
        <HeaderContextMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </HeaderContextMenu>

        <MasterTableView>
        <RowIndicatorColumn>
        <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>

        <ExpandCollapseColumn>
        <HeaderStyle Width="20px"></HeaderStyle>
        </ExpandCollapseColumn>
        <SortExpressions>
        <telerik:GridSortExpression FieldName="TalentMarkedCompleteDateTime" SortOrder="Ascending" />
        </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="IAProductionOrderID" UniqueName="IAProductionOrderID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Job Name" UniqueName="NameHidden" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn DataTextField="Name" HeaderText="Production Order" UniqueName="Name" SortExpression="Name" DataNavigateUrlFields="IAProductionOrderID" DataNavigateUrlFormatString="~/talent-production-order-details.aspx?s=c&id={0}"></telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn DataField="JobNumber" HeaderText="Job #" UniqueName="JobNumber" SortExpression="JobNumber"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="QCJobMarkedCompletDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="QC Marked Complete" UniqueName="QCMarkedCompleteDateTime"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</div>
<div class="sidebar">
    <h3>Download:</h3>
    <p>Use the filter to select a range of dates for completed production orders. Then, download the completed production orders in a Microsoft Excel spreadsheet.</p>
    <div class="button-row">
        <asp:LinkButton ID="m_btnDownload" runat="server" Text="Download Excel File" OnClick="OnDownload" CssClass="button" />
    </div>
</div>
</asp:Content>
