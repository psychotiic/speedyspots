<%@ Page Title="Speedy Spots :: Staff Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="staff-dashboard.aspx.cs" Inherits="SpeedySpots.staff_dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
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
    if (e.stopPropagation)
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
    else if(sParentID == "Job")
    {
        sID = "<%=m_cboJobStatus.ClientID %>";
    }
    else if(sParentID == "Labels")
    {
        sID = "<%=m_cboLabels.ClientID %>";
    }
    else if(sParentID == "Languages")
    {
        sID = "<%=m_cboRequestLanguage.ClientID %>";
    }
    else if(sParentID == "ProductionLanguages")
    {
        sID = "<%=m_cboInProductionLanguage.ClientID %>";
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
        if (m_chkStatus.checked)
        {
            sText += oItem.get_text() + "," ;
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
    return sValue.replace(/,$/,"");
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
                    <li
                    <% if(Request.QueryString["filter"] == null) %>
                    <% { %>
                    class="at"
                    <% } %>
                    ><a href="staff-dashboard.aspx">Requests</a></li>
                    <li
                     <% if(Request.QueryString["filter"] != null) %>
                    <% { %>
                            <% if(Request.QueryString["filter"] == "inproduction") %>
                            <% { %>
                            class="at"
                            <% } %>
                    <% } %>
                    ><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
                    <li><a href="staff-dashboard-completed.aspx">Completed</a></li>
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
                            <legend>Filter Requests:</legend>
                            <div class="group">
                                <asp:Label ID="m_lblRequestNumber" runat="server" Text="Request #:" AssociatedControlID="m_txtRequestNumber"></asp:Label>
                                <div class="inputgroup">
                                    <telerik:RadTextBox ID="m_txtRequestNumber" runat="server"></telerik:RadTextBox>
                                    <asp:Label ID="m_lblCreatedDate" runat="server" Text="Request Date:" AssociatedControlID="m_dtCreatedDate"></asp:Label>
                                    <telerik:RadDatePicker runat="server" ID="m_dtCreatedDate"></telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="group">
                                <asp:Label ID="m_lblRequestedBy" runat="server" Text="Requested By:" AssociatedControlID="m_txtRequestedBy"></asp:Label>
                                <div class="inputgroup">                               
                                    <telerik:RadTextBox ID="m_txtRequestedBy" EmptyMessage="name, email address or company name" runat="server" Columns="50"></telerik:RadTextBox>
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
                                                <asp:Label ID="m_lblStatus" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Name") %></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                    <asp:CheckBox ID="m_chkRequestIsAsap" runat="server" Text="ASAP" />
                                    <asp:Label ID="Label4" runat="server" Text="Labels:" AssociatedControlID="m_cboLabels"></asp:Label>
                                    <telerik:RadComboBox ID="m_cboLabels" runat="server" Width="250px" AllowCustomText="true" HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)">
                                                <asp:CheckBox ID="m_chkStatus" runat="server" onclick="OnCheckFilter(this, 'Labels')" />
                                                <asp:Label ID="m_lblLabel" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Text") %></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>  
                                </div>
                            </div>
                        </fieldset>
                        <div class="button-row action">
                            <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter Requests" CssClass="button primaryAction" OnClick="OnFilterRequests" />
                        </div>
                    </asp:Panel>
                </div>

                <h3>Requests:</h3>
                <telerik:RadGrid ID="m_grdRequests" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSourceRequests" onitemdatabound="OnItemDataBoundRequests" OnSortCommand="OnSortCommandRequests" OnDetailTableDataBind="OnDetailTableDataBindRequests" OnItemCreated="OnItemCreatedRequests" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
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
                            <telerik:GridBoundColumn DataField="IsPastDue" UniqueName="IsPastDue" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Expand" ItemStyle-CssClass="expandcol"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsRushOrder" HeaderText="Priority" UniqueName="IsRushOrder"></telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn DataTextField="IARequestID" HeaderText="Request #" UniqueName="RequestNumber" SortExpression="IARequestID" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/create-job.aspx?s=r&rid={0}"></telerik:GridHyperLinkColumn>
                            <telerik:GridHyperLinkColumn DataTextField="CompanyName" HeaderText="Company" UniqueName="CompanyName" SortExpression="CompanyName" DataNavigateUrlFields="IARequestID" DataNavigateUrlFormatString="~/create-job.aspx?s=r&rid={0}"></telerik:GridHyperLinkColumn>
                            <telerik:GridBoundColumn DataField="UserName" HeaderText="Requested By" UniqueName="UserName" SortExpression="Username"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CreatedDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Requested" UniqueName="CreatedDateTime"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Labels" UniqueName="Labels"></telerik:GridBoundColumn>
                        </Columns>

                        <DetailTables>
                            <telerik:GridTableView Name="Detail" AutoGenerateColumns="false" NoDetailRecordsText="No jobs have been created yet." ShowHeadersWhenNoRecords="False">
                                <ParentTableRelation>
                                    <telerik:GridRelationFields DetailKeyField="IARequestID" MasterKeyField="IARequestID" />
                                </ParentTableRelation>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="IARequestID" UniqueName="IARequestID" Visible="False"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Name" HeaderText="Job Name" UniqueName="Name"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="JobIDForDisplay" HeaderText="Job ID Number" UniqueName="JobIDNumber"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DueDateTime" DataFormatString="{0:MM/dd/yyyy a\t h:mm tt}" HeaderText="Due Date/Time" UniqueName="DueDateTime"></telerik:GridBoundColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>

                    <FilterMenu EnableTheming="True">
                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
            </div>

            <div id="m_divInProduction" runat="server">
                <div class="form-holder filter">
                    <asp:Panel ID="pnlProductionFilters" runat="server" DefaultButton="m_btnFilterJobs">
                        <fieldset>
                            <legend>Filter Jobs:</legend>
                            <div class="group">
                                <asp:Label ID="m_lblJobName" runat="server" Text="Job Name:" AssociatedControlID="m_txtJobName"></asp:Label>
                                <div class="inputgroup">
                                    <telerik:RadTextBox ID="m_txtJobName" runat="server" MaxLength="50"></telerik:RadTextBox>
                                    <asp:Label ID="m_lblOrderDate" runat="server" Text="Due Date/Time:" AssociatedControlID="m_dtOrderDate"></asp:Label>
                                    <telerik:RadDatePicker runat="server" ID="m_dtOrderDate"></telerik:RadDatePicker>
                                    <asp:CheckBox ID="m_chkInProductionIsAsap" runat="server" Text="ASAP" />
                                </div>
                            </div>
                            <div class="group">
                                <asp:Label ID="Label1" runat="server" Text="Job ID:" AssociatedControlID="m_txtJobID"></asp:Label>
                                <div class="inputgroup">
                                    <telerik:RadTextBox ID="m_txtJobID" runat="server" MaxLength="13"></telerik:RadTextBox>
                                    <asp:Label ID="Label2" runat="server" Text="Status:" AssociatedControlID="m_cboJobStatus"></asp:Label>
                                    <telerik:RadComboBox ID="m_cboJobStatus" runat="server" Width="250px" AllowCustomText="true" HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)">
                                                <asp:CheckBox ID="m_chkStatus" runat="server" onclick="OnCheckFilter(this, 'Job')" />
                                                <asp:Label ID="m_lblStatus" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Name") %></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="group">
                                <asp:Label ID="Label3" runat="server" Text="Language:" AssociatedControlID="m_cboInProductionLanguage"></asp:Label>
                                <div class="inputgroup">
                                    <telerik:RadComboBox ID="m_cboInProductionLanguage" runat="server" Width="250px" AllowCustomText="true" HighlightTemplatedItems="true">
                                        <ItemTemplate>
                                            <div onclick="StopPropagation(event)">
                                                <asp:CheckBox ID="m_chkStatus" runat="server" onclick="OnCheckFilter(this, 'ProductionLanguages')" />
                                                <asp:Label ID="m_lblLabel" runat="server" AssociatedControlID="m_chkStatus"><%# Eval("Value") %></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:RadComboBox>
                                    <label>Talent:</label>
                                    <telerik:RadComboBox ID="m_cboTalent" runat="server"></telerik:RadComboBox>
                                </div>
                            </div>
                        </fieldset>
                        <div class="button-row action">
                            <asp:LinkButton ID="m_btnFilterJobs" runat="server" Text="Filter Jobs" CssClass="button primaryAction" OnClick="OnFilterInProduction" />
                        </div>
                    </asp:Panel>
                </div>

                <h3>In Production:</h3>
                <telerik:RadGrid ID="m_grdInProduction" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSourceInProduction" onitemdatabound="OnItemDataBoundInProduction" OnDetailTableDataBind="OnDetailTableDataBindInProduction" OnSortCommand="OnSortCommandInProduction" OnItemCreated="OnItemCreatedInProduction" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
                    <MasterTableView NoMasterRecordsText="No Requests" DataKeyNames="IAJobID"></MasterTableView>
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
                    <telerik:GridSortExpression FieldName="IAJobID" SortOrder="Ascending" />
                    </SortExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="IARequestID" UniqueName="IARequestID" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsPastDue" UniqueName="IsPastDue" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Sequence" UniqueName="Sequence" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Expand" ItemStyle-CssClass="expandcol"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsAsap" HeaderText="Priority" UniqueName="Priority"></telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn DataTextField="JobName" HeaderText="Job Name" UniqueName="JobName" SortExpression="JobName" DataNavigateUrlFields="IAJobID" DataNavigateUrlFormatString="~/quality-control-job-details.aspx?jid={0}"></telerik:GridHyperLinkColumn>
                            <telerik:GridBoundColumn HeaderText="Job ID" UniqueName="JobID" ItemStyle-CssClass="jobRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DueDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Due Date/Time" UniqueName="DueDateTime" ItemStyle-CssClass="jobRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="JobStatus" HeaderText="Status" UniqueName="JobStatus" ItemStyle-CssClass="jobRow"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TalentFileCount" HeaderText="Files" UniqueName="TalentFileCount" ItemStyle-CssClass="jobRow"></telerik:GridBoundColumn>
                        </Columns>
                        <DetailTables>
                            <telerik:GridTableView Name="Detail" AutoGenerateColumns="false">
                                <ParentTableRelation>
                                    <telerik:GridRelationFields DetailKeyField="IAJobID" MasterKeyField="IAJobID" />
                                </ParentTableRelation>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IASpotID" UniqueName="IASpotID" Visible="False"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IsAsap" HeaderText="Priority" UniqueName="Priority"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IAProductionOrderID" UniqueName="IAProductionOrderID" Visible="False"></telerik:GridBoundColumn>
                                    <telerik:GridHyperLinkColumn DataTextField="Title" HeaderText="Spot Title" UniqueName="Title" SortExpression="Title" DataNavigateUrlFields="IAJobID,IASpotID" DataNavigateUrlFormatString="~/quality-control-spot-details.aspx?jid={0}&amp;sid={1}"></telerik:GridHyperLinkColumn>                                    
                                    <telerik:GridBoundColumn DataField="Talent" HeaderText="Talent" UniqueName="Talent"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DueDateTime" DataFormatString="{0:M/dd/yyyy a\t h:mm tt}" HeaderText="Due Date/Time" UniqueName="DueDateTime"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SpotStatus" HeaderText="Status" UniqueName="SpotStatus"></telerik:GridBoundColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                    </MasterTableView>

                    <FilterMenu EnableTheming="True">
                    <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
</div>
</asp:Content>
