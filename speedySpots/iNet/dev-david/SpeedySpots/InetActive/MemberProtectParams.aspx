<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="MemberProtectParams.aspx.cs" Inherits="SpeedySpots.InetActive.MemberProtectParams" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<h2>MemberProtect Parameters</h2>
<div class="form-container">

    <fieldset>
        <legend>Settings</legend>
        <div>
            <label>Passwords Expire (days):</label>
            <telerik:RadNumericTextBox ID="m_txtTimespanBeforePasswordsExpire" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of days from when a password is changed before the password will expire again.</i>
        </div>
        <div>
            <label>Password Email is Sent (days):</label>
            <telerik:RadNumericTextBox ID="m_txtTimespanBeforePasswordsExpireToNotifyUsers" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of days before password expiration at which point the user will be notified of their upcoming password expiration.</i>
        </div>
        <div>
            <label>Temp. Passwords Expire (days):</label>
            <telerik:RadNumericTextBox ID="m_txtTimespanBeforeTempPasswordsExpire" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of days from when a temporary password is issued before the password will no longer work.</i>
        </div>
        <div>
            <label>Account Locked (days):</label>
            <telerik:RadNumericTextBox ID="m_txtTimespanSinceLastLoginBeforeLockout" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of days of account inactivity since their last login before the user is locked out.</i>
        </div>
        <div>
            <label>Keep MemberProtect Logs (days):</label>
            <telerik:RadNumericTextBox ID="m_txtTimespanToKeepLogs" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of days to keep MemberProtect logs, deleting records older than set days.</i>
        </div>
        <div>
            <label>Password Tracking (total):</label>
            <telerik:RadNumericTextBox ID="m_txtMaxPasswordHistoryCount" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The amount of passwords to remember per users for purposes of tracking historical password changes. This is generally used to ensure when users change their
            password, they aren't re-using a recently used password again.</i>
        </div>
        <div>
            <label>Failed Login Attempts (total):</label>
            <telerik:RadNumericTextBox ID="m_txtMaxFailedLoginAttemptsBeforeLockout" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of failed login attemps before a user's account is locked and cannot be accessed until it is unlocked by an administrator.</i>
        </div>
        <div>
            <label>RBA Client System Tracking (total):</label>
            <telerik:RadNumericTextBox ID="m_txtRBASystemTrackCount" runat="server" MaxLength="2">
                <NumberFormat AllowRounding="false" DecimalDigits="0" />
            </telerik:RadNumericTextBox>
            <i>The number of client systems to track per user with respect to MemberProtect's Risk Based Authentication functionality.</i>
        </div>
    </fieldset>
    
    <div class="buttonrow">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="<span>Save changes</span>" cssclass="button positive" OnClick="OnSave" />
    </div>

</div>
</asp:Content>
