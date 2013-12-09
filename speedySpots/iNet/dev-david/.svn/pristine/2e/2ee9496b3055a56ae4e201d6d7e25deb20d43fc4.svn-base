<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-reactivate.aspx.cs" Inherits="SpeedySpots.user_reactivate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <h2>Request To Reactivate Your Account</h2>
    <p>Please provide your details in the form below to request that your account be reactivated. We will be in touch after your request is made to verify your account.</p>
	<div class="message">
		<p>All fields are required.</p>
	</div>
    <div class="form-holder">
        <fieldset>
            <legend>Account Information:</legend>
            <div class="group">
                    <asp:Label ID="m_lblName" runat="server" AssociatedControlID="m_txtName">
                        <asp:RequiredFieldValidator ID="m_reqName" runat="server" ControlToValidate="m_txtName" ErrorMessage="Please enter your name.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="m_expName" runat="server" ControlToValidate="m_txtName" ValidationExpression="^.{0,50}$" ErrorMessage="Name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                        <span class="required">Name:</span>
                    </asp:Label>
                    <telerik:RadTextBox ID="m_txtName" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
                </div>
            <div class="group hasnotes">
                <asp:Label ID="m_lblUsername" runat="server" AssociatedControlID="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your email address.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Email must be a valid email address up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Email Address:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtUsername" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblPhone" runat="server" AssociatedControlID="m_txtPhone">
                    <asp:RequiredFieldValidator ID="m_reqPhone" runat="server" ControlToValidate="m_txtPhone" ErrorMessage="Please enter your phone number.">*</asp:RequiredFieldValidator>
                    <span class="required">Phone:</span>
                </asp:Label>
                <div class="inputgroup">
                    <telerik:RadTextBox ID="m_txtPhone" runat="server" Columns="15" MaxLength="15"></telerik:RadTextBox>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblCompanyName" runat="server" AssociatedControlID="m_txtCompanyName">
                    <asp:RequiredFieldValidator ID="m_reqCompanyName" runat="server" ControlToValidate="m_txtCompanyName" ErrorMessage="Please enter your company name.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expCompanyName" runat="server" ControlToValidate="m_txtCompanyName" ValidationExpression="^.{0,50}$" ErrorMessage="Company name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Company Name:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtCompanyName" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
            <div class="group">
                <asp:Label ID="Label11" runat="server" AssociatedControlID="m_txtMessage">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_txtMessage" ErrorMessage="Please enter your reactivation request message.">*</asp:RequiredFieldValidator>
                    <span class="required">Message:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtMessage" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></telerik:RadTextBox>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Request Reactivation" CssClass="button primaryAction" OnClick="OnReactivate" />
    </div>
</div>
</asp:Content>