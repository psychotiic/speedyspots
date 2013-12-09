<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SpeedySpots.InetActive.Login" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
    <h2>Login</h2>
    <div class="form-container">
        <fieldset>
            <legend>Login to InetActive</legend>
            <div>
                <label for="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your username.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^.{0,50}$" ErrorMessage="Username may be up to 50 characters.">*</asp:RegularExpressionValidator>
                    Username
                </label>
                <telerik:RadTextBox id="m_txtUsername" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
            </div>
            <div>
                <label for="">
                    <asp:RequiredFieldValidator ID="m_reqPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Please enter your password.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Password may contain up to 15 characters." ValidationExpression="^.{0,15}$">*</asp:RegularExpressionValidator>
                    Password
                </label>
                <telerik:RadTextBox ID="m_txtPassword" runat="server" TextMode="Password" MaxLength="15" Columns="20" AutoCompleteType="Disabled"></telerik:RadTextBox>
            </div>
            <div class="controlset">
                <span class="label">Remember Me</span>
                <asp:CheckBox ID="m_chkRememberMe" runat="server" />
            </div>
            <div class="buttonrow">
                <asp:Panel runat="server" DefaultButton="m_btnLogin">
                    <asp:LinkButton ID="m_btnLogin" runat="server" Text="<span>Login</span>" CssClass="button positive" OnClick="OnLogin" />
                </asp:Panel>
            </div>
        </fieldset>
    </div>
    <div id="m_divNews" runat="server"></div>
</asp:Content>
