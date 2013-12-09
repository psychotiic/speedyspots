<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="SiteParams.aspx.cs" Inherits="SpeedySpots.InetActive.SiteParams" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<h2>Site Parameters</h2>
<div class="form-container">

    <fieldset>
        <legend>General Web Site Settings</legend>
        <div>
            <label>
                <asp:RequiredFieldValidator ID="m_reqSiteName" runat="server" ControlToValidate="m_txtSiteName" ErrorMessage="Please enter your site name.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expSiteName" runat="server" ControlToValidate="m_txtSiteName" ValidationExpression="^.{0,50}$" ErrorMessage="Site name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Site Name:
            </label>
            <telerik:RadTextBox ID="m_txtSiteName" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
        <div>
            <label>
                <asp:RequiredFieldValidator ID="m_reqHostName" runat="server" ControlToValidate="m_txtHostName" ErrorMessage="Please enter your host name.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expHostName" runat="server" ControlToValidate="m_txtHostName" ValidationExpression="^.{0,50}$" ErrorMessage="Host name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Host Name:
            </label>
            <telerik:RadTextBox ID="m_txtHostName" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
        <div>
            <label>
                <asp:RequiredFieldValidator ID="m_reqSiteDomain" runat="server" ControlToValidate="m_txtSiteDomain" ErrorMessage="Please enter your site domain.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expSiteDomain" runat="server" ControlToValidate="m_txtSiteDomain" ValidationExpression="^.{0,50}$" ErrorMessage="Site domain may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Site Domain:
            </label>
            <telerik:RadTextBox ID="m_txtSiteDomain" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
    </fieldset>

    <fieldset>
        <legend>Email SMTP Settings:</legend>
        <div>
            <label>SMTP Host:</label>
            <telerik:RadTextBox ID="m_txtSmtpHost" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
        <div>
            <label>SMTP Port:</label>
            <telerik:RadTextBox ID="m_txtSmtpPort" runat="server" MaxLength="5" Columns="5"></telerik:RadTextBox>
        </div>
        <div>
            <label>SMTP Username:</label>
            <telerik:RadTextBox ID="m_txtSmtpUsername" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
        <div>
            <label>SMTP Password:</label>
            <telerik:RadTextBox ID="m_txtSmtpPassword" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
    </fieldset>

    <fieldset>
        <legend>Email Settings:</legend>
        <div>
            <label>Email New Accounts:</label>
            <telerik:RadTextBox ID="m_txtEmailNewAccount" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
            <i>Separate multiple emails with semi-colons (;)</i>
        </div>
        <div>
            <label>Email Address From:</label>
            <telerik:RadTextBox ID="m_txtEmailAddressFrom" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
        <div>
            <label>Email Address From Name:</label>
            <telerik:RadTextBox ID="m_txtEmailAddressFromName" runat="server" MaxLength="50" Columns="50"></telerik:RadTextBox>
        </div>
    </fieldset>

    <fieldset>
        <legend>Authorize.NET settings:</legend>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Debug:" CssClass="required" AssociatedControlID="m_chkAuthorizeNetIsDebug"></asp:Label>
            <asp:CheckBox ID="m_chkAuthorizeNetIsDebug" runat="server" />
        </div>
        <div>
            <asp:Label ID="m_lblLoginID" runat="server" Text="Login ID:" CssClass="required" AssociatedControlID="m_txtAuthorizeNetLoginID"></asp:Label>
            <telerik:RadTextBox ID="m_txtAuthorizeNetLoginID" runat="server" MaxLength="50"></telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="m_txtAuthorizeNetLoginID" ErrorMessage="Please set your Authorize.NET Login ID.">*</asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:Label ID="m_lblTransactionKey" runat="server" Text="Transaction Key:" CssClass="required" AssociatedControlID="m_txtAuthorizeNetTransactionKey"></asp:Label><telerik:RadTextBox ID="m_txtAuthorizeNetTransactionKey" runat="server"></telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="m_txtAuthorizeNetTransactionKey" ErrorMessage="Please set your Authorize.NET Transaction Key.">*</asp:RequiredFieldValidator>
        </div>
    </fieldset>
    
    <div class="buttonrow">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="<span>Save changes</span>" cssclass="button positive" OnClick="OnSave" />
    </div>

</div>
</asp:Content>
