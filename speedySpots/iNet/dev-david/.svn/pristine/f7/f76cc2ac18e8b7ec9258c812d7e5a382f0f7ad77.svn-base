<%@ Page Title="Speedy Spots :: Modify Company" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="company-modify.aspx.cs" Inherits="SpeedySpots.company_modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<script language="javascript" type="text/javascript">
function OnBillingIsSame()
{
    // Company Fields
    var m_txtAddressLine1 = $find('<%=m_txtAddressLine1.ClientID %>');
    var m_txtAddressLine2 = $find('<%=m_txtAddressLine2.ClientID %>');
    var m_txtCity = $find('<%=m_txtCity.ClientID %>');
    var m_cboState = $find('<%=m_cboState.ClientID %>');
    var m_cboCountry = $find('<%=m_cboCountry.ClientID %>');
    var m_txtZip = $find('<%=m_txtZip.ClientID %>');

    // Billing Fields
    var m_txtBillingAddress1 = $find('<%=m_txtBillingAddress1.ClientID %>');
    var m_txtBillingAddress2 = $find('<%=m_txtBillingAddress2.ClientID %>');
    var m_txtBillingCity = $find('<%=m_txtBillingCity.ClientID %>');
    var m_cboBillingState = $find('<%=m_cboBillingState.ClientID %>');
    var m_cboBillingCountry = $find('<%=m_cboBillingCountry.ClientID %>');
    var m_txtBillingZip = $find('<%=m_txtBillingZip.ClientID %>');

    if($('#m_chkIsBillingSame').attr('checked'))
    {
        m_txtBillingAddress1.set_value(m_txtAddressLine1.get_value());
        m_txtBillingAddress2.set_value(m_txtAddressLine2.get_value());
        m_txtBillingCity.set_value(m_txtCity.get_value());
        m_cboBillingState.set_text(m_cboState.get_text());
        m_cboBillingCountry.set_text(m_cboCountry.get_text());
        m_txtBillingZip.set_value(m_txtZip.get_value());
    }
    else
    {
        m_txtBillingAddress1.set_value("");
        m_txtBillingAddress2.set_value("");
        m_txtBillingCity.set_value("");
        m_cboBillingState.set_text("Alabama");
        m_cboBillingCountry.set_text("United States");
        m_txtBillingZip.set_value("");
    }
}
</script>
<p class="breadcrumb"><a href="admin-dashboard.aspx">Dashboard</a> &raquo; <a href="companies.aspx">Companies</a> &raquo; <strong>Modify Company</strong></p>
<h2>Modify Company</h2>
<div class="form-holder">
    <fieldset>
        <legend>Company Information:</legend>
        <% if(!IsNew) %>
        <% { %>
        <div class="group">
            <asp:Label ID="Label1" runat="server" AssociatedControlID="m_txtCompanyName">
                <span class="required">Company ID:</span>
            </asp:Label>
            <telerik:RadTextBox id="m_txtCompanyID" runat="server" Columns="40" ReadOnly="True"></telerik:RadTextBox>
        </div>
        <% } %>
        <div class="group">
            <asp:Label ID="m_lblCompanyName" runat="server" AssociatedControlID="m_txtCompanyName">
                <asp:RequiredFieldValidator ID="m_reqCompanyName" runat="server" ControlToValidate="m_txtCompanyName" ErrorMessage="Please enter your company name.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expCompanyName" runat="server" ControlToValidate="m_txtCompanyName" ValidationExpression="^.{0,75}$" ErrorMessage="Company name may contain up to 75 characters.">*</asp:RegularExpressionValidator>
                <span class="required">Company Name:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtCompanyName" runat="server" Columns="50" MaxLength="75"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblAddressLine1" runat="server" AssociatedControlID="m_txtAddressLine1">
                <asp:RequiredFieldValidator ID="m_reqAddressLine1" runat="server" ControlToValidate="m_txtAddressLine1" ErrorMessage="Please enter your address line 1.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expAddressLine1" runat="server" ControlToValidate="m_txtAddressLine1" ValidationExpression="^.{0,75}$" ErrorMessage="Address line 1 may contain up to 75 characters.">*</asp:RegularExpressionValidator>
                <span class="required">Address line 1:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtAddressLine1" runat="server" Columns="25" MaxLength="75"></telerik:RadTextBox>
        </div>
        <div class="group hasnotes">
            <asp:Label ID="m_lblAddressLine2" runat="server" AssociatedControlID="m_txtAddressLine2">Address line 2:</asp:Label>
            <telerik:RadTextBox ID="m_txtAddressLine2" runat="server" Columns="25" MaxLength="75"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblCity" runat="server" AssociatedControlID="m_txtCity">
                <asp:RequiredFieldValidator ID="m_reqCity" runat="server" ControlToValidate="m_txtCity" ErrorMessage="Please enter your city.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expCity" runat="server" ControlToValidate="m_txtCity" ValidationExpression="^.{0,50}$" ErrorMessage="City may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                <span class="required">City:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtCity" runat="server" Columns="25" MaxLength="50"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblState" runat="server" Text="<span class=required>State:</span>" AssociatedControlID="m_cboState">
                <asp:RequiredFieldValidator ID="m_reqState" runat="server" ControlToValidate="m_cboState" ErrorMessage="Please select your state.">*</asp:RequiredFieldValidator>
            </asp:Label>
            <telerik:RadComboBox ID="m_cboState" runat="server" Height="200px">
                <Items>
                    <telerik:RadComboBoxItem Value="N/A" Text="Not Applicable" />
                    <telerik:RadComboBoxItem Selected="true" Value="AL" Text="Alabama" />
                    <telerik:RadComboBoxItem Value="AK" Text="Alaska" />
                    <telerik:RadComboBoxItem Value="AZ" Text="Arizona" />
                    <telerik:RadComboBoxItem Value="AR" Text="Arkansas" />
                    <telerik:RadComboBoxItem Value="CA" Text="California" />
                    <telerik:RadComboBoxItem Value="CO" Text="Colorado" />
                    <telerik:RadComboBoxItem Value="CT" Text="Connecticut" />
                    <telerik:RadComboBoxItem Value="DE" Text="Delaware" />
                    <telerik:RadComboBoxItem Value="DC" Text="District of Columbia" />
                    <telerik:RadComboBoxItem Value="FL" Text="Florida" />
                    <telerik:RadComboBoxItem Value="GA" Text="Georgia" />
                    <telerik:RadComboBoxItem Value="HI" Text="Hawaii" />
                    <telerik:RadComboBoxItem Value="ID" Text="Idaho" />
                    <telerik:RadComboBoxItem Value="IL" Text="Illinois" />
                    <telerik:RadComboBoxItem Value="IN" Text="Indiana" />
                    <telerik:RadComboBoxItem Value="IA" Text="Iowa" />
                    <telerik:RadComboBoxItem Value="KS" Text="Kansas" />
                    <telerik:RadComboBoxItem Value="KY" Text="Kentucky" />
                    <telerik:RadComboBoxItem Value="LA" Text="Louisiana" />
                    <telerik:RadComboBoxItem Value="ME" Text="Maine" />
                    <telerik:RadComboBoxItem Value="MD" Text="Maryland" />
                    <telerik:RadComboBoxItem Value="MA" Text="Massachusetts" />
                    <telerik:RadComboBoxItem Value="MI" Text="Michigan" />
                    <telerik:RadComboBoxItem Value="MN" Text="Minnesota" />
                    <telerik:RadComboBoxItem Value="MS" Text="Mississippi" />
                    <telerik:RadComboBoxItem Value="MO" Text="Missouri" />
                    <telerik:RadComboBoxItem Value="MT" Text="Montana" />
                    <telerik:RadComboBoxItem Value="NE" Text="Nebraska" />
                    <telerik:RadComboBoxItem Value="NV" Text="Nevada" />
                    <telerik:RadComboBoxItem Value="NH" Text="New Hampshire" />
                    <telerik:RadComboBoxItem Value="NJ" Text="New Jersey" />
                    <telerik:RadComboBoxItem Value="NM" Text="New Mexico" />
                    <telerik:RadComboBoxItem Value="NY" Text="New York" />
                    <telerik:RadComboBoxItem Value="NC" Text="North Carolina" />
                    <telerik:RadComboBoxItem Value="ND" Text="North Dakota" />
                    <telerik:RadComboBoxItem Value="OH" Text="Ohio" />
                    <telerik:RadComboBoxItem Value="OK" Text="Oklahoma" />
                    <telerik:RadComboBoxItem Value="OR" Text="Oregon" /> 
                    <telerik:RadComboBoxItem Value="PA" Text="Pennsylvania" /> 
                    <telerik:RadComboBoxItem Value="RI" Text="Rhode Island" /> 
                    <telerik:RadComboBoxItem Value="SC" Text="South Carolina" /> 
                    <telerik:RadComboBoxItem Value="SD" Text="South Dakota" /> 
                    <telerik:RadComboBoxItem Value="TN" Text="Tennessee" /> 
                    <telerik:RadComboBoxItem Value="TX" Text="Texas" /> 
                    <telerik:RadComboBoxItem Value="UT" Text="Utah" /> 
                    <telerik:RadComboBoxItem Value="VT" Text="Vermont" /> 
                    <telerik:RadComboBoxItem Value="VA" Text="Virginia" /> 
                    <telerik:RadComboBoxItem Value="WA" Text="Washington" /> 
                    <telerik:RadComboBoxItem Value="WV" Text="West Virginia" /> 
                    <telerik:RadComboBoxItem Value="WI" Text="Wisconsin" /> 
                    <telerik:RadComboBoxItem Value="WY" Text="Wyoming" /> 
                </Items>
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </telerik:RadComboBox>
        </div>
        <div class="group">
            <asp:Label ID="Label10" runat="server" AssociatedControlID="m_cboCountry">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_cboCountry" ErrorMessage="Please enter your country.">*</asp:RequiredFieldValidator>
                <span class="required">Country:</span>
            </asp:Label>
            <telerik:RadComboBox ID="m_cboCountry" runat="server" Height="200px">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </telerik:RadComboBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblZip" runat="server" AssociatedControlID="m_txtZip">
                <asp:RequiredFieldValidator ID="m_reqZip" runat="server" ControlToValidate="m_txtZip" ErrorMessage="Please enter your zip.">*</asp:RequiredFieldValidator>
                <span class="required">Zip:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtZip" runat="server" Columns="10" MaxLength="10"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblCompanyPhone" runat="server" AssociatedControlID="m_txtCompanyPhone">
                <asp:RequiredFieldValidator ID="m_reqCompanyPhone" runat="server" ControlToValidate="m_txtCompanyPhone" ErrorMessage="Please enter your company phone number.">*</asp:RequiredFieldValidator>
                <span class="required">Phone:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtCompanyPhone" runat="server" Columns="15" MaxLength="100"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label9" runat="server" AssociatedControlID="m_txtFax">
                <span class="required">Fax:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtFax" runat="server" Columns="15" MaxLength="100"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblPayFirst" runat="server" AssociatedControlID="m_txtZip">
                <span class="required">Pay First:</span>
            </asp:Label>
            <div class="inputgroup">
                <asp:RadioButtonList ID="m_radPayFirst" runat="server">
                    <asp:ListItem Text="Pre-Approved" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Requires Estimate" Value="Y"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="group">
            <asp:Label ID="m_lblIsVerified" runat="server" AssociatedControlID="m_chkIsVerified">Verified:</asp:Label>
            <asp:CheckBox ID="m_chkIsVerified" runat="server" />
        </div>
        <div class="group">
            <label>Created Date/time:</label>
            <asp:Label ID="m_lblCreatedDateTime" runat="server"></asp:Label>
        </div>
        <div id="m_divNotes" runat="server" class="group">
            <label>Notes:</label>
            <div class="inputgroup">
                <telerik:RadEditor ID="m_txtNotes" runat="server" Width="100%" EditModes="Design" MaxTextLength="5000" StripFormattingOptions="MSWord">
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
<div id="m_divBilling" runat="server" class="form-holder">
    <fieldset>
        <legend>Billing Information:</legend>
        <div id="m_divIsBillingSame" runat="server" class="group">
            <label>&nbsp;</label>
            <div class="inputgroup">
                <input type="checkbox" id="m_chkIsBillingSame" onclick="OnBillingIsSame()" /> <label for="m_chkIsBillingSame">Same as company information</label>
            </div>
        </div>
        <div class="group">
            <asp:Label ID="Label3" runat="server" AssociatedControlID="m_txtBillingAddress1">
                <asp:RegularExpressionValidator ID="m_expBillingAddress1" runat="server" ControlToValidate="m_txtBillingAddress1" ValidationExpression="^.{0,75}$" ErrorMessage="Billing address 1 may contain up to 75 characters.">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="m_reqBillingAddress1" runat="server" ControlToValidate="m_txtBillingAddress1" ErrorMessage="Billing address is required.">*</asp:RequiredFieldValidator>
                <span class="required">Address Line 1:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtBillingAddress1" runat="server" Columns="25" MaxLength="75"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label4" runat="server" AssociatedControlID="m_txtBillingAddress2">Address Line 2:</asp:Label>
            <telerik:RadTextBox ID="m_txtBillingAddress2" runat="server" Columns="25" MaxLength="75"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label5" runat="server" AssociatedControlID="m_txtBillingCity">
                <asp:RegularExpressionValidator ID="m_expBillingCity" runat="server" ControlToValidate="m_txtBillingCity" ValidationExpression="^.{0,50}$" ErrorMessage="Billing city may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="m_reqBillingCity" runat="server" ControlToValidate="m_txtBillingCity" ErrorMessage="Billing city is required.">*</asp:RequiredFieldValidator>
                <span class="required">City:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtBillingCity" runat="server" Columns="25" MaxLength="50"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label6" runat="server" Text="<span class=required>State:</span>" AssociatedControlID="m_cboBillingState">
            </asp:Label>
            <telerik:RadComboBox ID="m_cboBillingState" runat="server" Height="200px">
                <Items>
                    <telerik:RadComboBoxItem Selected="true" Value="" Text="" />
                    <telerik:RadComboBoxItem Value="N/A" Text="Not Applicable" />
                    <telerik:RadComboBoxItem Value="AL" Text="Alabama" />
                    <telerik:RadComboBoxItem Value="AK" Text="Alaska" />
                    <telerik:RadComboBoxItem Value="AZ" Text="Arizona" />
                    <telerik:RadComboBoxItem Value="AR" Text="Arkansas" />
                    <telerik:RadComboBoxItem Value="CA" Text="California" />
                    <telerik:RadComboBoxItem Value="CO" Text="Colorado" />
                    <telerik:RadComboBoxItem Value="CT" Text="Connecticut" />
                    <telerik:RadComboBoxItem Value="DE" Text="Delaware" />
                    <telerik:RadComboBoxItem Value="DC" Text="District of Columbia" />
                    <telerik:RadComboBoxItem Value="FL" Text="Florida" />
                    <telerik:RadComboBoxItem Value="GA" Text="Georgia" />
                    <telerik:RadComboBoxItem Value="HI" Text="Hawaii" />
                    <telerik:RadComboBoxItem Value="ID" Text="Idaho" />
                    <telerik:RadComboBoxItem Value="IL" Text="Illinois" />
                    <telerik:RadComboBoxItem Value="IN" Text="Indiana" />
                    <telerik:RadComboBoxItem Value="IA" Text="Iowa" />
                    <telerik:RadComboBoxItem Value="KS" Text="Kansas" />
                    <telerik:RadComboBoxItem Value="KY" Text="Kentucky" />
                    <telerik:RadComboBoxItem Value="LA" Text="Louisiana" />
                    <telerik:RadComboBoxItem Value="ME" Text="Maine" />
                    <telerik:RadComboBoxItem Value="MD" Text="Maryland" />
                    <telerik:RadComboBoxItem Value="MA" Text="Massachusetts" />
                    <telerik:RadComboBoxItem Value="MI" Text="Michigan" />
                    <telerik:RadComboBoxItem Value="MN" Text="Minnesota" />
                    <telerik:RadComboBoxItem Value="MS" Text="Mississippi" />
                    <telerik:RadComboBoxItem Value="MO" Text="Missouri" />
                    <telerik:RadComboBoxItem Value="MT" Text="Montana" />
                    <telerik:RadComboBoxItem Value="NE" Text="Nebraska" />
                    <telerik:RadComboBoxItem Value="NV" Text="Nevada" />
                    <telerik:RadComboBoxItem Value="NH" Text="New Hampshire" />
                    <telerik:RadComboBoxItem Value="NJ" Text="New Jersey" />
                    <telerik:RadComboBoxItem Value="NM" Text="New Mexico" />
                    <telerik:RadComboBoxItem Value="NY" Text="New York" />
                    <telerik:RadComboBoxItem Value="NC" Text="North Carolina" />
                    <telerik:RadComboBoxItem Value="ND" Text="North Dakota" />
                    <telerik:RadComboBoxItem Value="OH" Text="Ohio" />
                    <telerik:RadComboBoxItem Value="OK" Text="Oklahoma" />
                    <telerik:RadComboBoxItem Value="OR" Text="Oregon" /> 
                    <telerik:RadComboBoxItem Value="PA" Text="Pennsylvania" /> 
                    <telerik:RadComboBoxItem Value="RI" Text="Rhode Island" /> 
                    <telerik:RadComboBoxItem Value="SC" Text="South Carolina" /> 
                    <telerik:RadComboBoxItem Value="SD" Text="South Dakota" /> 
                    <telerik:RadComboBoxItem Value="TN" Text="Tennessee" /> 
                    <telerik:RadComboBoxItem Value="TX" Text="Texas" /> 
                    <telerik:RadComboBoxItem Value="UT" Text="Utah" /> 
                    <telerik:RadComboBoxItem Value="VT" Text="Vermont" /> 
                    <telerik:RadComboBoxItem Value="VA" Text="Virginia" /> 
                    <telerik:RadComboBoxItem Value="WA" Text="Washington" /> 
                    <telerik:RadComboBoxItem Value="WV" Text="West Virginia" /> 
                    <telerik:RadComboBoxItem Value="WI" Text="Wisconsin" /> 
                    <telerik:RadComboBoxItem Value="WY" Text="Wyoming" /> 
                </Items>
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>                
            </telerik:RadComboBox>
        </div>
        <div class="group">
            <asp:Label ID="Label11" runat="server" AssociatedControlID="m_cboBillingCountry">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_cboBillingCountry" ErrorMessage="Please enter your country.">*</asp:RequiredFieldValidator>
                <span class="required">Country:</span>
            </asp:Label>
            <telerik:RadComboBox ID="m_cboBillingCountry" runat="server" Height="200px">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </telerik:RadComboBox>
        </div>
        <div class="group">
            <asp:Label ID="Label7" runat="server" AssociatedControlID="m_txtBillingZip">
                <asp:RequiredFieldValidator ID="m_reqBillingZip" runat="server" ControlToValidate="m_txtBillingZip" ErrorMessage="Billing zip is required.">*</asp:RequiredFieldValidator>
                <span class="required">Zip:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtBillingZip" runat="server" Columns="10" MaxLength="10"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label2" runat="server" AssociatedControlID="m_txtBillingName">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="m_txtBillingName" ValidationExpression="^.{0,100}$" ErrorMessage="Name may contain up to 100 characters.">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_txtBillingName" ErrorMessage="Name is required.">*</asp:RequiredFieldValidator>
                <span class="required">Contact Name:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtBillingName" runat="server" Columns="50" MaxLength="100"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label8" runat="server" AssociatedControlID="m_txtBillingPhone">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_txtBillingPhone" ErrorMessage="Please enter your the billing contact phone number.">*</asp:RequiredFieldValidator>
                <span class="required">Phone:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtBillingPhone" runat="server" Columns="15" MaxLength="100"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="Label13" runat="server" AssociatedControlID="m_txtEmailInvoice">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_txtEmailInvoice" ErrorMessage="Please at least one invoice email address.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="m_txtEmailInvoice" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4},?[\s]*)*$" ErrorMessage="Email to receive invoices must be a valid email address up to 100 characters.">*</asp:RegularExpressionValidator>
                <span class="required">Invoice Email:</span>
            </asp:Label>
            <div class="note-holder">
                <telerik:RadTextBox ID="m_txtEmailInvoice" runat="server" Columns="55" MaxLength="250"></telerik:RadTextBox>
                <p class="note">Email address(s) to receive invoice notifications.<br />Please seperate addresses with a comma.</p>
            </div>
        </div>
    </fieldset>
