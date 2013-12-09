<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login-change-password.aspx.cs" Inherits="SpeedySpots.login_change_password" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div id="content">
    <div class="form-holder">
        <fieldset>
            <legend>Change password:</legend>
            <p>To complete your forgotten password reset please enter a new password below.</p>
            <div class="group">
                <asp:Label ID="m_lblPassword" runat="server" AssociatedControlID="m_txtPassword" CssClass="required">
                    <asp:RequiredFieldValidator ID="m_reqPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Please enter your password.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="m_expPassword" runat="server" ControlToValidate="m_txtPassword" ValidationExpression="^.{6,10}$" ErrorMessage="Password must contain between 6 and 10 characters">*</asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="m_cmpPassword" runat="server" ControlToValidate="m_txtVerifyPassword" ControlToCompare="m_txtPassword" ErrorMessage="Password and Verify Password must be the same value.">*</asp:CompareValidator>
                    New Password:
                </asp:Label>
                <div class="note-holder">
                    <telerik:RadTextBox ID="m_txtPassword" runat="server" Columns="10" MaxLength="10" TextMode="Password" AutoCompleteType="Disabled"></telerik:RadTextBox>
                    <p class="note">Create a password between 6 and 10 characters long.</p>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblVerifyPassword" runat="server" Text="Verify Password:" AssociatedControlID="m_txtVerifyPassword" CssClass="required"></asp:Label>
                <telerik:RadTextBox ID="m_txtVerifyPassword" runat="server" Columns="10" MaxLength="10" TextMode="Password" AutoCompleteType="Disabled"></telerik:RadTextBox>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnChangePassword" runat="server" Text="Change Password" CssClass="button primaryAction" OnClick="OnChangePassword" />
    </div>
</div>
</asp:Content>
