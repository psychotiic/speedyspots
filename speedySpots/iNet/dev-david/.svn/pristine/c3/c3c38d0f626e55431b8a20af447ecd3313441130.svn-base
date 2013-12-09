<%@ Page Title="Speedy Spots :: Email Templates" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="email-templates.aspx.cs" Inherits="SpeedySpots.email_templates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <div id="producer_qc_switch">
        <strong>Working as:</strong> <a href="staff-dashboard.aspx">Staff</a> | <a href="admin-dashboard.aspx" class="at">Admin</a>
    </div>

    <h2><a href="user-account.aspx"><%=string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")) %></a> Dashboard</h2>
    <div class="tab-holder group">
        <ul id="dashboard-tabs">
            <li><a href="staff-dashboard.aspx">Requests</a></li>
            <li><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
            <li><a href="staff-dashboard-completed.aspx">Completed</a></li>
            <li class="at"><a href="admin-dashboard.aspx">Admin</a></li>
        </ul>
    </div>
    <ul id="dashboard-subnav" class="group">
        <li><a href="admin-dashboard.aspx">Users</a></li>
        <li><a href="companies.aspx">Companies</a></li>
        <li><a href="producer-talent-availability.aspx">Talent Availability</a></li>
        <li class="at"><a href="email-templates.aspx">Email Templates</a></li>
        <li><a href="out-of-office.aspx">Out of Office Notifications</a></li>
    </ul>

    <div class="button-row action">
        <asp:LinkButton ID="m_btnCreate" runat="server" Text="Add New Email Template" CssClass="button primaryAction" OnClick="OnAdd" />
    </div>

    <h3>Email Templates</h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoMasterRecordsText="No Requests"></MasterTableView>
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
        <telerik:GridSortExpression FieldName="Name" SortOrder="Descending" />
        </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="IAEmailTemplateID" UniqueName="IAEmailTemplateID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridButtonColumn DataTextField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name" CommandName="View"></telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="CreatedDateTime" HeaderText="Created Date/Time" UniqueName="CreatedDateTime"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</div>
</asp:Content>
