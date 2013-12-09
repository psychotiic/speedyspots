<%@ Page Title="Speedy Spots :: Reset Your Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login-reset.aspx.cs" Inherits="SpeedySpots.login_reset" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <h2>Reset your password</h2>
    <p>Enter the email address you used to register and we will send you a new temporary password.</p>
    <div class="form-holder">
        <fieldset>
            <legend>Password Reset:</legend>
            <div class="group">
                <asp:Label ID="m_lblUsername" runat="server" AssociatedControlID="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your email address.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Email must be a valid email address up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Email Address:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtUsername" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnReset" runat="server" Text="Reset Password" CssClass="button primaryAction" OnClick="OnReset" />
        <a href="login.aspx" class="button">Cancel</a>
    </div>
</div>
</asp:Content>