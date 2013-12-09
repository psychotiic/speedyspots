<%@ Page Title="Speedy Spots :: Labels" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="labels.aspx.cs" Inherits="SpeedySpots.labels" %>
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
            <li><a href="messages-inbox.aspx">Messages</a></li>
        </ul>
    </div>
    <ul id="dashboard-subnav" class="group">
        <li><a href="admin-dashboard.aspx">Users</a></li>
        <li><a href="companies.aspx">Companies</a></li>
        <li><a href="producer-talent-availability.aspx">Talent Availability</a></li>
        <li><a href="out-of-office.aspx">Out of Office Notifications</a></li>
        <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
        <% { %>
        <li class="at"><a href="labels.aspx">Labels</a></li>
        <li><a href="site-settings.aspx">Site Settings</a></li>
        <% } %>
    </ul>

    <div class="button-row action">
        <asp:LinkButton ID="m_btnCreate" runat="server" Text="Add New Label" CssClass="button primaryAction" OnClick="OnAdd" />
    </div>

    <h3>Labels</h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoMasterRecordsText="No Labels"></MasterTableView>
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
        <telerik:GridSortExpression FieldName="Text" SortOrder="Ascending" />
        </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="IALabelID" UniqueName="IALabelID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsCustomerVisible" UniqueName="IsCustomerVisible" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridButtonColumn DataTextField="Text" HeaderText="Text" UniqueName="Text" SortExpression="Text" CommandName="View"></telerik:GridButtonColumn>
                <telerik:GridBoundColumn HeaderText="Customer Visible" UniqueName="CustomerVisible"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</div>
</asp:Content>
