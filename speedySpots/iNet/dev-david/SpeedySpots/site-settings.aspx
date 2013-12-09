<%@ Page Title="Speedy Spots :: Site Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="site-settings.aspx.cs" Inherits="SpeedySpots.site_settings" %>
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
        <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
        <% { %>
        <li><a href="producer-talent-availability.aspx">Talent Availability</a></li>
        <li><a href="out-of-office.aspx">Out of Office Notifications</a></li>
        <li><a href="labels.aspx">Labels</a></li>
        <li class="at"><a href="site-settings.aspx">Site Settings</a></li>
        <% } %>
    </ul>

    <div class="button-row action">
        <asp:LinkButton ID="m_btnCreate" runat="server" Text="Add New Email Template" CssClass="button primaryAction" CausesValidation="false" OnClick="OnAdd" />
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

    <div class="hr"><hr /></div>
    <h2>Other Settings</h2>

    <div class="form-holder">
        <fieldset>
            <legend>Site Settings:</legend>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_txtRequestThreshold" ErrorMessage="Request Day Threshold is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Request Day Threshold:</span>
                </label>
                <telerik:RadNumericTextBox ID="m_txtRequestThreshold" runat="server" Width="25" MaxLength="3" MinValue="0" MaxValue="365">
                    <NumberFormat DecimalDigits="0" />
                </telerik:RadNumericTextBox> day(s)
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_txtRequestActivityInterval" ErrorMessage="Request Activity Interval is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Request Activity Interval:</span>
                </label>
                <telerik:RadNumericTextBox ID="m_txtRequestActivityInterval" runat="server" Width="25" MaxLength="4" MinValue="0" MaxValue="9999">
                    <NumberFormat DecimalDigits="0" />
                </telerik:RadNumericTextBox> second(s)
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_txtFeedbackQuestion" ErrorMessage="Feedback Question Email is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Feedback Question Email:</span>
                </label>
                <telerik:RadTextBox ID="m_txtFeedbackQuestion" runat="server" Columns="50" MaxLength="200"></telerik:RadTextBox>
                <i>(separate emails with commas)</i>
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_txtFeedbackProblem" ErrorMessage="Feedback Bug Email is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Feedback Bug Email:</span>
                </label>
                <telerik:RadTextBox ID="m_txtFeedbackProblem" runat="server" Columns="50" MaxLength="200"></telerik:RadTextBox>
                <i>(separate emails with commas)</i>
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_txtEmailSystemNotifications" ErrorMessage="System Notification Email is required.">*</asp:RequiredFieldValidator>
                    <span class="required">System Notifications Email:</span>
                </label>
                <telerik:RadTextBox ID="m_txtEmailSystemNotifications" runat="server" Columns="50" MaxLength="200"></telerik:RadTextBox>
                <i>(separate emails with commas)</i>
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_txtEmailBillings" ErrorMessage="Billings Email is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Billings Email:</span>
                </label>
                <telerik:RadTextBox ID="m_txtEmailBillings" runat="server" Columns="50" MaxLength="200"></telerik:RadTextBox>
                <i>(separate emails with commas)</i>
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_txtEmailEstimatePayment" ErrorMessage="Estimate Payment Email is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Estimate Payment Email:</span>
                </label>
                <telerik:RadTextBox ID="m_txtEmailEstimatePayment" runat="server" Columns="50" MaxLength="200"></telerik:RadTextBox>
                <i>(separate emails with commas)</i>
            </div>
        </fieldset>
    </div>

    <div class="button-row">
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Save Changes" CssClass="button primaryAction" OnClick="OnSave" />
    </div>

</div>
</asp:Content>
