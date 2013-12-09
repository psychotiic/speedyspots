<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="staff-dashboard-new.aspx.cs" Inherits="SpeedySpots.staff_dashboard_new" %>
<%@ Register Src="~/Controls/StaffDashboardRequests.ascx" TagName="RequestDashboard" TagPrefix="SS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
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
<script src="js/Staff.Dashboard.Requests.js" type="text/javascript"></script>
<script src="js/jquery.multiSelect.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<div class="full">
    <div id="producer_qc_switch">
        <strong>Working as:</strong> <a href="staff-dashboard.aspx" class="at">Staff</a> | <a href="admin-dashboard.aspx">Admin</a>
    </div>
    <h2><a href="user-account.aspx"><asp:Literal ID="m_listUsersName" runat="server" /></a> Dashboard</h2>

    <div class="group">
        <div class="order-list">
            <div class="tab-holder group">
                <ul id="dashboard-tabs">
                    <li class="at"><a href="staff-dashboard.aspx">Requests</a></li>
                    <li><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
                    <li><a href="staff-dashboard-completed.aspx">Completed</a></li>
                    <li><a href="admin-dashboard.aspx">Admin</a></li>
                    <li><a href="messages-inbox.aspx">Messages</a></li>
                </ul>
            </div>
            
            <div class="button-row action">
                <a href="create-request.aspx" class="button primaryAction">Create Request For Customer</a>
            </div>
        </div>

        <div class="form-holder filter">
            <fieldset>
                <legend>Filter Requests:</legend>
                <div class="group">
                    <asp:Label ID="m_lblRequestNumber" runat="server" Text="Request #:" AssociatedControlID="m_txtRequestNumber" EnableViewState="false" />
                    <div class="inputgroup">
                        <asp:TextBox ID="m_txtRequestNumber" runat="server" />
                        <asp:Label ID="m_lblCreatedDate" runat="server" Text="Request Date:" AssociatedControlID="m_dtCreatedDate"></asp:Label>
                        <telerik:RadDatePicker runat="server" ID="m_dtCreatedDate"></telerik:RadDatePicker>
                    </div>
                </div>
                <div class="group">
                    <asp:Label ID="m_lblRequestedBy" runat="server" Text="Requested By:" AssociatedControlID="m_txtRequestedBy" EnableViewState="false" />
                    <div class="inputgroup">                               
                        <telerik:RadTextBox ID="m_txtRequestedBy" EmptyMessage="name, email address or company name" runat="server" Columns="50"></telerik:RadTextBox>
                        <asp:Label ID="m_lblLanguage" runat="server" Text="Language:" AssociatedControlID="m_lstLanugage" EnableViewState="false" />
                        <span id="languageWrap"><asp:ListBox ID="m_lstLanugage" runat="server" SelectionMode="Multiple" /></span>
                    </div>
                </div>
                <div class="group">
                    <asp:Label ID="m_lblStatus" runat="server" Text="Status:" AssociatedControlID="m_lstStatus" EnableViewState="false" />
                    <div class="inputgroup">   
                        <span id="statusWrap"><asp:ListBox ID="m_lstStatus" runat="server" SelectionMode="Multiple" /></span>
                        <asp:CheckBox ID="m_chkRequestIsAsap" runat="server" Text="ASAP" />
                        <asp:Label ID="Label4" runat="server" Text="Labels:" AssociatedControlID="m_lstLabels" EnableViewState="false" />
                        <span id="labelsWrap"><asp:ListBox ID="m_lstLabels" runat="server" SelectionMode="Multiple" /></span>
                    </div>
                </div>
            </fieldset>
            <div class="button-row action">
                <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter Requests" CssClass="button primaryAction" OnClick="OnFilterRequests" EnableViewState="false" />
            </div>
        </div>

        <div id="listWrap" class="RadGrid_test nontel">
            <table id="requests" cellspacing="0" class="rgMasterTable" border="0" style="width:100%;table-layout:auto;empty-cells:show;">
                <thead>
                    <tr>
                        <th class="rgHeader"><a href="staff-dashboard-new.aspx" data-sortName="IsRushOrder" data-sortDirection="DESC" data-orginalSortDir="DESC">Priority</a></th>
                        <th class="rgHeader"><a href="staff-dashboard-new.aspx" data-sortName="IARequestID" data-sortDirection="DESC" data-orginalSortDir="DESC">Request #</a></th>
                        <th class="rgHeader"><a href="staff-dashboard-new.aspx" data-sortName="CompanyName" data-sortDirection="ASC" data-orginalSortDir="ASC">Company</a></th>
                        <th class="rgHeader"><a href="staff-dashboard-new.aspx" data-sortName="UserName" data-sortDirection="ASC" data-orginalSortDir="ASC">Requested By</a></th>
                        <th class="rgHeader"><a href="staff-dashboard-new.aspx" data-sortName="CreatedDateTime" data-sortDirection="DESC" data-orginalSortDir="DESC">Requested</a></th>
                        <th class="rgHeader"><a href="staff-dashboard-new.aspx" data-sortName="Status" data-sortDirection="ASC" data-orginalSortDir="ASC">Status</a></th>
                        <th class="rgHeader">Labels</th>
                    </tr>
                </thead>
                <tbody><SS:RequestDashboard ID="m_RequestDashboard" runat="server" /></tbody>
            </table>

            <div class="button-row action">
                <div id="itemCountText" style="float: right; text-align: right;"></div>
                <a id="prevButton" href="staff-dashboard-new.aspx" class="button">&laquo; Prev</a><a id="nextButton" href="staff-dashboard-new.aspx" class="button">Next &raquo;</a>
                <input type="text" id="currentPage" size="2" /> of <span id="totalPages"></span> <a href="staff-dashboard-new.aspx" id="goToPage" class="button">Go</a>
                Page Size: <input type="text" id="itemsPerPage" size="2" /> <a href="staff-dashboard-new.aspx" id="goChangePerPage" class="button">Change</a>
            </div>
            
            <div id="dashboardSettingsTracking">
                <asp:HiddenField ID="m_hdnLanugage" runat="server" /><asp:HiddenField ID="m_hdnStatus" runat="server" />
                <asp:HiddenField ID="m_hdnLabels" runat="server" /><asp:HiddenField ID="m_hdnPageSize" runat="server" />
                <asp:HiddenField ID="m_hdnSortName" runat="server" /><asp:HiddenField ID="m_hdnSortDir" runat="server" />
            </div>
        </div>
    </div>

