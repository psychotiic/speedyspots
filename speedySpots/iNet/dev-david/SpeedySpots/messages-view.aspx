<%@ Page Title="Speedy Spots :: View Message" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="messages-view.aspx.cs" Inherits="SpeedySpots.messages_view" %>
<%@ Register Src="~/Controls/Tabs/Customer-Tabs.ascx" TagName="CustomerTabs" TagPrefix="SS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<h2>Speedy Spots Messaging System</h2>

<% if(ApplicationContext.IsCustomer) %>
<% { %>
    <SS:CustomerTabs ID="m_oTabs" runat="server" SelectedTab="Messages" />
<% } %>

<div class="tab-holder group">
    <ul id="dashboard-tabs">
        <% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
        <% { %>
            <li><a href="staff-dashboard.aspx">Requests</a></li>
            <li><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
            <li><a href="staff-dashboard-completed.aspx">Completed</a></li>
            <li><a href="admin-dashboard.aspx">Admin</a></li>
            <li class="at"><a href="messages-inbox.aspx">Messages</a></li>
        <% } %>

        <% if(ApplicationContext.IsTalent) %>
        <% { %>       
            <li><a href="talent-dashboard.aspx">Pending POs</a></li>
            <li><a href="talent-production-orders-completed.aspx">Completed POs</a></li>
            <!-- 11/1/2011 - Hidden temporarily at request of SpeedySpots for launch -->
            <!--<li><a href="talent-my-availability.aspx">My Availability</a></li>-->
            <li class="at"><a href="messages-inbox.aspx">Messages</a></li>
        <% } %>
    </ul>
</div>

<div class="button-row action">
    <a href="messages-inbox.aspx" class="button">Back to Inbox</a>
</div>

    <div class="form-holder">
        <fieldset>
            <legend>Message:</legend>
            <div class="group">
                <label>From:</label>
                <span class="output"><%=MemberProtect.User.GetDataItem(IAMessage.MPUserID, "FirstName") + " " + MemberProtect.User.GetDataItem(IAMessage.MPUserID, "LastName") %></span>
            </div>
            <div class="group">
                <label>Subject:</label>
                <span class="output"><%=IAMessage.Subject %></span>
            </div>
            <div class="group">
                <label>Date:</label>
                <span class="output"><%=string.Format("{0:MMMM d, yyyy} to {1:MMMM d, yyyy}", IAMessage.DisplayStartDateTime, IAMessage.DisplayEndDateTime) %></span>
            </div>
            <div class="group">
                <label>Message:</label>
                <div class="inputgroup output">
                    <%=IAMessage.Body %>
                </div>
            </div>
        </fieldset>
    </div>

    <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
    <% { %>
    <div class="RadGrid_test nontel">
        <table width="100%">
            <asp:Repeater ID="m_repeaterRecipients" runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="rgHeader">Recipient</th>
                            <th class="rgHeader">Acknowledged</th>
                            <th class="rgHeader">Acknowledged Date/Time</th>
                        </tr>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# DataBinder.Eval(Container.DataItem, "Recipient") %></td>
                        <td><%# GetYesNo(DataBinder.Eval(Container.DataItem, "IsAcknowledged").ToString()) %></td>
                        <td><%# GetDate((DateTime)(DataBinder.Eval(Container.DataItem, "AcknowledgedDateTime"))) %></td>
                        <td></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <% } %>
</asp:Content>
