<%@ Page Title="" Language="C#" MasterPageFile="~/InetActive/InetActive.Master" AutoEventWireup="true" CodeBehind="OrganizationList.aspx.cs" Inherits="SpeedySpots.InetActive.OrganizationList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<telerik:RadAjaxManager ID="m_oAjaxManager" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="m_btnFilter">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="m_lblCount" />
                <telerik:AjaxUpdatedControl ControlID="m_spanFilter" />
                <telerik:AjaxUpdatedControl ControlID="m_grdList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<h2>Organizations</h2>
<div class="form-container">
    <div class="buttonrow">
        <a href="OrganizationEdit.aspx" class="button positive"><span>Add New Organization</span></a>
    </div>
    <div class="clear">&nbsp;</div>
    
    <div class="list-container">
    <fieldset>
        <legend>Filter:</legend>
        <div>
            <label>Name:</label>
            <telerik:RadTextBox ID="m_txtName" runat="server"></telerik:RadTextBox>
        </div>
        <div class="smbuttonrow">
            <asp:LinkButton ID="m_btnFilter" runat="server" Text="<span>Filter Organizations</span>" CssClass="button" ValidationGroup="Filter" onclick="OnFilter" />
        </div>
    </fieldset>
    </div>
    
    <h3><asp:Label ID="m_lblCount" runat="server"></asp:Label><span id="m_spanFilter" runat="server">(<a href="OrganizationList.aspx">Remove Filter</a>)</span></h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced">
        <HeaderContextMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </HeaderContextMenu>

        <MasterTableView>
        <RowIndicatorColumn>
        <HeaderStyle Width="20px"></HeaderStyle>
        </RowIndicatorColumn>

        <ExpandCollapseColumn>
        <HeaderStyle Width="20px"></HeaderStyle>
        </ExpandCollapseColumn>
        <SortExpressions>
        <telerik:GridSortExpression FieldName="Name" SortOrder="Ascending" />
        </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="MPOrgID" UniqueName="MPOrgID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridButtonColumn DataTextField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name" CommandName="View"></telerik:GridButtonColumn>
            </Columns>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</div>
</asp:Content>
