<%@ Page Title="Speedy Spots :: Email Templates Modify" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="email-templates-modify.aspx.cs" Inherits="SpeedySpots.email_templates_modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<p class="breadcrumb"><a href="admin-dashboard.aspx">Dashboard</a> &raquo; <a href="site-settings.aspx">Site Settings</a> &raquo; <strong>Modify Email Template</strong></p>
<h2>Modify Email Template</h2>
<div class="form-holder">
    <fieldset>
        <div class="group">
            <asp:Label ID="m_lblCompanyName" runat="server" AssociatedControlID="m_txtName">
                <asp:RequiredFieldValidator ID="m_reqEmailTemplateName" runat="server" ControlToValidate="m_txtName" ErrorMessage="Please enter the template name.">*</asp:RequiredFieldValidator>
                <span class="required">Template Name:</span>
            </asp:Label>
            <telerik:RadTextBox ID="m_txtName" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
        </div>
        <div class="group">
            <label><span class="required">Estimate Body:</span></label>
            <div class="inputgroup">
                <telerik:RadEditor ID="m_txtBody" runat="server" Width="621px" EditModes="Design">
                    <Tools>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="ToggleScreenMode" />
                            <telerik:EditorTool Name="PasteStrip" />
                        </telerik:EditorToolGroup>
                    </Tools>
                </telerik:RadEditor>
            </div>
        </div>
    </fieldset>
</div>

<div class="button-row">
    <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Save Changes" CssClass="button primaryAction" OnClick="OnSave" />
    <asp:LinkButton ID="m_btnBack" runat="server" Text="Cancel Changes" CssClass="button" CausesValidation="false" OnClick="OnBack" />
    <% if(!IsNew) %>
    <% { %>
    <asp:LinkButton ID="m_btnDelete" runat="server" Text="Delete Email Template" CssClass="button negativeAction" OnClientClick="return ConfirmUser('Are you sure you want to delete this email template?');" OnClick="OnDelete" />
    <% } %>
</div>
</asp:Content>
