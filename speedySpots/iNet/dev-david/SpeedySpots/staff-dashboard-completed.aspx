<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="staff-dashboard-completed.aspx.cs" Inherits="SpeedySpots.staff_dashboard_completed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
thead .rgExpandCol,
tbody .expandcol 
{
    display: none;
}
</style>
<style type="text/css">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<script language="javascript" type="text/javascript">
function StopPropagation(e)
{
    // Cancel event bubbling
    e.cancelBubble = true;
    if(e.stopPropagation)
    {
        e.stopPropagation();
    }
}

function OnCheckFilter(chkStatus, sParentID)
{
    var sID = "";
    if(sParentID == "Request")
    {
        sID = "<%=m_cboRequestStatus.ClientID %>";
    }
    else if(sParentID == "Languages")
    {
        sID = "<%=m_cboRequestLanguage.ClientID %>";
    }

    var m_cboFilter = $find(sID);

    var sText = "";
    var sValues = "";
    var oItems = m_cboFilter.get_items();

    for(var i = 0; i < oItems.get_count(); i++)
    {
        var oItem = oItems.getItem(i);

        //get the checkbox element of the current item
        var m_chkStatus = $get(m_cboFilter.get_id() + "_i" + i + "_m_chkStatus");
        if(m_chkStatus.checked)
        {
            sText += oItem.get_text() + ",";
            sValues += oItem.get_value() + ",";
        }
    }

    sText = RemoveLastComma(sText);
    sValues = RemoveLastComma(sValues);

    if(sText.length > 0)
    {
        m_cboFilter.set_text(sText);
        m_cboFilter.set_value(sValues);
    }
    else
    {
        m_cboFilter.set_text("");
        m_cboFilter.set_value("");
    }
}

function RemoveLastComma(sValue)
{
    return sValue.replace(/,$/, "");
}
</script>

<div class="full">
    <div id="producer_qc_switch">
        <strong>Working as:</strong> <a href="staff-dashboard.aspx" class="at">Staff</a> | <a href="admin-dashboard.aspx">Admin</a>
    </div>

    <h2><a href="user-account.aspx"><%=string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName")) %></a> Dashboard</h2>
    <div class="group">
        <div class="order-list">
            <div class="tab-holder group">
                <ul id="dashboard-tabs">
                    <li><a href="staff-dashboard.aspx">Requests</a></li>
                    <li><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
                    <li class="at"><a href="staff-dashboard-completed.aspx">Completed</a></li>
                    <li><a href="admin-dashboard.aspx">Admin</a></li>
                    <li><a href="messages-inbox.aspx">Messages</a></li>
                </ul>
            </div>
            
            <div class="button-row action">
                <a href="create-request.aspx" class="button primaryAction">Create Request For Customer</a>
            </div>

            <div id="m_divRequests" runat="server">
                <div class="form-holder filter">
                    <asp:Panel ID="pnlRequestFilters" runat="server" DefaultButton="m_btnFilter">
                        <fieldset>
                            <legend>Filter Jobs:</legend>
                            <div class="group">
                                <asp:Label ID="m_lblCreatedDate" runat="server" Text="Request Date:" AssociatedControlID="m_dtCreatedStartDate"></asp:Label>
                                <telerik:RadDatePicker runat="server" ID="m_dtCreatedStartDate"></telerik:RadDatePicker> to <telerik:RadDatePicker runat="server" ID="m_dtCreatedEndDate"></telerik:RadDatePicker>
                            </div>
                            <div class="group">
                                <asp:Label ID="m_lblCompletedDate" runat="server" Text="Completed Date:" AssociatedControlID="m_dtCompletedStartDate"></asp:Label>
                                <telerik:RadDatePicker runat="server" ID="m_dtCompletedStartDate"></telerik:RadDatePicker> to <telerik:RadDatePicker runat="server" ID="m_dtCompletedEndDate"></telerik:RadDatePicker>
                            </div>
                            <div class="group">
                                <asp:Label ID="m_lblJobName" runat="server" Text="Job Name:" AssociatedControlID="m_txtJobName"></asp:Label>
                                <div class="inputgroup">                               
                                    <telerik:RadTextBox ID="m_txtJobName" EmptyMessage="job title, company name, request by name, requested by email" runat="server" Columns="50"></telerik:RadTextBox>
                                    <asp:Label ID="m_lblLanguage" runat="server" Text="Language:" AssociatedControlID="m_cboRequestLanguage"></asp:Label>
                                    <telerik:RadComboBox ID="m_cboRequestLanguage" runat="server" AllowCustomText="true" HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)">
                                                <asp:CheckBox ID="m_chkStatus" runat="server" onclick="OnCheckFilter(this, 'Languages')" />
                                                <asp:Label ID="m_lblLabel" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Value") %></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="group">
                                <asp:Label ID="m_lblStatus" runat="server" Text="Status:" AssociatedControlID="m_cboRequestStatus"></asp:Label>
                                <div class="inputgroup">   
                                    <telerik:RadComboBox ID="m_cboRequestStatus" runat="server" Width="250px" AllowCustomText="true" HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)">
                                                <asp:CheckBox ID="m_chkStatus" runat="server" onclick="OnCheckFilter(this, 'Request')" />
                                                <asp:Label ID="m_lblStatus" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Value") %></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                    <asp:CheckBox ID="m_chkRequestIsAsap" runat="server" Text="ASAP" />
                                </div>
                            </div>
                        </fieldset>
                        <div class="button-row action">
                            <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter Requests" CssClass="button primaryAction" OnClick="OnFilterRequests" />
                        </div>
                    </asp:Panel>
                </div>

                <div class="completed">
                    <h3>Jobs:</h3>
                    <telerik:RadGrid ID="m_grdRequests" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSourceRequests" onitemdatabound="OnItemDataBoundRequests" OnSortCommand="OnSortCommandRequests" OnDetailTableDataBind="OnDetailTableDataBindRequests" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
                        <MasterTableView NoMasterRecordsText="No Requests" DataKeyNames="IARequestID"></MasterTableView>
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
                        <telerik:GridSortExpression FieldName="IARequestID" SortOrder="Descending" />
                        </SortExpressions>
                            <Columns>
                                <telerik:GridBoundColumn DataField="IARequestID" UniqueName="IARequestID" Visible="False"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IsRushOrder" HeaderText="Priority" UniqueName="IsRushOrder" Visible="false"></telerik:GridBoundColumn>
                                <telerik:GridHyperLinkColumn DataTextField="JobName" HeaderText="Job Title" UniqueName="JobName" SortExpression="JobName" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/create-job.aspx?s=c&rid={0}"></telerik:GridHyperLinkColumn>
                                <telerik:GridHyperLinkColumn DataTextField="IARequestID" HeaderText="Request #" UniqueName="RequestNumber" SortExpression="IARequestID" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/create-job.aspx?s=c&rid={0}"></telerik:GridHyperLinkColumn>
                                <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company" UniqueName="CompanyName"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="UserName" HeaderText="Requested By" UniqueName="UserName"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CreatedDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Requested" UniqueName="CreatedDateTime"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ProductionDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Production" UniqueName="ProductionDateTime"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CompletedDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Completed" UniqueName="CompletedDateTime"></telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>

                        <FilterMenu EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                        </FilterMenu>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>