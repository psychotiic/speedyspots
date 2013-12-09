<%@ Page Title="Speedy Spots :: User Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-dashboard.aspx.cs" Inherits="SpeedySpots.user_dashboard" %>
<%@ Register Src="~/Controls/Tabs/Customer-Tabs.ascx" TagName="CustomerTabs" TagPrefix="SS" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
h2.nomargin 
{
	margin-bottom: 0;
	padding-bottom: 0;
	line-height: 1em;
}

p.company-name 
{
	margin-top: 0;
	padding-top: 0;
	font-size: 1em;
	font-weight: bold;
	color: #666;
}

thead .rgExpandCol,
tbody .expandcol 
{
    display: none;
}

thead a.headerButton
{
    padding: 0 5px;
	background: #c4c4c4 url("img/glass.png") repeat-x 0 50%;
	border: 1px solid #9e9e9e;
	-border-radius: 3px;
	-moz-border-radius: 3px;
	-webkit-border-radius: 3px;
    max-height: 20px;
    min-width: 20px;
    color: #4e4e4e !important;
    text-shadow: 0 1px 1px #fff;
    box-shadow: 0 1px 1px #ccc;
}

thead a.headerButton:hover {
	color: #fff !important;
	background-color: #757575;
	cursor: pointer;
	border-color: #666;
	text-shadow: 0 -1px 1px #333;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <h2 class="nomargin"><%=string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")) %> Dashboard</h2>
    <p class="company-name"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(MemberProtect.CurrentUser.UserID)))%></p>

    <SS:CustomerTabs ID="m_oTabs" runat="server" SelectedTab="Requests" />

    <div class="group">
        <div class="order-list">
            <div class="button-row action">
                <a href="create-request.aspx" class="button primaryAction"><i class="icon-plus"></i> Create New Request</a>
                <a href="javascript:popUp('music.aspx');" class="button"><i class="icon-music"></i> Open Music Library</a>
            </div>
        </div>
    </div>

    <div class="form-holder filter">
        <fieldset>
            <legend>Filter My Requests:</legend>
            <div class="group">
                <asp:Label ID="m_lblCreatedDate" runat="server" Text="Request Date From:" AssociatedControlID="m_dtCreatedDateStart"></asp:Label>
                <div class="inputgroup">
                    <telerik:RadDatePicker runat="server" ID="m_dtCreatedDateStart"></telerik:RadDatePicker>
                    <asp:Label ID="Label1" runat="server" Text="To:" AssociatedControlID="m_dtCreatedDateEnd"></asp:Label>
                    <telerik:RadDatePicker runat="server" ID="m_dtCreatedDateEnd"></telerik:RadDatePicker>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblJobTitle" runat="server" Text="Job:" AssociatedControlID="m_txtJobTitle"></asp:Label>
                <telerik:RadTextBox ID="m_txtJobTitle" runat="server" EmptyMessage="Request # or job title" Columns="50"></telerik:RadTextBox>
            </div>
        </fieldset>
        <div class="button-row action">
            <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter Requests" CssClass="button primaryAction" OnClick="OnFilterRequests" />
        </div>
    </div>

    <h3>Requests:</h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false" CssClass="buttoned">
        <MasterTableView NoMasterRecordsText="No Requests" DataKeyNames="IARequestID"></MasterTableView>
        <HeaderContextMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </HeaderContextMenu>

        <MasterTableView Name="Master" HierarchyDefaultExpanded="false">
        <RowIndicatorColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>

        <ExpandCollapseColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
        </ExpandCollapseColumn>
        <SortExpressions>
        <telerik:GridSortExpression FieldName="CreatedDateTime" SortOrder="Descending" />
        </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IARequestID" UniqueName="IARequestID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn DataTextField="Name" HeaderText="Job Title" UniqueName="Title" SortExpression="Name" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/order-details.aspx?s=m&rid={0}"></telerik:GridHyperLinkColumn>
                <telerik:GridHyperLinkColumn  DataTextField="IARequestID" HeaderText="Req. No." UniqueName="RequestNumber" SortExpression="IARequestID" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/order-details.aspx?s=m&rid={0}"></telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn HeaderText="Expected Delivery" UniqueName="ExpectedDelivery"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Labels" UniqueName="Labels"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CreatedDateTime" DataFormatString="{0:MM/dd/yyyy a\t h:mm tt}" HeaderText="Requested" UniqueName="CreatedDateTime"></telerik:GridBoundColumn>                
            </Columns>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</div>
</asp:Content>
