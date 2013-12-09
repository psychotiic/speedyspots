<%@ Page Title="Speedy Spots :: User Sign Up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-registration.aspx.cs" Inherits="SpeedySpots.user_registration" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<script language="javascript" type="text/javascript">
function OnBillingIsSame()
{
    // Company Fields
    var m_txtAddressLine1 = $('#<%=m_txtAddressLine1.ClientID %>');
    var m_txtAddressLine2 = $('#<%=m_txtAddressLine2.ClientID %>');
    var m_txtCity = $('#<%=m_txtCity.ClientID %>');
    var m_cboState = $('#<%=m_cboState.ClientID %>');
    var m_cboCountry = $('#<%=m_cboCountry.ClientID %>');
    var m_txtZip = $('#<%=m_txtZip.ClientID %>');

    // Billing Fields
    var m_txtBillingAddress1 = $('#<%=m_txtBillingAddress1.ClientID %>');
    var m_txtBillingAddress2 = $('#<%=m_txtBillingAddress2.ClientID %>');
    var m_txtBillingCity = $('#<%=m_txtBillingCity.ClientID %>');
    var m_cboBillingState = $('#<%=m_cboBillingState.ClientID %>');
    var m_cboBillingCountry = $('#<%=m_cboBillingCountry.ClientID %>');
    var m_txtBillingZip = $('#<%=m_txtBillingZip.ClientID %>');

    if($('#m_chkIsBillingSame').attr('checked'))
    {
        m_txtBillingAddress1.val(m_txtAddressLine1.val());
        m_txtBillingAddress2.val(m_txtAddressLine2.val());
        m_txtBillingCity.val(m_txtCity.val());
        m_cboBillingState.val(m_cboState.val());
        m_cboBillingCountry.val(m_cboCountry.val());
        m_txtBillingZip.val(m_txtZip.val());
    }
    else
    {
        m_txtBillingAddress1.val('');
        m_txtBillingAddress2.val('');
        m_txtBillingCity.val('');
        m_cboBillingState.val('AL');
        m_cboBillingCountry.val('United States');
        m_txtBillingZip.val('');
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <h2>User Sign Up</h2>
	<div id="divUserMessage" runat="server" class="message">
        <p>If your company already has an existing account with Speedy Spots, please call us before signing up for your personal user account (800-697-8819).</p>
	</div>
        <div class="form-holder">
        <asp:HiddenField ID="m_hdnPastID" runat="server" />
        <fieldset>
            <p>Required fields are <span class="required">bold and underlined</span>.</p>
            <legend>Account Setup:</legend>
            <div class="group hasnotes">
                <asp:Label ID="m_lblUsername" runat="server" AssociatedControlID="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your email address.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Email must be a valid email address up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Email Address:</span>
                </asp:Label>
                <div class="note-holder">
                    <asp:TextBox ID="m_txtUsername" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
                    <p class="note">Example: yourname@yourcompany.com. <strong>This will become your username.</strong></p>
                </div>
            </div>
            <div class="group hasnotes">
                <asp:Label ID="m_lblPassword" runat="server" AssociatedControlID="m_txtPassword">
                    <asp:RequiredFieldValidator ID="m_reqPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Please enter your password.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expPassword" runat="server" ControlToValidate="m_txtPassword" ValidationExpression="^.{6,20}$" ErrorMessage="Password must contain between 6 and 20 characters">*</asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="m_cmpPassword" runat="server" ControlToValidate="m_txtVerifyPassword" ControlToCompare="m_txtPassword" ErrorMessage="Password and Verify Password must be the same value.">*</asp:CompareValidator>
                    <span class="required">Password:</span>
                </asp:Label>
                <div class="note-holder">
                    <asp:Textbox ID="m_txtPassword" runat="server" Columns="20" MaxLength="20" TextMode="Password" AutoCompleteType="Disabled"></asp:Textbox>
                    <p class="note">Create a password between 6 and 20 characters long.</p>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblVerifyPassword" runat="server" Text="<span class=required>Verify Password:</span>" AssociatedControlID="m_txtVerifyPassword"></asp:Label>
                <asp:Textbox ID="m_txtVerifyPassword" runat="server" Columns="20" MaxLength="20" TextMode="Password" AutoCompleteType="Disabled"></asp:Textbox>
            </div>
        </fieldset>
        </div>
        <div class="form-holder">
            <fieldset>
                <p>Required fields are <span class="required">bold and underlined</span>.</p>
                <legend>Your Information:</legend>
                <div class="group">
                    <asp:Label ID="m_lblFirstName" runat="server" AssociatedControlID="m_txtFirstName">
                        <asp:RequiredFieldValidator ID="m_reqFirstName" runat="server" ControlToValidate="m_txtFirstName" ErrorMessage="Please enter your first name.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="m_expFirstName" runat="server" ControlToValidate="m_txtFirstName" ValidationExpression="^.{0,25}$" ErrorMessage="First name may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                        <span class="required">First Name:</span>
                    </asp:Label>
                    <asp:TextBox ID="m_txtFirstName" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
                </div>
                <div class="group">
                    <asp:Label ID="m_lblLastName" runat="server" AssociatedControlID="m_txtLastName">
                        <asp:RequiredFieldValidator ID="m_reqLastName" runat="server" ControlToValidate="m_txtLastName" ErrorMessage="Please enter your last name.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="m_expLastName" runat="server" ControlToValidate="m_txtLastName" ValidationExpression="^.{0,25}$" ErrorMessage="Last name may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                        <span class="required">Last Name:</span>
                    </asp:Label>
                    <asp:TextBox ID="m_txtLastName" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
                </div>
                <div class="group">
                    <asp:Label ID="m_lblPhone" runat="server" AssociatedControlID="m_txtPhone">
                        <asp:RequiredFieldValidator ID="m_reqPhone" runat="server" ControlToValidate="m_txtPhone" ErrorMessage="Please enter your phone number.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="m_expPhoneExt" runat="server" ControlToValidate="m_txtPhoneExt" ValidationExpression="^\d{0,5}$" ErrorMessage="Phone Extension may be up to 5 digits.">*</asp:RegularExpressionValidator>
                        <span class="required">Phone:</span>
                    </asp:Label>
                    <div class="inputgroup">
                        <asp:TextBox ID="m_txtPhone" runat="server" Columns="15" MaxLength="15"></asp:TextBox>
                        <asp:Label ID="m_lblPhoneExt" runat="server" Text="Ext:" AssociatedControlID="m_txtPhoneExt"></asp:Label>
                        <asp:TextBox ID="m_txtPhoneExt" runat="server" Columns="5" MaxLength="5"></asp:TextBox>
                    </div>
                </div>
                <div class="group">
                    <asp:Label ID="m_lblMobilePhone" runat="server" AssociatedControlID="m_txtMobilePhone">
                        Mobile Phone:
                    </asp:Label>
                    <asp:TextBox ID="m_txtMobilePhone" runat="server" Columns="15" MaxLength="15"></asp:TextBox>
                </div>
                <div class="group">
                    <asp:Label ID="m_lblDepartment" runat="server" AssociatedControlID="m_txtDepartment">
                        <asp:RegularExpressionValidator ID="m_expDepartment" runat="server" ControlToValidate="m_txtDepartment" ValidationExpression="^.{0,25}$" ErrorMessage="Department may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                        Department:
                    </asp:Label>
                    <asp:TextBox ID="m_txtDepartment" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
                </div>
            </fieldset>
        </div>
        <div class="message">
            <p>Please know after creating your account you will be unable to update your company or billing details without contacting us.</p>
        </div>
        <div class="form-holder">
        <fieldset>
            <p>Required fields are <span class="required">bold and underlined</span>.</p>
            <legend>Company Information:</legend>
            <div class="group">
                <asp:Label ID="m_lblCompanyName" runat="server" AssociatedControlID="m_txtCompanyName">
                    <asp:RequiredFieldValidator ID="m_reqCompanyName" runat="server" ControlToValidate="m_txtCompanyName" ErrorMessage="Please enter your company name.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expCompanyName" runat="server" ControlToValidate="m_txtCompanyName" ValidationExpression="^.{0,50}$" ErrorMessage="Company name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Company Name:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtCompanyName" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblAddressLine1" runat="server" AssociatedControlID="m_txtAddressLine1">
                    <asp:RequiredFieldValidator ID="m_reqAddressLine1" runat="server" ControlToValidate="m_txtAddressLine1" ErrorMessage="Please enter your address line 1.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expAddressLine1" runat="server" ControlToValidate="m_txtAddressLine1" ValidationExpression="^.{0,25}$" ErrorMessage="Address line 1 may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Address line 1:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtAddressLine1" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
            </div>
            <div class="group hasnotes">
                <asp:Label ID="m_lblAddressLine2" runat="server" AssociatedControlID="m_txtAddressLine2">Address line 2:</asp:Label>
                <asp:TextBox ID="m_txtAddressLine2" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblCity" runat="server" AssociatedControlID="m_txtCity">
                    <asp:RequiredFieldValidator ID="m_reqCity" runat="server" ControlToValidate="m_txtCity" ErrorMessage="Please enter your city.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expCity" runat="server" ControlToValidate="m_txtCity" ValidationExpression="^.{0,25}$" ErrorMessage="City may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">City:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtCity" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblState" runat="server" Text="<span class=required>State:</span>" AssociatedControlID="m_cboState">
                    <asp:RequiredFieldValidator ID="m_reqState" runat="server" ControlToValidate="m_cboState" ErrorMessage="Please select your state.">*</asp:RequiredFieldValidator>
                </asp:Label>
                <asp:DropDownList ID="m_cboState" runat="server">
					<asp:ListItem Value="N/A" Text="Not Applicable" />
					<asp:ListItem Selected="true" Value="AL" Text="Alabama" />
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
            </div>
            <div class="group">
                <asp:Label ID="Label9" runat="server" Text="<span class=required>Country:</span>" AssociatedControlID="m_cboCountry">
                    <asp:RequiredFieldValidator ID="m_reqCountry" runat="server" ControlToValidate="m_cboCountry" ErrorMessage="Please select your country.">*</asp:RequiredFieldValidator>
                </asp:Label>
                <asp:DropDownList ID="m_cboCountry" runat="server" />
            </div>
            <div class="group">
                <asp:Label ID="m_lblZip" runat="server" AssociatedControlID="m_txtZip">
                    <asp:RequiredFieldValidator ID="m_reqZip" runat="server" ControlToValidate="m_txtZip" ErrorMessage="Please enter your zip.">*</asp:RequiredFieldValidator>
                    <span class="required">Zip:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtZip" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblCompanyPhone" runat="server" AssociatedControlID="m_txtCompanyPhone">
                    <asp:RequiredFieldValidator ID="m_reqCompanyPhone" runat="server" ControlToValidate="m_txtCompanyPhone" ErrorMessage="Please enter your company phone number.">*</asp:RequiredFieldValidator>
                    <span class="required">Phone:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtCompanyPhone" runat="server" Columns="15" MaxLength="15"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="Label8" runat="server" AssociatedControlID="m_txtFax">
                    <span>Fax:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtFax" runat="server" Columns="15" MaxLength="15"></asp:TextBox>
            </div>
        </fieldset>
    </div>
    <div class="form-holder">
        <fieldset>
            <p>Required fields are <span class="required">bold and underlined</span>.</p>
            <legend>Billing Information:</legend>
            <div id="m_divIsBillingSame" runat="server" class="group">
                <label>&nbsp;</label>
                <div class="inputgroup">
                    <label><input type="checkbox" id="m_chkIsBillingSame" onclick="OnBillingIsSame()" /> Same as company information</label>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="Label3" runat="server" AssociatedControlID="m_txtBillingAddress1">
                    <asp:RegularExpressionValidator ID="m_expBillingAddress1" runat="server" ControlToValidate="m_txtBillingAddress1" ValidationExpression="^.{0,25}$" ErrorMessage="Billing address 1 may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="m_reqBillingAddress1" runat="server" ControlToValidate="m_txtBillingAddress1" ErrorMessage="Billing address is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Address line 1:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtBillingAddress1" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
            </div>
            <div class="group hasnotes">
                <asp:Label ID="Label4" runat="server" AssociatedControlID="m_txtBillingAddress2">Address line 2:</asp:Label>
                <asp:TextBox ID="m_txtBillingAddress2" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="Label5" runat="server" AssociatedControlID="m_txtBillingCity">
                    <asp:RegularExpressionValidator ID="m_expBillingCity" runat="server" ControlToValidate="m_txtBillingCity" ValidationExpression="^.{0,25}$" ErrorMessage="Billing city may contain up to 25 characters.">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="m_reqBillingCity" runat="server" ControlToValidate="m_txtBillingCity" ErrorMessage="Billing city is required.">*</asp:RequiredFieldValidator>
                    <span class="required">City:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtBillingCity" runat="server" Columns="25" MaxLength="25"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="Label6" runat="server" AssociatedControlID="m_cboBillingState">
                    <asp:RequiredFieldValidator ID="m_reqBillingState" runat="server" ControlToValidate="m_cboBillingState" ErrorMessage="Billing state is required.">*</asp:RequiredFieldValidator>
                    <span class="required">State:</span>
                </asp:Label>
                <asp:DropDownList ID="m_cboBillingState" runat="server">
					<asp:ListItem Value="N/A" Text="Not Applicable" />
					<asp:ListItem Selected="true" Value="AL" Text="Alabama" />
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
            </div>
            <div class="group">
                <asp:Label ID="Label10" runat="server" Text="<span class=required>Country:</span>" AssociatedControlID="m_cboBillingCountry">
                    <asp:RequiredFieldValidator ID="m_reqBillingCountry" runat="server" ControlToValidate="m_cboBillingCountry" ErrorMessage="Please select your country.">*</asp:RequiredFieldValidator>
                </asp:Label>
                <asp:DropDownList ID="m_cboBillingCountry" runat="server" />
            </div>
            <div class="group">
                <asp:Label ID="Label7" runat="server" AssociatedControlID="m_txtBillingZip">
                    <asp:RequiredFieldValidator ID="m_reqBillingZip" runat="server" ControlToValidate="m_txtBillingZip" ErrorMessage="Billing zip is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Zip:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtBillingZip" runat="server" Columns="10" MaxLength="10"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="Label1" runat="server" AssociatedControlID="m_txtBillingName">
                    <asp:RegularExpressionValidator ID="m_expBillingName" runat="server" ControlToValidate="m_txtBillingName" ValidationExpression="^.{0,50}$" ErrorMessage="Name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="m_reqBillingName" runat="server" ControlToValidate="m_txtBillingName" ErrorMessage="Name is required.">*</asp:RequiredFieldValidator>
                    <span class="required">Contact Name:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtBillingName" runat="server" Columns="50" MaxLength="50"></asp:TextBox>
            </div>
            <div class="group">
                <asp:Label ID="Label2" runat="server" AssociatedControlID="m_txtBillingPhone">
                    <asp:RequiredFieldValidator ID="m_reqBillingPhone" runat="server" ControlToValidate="m_txtBillingPhone" ErrorMessage="Please enter your the billing contact phone number.">*</asp:RequiredFieldValidator>
                    <span class="required">Phone:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtBillingPhone" runat="server" Columns="15" MaxLength="15"></asp:TextBox>
            </div>
            <div class="group hasnotes">
                <asp:Label ID="Label13" runat="server" AssociatedControlID="m_txtEmailInvoice">
                    <asp:RequiredFieldValidator ID="m_reqEmailInvoice" runat="server" ControlToValidate="m_txtEmailInvoice" ErrorMessage="Please at least one invoice email address.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expEmailInvoice" runat="server" ControlToValidate="m_txtEmailInvoice" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4},?[\s]*)*$" ErrorMessage="Email to receive invoices must be a valid email address up to 100 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Invoice Email:</span>
                </asp:Label>
                <div class="note-holder">
                    <asp:TextBox ID="m_txtEmailInvoice" runat="server" Columns="55" MaxLength="250"></asp:TextBox>
                    <p class="note">Email address(s) to receive invoice notifications.<br />Please seperate addresses with a comma.</p>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Sign Up" CssClass="button primaryAction" OnClick="OnSubmit" />
    </div>
</div>
</asp:Content>
