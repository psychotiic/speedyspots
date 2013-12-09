<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="admin-dashboard.aspx.cs" Inherits="SpeedySpots.admin_dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div id="content">
    <div id="producer_qc_switch">
        <strong>Working as:</strong> <a href="staff-dashboard.aspx">Staff</a> | <a href="admin-dashboard.aspx" class="at">Admin</a>
    </div>
    <h2><a href="user-account.aspx"><%=string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")) %></a> Dashboard</h2>
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
                <li class="at"><a href="admin-dashboard.aspx">Users</a></li>
                <li><a href="companies.aspx">Companies</a></li>
                <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
                <% { %>
                <li><a href="producer-talent-availability.aspx">Talent Availability</a></li>
                <li><a href="out-of-office.aspx">Out of Office Notifications</a></li>
                <li><a href="labels.aspx">Labels</a></li>
                <li><a href="site-settings.aspx">Site Settings</a></li>
                <% } %>
            </ul>

            <% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
            <% { %>
            <div class="button-row action">
                <asp:LinkButton ID="m_btnCreate" runat="server" Text="Create User" CssClass="button primaryAction" OnClick="OnCreate" />
            </div>
            <% } %>

            <asp:Panel ID="m_oPanel" runat="server" DefaultButton="m_btnFilter">
                <div class="form-holder filter">
                    <fieldset>
                        <legend>Filter User:</legend>
                        <div class="group">
                            <asp:Label ID="m_lblName" runat="server" Text="Name:" AssociatedControlID="m_txtName"></asp:Label>
                            <div class="inputgroup">
                                <telerik:RadTextBox ID="m_txtName" runat="server"></telerik:RadTextBox>
                                <asp:Label ID="m_lblEmail" runat="server" Text="Email:" AssociatedControlID="m_txtEmail"></asp:Label>
                                <telerik:RadTextBox ID="m_txtEmail" runat="server"></telerik:RadTextBox>
                            </div>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblType" runat="server" Text="Type:" AssociatedControlID="m_cboType"></asp:Label>
                            <div class="inputgroup">
                                <telerik:RadComboBox ID="m_cboType" runat="server" Width="250px">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="-- All --" />
                                        <telerik:RadComboBoxItem Value="Customer" Text="Customer" />
                                        <telerik:RadComboBoxItem Value="Staff" Text="Staff" />
                                        <telerik:RadComboBoxItem Value="Talent" Text="Talent" />
                                        <telerik:RadComboBoxItem Value="Admin" Text="Admin" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblShowArchived" runat="server" Text="Include archived users:" AssociatedControlID="m_chkShowArchived"></asp:Label>
                            <div class="inputgroup">
                                <asp:CheckBox ID="m_chkShowArchived" runat="server" />
                            </div>
                        </div>
                    </fieldset>
                    <div class="button-row">
                        <speedyspots:InetLinkButton ID="m_btnFilter" runat="server" Text="Filter Users" CssClass="button primaryAction" OnClick="OnFilter" />
                    </div>
                </div>
            </asp:Panel>

            <h3>Users:</h3>
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
                <telerik:GridSortExpression FieldName="Name" SortOrder="Ascending" />
                </SortExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="MPUserID" UniqueName="MPUserID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsCustomer" UniqueName="IsCustomer" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsStaff" UniqueName="IsStaff" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsTalent" UniqueName="IsTalent" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsAdmin" UniqueName="IsAdmin" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsArchived" UniqueName="IsArchived" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn DataTextField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name" CommandName="View"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn HeaderText="Type" UniqueName="Type"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastLoginOn" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Last Login Date/Time" UniqueName="LastLoginOn"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>

                <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </FilterMenu>
            </telerik:RadGrid>
        </div>
    </div>
</div>
</asp:Content>
