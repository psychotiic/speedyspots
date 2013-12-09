<%@ Page Title="Speedy Spots :: Review Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="create-job.aspx.cs" Inherits="SpeedySpots.create_job" %>
<%@ Import Namespace="SpeedySpots.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<link media="screen" rel="stylesheet" href="css/colorbox.css" />
<script type="text/javascript" src="js/jquery.colorbox-min.js"></script>
<script language="javascript" type="text/javascript">
$(document).ready(function()
{
    $('#loading').hide();

    $('.user').hover(  
         function() {  
            $(this).addClass('hover');  
         },  
         function() {  
            $(this).removeClass('hover');  
         }  
    );  

    UpdateActivity();
});

function UpdateActivity()
{
    $.ajax(
    {
        url: "<%=ResolveUrl("~/ajax-update-activity.aspx") %>",
        data: ({userid: '<%=MemberProtect.CurrentUser.UserID %>', itemtype: "Request", itemid: <%=IARequest.IARequestID %>}),
        type: "POST",
        success: OnUpdateActivitySuccess,
        error: OnUpdateActivityError
    });
}

function OnUpdateActivitySuccess(data, textStatus, jqXHR)
{
    var viewing = $("#viewing");

    if(data.length > 0) {
        viewing.find('div').remove();
        viewing.append(data);

        if(viewing.find('div').length > 0 && viewing.is(":hidden")) {
            viewing.fadeIn('fast');
        } else if(viewing.find('div').length <= 0 && viewing.is(":visible")) {
            viewing.fadeOut('fast');
        }
    } else {
        if(viewing.is(":visible")) {
            viewing.fadeOut('fast');
            viewing.find('div').remove();
        }
    }

    setTimeout("UpdateActivity()", <%=ActivityInterval * 1000 %>);
}

function OnUpdateActivityError(jqXHR, textStatus, errorThrown)
{
    //alert(textStatus + ": " + jqXHR.responseText);
}

function ScrollToBottom()
{
    if(document.body.scrollHeight)
    {
        window.scrollTo(0 , document.body.scrollHeight);
        document.getElementById("<%=m_txtJobName.ClientID %>").focus();
    } 
}

function StopPropagation(e)
{
    // Cancel event bubbling
    e.cancelBubble = true;
    if (e.stopPropagation)
    {
        e.stopPropagation();
    }
}

function OnModifyLabel(chkStatus, iIALabelID)
{
    var sAction = "";

    if(chkStatus.checked)
    {
        sAction = "add";
    }
    else
    {
        sAction = "delete";
    }

    $('#loading').fadeIn();
    OnEditLabel(sAction, iIALabelID);
}

function OnRemoveLabel(iIALabelID)
{
    $('#loading').fadeIn();
    OnEditLabel("delete", iIALabelID);

    var cboLabels = $find("<%=m_cboLabels.ClientID %>");
    var oItems = cboLabels.get_items();
    for(var i=0; i<oItems.get_count(); i++)
    {
        if(oItems.getItem(i).get_value() == iIALabelID.toString())
        {
            var oCheckbox = document.getElementById("<%=m_cboLabels.ClientID %>" + "_i" + i + "_m_chkLabel");
            oCheckbox.checked = false;
            break;
        }
    }
}

function OnEditLabel(sAction, iIALabelID)
{
    $.ajax(
    {
        url: "<%=ResolveUrl("~/ajax-request-label.aspx") %>",
        data: ({requestid: <%=IARequest.IARequestID %>, labelid: iIALabelID, action: sAction}),
        type: "POST",
        success: OnEditLabelSuccess,
        error: OnEditLabelError
    });
}

function OnEditLabelSuccess(data, textStatus, jqXHR)
{
    var divLabels = $("#<%=m_divLabels.ClientID %>");

    divLabels.html(data);

    setTimeout(function() { $('#loading').fadeOut(); }, 2000);
}

