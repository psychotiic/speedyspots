<%@ Page Title="Speedy Spots :: Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SpeedySpots.login" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <h2>Log in to your account</h2>
    <div class="message">
        <p>Don't have an account? <a href="user-registration.aspx">Sign-up</a>.</p>
    </div>
    <div class="form-holder">
        <fieldset>
            <legend>Login:</legend>
            <div class="group">
                <asp:Label ID="m_lblUsername" runat="server" AssociatedControlID="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your email address.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Email must be a valid email address up to 50 characters.">*</asp:RegularExpressionValidator>
                    <span class="required">Email Address:</span>
                </asp:Label>
                <asp:TextBox ID="m_txtUsername" runat="server" Columns="50" MaxLength="50" />
            </div>
            <div class="group hasnotes">
                <asp:Label ID="m_lblPassword" runat="server" AssociatedControlID="m_txtPassword">
                    <asp:RequiredFieldValidator ID="m_reqPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Please enter your password.">*</asp:RequiredFieldValidator>
                    <span class="required">Password:</span>
                </asp:Label>
                <div class="note-holder">
                    <asp:TextBox ID="m_txtPassword" runat="server" Columns="20" MaxLength="20" TextMode="Password" AutoCompleteType="Disabled" />
                    <p class="note"><a href="login-reset.aspx">Forgot your password?</a></p>
                </div>
            </div>
            <div class="group nospace">
                <label>&nbsp;</label>
                <div class="inputgroup">
                    <asp:CheckBox ID="m_chkRemember" runat="server" Text="Remember Me" />
                </div>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnLogin" runat="server" Text="Log In" CssClass="button primaryAction" OnClick="OnLogin" />
    </div>
</div>
</asp:Content>
