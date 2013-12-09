<%@ Page Title="Speedy Spots :: Companies" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="companies.aspx.cs" Inherits="SpeedySpots.companies" %>
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
        <li class="at"><a href="companies.aspx">Companies</a></li>
        <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
        <% { %>
        <li><a href="producer-talent-availability.aspx">Talent Availability</a></li>
        <li><a href="out-of-office.aspx">Out of Office Notifications</a></li>
        <li><a href="labels.aspx">Labels</a></li>
        <li><a href="site-settings.aspx">Site Settings</a></li>
        <% } %>
    </ul>

    <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
    <% { %>
    <div class="button-row action">
        <asp:LinkButton ID="m_btnCreate" runat="server" Text="Add New Company" CssClass="button primaryAction" OnClick="OnAdd" />
    </div>
    <% } %>

    <asp:Panel ID="m_oPanel" runat="server" DefaultButton="m_btnFilter">
        <div class="form-holder filter">
            <fieldset>
                <legend>Filter Companies:</legend>
                <div class="group">
                    <asp:Label ID="m_lblName" runat="server" Text="Company Name:" AssociatedControlID="m_txtName"></asp:Label>
                    <telerik:RadTextBox ID="m_txtName" runat="server" Columns="50"></telerik:RadTextBox>
                </div>
                <div class="group">
                    <label>City:</label>
                    <div class="inputgroup">
                        <telerik:RadTextBox ID="m_txtCity" runat="server"></telerik:RadTextBox>
                        <label>State:</label>
                        <asp:DropDownList ID="m_cboState" runat="server">
							<asp:ListItem Selected Value="" Text="-- All --" />
							<asp:ListItem Value="AL" Text="Alabama" />
							<asp:ListItem Value="AK" Text="Alaska" />
							<asp:ListItem Value="AZ" Text="Arizona" />
							<asp:ListItem Value="AR" Text="Arkansas" />
							<asp:ListItem Value="CA" Text="California" />
							<asp:ListItem Value="CO" Text="Colorado" />
							<asp:ListItem Value="CT" Text="Connecticut" />
							<asp:ListItem Value="DE" Text="Delaware" />
							<asp:ListItem Value="DC" Text="District of Columbia" />
							<asp:ListItem Value="FL" Text="Florida" />
							<asp:ListItem Value="GA" Text="Georgia" />
							<asp:ListItem Value="HI" Text="Hawaii" />
							<asp:ListItem Value="ID" Text="Idaho" />
							<asp:ListItem Value="IL" Text="Illinois" />
							<asp:ListItem Value="IN" Text="Indiana" />
							<asp:ListItem Value="IA" Text="Iowa" />
							<asp:ListItem Value="KS" Text="Kansas" />
							<asp:ListItem Value="KY" Text="Kentucky" />
							<asp:ListItem Value="LA" Text="Louisiana" />
							<asp:ListItem Value="ME" Text="Maine" />
							<asp:ListItem Value="MD" Text="Maryland" />
							<asp:ListItem Value="MA" Text="Massachusetts" />
							<asp:ListItem Value="MI" Text="Michigan" />
							<asp:ListItem Value="MN" Text="Minnesota" />
							<asp:ListItem Value="MS" Text="Mississippi" />
							<asp:ListItem Value="MO" Text="Missouri" />
							<asp:ListItem Value="MT" Text="Montana" />
							<asp:ListItem Value="NE" Text="Nebraska" />
							<asp:ListItem Value="NV" Text="Nevada" />
							<asp:ListItem Value="NH" Text="New Hampshire" />
							<asp:ListItem Value="NJ" Text="New Jersey" />
							<asp:ListItem Value="NM" Text="New Mexico" />
							<asp:ListItem Value="NY" Text="New York" />
							<asp:ListItem Value="NC" Text="North Carolina" />
							<asp:ListItem Value="ND" Text="North Dakota" />
							<asp:ListItem Value="OH" Text="Ohio" />
							<asp:ListItem Value="OK" Text="Oklahoma" />
							<asp:ListItem Value="OR" Text="Oregon" /> 
							<asp:ListItem Value="PA" Text="Pennsylvania" /> 
							<asp:ListItem Value="RI" Text="Rhode Island" /> 
							<asp:ListItem Value="SC" Text="South Carolina" /> 
							<asp:ListItem Value="SD" Text="South Dakota" /> 
							<asp:ListItem Value="TN" Text="Tennessee" /> 
							<asp:ListItem Value="TX" Text="Texas" /> 
							<asp:ListItem Value="UT" Text="Utah" /> 
							<asp:ListItem Value="VT" Text="Vermont" /> 
							<asp:ListItem Value="VA" Text="Virginia" /> 
							<asp:ListItem Value="WA" Text="Washington" /> 
							<asp:ListItem Value="WV" Text="West Virginia" /> 
							<asp:ListItem Value="WI" Text="Wisconsin" /> 
							<asp:ListItem Value="WY" Text="Wyoming" /> 
                        </asp:DropDownList>
                        <label>Zip:</label>
                        <telerik:RadTextBox ID="m_txtZip" runat="server" Columns="10" MaxLength="10"></telerik:RadTextBox>
                    </div>
                </div>
                <div class="group">
                    <label>Phone:</label>
                    <div class="inputgroup">
                        <telerik:RadTextBox ID="m_txtPhone" runat="server" Columns="12"></telerik:RadTextBox>
                        <label style="margin-left: 66px;">Verified:</label>
                        <asp:DropDownList ID="ddlVerified" runat="server">
                            <asp:ListItem Value="Both" Text="Both" />
                            <asp:ListItem Value="Unverified" Text="Unverified" />
                            <asp:ListItem Value="Verified" Text="Verified" />
                        </asp:DropDownList>
                        <label style="margin-left: 47px;">Prepay:</label>
                        <asp:DropDownList ID="ddlPrepay" runat="server">
                            <asp:ListItem Value="Both" Text="Both" />
                            <asp:ListItem Value="Yes" Text="Yes" />
                            <asp:ListItem Value="No" Text="No" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="group">
                    <label>&nbsp;</label>
                    <div class="inputgroup nospace">
                        <label>
                            <asp:CheckBox ID="m_chkIsArchived" runat="server" />
                            Include Archived Companies
                        </label>
                    </div>
                </div>
            </fieldset>
        
            <div class="button-row action">
                <speedyspots:InetLinkButton ID="m_btnFilter" runat="server" Text="Filter Companies" CssClass="button primaryAction" OnClick="OnFilter" />
            </div>
        </div>
    </asp:Panel>

    <h3>Companies</h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoMasterRecordsText="No Companies"></MasterTableView>
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
                <telerik:GridBoundColumn DataField="MPOrgID" UniqueName="MPOrgID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridButtonColumn DataTextField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name" CommandName="View"></telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="UserCount" HeaderText="Users" UniqueName="UserCount"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="City" HeaderText="City" UniqueName="City"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="State" HeaderText="State" UniqueName="State"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Zip" HeaderText="Zip" UniqueName="Zip"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" UniqueName="Phone"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsPayFirst" HeaderText="Prepay" UniqueName="PrePay"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IsVerified" HeaderText="Verified" UniqueName="Verified"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</div>
</asp:Content>
