<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="OrganizationEdit.aspx.cs" Inherits="SpeedySpots.InetActive.OrganizationEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
    <h2>Edit Organization</h2>
    <div class="form-container">

    <fieldset>
        <legend>Organization Information</legend>
        <div>
            <label for="m_txtName">
                <asp:RequiredFieldValidator ID="m_reqName" runat="server" ControlToValidate="m_txtName" ErrorMessage="Please enter the organization name.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expName" runat="server" ControlToValidate="m_txtName" ValidationExpression="^.{0,50}$" ErrorMessage="Organization name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Name:
            </label>
            <telerik:RadTextBox id="m_txtName" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
        </div>
        <div>
            <label for="m_txtAddress1">
                <asp:RequiredFieldValidator ID="m_reqAddress1" runat="server" ControlToValidate="m_txtAddress1" ErrorMessage="Address 1 is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expAddress1" runat="server" ControlToValidate="m_txtAddress1" ValidationExpression="^.{0,50}$" ErrorMessage="Address 1 may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Address 1:
            </label>
            <telerik:RadTextBox id="m_txtAddress1" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
        </div>
        <div>
            <label for="m_txtAddress2">
                <asp:RegularExpressionValidator ID="m_expAddress2" runat="server" ControlToValidate="m_txtAddress2" ValidationExpression="^.{0,50}$" ErrorMessage="Address 2 may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Address 2:
            </label>
            <telerik:RadTextBox id="m_txtAddress2" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
        </div>
        <div>
            <label for="m_txtCity">
                <asp:RequiredFieldValidator ID="m_reqCity" runat="server" ControlToValidate="m_txtCity" ErrorMessage="City is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expCity" runat="server" ControlToValidate="m_txtCity" ValidationExpression="^.{0,50}$" ErrorMessage="City may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                City:
            </label>
            <telerik:RadTextBox id="m_txtCity" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
        </div>
        <div>
            <label for="m_cboState">
                State:
            </label>
            <telerik:RadComboBox ID="m_cboState" runat="server">
                <Items>
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
            </telerik:RadComboBox>
        </div>
        <div>
            <label for="m_txtZip">Zip:</label>
            <telerik:RadTextBox id="m_txtZip" runat="server" Columns="10" MaxLength="10"></telerik:RadTextBox>
        </div>
        <div>
            <label for="m_txtPhone" class="required">Phone:</label>
            <telerik:RadTextBox ID="m_txtPhone" runat="server" Columns="15" MaxLength="12"></telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="m_reqPhone" runat="server" ControlToValidate="m_txtPhone" ErrorMessage="Phone is required.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="m_expPhone" runat="server" ControlToValidate="m_txtPhone" ValidationExpression="^\d{3}-\d{3}-\d{4}$" ErrorMessage="Phone must be a valid phone number (123-123-1234).">*</asp:RegularExpressionValidator>
        </div>
    </fieldset>

    <div class="buttonrow">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="<span>Save Organization</span>" 
            CssClass="button positive" onclick="OnSave" />
        <a href="OrganizationList.aspx" class="button"><span>Back</span></a>
        <asp:LinkButton ID="m_btnDelete" runat="server" Text="<span>Delete Organization</span>" CssClass="button negative" CausesValidation="False" onclick="OnDelete" OnClientClick="return ConfirmUser('Are you sure you want to delete this organization?');" />
    </div>
    </div>
</asp:Content>