function OnEditLabelError(jqXHR, textStatus, errorThrown)
{
    alert(textStatus + ": " + jqXHR.responseText);
}

function OnOpenRecut(iID)
{
    var m_txtID = $("#<%=m_txtID.ClientID %>");
    var m_txtDescription = $("#<%=m_txtDescription.ClientID %>");

    m_txtID.val(iID);
    m_txtDescription.val("");

    return false;
}
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".cancelWindow").colorbox({ width: "50%", overlayClose: false, inline: true, close: "Back", href: "#cancelWindow" });
        $(".unlockWindow").colorbox({ width: "50%", overlayClose: false, inline: true, close: "Back", href: "#unlockWindow" });
        $(".recutWindow").colorbox({ width: "50%", overlayClose: false, inline: true, close: "Back", href: "#recutWindow" });
    });
</script>
<div class="main">
    <div id="viewing">
	    <strong>Users Viewing:</strong>
    </div>
    <p class="breadcrumb"><asp:HyperLink id="hlDashboard" runat="server" Text="Dashboard" /> &raquo; <strong>Jobs</strong></p>
    <h2><%=MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(IARequest.MPUserID)) %> Request No: <%=IARequest.RequestIdForDisplay %></h2>

        <% if( (IARequest.IARequestEstimates.Count == 0 && IARequest.IARequestID != ApplicationContext.GetRequestStatusID(RequestStatus.Completed)) || (IARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.InProduction) && IARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Completed) && IARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Canceled)) ) %>
    <% { %>
        <div class="button-row action">
            <asp:HyperLink ID="hlEstimate" runat="server" CssClass="small button" NavigateUrl="~/create-estimate.aspx?rid={0}" />

        <% if(IARequest.IsLocked) %>
        <% { %>
            <asp:LinkButton ID="m_btnUnlock" runat="server" CausesValidation="false" Text="Unlock Request" CssClass="small button unlockWindow" />
        <% } %>
        <% else %>
        <% { %>
            <asp:LinkButton ID="m_btnLock" runat="server" CausesValidation="false" Text="Lock Request" CssClass="small button" OnClientClick="return ConfirmUser('Are you sure you want to lock this request?');" OnClick="OnLockRequest" />
        <% } %>

        <asp:LinkButton ID="lbPushToProcessing" runat="server" CausesValidation="false" Text="Push to Processing" CssClass="small button" Visible="false" OnClick="OnPushRequestToProcessing" />

        <% if(IARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.InProduction) && IARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Completed) && IARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Canceled))  %>
        <% { %>
            <asp:LinkButton ID="m_btnCancelRequest" runat="server" CausesValidation="false" Text="Cancel Request" CssClass="small button negativeAction cancelWindow" />
        <% } %>

        </div>
    <% } %>

    <div id="label-box">
        <div id="label-box-head" class="group">
            <label>Labels</label>
            <div id="label-box-combo">
                <telerik:RadComboBox ID="m_cboLabels" runat="server" Width="250px" AllowCustomText="true" HighlightTemplatedItems="true">
                    <ItemTemplate>
                        <div onclick="StopPropagation(event)">
                            <asp:CheckBox ID="m_chkLabel" runat="server" onclick='<%# Eval("IALabelID", "OnModifyLabel(this, {0})") %>' />
                            <asp:Label ID="m_lblLabel" runat="server" AssociatedControlID="m_chkLabel"><%# Eval("Text") %></asp:Label>
                        </div>
                    </ItemTemplate>
                </telerik:RadComboBox>
            </div>
        </div>
        <div id="m_divLabels" runat="server"></div>
    </div>

    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" OnDetailTableDataBind="OnDetailTableDataBind" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoMasterRecordsText="No jobs. Create one below." DataKeyNames="IAJobID"></MasterTableView>
        <HeaderContextMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </HeaderContextMenu>

        <MasterTableView Name="Master" HierarchyDefaultExpanded="true">
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
                <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Sequence" UniqueName="Sequence" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IARequestID" UniqueName="IARequestID" Visible="False"></telerik:GridBoundColumn>               
                <telerik:GridButtonColumn DataTextField="Name" HeaderText="Name" UniqueName="Name" SortExpression="Name" CommandName="EditJob"></telerik:GridButtonColumn>
                <telerik:GridBoundColumn DataField="IAJobID" HeaderText="Job ID" UniqueName="JobID" ItemStyle-CssClass="jobRow"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DueDateTime" DataFormatString="{0:ddd d a\t h:mm tt}" HeaderText="Due Date/Time" UniqueName="DueDateTime" ItemStyle-CssClass="jobRow"></telerik:GridBoundColumn>
                <telerik:GridButtonColumn Text="Add PO" UniqueName="AddPo" CommandName="AddProductionOrder" ButtonCssClass="button"></telerik:GridButtonColumn>
                <telerik:GridButtonColumn Text="Reactivate" UniqueName="Reactivate" CommandName="Reactivate" ButtonCssClass="button" ConfirmTitle="Contact Billings" ConfirmText="Please ensure that you personally contact billings and inform them of this action.\n\nAre you sure you wish to reactivate this job?"></telerik:GridButtonColumn>
                <telerik:GridButtonColumn Text="" UniqueName="Production" CommandName="" ButtonCssClass="button"></telerik:GridButtonColumn>
                <telerik:GridButtonColumn Text="Recut" UniqueName="Recut" CommandName="Recut"></telerik:GridButtonColumn>
                <telerik:GridButtonColumn Text="Delete Job" UniqueName="Delete" CommandName="Delete" ConfirmText="Are you sure you want to delete this job?" ButtonCssClass="delete"></telerik:GridButtonColumn>
            </Columns>
            
            <DetailTables>
                <telerik:GridTableView Name="Detail" AutoGenerateColumns="false" NoDetailRecordsText="No production orders. Add one using the 'Add PO' button next to the proper job." ShowHeadersWhenNoRecords="False">
                    <ParentTableRelation>
                        <telerik:GridRelationFields DetailKeyField="IAJobID" MasterKeyField="IAJobID" />
                    </ParentTableRelation>
                    <Columns>
                        <telerik:GridBoundColumn DataField="IAJobID" UniqueName="IAJobID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IAProductionOrderID" UniqueName="IAProductionOrderID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn DataTextField="Talent" HeaderText="Talent" UniqueName="Talent" SortExpression="Talent" CommandName="View"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="Spots" HeaderText="Spots" UniqueName="Spots"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn ButtonCssClass="button" Text="Duplicate" UniqueName="Duplicate" CommandName="Duplicate"></telerik:GridButtonColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>

    <p>&nbsp;</p> <!-- remove when we fix up the skin -->

    <div class="form-holder">
        <fieldset>
            <legend><asp:Label ID="m_lblJob" runat="server"></asp:Label></legend>
            <div class="group">
                <asp:Label id="m_lblJobName" runat="server" AssociatedControlID="m_txtJobName">
                        <asp:RequiredFieldValidator ID="m_reqJobName" runat="server" ControlToValidate="m_txtJobName" ErrorMessage="Please enter the name of the job.">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="m_expJobName" runat="server" ControlToValidate="m_txtJobName" ValidationExpression="^.{0,50}$" ErrorMessage="Job name may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                        <span class="required">Job Name:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtJobName" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
            <div class="group">
                <asp:Label ID="m_lblJobDueDate" runat="server" AssociatedControlID="m_dtJobDueDateTime">
                    <asp:RequiredFieldValidator ID="m_reqDueDateTime" runat="server" ControlToValidate="m_dtJobDueDateTime" ErrorMessage="Please enter the due date/time for the job.">*</asp:RequiredFieldValidator>
                    <span class="required">Due Date/Time:</span>
                </asp:Label>
                <div class="inputgroup">
                    <telerik:RadDateTimePicker runat="server" ID="m_dtJobDueDateTime" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                        <Calendar>
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                            </SpecialDays>
                        </Calendar>
                    </telerik:RadDateTimePicker>
                    <asp:CheckBox ID="m_chkASAP" runat="server" Text="ASAP" />
                </div>
            </div>
            <div class="group">
                <asp:Label id="m_lblLanguage" runat="server" AssociatedControlID="m_cboLanguage"><span class="required">Language:</span></asp:Label>
                <asp:DropDownList ID="m_cboLanguage" runat="server" />
            </div>
        </fieldset>            
        <div class="button-row">
            <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Create Job" CssClass="button primaryAction" OnClick="OnCreateJob" />
            <asp:LinkButton ID="m_btnUpdate" runat="server" Text="Update Job" CssClass="button primaryAction" OnClick="OnUpdateJob" />
        </div>
    </div>
