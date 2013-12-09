<%@ Page Title="Speedy Spots :: Message Inbox" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="messages-inbox.aspx.cs" Inherits="SpeedySpots.messages_inbox" %>
<%@ Register Src="~/Controls/Tabs/Customer-Tabs.ascx" TagName="CustomerTabs" TagPrefix="SS" %>
<%@ Register Src="~/Controls/Tabs/Talent-Tabs.ascx" TagName="TalentTabs" TagPrefix="SS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<h2>Speedy Spots Messaging System</h2>

<% if(ApplicationContext.IsCustomer) %>
<% { %>
   <SS:CustomerTabs ID="m_oTabs" runat="server" SelectedTab="Messages" />
<% } %>

<% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
<% { %>
<div class="tab-holder group">
    <ul id="dashboard-tabs">
        <li><a href="staff-dashboard.aspx">Requests</a></li>
        <li><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
        <li><a href="staff-dashboard-completed.aspx">Completed</a></li>
        <li><a href="admin-dashboard.aspx">Admin</a></li>
        <li class="at"><a href="messages-inbox.aspx">Messages</a></li>        
    </ul>
</div>
<% } %>

<% if(ApplicationContext.IsTalent) %>
<% { %>
<SS:TalentTabs id="ucTalentTabs" SelectedTab="Messages" runat="server" />
<% } %>

<% if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff) %>
<% { %>
<div class="button-row action">
    <a href="messages-send.aspx" class="button primaryAction">New Message</a>
</div>
<% } %>

 <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
    <MasterTableView NoMasterRecordsText="No Messages"></MasterTableView>
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
    <telerik:GridSortExpression FieldName="CreatedDateTime" SortOrder="Ascending" />
    </SortExpressions>
        <Columns>
            <telerik:GridBoundColumn DataField="IAMessageID" UniqueName="IAMessageID" Visible="False"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="DisplayStartDateTime" UniqueName="DisplayStartDateTime" Visible="False"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="DisplayEndDateTime" UniqueName="DisplayEndDateTime" Visible="False"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" UniqueName="Subject"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Sender" HeaderText="From" UniqueName="Sender"></telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="DateRanges" HeaderText="Date Ranges" UniqueName="DateRanges" SortExpression="DisplayStartDateTime"></telerik:GridBoundColumn>
            <telerik:GridButtonColumn Text="Delete" UniqueName="Delete" ConfirmText="Are you sure you want to delete this message?" CommandName="Delete"></telerik:GridButtonColumn>
        </Columns>
    </MasterTableView>

    <FilterMenu EnableTheming="True">
    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    </FilterMenu>
</telerik:RadGrid>

</asp:Content>
