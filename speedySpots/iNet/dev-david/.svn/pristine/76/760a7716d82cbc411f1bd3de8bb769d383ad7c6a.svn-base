<%@ Page Title="Speedy Spots :: Modify Label" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="label-modify.aspx.cs" Inherits="SpeedySpots.label_modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<p class="breadcrumb"><a href="admin-dashboard.aspx">Dashboard</a> &raquo; <a href="labels.aspx">Labels</a> &raquo; <strong>Modify Label</strong></p>
<h2>Modify Label</h2>
<div class="form-holder">
    <fieldset>
        <legend>Label Information:</legend>
        <div class="group">
            <asp:Label ID="m_lblText" runat="server" AssociatedControlID="m_txtText">
                <asp:RequiredFieldValidator ID="m_reqText" runat="server" ControlToValidate="m_txtText" ErrorMessage="Please enter your label text.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expText" runat="server" ControlToValidate="m_txtText" ValidationExpression="^.{0,50}$" ErrorMessage="Label text may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                <span class="required">Label Text:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtText" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
        </div>
        <div class="group">
            <asp:Label ID="m_lblIsCustomerVisisble" runat="server" AssociatedControlID="m_chkIsCustomerVisible">Customer Visible:</asp:Label>
            <asp:CheckBox ID="m_chkIsCustomerVisible" runat="server" />
        </div>
    </fieldset>
</div>

<div class="button-row">
    <% if(IsNew || (!IsNew && ApplicationContext.IsAdmin && ApplicationContext.IsStaff)) %>
    <% { %>
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Save Changes" CssClass="button primaryAction" OnClick="OnSave" />
    <% } %>
    <asp:LinkButton ID="m_btnBack" runat="server" Text="Cancel Changes" CssClass="button" CausesValidation="false" OnClick="OnBack" />
    <% if(!IsNew && ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
    <% { %>
    <asp:LinkButton ID="m_btnDelete" runat="server" Text="Delete Label" CssClass="button negativeAction" OnClientClick="return ConfirmUser('Deleting this label will remove it from any requests currently using it, are you sure you want to delete this label?');" OnClick="OnDelete" />
    <% } %>
</div>
</asp:Content>
