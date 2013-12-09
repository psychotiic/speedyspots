<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-upgrade.aspx.cs" Inherits="SpeedySpots.user_upgrade" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <h2>Upgrade your account</h2>
    <div class="message">Welcome to the new SpeedySpots site. We're excited to introduce you to our new system for submitting, receiving and paying for spots. Please fill out the details below to upgrade your account.</div>
    <div class="form-holder">
        <fieldset>
            <legend>Login:</legend>
            <div class="group">
                <asp:Label ID="m_lblUsername" runat="server" AssociatedControlID="m_txtUsername">
                    <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your user ID.">*</asp:RequiredFieldValidator>
                    <span class="required">User ID:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtUsername" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
            <div class="group hasnotes">
                <asp:Label ID="m_lblPassword" runat="server" AssociatedControlID="m_txtPassword">
                    <asp:RequiredFieldValidator ID="m_reqPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Please enter your password.">*</asp:RequiredFieldValidator>
                    <span class="required">Password:</span>
                </asp:Label>
                <div class="note-holder">
                    <telerik:RadTextBox ID="m_txtPassword" runat="server" Columns="50" MaxLength="50" TextMode="Password" AutoCompleteType="Disabled"></telerik:RadTextBox>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnLogin" runat="server" Text="Sign In" CssClass="button primaryAction" OnClick="OnLogin" />
    </div>
</div>
</asp:Content>
