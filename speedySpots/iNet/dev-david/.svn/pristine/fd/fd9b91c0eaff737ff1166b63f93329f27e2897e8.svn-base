<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="SpeedySpots.InetActive.UserEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oContent" runat="server">

<h2>Edit User</h2>
<div class="form-container">

<fieldset>
    <legend>User ID Information</legend>
        <div>
        <label for="m_txtUsername">
            <asp:RequiredFieldValidator ID="m_reqUsername" runat="server" ControlToValidate="m_txtUsername" ErrorMessage="Please enter your username.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="m_expUsername" runat="server" ControlToValidate="m_txtUsername" ValidationExpression="^.{0,20}$" ErrorMessage="Username may contain up to 20 characters.">*</asp:RegularExpressionValidator>
            Username:
        </label>
        <telerik:RadTextBox id="m_txtUsername" runat="server" MaxLength="20" Columns="25"></telerik:RadTextBox>
    </div>
    <div>
        <label for="m_txtPassword">
            <asp:RequiredFieldValidator ID="m_reqPassword" runat="server" ControlToValidate="m_txtPassword" ErrorMessage="Please enter your password.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="m_expPassword" runat="server" ControlToValidate="m_txtPassword" ValidationExpression="^.{6,15}$" ErrorMessage="Password must contain between 6 and 15 characters">*</asp:RegularExpressionValidator>
            Password:
        </label>
        <telerik:RadTextBox id="m_txtPassword" runat="server" MaxLength="15" Columns="20"></telerik:RadTextBox>
    </div>
</fieldset>

<fieldset>
    <legend>User Information:</legend>
    <div>
        <label for="m_txtFirstName">
            <asp:RequiredFieldValidator ID="m_reqFirstName" runat="server" ControlToValidate="m_txtFirstName" ErrorMessage="Please enter your first name.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="m_expFirstName" runat="server" ControlToValidate="m_txtFirstName" ValidationExpression="^.{0,25}$" ErrorMessage="First name may contain up to 25 characters">*</asp:RegularExpressionValidator>
            First Name:
        </label>
        <telerik:RadTextBox id="m_txtFirstName" runat="server" MaxLength="25" Columns="30"></telerik:RadTextBox>
    </div>
    <div>
        <label for="m_txtLastName">
            <asp:RequiredFieldValidator ID="m_reqLastName" runat="server" ControlToValidate="m_txtLastName" ErrorMessage="Please enter your last name.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="m_expLastName" runat="server" ControlToValidate="m_txtLastName" ValidationExpression="^.{0,25}$" ErrorMessage="Last name may contain up to 25 characters">*</asp:RegularExpressionValidator>
            Last Name:
        </label>
        <telerik:RadTextBox id="m_txtLastName" runat="server" MaxLength="25" Columns="30"></telerik:RadTextBox>
    </div>
    <div>
        <label for="m_txtEmail">
            <asp:RequiredFieldValidator ID="m_reqEmail" runat="server" ControlToValidate="m_txtEmail" ErrorMessage="Please enter your email.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="m_expEmail" runat="server" ControlToValidate="m_txtEmail" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Email must be a valid email address.">*</asp:RegularExpressionValidator>
            Email:
        </label>
        <telerik:RadTextBox id="m_txtEmail" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
    </div>
</fieldset>

<fieldset>
    <legend>Security Settings:</legend>
    <div class="controlset">
        <span class="label">Locked Out:</span>
        <asp:CheckBox ID="m_chkLockedOut" runat="server" Text="User is Locked Out" />
        <p class="note">Disable the user but do not remove their ID</p>
    </div>
    <div class="controlset">
        <span class="label">Assigned Roles:</span>
        <asp:CheckBoxList ID="m_chkRoles" runat="server" OnDataBound="OnRolesDataBound">
        </asp:CheckBoxList>
    </div>
</fieldset>

<fieldset id="m_oAuditInformation" runat="server">
    <legend>Audit Information</legend>
    <div class="controlset">
        <span class="label">Created On:</span>
        <asp:Label ID="m_lblCreatedOn" runat="server"></asp:Label>
    </div>
    <div class="controlset">
        <span class="label">Failed Login Attempts:</span>
        <asp:Label ID="m_lblFailedLoginAttempts" runat="server"></asp:Label>
    </div>
    <div class="controlset">
        <span class="label">Last IP Address:</span>
        <asp:Label ID="m_lblLastIPAddress" runat="server"></asp:Label>
    </div>
    <div class="controlset">
        <span class="label">Last Login Date:</span>
        <asp:Label ID="m_lblLastLoginDate" runat="server"></asp:Label>
    </div>
    <div class="controlset">
        <span class="label">Last Failed Login Date:</span>
        <asp:Label ID="m_lblLastFailedLoginDate" runat="server"></asp:Label>
    </div>
    <div class="controlset">
        <span class="label">Password Changed On:</span>
        <asp:Label ID="m_lblPasswordChangedOn" runat="server"></asp:Label>
    </div>
</fieldset>

<div class="buttonrow">
    <asp:LinkButton ID="m_btnSave" runat="server" Text="<span>Save User</span>" 
        CssClass="button positive" onclick="OnSave" />
    <a href="UserList.aspx" class="button"><span>Back</span></a>
    <asp:LinkButton ID="m_btnDelete" runat="server" Text="<span>Delete User</span>" CssClass="button negative" CausesValidation="False" onclick="OnDelete" OnClientClick="return ConfirmUser('Are you sure you want to delete this user?');" />
</div>
</div>
</asp:Content>
