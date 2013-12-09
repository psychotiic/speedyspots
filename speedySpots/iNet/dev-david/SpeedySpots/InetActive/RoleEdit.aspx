<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="SpeedySpots.InetActive.RoleEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
    <h2>Edit Role</h2>
    <div class="form-container">

    <fieldset>
        <legend>Role Information</legend>
        <div>
            <label for="m_txtName">
                <asp:RequiredFieldValidator ID="m_reqName" runat="server" ControlToValidate="m_txtName" ErrorMessage="Please enter the role name.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expName" runat="server" ControlToValidate="m_txtName" ValidationExpression="^.{0,50}$" ErrorMessage="Role name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                Name:
            </label>
            <telerik:RadTextBox id="m_txtName" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
        </div>
        <div>
            <label for="m_txtDescription">
                <asp:RequiredFieldValidator ID="m_reqDescription" runat="server" ControlToValidate="m_txtDescription" ErrorMessage="Description is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="m_expDescription" runat="server" ControlToValidate="m_txtDescription" ValidationExpression="^.{0,255}$" ErrorMessage="Description may contain up to 255 characters.">*</asp:RegularExpressionValidator>
                Description:
            </label>
            <telerik:RadTextBox id="m_txtDescription" runat="server" MaxLength="50" Columns="55"></telerik:RadTextBox>
        </div>
    </fieldset>

    <fieldset>
        <legend>Privilege Assignments</legend>
        <telerik:RadTreeView ID="m_treePrivileges" runat="server" Width="300px" Height="250px" CheckBoxes="true" OnDataBound="OnPrivilegesDataBound">
        </telerik:RadTreeView>
    </fieldset>

    <div class="buttonrow">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="<span>Save Role</span>" 
            CssClass="button positive" onclick="OnSave" />
        <a href="RoleList.aspx" class="button"><span>Back</span></a>
        <asp:LinkButton ID="m_btnDelete" runat="server" Text="<span>Delete Role</span>" CssClass="button negative" CausesValidation="False" onclick="OnDelete" OnClientClick="return ConfirmUser('Are you sure you want to delete this role?');" />
    </div>
    </div>
</asp:Content>
