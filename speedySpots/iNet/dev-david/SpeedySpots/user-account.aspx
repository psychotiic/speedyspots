<%@ Page Title="Speedy Spots :: Manage Your Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-account.aspx.cs" Inherits="SpeedySpots.user_account" %>
<%@ Register Src="~/Controls/Tabs/Customer-Tabs.ascx" TagName="CustomerTabs" TagPrefix="SS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<% if (ApplicationContext.IsCustomer)
   { %>
<style type="text/css">
.main 
{
    width: 100%;
    margin-right: 0;
}
</style>
<% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<script language="javascript" type="text/javascript">

function OnSaveSchedule()
{
    var m_dtMondayIn = $find('<%=m_dtMondayIn.ClientID %>');
    var m_dtMondayOut = $find('<%=m_dtMondayOut.ClientID %>');
    var m_dtTuesdayIn = $find('<%=m_dtTuesdayIn.ClientID %>');
    var m_dtTuesdayOut = $find('<%=m_dtTuesdayOut.ClientID %>');
    var m_dtWednesdayIn = $find('<%=m_dtWednesdayIn.ClientID %>');
    var m_dtWednesdayOut = $find('<%=m_dtWednesdayOut.ClientID %>');
    var m_dtThursdayIn = $find('<%=m_dtThursdayIn.ClientID %>');
    var m_dtThursdayOut = $find('<%=m_dtThursdayOut.ClientID %>');
    var m_dtFridayIn = $find('<%=m_dtFridayIn.ClientID %>');
    var m_dtFridayOut = $find('<%=m_dtFridayOut.ClientID %>');
    
    if(m_dtMondayIn.get_selectedDate() != null && m_dtMondayOut.get_selectedDate() == null || m_dtMondayIn.get_selectedDate() == null && m_dtMondayOut.get_selectedDate() != null)
    {
        alert("Please fill in the whole range for Monday.");
        return false;
    }

    if(m_dtTuesdayIn.get_selectedDate() != null && m_dtTuesdayOut.get_selectedDate() == null || m_dtTuesdayIn.get_selectedDate() == null && m_dtTuesdayOut.get_selectedDate() != null)
    {
        alert("Please fill in the whole range for Tuesday.");
        return false;
    }

    if(m_dtWednesdayIn.get_selectedDate() != null && m_dtWednesdayOut.get_selectedDate() == null || m_dtWednesdayIn.get_selectedDate() == null && m_dtWednesdayOut.get_selectedDate() != null)
    {
        alert("Please fill in the whole range for Wednesday.");
        return false;
    }

    if(m_dtThursdayIn.get_selectedDate() != null && m_dtThursdayOut.get_selectedDate() == null || m_dtThursdayIn.get_selectedDate() == null && m_dtThursdayOut.get_selectedDate() != null)
    {
        alert("Please fill in the whole range for Thursday.");
        return false;
    }

    if(m_dtFridayIn.get_selectedDate() != null && m_dtFridayOut.get_selectedDate() == null || m_dtFridayIn.get_selectedDate() == null && m_dtFridayOut.get_selectedDate() != null)
    {
        alert("Please fill in the whole range for Friday.");
        return false;
    }

    return true;
}

function setNewCompany(id, name) {
    $("#viewCompanLink").attr("href", "company-modify.aspx?id=" + id);
    $("#Master_m_oContent_m_hdnAssignedCompanyID").attr("value", id);
    $("#Master_m_oContent_m_hdnAssignedCompanyName").attr("value", name);

    $.getJSON('ajax-company-lookup.aspx?t=f&id=' + id, function (data) {
        $("#CompanyAddress1").html(data.Address1);
        $("#CompanyAddress2").html(data.Address2);
        $("#CompanyCity").html(data.City);
        $("#CompanyCountry").html(data.Country);
        $("#CompanyState").html(data.State);
        $("#CompanyZip").html(data.Zip);
        $("#CompanyPhone").html(data.Phone);
        $("#CompanyBillingAddress1").html(data.BillingAddress1);
        $("#CompanyBillingAddress2").html(data.BillingAddress2);
        $("#CompanyBillingCity").html(data.BillingCity);
        $("#CompanyBillingCountry").html(data.BillingCountry);
        $("#CompanyBillingState").html(data.BillingState);
        $("#CompanyBillingEmail").html(data.BillingEmail);
    });
}

</script>

<div class="main">
    <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
    <% { %>
        <p class="breadcrumb"><a href="admin-dashboard.aspx">Dashboard</a> &raquo; <a href="admin-dashboard.aspx">Users</a> &raquo;
    <% } %>
    <% else if(ApplicationContext.IsCustomer) %>
    <% { %>
        <p class="breadcrumb"><a href="user-dashboard.aspx">Dashboard</a> &raquo;
    <% } %>
    <% else if(ApplicationContext.IsStaff) %>
    <% { %>
        <p class="breadcrumb"><a href="staff-dashboard.aspx">Dashboard</a> &raquo;
    <% } %>
    <% else if(ApplicationContext.IsTalent) %>
    <% { %>
        <p class="breadcrumb"><a href="talent-dashboard.aspx">Dashboard</a> &raquo;
    <% } %>
    <% if(MPUserID == Guid.Empty) %>
    <% { %>
        New User
    <% } %>
    <% else %>
    <% { %>
        <%=string.Format("{0} {1}", MemberProtect.User.GetDataItem(MPUserID, "FirstName"), MemberProtect.User.GetDataItem(MPUserID, "LastName"))%>
    <% } %>
    </p>

    <div id="m_divDuplicates" runat="server" class="message negative" style="margin-top: 1em; margin-bottom: 0;">
        <p>The company (<%=CompanyName %>) assigned to this user might be a duplicate.</p>
        <p>Possible alternatives:</p>
        <ul>
            <asp:Repeater ID="m_oDuplicateRepeater" runat="server">
                <ItemTemplate>
                    <li><a href='company-modify.aspx?id=<%# DataBinder.Eval(Container.DataItem, "MPOrgID") %>'><%# DataBinder.Eval(Container.DataItem, "Name") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>

    <h2>
    Manage 
    <% if(MPUserID == Guid.Empty) %>
    <% { %>
        New User
    <% } %>
    <% else %>
    <% { %>
        <%=string.Format("{0} {1}", MemberProtect.User.GetDataItem(MPUserID, "FirstName"), MemberProtect.User.GetDataItem(MPUserID, "LastName"))%>
    <% } %>
    </h2>

    <% if(ApplicationContext.IsCustomer) %>
    <% { %>
        <SS:CustomerTabs ID="m_oTabs" runat="server" SelectedTab="ManageAccount" />
    <% } %>

    <div class="form-holder">
        <fieldset>
            <legend>Account Information:</legend>
            <div class="group">
                <asp:Label ID="m_lblUsername" runat="server" AssociatedControlID="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your email address.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Email must be a valid email address up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Email Address:</span>
                </asp:Label>
                <div class="note-holder">
                    <telerik:RadTextBox ID="m_txtUsername" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
                    <p class="note">Example: yourname@yourcompany.com</p>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblPassword" runat="server" AssociatedControlID="m_txtPassword">
                    <asp:RegularExpressionValidator ID="m_expPassword" runat="server" ControlToValidate="m_txtPassword" ValidationExpression="^.{6,20}$" ErrorMessage="Password must contain between 6 and 20 characters">*</asp:RegularExpressionValidator>
                    <span class="required">Password:</span>
                </asp:Label>
                <div class="note-holder">
                    <telerik:RadTextBox ID="m_txtPassword" TextMode="Password" runat="server" Columns="20" MaxLength="20" AutoCompleteType="Disabled"></telerik:RadTextBox>
                    <p class="note">Password must be between 6 and 20 characters long.</p>
                </div>
            </div>

<% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
<% { %>
            <div class="group">
                <label>Account Type:</label>
                <div class="inputgroup nospace">
                    <% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
                    <% { %>
                    <asp:CheckBox ID="m_chkIsCustomer" runat="server" AutoPostBack="true" Text="Customer" OnCheckedChanged="OnCustomer" />
                    <% } %>

                    <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
                    <% { %>
                    <asp:CheckBox ID="m_chkIsStaff" runat="server" AutoPostBack="true" Text="Staff" OnCheckedChanged="OnStaff" />
                    <asp:CheckBox ID="m_chkIsTalent" runat="server" AutoPostBack="true" Text="Talent" OnCheckedChanged="OnTalent" />
                    <asp:CheckBox ID="m_chkIsAdmin" runat="server" Text="Admin" />
                    <% } %>
                </div>
            </div>

            <div id="m_divTalentType" runat="server" class="group">
                <label>Talent Type:</label>
                <div class="inputgroup">
                    <asp:CheckBoxList ID="m_chkTalentType" runat="server"></asp:CheckBoxList>
                </div>
            </div>
<% } %>
            <div id="m_divStaffDefaultTab" runat="server" class="group">
                <label>Default Tab:</label>
                <div class="inputgroup">
                    <asp:RadioButtonList ID="m_radDefaultTab" runat="server">
                        <asp:ListItem Selected Text="Requests" Value="Requests"></asp:ListItem>
                        <asp:ListItem Text="In Production" Value="In Production"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="m_divNotes" runat="server" class="group">
                <label>Notes:</label>
                <div class="inputgroup">
                    <telerik:RadEditor ID="m_txtNotes" runat="server" Width="450px" EditModes="Design">
                        <Tools>
                            <telerik:EditorToolGroup>
                                <telerik:EditorTool Name="PasteStrip" />
                            </telerik:EditorToolGroup>
                        </Tools>
                    </telerik:RadEditor>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="form-holder" id="m_divCoworkerManager" runat="server" visible="false">
        <fieldset>
            <legend>Coworker Manager:</legend>
            <p>Move users to the right box to to allow <strong><%=string.Format("{0} {1}", MemberProtect.User.GetDataItem(MPUserID, "FirstName"), MemberProtect.User.GetDataItem(MPUserID, "LastName"))%></strong> to view their requests.</p>
            <div class="group">
                <telerik:RadListBox ID="m_RadEmployees" runat="server" AllowTransfer="true" AllowTransferOnDoubleClick="true" TransferToID="m_RadSelectedEmployees" width="300" Height="200">
                </telerik:RadListBox>
                <telerik:RadListBox ID="m_RadSelectedEmployees" runat="server" width="300" Height="200">
                </telerik:RadListBox>
            </div>
        </fieldset>
    </div>
    <div class="form-holder">
        <fieldset>
            <legend>User Information:</legend>
            <div class="group">
                <asp:Label ID="m_lblFirstName" runat="server" AssociatedControlID="m_txtFirstName">
                    <asp:RequiredFieldValidator ID="m_reqFirstName" runat="server" ControlToValidate="m_txtFirstName" ErrorMessage="Please enter your first name.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expFirstName" runat="server" ControlToValidate="m_txtFirstName" ValidationExpression="^.{0,25}$" ErrorMessage="First name may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="m_reqLastName" runat="server" ControlToValidate="m_txtLastName" ErrorMessage="Please enter your last name.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expLastName" runat="server" ControlToValidate="m_txtLastName" ValidationExpression="^.{0,25}$" ErrorMessage="Last name may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Name:</span>
                </asp:Label>
                <div class="inputgroup">
                    <div class="note-holder">
                        <telerik:RadTextBox ID="m_txtFirstName" runat="server" Columns="25" MaxLength="25"></telerik:RadTextBox>
                        <p class="note">First</p>
                    </div>
                    <div class="note-holder">
                        <telerik:RadTextBox ID="m_txtLastName" runat="server" Columns="25" MaxLength="25"></telerik:RadTextBox>
                        <p class="note">Last</p>
                    </div>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblPhone" runat="server" AssociatedControlID="m_txtPhone">
                    <asp:RequiredFieldValidator ID="m_reqPhone" runat="server" ControlToValidate="m_txtPhone" ErrorMessage="Please enter your phone number.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expPhoneExt" runat="server" ControlToValidate="m_txtPhoneExt" ValidationExpression="^\d{0,5}$" ErrorMessage="Phone Extension may be up to 5 digits.">*</asp:RegularExpressionValidator>
                    <span class="required">Phone:</span>
                </asp:Label>
                <div class="inputgroup">
                    <telerik:RadTextBox ID="m_txtPhone" runat="server" Columns="15" MaxLength="15"></telerik:RadTextBox>
                    <asp:Label ID="m_lblPhoneExt" runat="server" Text="Ext:" AssociatedControlID="m_txtPhoneExt"></asp:Label>
                    <telerik:RadTextBox ID="m_txtPhoneExt" runat="server" Columns="5" MaxLength="5"></telerik:RadTextBox>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblMobilePhone" runat="server" AssociatedControlID="m_txtMobilePhone">
                    Mobile Phone:
                </asp:Label>
                <telerik:RadTextBox ID="m_txtMobilePhone" runat="server" Columns="15" MaxLength="15"></telerik:RadTextBox>
            </div>

<% if(ApplicationContext.IsUserCustomer(MPUserID)) %>
<% { %>
            <div class="group">
                <asp:Label ID="m_lblDepartment" runat="server" AssociatedControlID="m_txtDepartment">
                    <asp:RegularExpressionValidator ID="m_expDepartment" runat="server" ControlToValidate="m_txtDepartment" ValidationExpression="^.{0,25}$" ErrorMessage="Department may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    Department:
                </asp:Label>
                <telerik:RadTextBox ID="m_txtDepartment" runat="server" Columns="25" MaxLength="25"></telerik:RadTextBox>
            </div>
            <% } %>
            <% if(ApplicationContext.IsUserStaff(MPUserID)) %>
            <% { %>
            <div id="m_divComments" runat="server" class="group">
                <asp:Label ID="m_lblComments" runat="server" AssociatedControlID="m_txtComments">
                    Comments:
                </asp:Label>
                <telerik:RadTextBox ID="m_txtComments" runat="server" Columns="50" Rows="10" MaxLength="2000" TextMode="MultiLine"></telerik:RadTextBox>
            </div>
            <% } %>
            <div class="group">
                <asp:Label ID="Label3" runat="server" AssociatedControlID="m_cboGridPageSize">
                    Grid Page Size:
                </asp:Label>
                <telerik:RadComboBox ID="m_cboGridPageSize" runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="10" Value="10" />
                        <telerik:RadComboBoxItem Text="25" Value="25" />
                        <telerik:RadComboBoxItem Text="50" Value="50" />
                        <telerik:RadComboBoxItem Text="100" Value="100" />
                    </Items>
                </telerik:RadComboBox>
            </div>

            <% if(ApplicationContext.IsUserTalent(MPUserID)) %>
            <% { %>
            <div class="group">
                <asp:Label ID="Label4" runat="server" AssociatedControlID="m_txtAdditionalEmails">
                    Additional Emails:
                </asp:Label>
                <div class="note-holder">
                    <telerik:RadTextBox ID="m_txtAdditionalEmails" runat="server" Columns="50" Rows="10" MaxLength="500"></telerik:RadTextBox>
                    <p class="note">Additional emails may be separated with commas</p>
                </div>
            </div>
            <% } %>
        </fieldset>
    </div>
    
    <div id="m_divCompany" runat="server">
        <% if(!ApplicationContext.IsStaff) %>
        <% { %>
        <div class="message">
            <p>To update your company information, email us at <a href="mailto:billing@speedyspots.com?Subject=Company Update: <%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(MPUserID)))%>">billing@speedyspots.com</a> or call us at 734-475-9327.</p>
        </div>
        <% } %>
        <div class="form-holder">
            <fieldset>
                <legend>Company Information:</legend>
                <% if(!IsNew) %>
                <% { %>
                <div class="group">
                    <label>Company ID:</label>
                    <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(ApplicationContext.GetCompanyID(MPOrgID).ToString())%></span>
                </div>
                <% } %>

                <% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
                <% { %>
                    <div>
                        <label class="required">Company Name:</label>
                        
                        <link rel="stylesheet" type="text/css" href="css/jquery.flexbox.css" />
                        <script type="text/javascript" src="js/jquery.flexbox.min.js"></script>

                        <div class="output">
				            <div id="ffb2"></div>
			            </div>

                        <script type="text/javascript">
                            $(function () {
                                var companyBox = $('#ffb2').flexbox("ajax-company-lookup.aspx", {
                                    showArrow: false,
                                    width: 400,
                                    minChars: 2,
                                    queryDelay: 400,
                                    paging: false,
                                    resultTemplate: '{name}, {City}, Verified: {IsVerified}',
                                    onSelect: function () {
                                        setNewCompany($("#ffb2_hidden").val(), this.value);
                                    }
                                });

                                companyBox.setValue($("#Master_m_oContent_m_hdnAssignedCompanyName").attr("value"));
                            });
                        </script>
                        <% if(ApplicationContext.IsStaff && ApplicationContext.IsAdmin) %>
                        <% { %>
                            <a href="company-modify.aspx?id=<%=MPOrgID %>" id="viewCompanLink">>></a>
                        <% } %>
                        <asp:HiddenField ID="m_hdnAssignedCompanyID" runat="server" />
                        <asp:HiddenField ID="m_hdnAssignedCompanyName" runat="server" />
                    </div>
                <% } %>
                <% else %>
                <% { %>
                    <div class="group">
                        <label>Company Name:</label>
                        <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(MPUserID)))%></span>
                        <% if(ApplicationContext.IsStaff && ApplicationContext.IsAdmin) %>
                        <% { %>
                            &nbsp;<a href="company-modify.aspx?id=<%=MPOrgID %>" id="A1">>></a>
                        <% } %>
                    </div>
                <% } %>
                <div class="group">
                    <label>Address Line 1:</label>
                    <span class="output" id="CompanyAddress1"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "Address1"))%></span>
                </div>
                <div class="group">
                    <label>Address Line 2:</label>
                    <span class="output" id="CompanyAddress2"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "Address2"))%></span>
                </div>
                <div class="group">
                    <label>City: </label>
                    <span class="output" id="CompanyCity"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "City"))%></span>
                </div>
                <div class="group">
                    <label>Country: </label>
                    <span class="output" id="CompanyCountry"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "Country"))%></span>
                </div>
                <div class="group">
                    <label>State:</label>
                    <span class="output" id="CompanyState"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "State"))%></span>
                </div>
                <div class="group">
                    <label>Zip:</label>
                    <span class="output" id="CompanyZip"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "Zip"))%></span>
                </div>
                <div class="group">
                    <label>Phone:</label>
                    <span class="output" id="CompanyPhone"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "Phone"))%></span>
                </div>
                <div class="group">
                    <label>Fax:</label>
                    <span class="output" id="CompanyFax"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "Fax"))%></span>
                </div>
                <h2>Billing Details:</h2>
                <div class="group">
                    <label>Contact Name:</label>
                    <span class="output" id="Span1"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingName"))%></span>
                </div>
                <div class="group">
                    <label>Phone:</label>
                    <span class="output" id="Span2"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingPhone"))%></span>
                </div>
                <div class="group">
                    <label>Address Line 1:</label>
                    <span class="output" id="CompanyBillingAddress1"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingAddress1"))%></span>
                </div>
                <div class="group">
                    <label>Address Line 2:</label>
                    <span class="output" id="CompanyBillingAddress2"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingAddress2"))%></span>
                </div>
                <div class="group">
                    <label>City:</label>
                    <span class="output" id="CompanyBillingCity"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingCity"))%></span>
                </div>
                <div class="group">
                    <label>Country:</label>
                    <span class="output" id="CompanyBillingCountry"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingCountry"))%></span>
                </div>
                <div class="group">
                    <label>State:</label>
                    <span class="output" id="CompanyBillingState"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingState"))%></span>
                </div>
                <div class="group">
                    <label>Zip:</label>
                    <span class="output" id="CompanyBillingZip"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "BillingZip"))%></span>
                </div>
                <div class="group">
                    <label>Invoice Email:</label>
                    <span class="output" id="CompanyBillingEmail"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(MPUserID), "EmailInvoice"))%></span>
                </div>
            </fieldset>
        </div>
    </div>   

    <% if((ApplicationContext.IsAdmin && ApplicationContext.IsStaff) || MemberProtect.CurrentUser.UserID == MPUserID || IsNew) %>
    <% { %>
    <div class="button-row">
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Save Changes" CssClass="button primaryAction" OnClick="OnSubmit" />
        <asp:LinkButton ID="m_btnBack" runat="server" Text="Cancel Changes" CssClass="button" CausesValidation="false" OnClick="OnBack" />

        <% if(ApplicationContext.IsAdmin && !IsNew) %>
        <% { %>
            <asp:LinkButton ID="m_btnArchive" runat="server" Text="Archive" CssClass="button negativeAction" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to archive this user?');" OnClick="OnArchive" />
            <asp:LinkButton ID="m_btnReactivate" runat="server" Text="Reactivate" CssClass="button negativeAction" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to reactivate this user?');" OnClick="OnReactivate" />
        <% } %>
    </div>
    <% } %>
</div>
<div id="m_divSchedule" runat="server" class="sidebar">
    <ul id="side-tabs" class="group">
        <li id="tabSched" class="selected"><a href="#">Schedule</a></li>
    </ul>
    <div class="form-holder schedule">
        <fieldset>
            <legend>Talent Schedule:</legend>
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
            <% if(!IsNew && ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
            <% { %>
            <div class="button-row">
                <asp:LinkButton ID="m_btnSaveSchedule" runat="server" Text="Save Schedule" CssClass="button primaryAction" OnClientClick="return OnSaveSchedule();" OnClick="OnSaveSchedule" />
            </div>
            <% } %>
        </fieldset>
    </div>
</div>
</asp:Content>