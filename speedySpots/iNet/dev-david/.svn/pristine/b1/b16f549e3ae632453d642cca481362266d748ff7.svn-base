<%@ Page Title="Speedy Spots :: Out of Office Notifications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="out-of-office.aspx.cs" Inherits="SpeedySpots.out_of_office" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div id="content">
    <div id="producer_qc_switch">
        <strong>Working as:</strong> <a href="staff-dashboard.aspx">Staff</a> | <a href="admin-dashboard.aspx" class="at">Admin</a>
    </div>
    <h2>Out of Office Notifications</h2>
    <div class="group">
        <div class="order-list">
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
                <li class="at"><a href="out-of-office.aspx">Out of Office Notifications</a></li>
                <li><a href="labels.aspx">Labels</a></li>
                <li><a href="site-settings.aspx">Site Settings</a></li>
                <% } %>
            </ul>
        </div>
    </div>
    <div class="form-holder">
        <fieldset>
            <legend>Notification:</legend>
            <div class="group">
                <label><span class="required">Message:</span></label>
                <div class="inputgroup">
                    <telerik:RadEditor ID="m_txtMessage" runat="server" Width="621px" EditModes="Design">
                        <Tools>
                            <telerik:EditorToolGroup>
                                <telerik:EditorTool Name="ToggleScreenMode" />
                                <telerik:EditorTool Name="PasteStrip" />
                            </telerik:EditorToolGroup>
                        </Tools>
                    </telerik:RadEditor>
                </div>
            </div>
            <div class="group">
                <label>Override:</label>
                <div class="inputgroup">
                    <asp:CheckBox ID="m_chkClosedMessageDisplayAlways" runat="server" Text="Display this message during all hours." value="1" />
                </div>
            </div>
        </fieldset>
    </div>
    <div class="form-holder">
        <fieldset>
            <legend>Hours:</legend>
            <p>Message will be displayed when outside of the time ranges below, or at any time if no time is specified.</p>
            <div class="group">
                <label><span class="required">Mon:</span></label>
                <div class="inputgroup">
                    <telerik:RadTimePicker runat="server" ID="m_dtMondayIn" Width="80" TimeView-RenderDirection="Vertical" TimeView-Columns="4"></telerik:RadTimePicker> to <telerik:RadTimePicker runat="server" ID="m_dtMondayOut" Width="80"></telerik:RadTimePicker>
                </div>
            </div>
            <div class="group">
                <label><span class="required">Tue:</span></label>
                <div class="inputgroup">
                    <telerik:RadTimePicker runat="server" ID="m_dtTuesdayIn" Width="80" TimeView-RenderDirection="Vertical" TimeView-Columns="4"></telerik:RadTimePicker> to <telerik:RadTimePicker runat="server" ID="m_dtTuesdayOut" Width="80"></telerik:RadTimePicker>
                </div>
            </div>
            <div class="group">
                <label><span class="required">Wed:</span></label>
                <div class="inputgroup">
                    <telerik:RadTimePicker runat="server" ID="m_dtWednesdayIn" Width="80" TimeView-RenderDirection="Vertical" TimeView-Columns="4"></telerik:RadTimePicker> to <telerik:RadTimePicker runat="server" ID="m_dtWednesdayOut" Width="80"></telerik:RadTimePicker>
                </div>
            </div>
            <div class="group">
                <label><span class="required">Thu:</span></label>
                <div class="inputgroup">
                    <telerik:RadTimePicker runat="server" ID="m_dtThursdayIn" Width="80" TimeView-RenderDirection="Vertical" TimeView-Columns="4"></telerik:RadTimePicker> to <telerik:RadTimePicker runat="server" ID="m_dtThursdayOut" Width="80"></telerik:RadTimePicker>
                </div>
            </div>
            <div class="group">
                <label><span class="required">Fri:</span></label>
                <div class="inputgroup">
                    <telerik:RadTimePicker runat="server" ID="m_dtFridayIn" Width="80" TimeView-RenderDirection="Vertical" TimeView-Columns="4"></telerik:RadTimePicker> to <telerik:RadTimePicker runat="server" ID="m_dtFridayOut" Width="80"></telerik:RadTimePicker>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="Save Changes" CssClass="button primaryAction" OnClick="OnSave" />
    </div>
</div>
</asp:Content>
