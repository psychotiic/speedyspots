<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="producer-talent-availability.aspx.cs" Inherits="SpeedySpots.producer_talent_availability" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div id="content">
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
                <li><a href="admin-dashboard.aspx">Users</a></li>
                <li><a href="companies.aspx">Companies</a></li>
                <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
                <% { %>
                <li class="at"><a href="producer-talent-availability.aspx">Talent Availability</a></li>
                <li><a href="out-of-office.aspx">Out of Office Notifications</a></li>
                <li><a href="labels.aspx">Labels</a></li>
                <li><a href="site-settings.aspx">Site Settings</a></li>
                <% } %>
            </ul>
            
            <asp:Panel ID="m_oPanel" runat="server" DefaultButton="m_btnFilter">
                <div class="form-holder filter">
                    <fieldset>
                        <legend>Filter:</legend>
                        <div class="group">
                            <asp:Label ID="m_lblFromFilter" runat="server" Text="From Date/Time:" AssociatedControlID="m_dtFromFilter"></asp:Label>
                            <div class="inputgroup">
                                <telerik:RadDatePicker runat="server" ID="m_dtFromFilter"></telerik:RadDatePicker>
                                <asp:Label ID="m_lblToFilter" runat="server" Text="To Date/Time:" AssociatedControlID="m_dtToFilter"></asp:Label>
                                <telerik:RadDatePicker runat="server" ID="m_dtToFilter"></telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblTalent" runat="server" Text="Talent:" AssociatedControlID="m_cboTalentFilter"></asp:Label>
                            <telerik:RadComboBox ID="m_cboTalentFilter" runat="server"></telerik:RadComboBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblStatus" runat="server" Text="Status:" AssociatedControlID="m_cboStatusFilter"></asp:Label>
                            <telerik:RadComboBox ID="m_cboStatusFilter" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="-- All --" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem runat="server" Text="Pending" Value="Pending" />
                                    <telerik:RadComboBoxItem runat="server" Text="Approved" Value="Approved" />
                                    <telerik:RadComboBoxItem runat="server" Text="Denied" Value="Denied" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </fieldset>
                    <div class="button-row">
                        <speedyspots:InetLinkButton ID="m_btnFilter" runat="server" Text="Filter" CssClass="button primaryAction" OnClick="OnFilter" />
                    </div>
                </div>
            </asp:Panel>

            <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
                <MasterTableView NoMasterRecordsText="No unavailability submissions"></MasterTableView>
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
                <telerik:GridSortExpression FieldName="CreatedDateTime" SortOrder="Descending" />
                </SortExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="IATalentUnavailabilityID" UniqueName="IATalentUnavailabilityID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn DataTextField="Talent" HeaderText="Talent" UniqueName="Talent" CommandName="View"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="FromDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="From" UniqueName="From"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="To" UniqueName="To"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Notes" HeaderText="Notes" UniqueName="Notes"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn Text="Approve" UniqueName="Approve" CommandName="Approve"></telerik:GridButtonColumn>
                        <telerik:GridButtonColumn Text="Deny" UniqueName="Deny" CommandName="Deny"></telerik:GridButtonColumn>
                        <telerik:GridButtonColumn Text="Delete" UniqueName="Delete" CommandName="Delete" ConfirmText="Are you sure you want to delete this unavailability?"></telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>

                <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                </FilterMenu>
            </telerik:RadGrid>
        </div>
    </div>

    <br />

    <div id="box"> 
    <span id="boxtitle"></span> 
    <div class="form-holder">
        <fieldset>
            <legend>Add New Unavailablity:</legend>
            <div class="group">
                <label>
                    <span class="required">Talent:</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddNew" ControlToValidate="m_cboTalent" ErrorMessage="Please select the talent.">*</asp:RequiredFieldValidator>
                </label>
                <div class="inputgroup">
                    <telerik:RadComboBox ID="m_cboTalent" runat="server"></telerik:RadComboBox>
                </div>
            </div>
            <div class="group">
                <label>
                    <span class="required">Delivery Obligation:</span>
                    <asp:RequiredFieldValidator ID="m_reqFrom" runat="server" ValidationGroup="AddNew" ControlToValidate="m_dtFrom" ErrorMessage="Please enter the date the talent will be unavailable from.">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="m_reqTo" runat="server" ValidationGroup="AddNew" ControlToValidate="m_dtTo" ErrorMessage="Please enter the date the talent will return.">*</asp:RequiredFieldValidator>
                </label>
                <div class="inputgroup">
                    Talent will be unavailable from <telerik:RadDateTimePicker runat="server" ID="m_dtFrom" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                        <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDateTimePicker> and good for <telerik:RadDateTimePicker runat="server" ID="m_dtTo" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                        <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                    </SpecialDays>
                                </Calendar>
                            </telerik:RadDateTimePicker> delivery when they return.
                </div>
            </div>
            <div class="group">
                <label>Notes/Description:</label>
                <div class="inputgroup">
                    <telerik:RadTextBox ID="m_txtNotes" runat="server" TextMode="MultiLine" Rows="4" Columns="50" MaxLength="500"></telerik:RadTextBox>
                </div>
            </div>
        </fieldset>
         <div class="button-row">
            <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="AddNew" Text="Submit Unavailability" CssClass="button primaryAction" OnClick="OnSubmit" />
            <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="Clear" Text="Clear" CssClass="button primaryAction" OnClick="OnClear" />
        </div>
    </div>
</div>
</div>
</asp:Content>