</div>
<div class="sidebar">
    <speedyspots:SpeedySpotsTabs id="m_oTabs" runat="server" DefaultView="Producer" ShowRequestDetails="true" ShowRequestNotes="true" />
</div>
<!-- This contains the hidden content for inline calls -->
<div style="display:none">
	<div id="cancelWindow" style="padding:10px; background:#fff;">
        <div class="form-holder">
		    <fieldset>
                <legend>Cancellation Reason</legend>
                <div class="group">
                    <telerik:RadTextBox ID="m_txtCancellationReason" runat="server" Columns="68" TextMode="MultiLine" Rows="18"></telerik:RadTextBox>
                </div>
            </fieldset>
        </div>
        <div class="button-row">
            <asp:LinkButton ID="m_btnWindowBack" runat="server" CausesValidation="false" Text="Back" CssClass="button" />
            <asp:LinkButton ID="m_btnWindowConfirm" runat="server" CausesValidation="false" Text="Confirm Cancellation" CssClass="button negativeAction" OnClientClick="$.colorbox.close();" OnClick="OnCancelRequest" />
        </div>
	</div>
</div>
<div style="display:none">
	<div id="unlockWindow" style="padding:10px; background:#fff;">
        <div class="form-holder">
		    <fieldset>
                <legend>Unlock Reason</legend>
                <div class="group">
                    <telerik:RadTextBox ID="m_txtUnlockReason" runat="server" Columns="68" TextMode="MultiLine" Rows="18"></telerik:RadTextBox>
                </div>
            </fieldset>
        </div>
        <div class="button-row">
            <asp:LinkButton ID="m_btnUnlockBack" runat="server" CausesValidation="false" Text="Back" CssClass="button" />
            <asp:LinkButton ID="m_btnUnlockConfirm" runat="server" CausesValidation="false" Text="Confirm Unlock" CssClass="button negativeAction" OnClientClick="$.colorbox.close();" OnClick="OnUnlockRequest" />
        </div>
	</div>
</div>
<div style="display:none">
	<div id="recutWindow" style="padding:10px; background:#fff;">
        <div class="form-holder">
		    <fieldset>
                <legend>Recut Request</legend>
                <div class="group">
                    <div class="inputgroup">
                        <asp:TextBox ID="m_txtDescription" runat="server" MaxLength="100" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        <p class="note red"><strong>NOTE:</strong> By requesting a recut you are indicating the talent made an error and you would like it to be corrected.</p>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="button-row">
        <asp:HiddenField ID="m_txtID" runat="server" />
            <asp:LinkButton ID="m_btnBack" runat="server" CausesValidation="false" Text="Back" CssClass="button recutWindow" OnClientClick="$.colorbox.close();" />
            <asp:LinkButton ID="m_btnRecut" runat="server" CausesValidation="false" Text="Submit Request" CssClass="button primaryAction" OnClientClick="$.colorbox.close();" OnClick="OnRecut" />
        </div>
	</div>
</div>
</asp:Content>