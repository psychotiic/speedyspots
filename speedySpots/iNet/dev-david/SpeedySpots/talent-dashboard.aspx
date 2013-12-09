<%@ Page Title="Speedy Spots :: Talent Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="talent-dashboard.aspx.cs" Inherits="SpeedySpots.talent_dashboard" %>
<%@ Register Src="~/Controls/Tabs/Talent-Tabs.ascx" TagName="TalentTabs" TagPrefix="SS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <h2><a href="user-account.aspx"><%=string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")) %></a> Dashboard</h2>
    <div class="group">
        <SS:TalentTabs id="ucTalentTabs" SelectedTab="Pending" runat="server" />
        
        <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
            <MasterTableView NoMasterRecordsText="No Production Orders"></MasterTableView>
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
            <telerik:GridSortExpression FieldName="DueDateTime" SortOrder="Ascending" />
            </SortExpressions>
                <Columns>
                    <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IAProductionOrderID" UniqueName="IAProductionOrderID" Visible="False"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IsAsap" UniqueName="IsAsap" Visible="False"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="HasBeenViewedByTalent" UniqueName="HasBeenViewedByTalent" Visible="False"></telerik:GridBoundColumn>
                    <telerik:GridHyperLinkColumn DataTextField="JobName" HeaderText="Production Order" UniqueName="JobName" SortExpression="JobName" DataNavigateUrlFields="IAProductionOrderID" DataNavigateUrlFormatString="~/talent-production-order-details.aspx?s=p&id={0}"></telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="JobNumber" HeaderText="Job #" UniqueName="JobNumber" SortExpression="JobNumber"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SpotCount" HeaderText="Spots" UniqueName="SpotCount"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DueDateTime" DataFormatString="{0:ddd dd a\t h:mm tt}" HeaderText="Earliest Due Date/Time" UniqueName="DueDateTime"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>

            <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
        </telerik:RadGrid>
        
    </div>
</div>
</asp:Content>
