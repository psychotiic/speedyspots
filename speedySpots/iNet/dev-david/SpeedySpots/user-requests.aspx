<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-requests.aspx.cs" Inherits="SpeedySpots.user_requests" %>
<%@ Register Src="~/Controls/Tabs/Customer-Tabs.ascx" TagName="CustomerTabs" TagPrefix="SS" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
h2.nomargin 
{
	margin-bottom: 0;
	padding-bottom: 0;
	line-height: 1em;
}

p.company-name 
{
	margin-top: 0;
	padding-top: 0;
	font-size: 1em;
	font-weight: bold;
	color: #666;
}

thead .rgExpandCol,
tbody .expandcol 
{
    display: none;
}

thead a.headerButton
{
    padding: 0 5px;
	background: #c4c4c4 url("img/glass.png") repeat-x 0 50%;
	border: 1px solid #9e9e9e;
	-border-radius: 3px;
	-moz-border-radius: 3px;
	-webkit-border-radius: 3px;
    max-height: 20px;
    min-width: 20px;
    color: #4e4e4e !important;
    text-shadow: 0 1px 1px #fff;
    box-shadow: 0 1px 1px #ccc;
}

thead a.headerButton:hover {
	color: #fff !important;
	background-color: #757575;
	cursor: pointer;
	border-color: #666;
	text-shadow: 0 -1px 1px #333;
}
</style>
<script language="javascript" type="text/javascript">
function StopPropagation(e)
{
    // Cancel event bubbling
    e.cancelBubble = true;
    if (e.stopPropagation)
    {
        e.stopPropagation();
    }
}
function OnCheckFilter(chkStatus, sParentID) {
    var sID = "<%=m_cboRequestCoworker.ClientID %>";
    
    var m_cboFilter = $find(sID);

    var sText = "";
    var sValues = "";
    var oItems = m_cboFilter.get_items();

    for (var i = 0; i < oItems.get_count(); i++) {
        var oItem = oItems.getItem(i);

        //get the checkbox element of the current item
        var m_chkStatus = $("#" + m_cboFilter.get_id() + "_i" + i + "_m_chkStatus");
        if (m_chkStatus.is(':checked')) {
            var someParent = m_chkStatus.parent();
            var userID = someParent.attr("data-userid");
            
            sText += oItem.get_text() + ",";
            sValues += userID + ",";
        }
    }

    sText = RemoveLastComma(sText);
    sValues = RemoveLastComma(sValues);

    if (sText.length > 0) {
        m_cboFilter.set_text(sText);
        m_cboFilter.set_value(sValues);
    }
    else {
        m_cboFilter.set_text("");
        m_cboFilter.set_value("");
    }
}

function RemoveLastComma(sValue) {
    return sValue.replace(/,$/, "");
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="full">
    <h2 class="nomargin"><%=string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")) %> Dashboard</h2>
    <p class="company-name"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(MemberProtect.CurrentUser.UserID)))%></p>

    <SS:CustomerTabs ID="m_oTabs" runat="server" SelectedTab="RequestsAll" />

    <div class="group">
        <div class="order-list">
            <div class="button-row action">
                <a href="create-request.aspx" class="button primaryAction"><i class="icon-plus"></i> Create New Request</a>
                <a href="javascript:popUp('music.aspx');" class="button"><i class="icon-music"></i> Open Music Library</a>
            </div>
        </div>
    </div>

    <div class="form-holder filter">
        <fieldset>
            <legend>Filter Requests:</legend>
            <div class="group">
                <asp:Label ID="m_lblCreatedDate" runat="server" Text="Request Date From:" AssociatedControlID="m_dtCreatedDateStart"></asp:Label>
                <div class="inputgroup">
                    <telerik:RadDatePicker runat="server" ID="m_dtCreatedDateStart"></telerik:RadDatePicker>
                    <asp:Label ID="Label1" runat="server" Text="To:" AssociatedControlID="m_dtCreatedDateEnd"></asp:Label>
                    <telerik:RadDatePicker runat="server" ID="m_dtCreatedDateEnd"></telerik:RadDatePicker>
                </div>
            </div>
            <div class="group">
                <asp:Label ID="m_lblJobTitle" runat="server" Text="Job:" AssociatedControlID="m_txtJobTitle"></asp:Label>
                <telerik:RadTextBox ID="m_txtJobTitle" EmptyMessage="Request #, job title, etc" Columns="50" runat="server"></telerik:RadTextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblCoworker" runat="server" Text="Coworkers:" AssociatedControlID="m_cboRequestCoworker"></asp:Label>
                <telerik:RadComboBox ID="m_cboRequestCoworker" runat="server" OnItemDataBound="cboRequestCoworkerItemDataBind" AllowCustomText="true" HighlightTemplatedItems="true">
                    <ItemTemplate>
                        <div onclick="StopPropagation(event)">
                            <asp:CheckBox ID="m_chkStatus" runat="server" onClick="OnCheckFilter(this, 'Coworkers');" />
                            <asp:Label ID="m_lblLabel" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Name") %></asp:Label>
                        </div>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
        </fieldset>
        <div class="button-row action">
            <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter Requests" CssClass="button primaryAction" OnClick="OnFilterRequests" />
        </div>
    </div>

    <h3>All Requests:</h3>
    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" OnSortCommand="OnSortCommandRequests" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false" CssClass="buttoned">
        <MasterTableView Name="Master" NoMasterRecordsText="No Requests" DataKeyNames="IARequestID" HierarchyDefaultExpanded="false">
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>

            <SortExpressions>
                <telerik:GridSortExpression FieldName="CreatedDateTime" SortOrder="Descending" />
            </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IARequestID" UniqueName="IARequestID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn DataTextField="Name" HeaderText="Job Title" UniqueName="Title" SortExpression="Name" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/order-details.aspx?s=a&rid={0}"></telerik:GridHyperLinkColumn>
                <telerik:GridHyperLinkColumn  DataTextField="IARequestID" HeaderText="Req. No." UniqueName="RequestNumber" SortExpression="IARequestID" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/order-details.aspx?s=a&rid={0}"></telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn HeaderText="Expected Delivery" UniqueName="ExpectedDelivery"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Labels" UniqueName="Labels"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="By" UniqueName="TemplateColumn" SortExpression="FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FirstName") %> <%# DataBinder.Eval(Container.DataItem, "LastName") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="CreatedDateTime" DataFormatString="{0:MM/dd/yyyy a\t h:mm tt}" HeaderText="Requested" UniqueName="CreatedDateTime"></telerik:GridBoundColumn>                
                <telerik:GridBoundColumn DataField="FirstName" UniqueName="FirstName" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastName" UniqueName="LastName" Visible="false"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</div>
</asp:Content>