</div>
<% if(!IsNew) %>
<% { %>

    <% if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
    <% { %>
    <div class="button-row action">
        <asp:LinkButton ID="m_btnAddUser" runat="server" Text="Add User" OnClick="OnAddUser" CssClass="button primaryAction"></asp:LinkButton>
    </div>
    <% } %>

    <div class="form-holder">
        <fieldset>
            <legend><%=UserCount%> Assigned Users:</legend>
            <div class="group">
                <asp:Repeater ID="m_oRepeaterUsers" runat="server">
                    <ItemTemplate>
                        <a href='user-account.aspx?id=<%# FormatURLFriendlyGUID(DataBinder.Eval(Container.DataItem, "MPUserID").ToString()) %>'><%# DataBinder.Eval(Container.DataItem, "FirstName") + " " + DataBinder.Eval(Container.DataItem, "LastName") %></a><%# FormatIsArchived(DataBinder.Eval(Container.DataItem, "IsArchived").ToString())%><br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </fieldset>
    </div>
<% } %>

<div class="button-row">
    <% if(IsNew || (!IsNew && ApplicationContext.IsAdmin && ApplicationContext.IsStaff)) %>
    <% { %>
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Save Changes" CssClass="button primaryAction" OnClick="OnSave" />
        <asp:LinkButton ID="m_btnBack" runat="server" Text="Cancel Changes" CssClass="button" CausesValidation="false" OnClick="OnBack" />
    <% } %>
    <% if(!IsNew && ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
    <% { %>
    <asp:LinkButton ID="m_btnArchive" runat="server" Text="Archive Company" CssClass="button negativeAction" OnClientClick="return ConfirmUser('Are you sure you wish to archive this company and all associated users?');" OnClick="OnArchive" />
    <asp:LinkButton ID="m_btnReactivate" runat="server" Text="Reactivate" CssClass="button negativeAction" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to reactivate this company?');" OnClick="OnReactivate" />
    <% } %>
</div>
</asp:Content>
