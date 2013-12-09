<%@ Page Title="Speedy Spots :: My Credit Cards" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="user-creditcards.aspx.cs" Inherits="SpeedySpots.user_creditcards" %>

<%@ Register Src="~/Controls/Tabs/Customer-Tabs.ascx" TagName="CustomerTabs" TagPrefix="SS" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
   <div class="full">
      <p class="breadcrumb">      
      <% if (ApplicationContext.IsAdmin && ApplicationContext.IsStaff) %>
      <% { %>
         <a href="admin-dashboard.aspx">Dashboard</a> &raquo; <a href="admin-dashboard.aspx">Users</a> &raquo;
      <% } %>
      <% else if (ApplicationContext.IsCustomer) %>
      <% { %>
         <a href="user-dashboard.aspx">Dashboard</a> &raquo;
      <% } %>
      <% else if (ApplicationContext.IsStaff) %>
      <% { %>
         <a href="staff-dashboard.aspx">Dashboard</a> &raquo;
      <% } %>
      <% else if (ApplicationContext.IsTalent) %>
      <% { %>
         <a href="talent-dashboard.aspx">Dashboard</a> &raquo;
      <% } %>
      My Credit Cards
      </p>
      <h2>Credit Cards for
         <% if (MPUserID == Guid.Empty) %>
         <% { %>
         New User
         <% } %>
         <% else %>
         <% { %>
         <%=string.Format("{0} {1}", MemberProtect.User.GetDataItem(MPUserID, "FirstName"), MemberProtect.User.GetDataItem(MPUserID, "LastName"))%>
         <% } %>
      </h2>

      <% if (ApplicationContext.IsCustomer) %>
      <% { %>
      <SS:CustomerTabs ID="m_oTabs" runat="server" SelectedTab="CreditCards" />
      <% } %>

      <div class="group">
        <div class="order-list">
            <div class="button-row action">
                <a href="user-edit-creditcard.aspx" class="button primaryAction"><i class="icon-plus"></i> Add New Credit Card</a>
            </div>
        </div>
      </div>

      <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
         GridLines="None" OnNeedDataSource="OnNeedDataSource" OnItemDataBound="OnItemDataBound"
         AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false"
         CssClass="buttoned">
         <MasterTableView NoMasterRecordsText="No Stored Credit Cards" DataKeyNames="CreditCardId"></MasterTableView>

         <HeaderContextMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
         </HeaderContextMenu>

         <MasterTableView Name="Master" HierarchyDefaultExpanded="false">
            <RowIndicatorColumn>
               <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>

            <ExpandCollapseColumn>
               <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <SortExpressions>
               <telerik:GridSortExpression FieldName="Alias" SortOrder="Descending" />
            </SortExpressions>
            <Columns>
               <telerik:GridBoundColumn DataField="MPUserId" UniqueName="MPUserId" Visible="False"></telerik:GridBoundColumn>
               <telerik:GridBoundColumn DataField="CreditCardId" UniqueName="CreditCardId" Visible="False"></telerik:GridBoundColumn>
               <telerik:GridHyperLinkColumn DataTextField="Alias" HeaderText="Card" UniqueName="Alias" SortExpression="Alias" DataNavigateUrlFields="CreditCardId" DataNavigateUrlFormatString="~/user-edit-creditcard.aspx?id={0}"></telerik:GridHyperLinkColumn>
               <telerik:GridBoundColumn DataField="ExpirationDate" HeaderText="Expiration" UniqueName="ExpirationDate"></telerik:GridBoundColumn>
               <telerik:GridBoundColumn DataField="ReceiptEmailAddressCsv" HeaderText="Payment Receipt" UniqueName="ReceiptEmailAddressCsv"></telerik:GridBoundColumn>
            </Columns>
         </MasterTableView>

         <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
         </FilterMenu>
      </telerik:RadGrid>
   </div>
</asp:Content>