</div>

<style>
tr.overlay {
    width: 100%;
    height: 150px;
    text-align:center;
}
</style>

<script>
    $(function () {
        var dashboardSettingsTracking = $('#dashboardSettingsTracking');
        var itemsPerPageTracker = dashboardSettingsTracking.find('#<%=m_hdnPageSize.ClientID %>');
        var itemsSortNameTracker = dashboardSettingsTracking.find('#<%=m_hdnSortName.ClientID %>');
        var itemsSortDirTracker = dashboardSettingsTracking.find('#<%=m_hdnSortDir.ClientID %>');
        var currentPage = <%=InitalPageNumber %>;
                
        StaffDashboardRequests.Init(currentPage, itemsPerPageTracker, itemsSortNameTracker, itemsSortDirTracker);

        var languageHiddenField = dashboardSettingsTracking.find('#<%=m_hdnLanugage.ClientID %>');
        var languageSelect = $('#<%=m_lstLanugage.ClientID %>');
        StaffDashboardRequests.MultiSelectFilterConfigure($('#languageWrap'), languageHiddenField, languageSelect);

        var statusHiddenField = dashboardSettingsTracking.find('#<%=m_hdnStatus.ClientID %>');
        var statusSelect = $('#<%=m_lstStatus.ClientID %>');
        StaffDashboardRequests.MultiSelectFilterConfigure($('#statusWrap'), statusHiddenField, statusSelect);
        
        var labelsHiddenField = dashboardSettingsTracking.find('#<%=m_hdnLabels.ClientID %>');
        var labelsSelect = $('#<%=m_lstLabels.ClientID %>');
        StaffDashboardRequests.MultiSelectFilterConfigure($('#labelsWrap'), labelsHiddenField, labelsSelect);
    });   
</script>
</asp:Content>