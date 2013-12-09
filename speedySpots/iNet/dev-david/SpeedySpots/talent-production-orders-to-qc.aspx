<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="talent-production-orders-to-qc.aspx.cs" Inherits="SpeedySpots.talent_production_orders_to_qc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Controls/Tabs/Talent-Tabs.ascx" TagName="TalentTabs" TagPrefix="SS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <h2>Production Orders Sent To QC</h2>
    <div class="group">
        <SS:TalentTabs id="ucTalentTabs" SelectedTab="ToQC" runat="server" />
    
        <div class="form-holder filter">
            <fieldset>
                <legend>Sent to QC Filter:</legend>
                <div class="group">
                    <asp:Label ID="m_lblFromDate" runat="server" Text="From Date/Time:" AssociatedControlID="m_dtFrom"></asp:Label>
                    <div class="inputgroup">
                        <telerik:RadDateTimePicker runat="server" ID="m_dtFrom" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                            <Calendar>
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                    <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDateTimePicker>
                        <asp:Label ID="m_lblToDate" runat="server" Text="To Date/Time:" AssociatedControlID="m_dtTo"></asp:Label>
                        <telerik:RadDateTimePicker runat="server" ID="m_dtTo" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                            <Calendar>
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                    <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDateTimePicker>
                    </div>
                </div>
            </fieldset>
            <div class="button-row">
                <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter" CssClass="button primaryAction" OnClick="OnFilter" />
            </div>
        </div>

        <h3>Production Orders sent to QC from <%=string.Format("{0:d}", m_dtFrom.SelectedDate) %> to <%=string.Format("{0:d}", m_dtTo.SelectedDate) %></h3>
        <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
            <MasterTableView NoMasterRecordsText="No Production Orders sent to QC durring this period"></MasterTableView>
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
                    <telerik:GridHyperLinkColumn DataTextField="Name" HeaderText="Production Order" UniqueName="Name" SortExpression="Name" DataNavigateUrlFields="IAProductionOrderID" DataNavigateUrlFormatString="~/talent-production-order-details.aspx?s=s&id={0}"></telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="JobNumber" HeaderText="Job #" UniqueName="JobNumber" SortExpression="JobNumber"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TalentMarkedCompleteDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Sent to QC" UniqueName="TalentMarkedCompleteDateTime"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QCJobMarkedCompletDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="QC Marked Complete" UniqueName="QCMarkedCompleteDateTime"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>

            <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
        </telerik:RadGrid>
    </div>
</div>
</asp:Content>