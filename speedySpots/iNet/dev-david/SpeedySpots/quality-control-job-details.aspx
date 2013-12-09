<%@ Page Title="Speedy Spots :: Edit Request" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="quality-control-job-details.aspx.cs" Inherits="SpeedySpots.quality_control_job_details" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<link type="text/css" rel="stylesheet" href="css/lightbox-form.css">
<script src="js/lightbox-form.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<telerik:RadCodeBlock ID="m_oCodeBlock2" runat="server">
<script language="javascript" type="text/javascript">
$(document).ready(function()
{
    UpdateActivity();
});

function UpdateActivity()
{
    $.ajax(
    {
        url: "<%=ResolveUrl("~/ajax-update-activity.aspx") %>",
        data: ({userid: '<%=MemberProtect.CurrentUser.UserID %>', itemtype: "Job", itemid: <%=IAJob.IAJobID %>}),
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
    alert(textStatus + ": " + jqXHR.responseText);
}

function OnFileSelected(oUpload, oArgs)
{
    var oSentFilenames = new Array();
    
    <% foreach(string sFilename in SentFilenames) %>
    <% { %>
        <%=string.Format("oSentFilenames[oSentFilenames.length] = '{0}';", sFilename)%>
    <% } %>

    var oInput = oArgs.get_fileInputField();
    var iRowIndex = oArgs.get_rowIndex();
    for(var i=0; i<oSentFilenames.length; i++)
    {
        if(oSentFilenames[i] == oInput.value)
        {
            var bResponse = confirm("Are you sure you want to send the file '" + oInput.value + "' again?");
            if(!bResponse)
            {
                oUpload.clearFileInputAt(iRowIndex);
            }
        }
    }
}

function OnRowSelecting(sender, args)
{
    var oGrid = sender;
    var oMasterTable = oGrid.get_masterTableView();
    var oRow = oMasterTable.get_dataItems()[args.get_itemIndexHierarchical()];
    var oSentCell = oMasterTable.getCellByColumnUniqueName(oRow, "Sent");
    var oFilenameCell = oMasterTable.getCellByColumnUniqueName(oRow, "NewFilename");
    var sFilename = $(oFilenameCell.innerHTML).find("input:first").val();

    // If the Sent column cell has no information (no image) then we can assume it's never been sent
    // if it has been sent before then we want to inform the user of that and let them decide if they want to really re-send the file
    if(oSentCell.innerHTML != "")
    {
        var bResponse = confirm("Are you sure you want to send the file '" + sFilename + "' again?");
        if(!bResponse)
        {
            args.set_cancel(true);
        }
    }
}
</script>
</telerik:RadCodeBlock>

<telerik:RadAjaxManagerProxy ID="m_oAjaxManager" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="m_grdList">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="m_grdList" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>

<div id="filter"></div>
<div class="main">
    <div id="viewing">
	    <strong>Users Viewing:</strong>
    </div>
    <p class="breadcrumb"><a href="staff-dashboard.aspx?filter=inproduction">Dashboard</a> &raquo; <strong>Edit Job</strong></p>

    <telerik:RadCodeBlock ID="m_oCodeBlock" runat="server">
        <h2>Job: <%=IAJob.Name %> | Due: <%=string.Format("{0:dddd, dd a\\t h:mm tt}", IAJob.DueDateTime) %></h2>
    </telerik:RadCodeBlock>

    <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AllowMultiRowSelection="true" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" on onitemcommand="OnItemCommand" OnDetailTableDataBind="OnDetailTableDataBind" AllowPaging="false" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoMasterRecordsText="No Production Orders" DataKeyNames="IAProductionOrderID" ShowHeader="false"></MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
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
                <telerik:GridSortExpression FieldName="IAProductionOrderID" SortOrder="Ascending" />
            </SortExpressions>
            <Columns>
                <telerik:GridBoundColumn DataField="IAProductionOrderID" UniqueName="IAProductionOrderID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TalentName" DataFormatString="Production Order: {0}" UniqueName="TalentName"></telerik:GridBoundColumn>
            </Columns>
            <DetailTables>
                <telerik:GridTableView Name="Detail" AutoGenerateColumns="false">
                    <ParentTableRelation>
                        <telerik:GridRelationFields DetailKeyField="IAProductionOrderID" MasterKeyField="IAProductionOrderID" />
                    </ParentTableRelation>
                    <Columns>
                        <telerik:GridBoundColumn DataField="IAProductionOrderID" UniqueName="IAProductionOrderID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IASpotID" UniqueName="IASpotID" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsAsap" UniqueName="IsAsap" Visible="False"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Status" UniqueName="Status"></telerik:GridBoundColumn>
                        <telerik:GridClientSelectColumn UniqueName="Recut"></telerik:GridClientSelectColumn>
                        <telerik:GridButtonColumn DataTextField="Title" HeaderText="Spot Title" UniqueName="Title" SortExpression="Title" CommandName="View"></telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="Length" HeaderText="Length" UniqueName="Length"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Fee" HeaderText="Fee(s)" UniqueName="Fee"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DueDateTime" DataFormatString="{0:ddd, dd a\t h:mmt}" HeaderText="Talent Due By" UniqueName="DueDateTime"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="File" HeaderText="File(s)" UniqueName="File"></telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="Notes" CommandName="ViewNotes"></telerik:GridButtonColumn>
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>

        <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>

    <div class="button-row action">
        <asp:LinkButton ID="m_btnRerecord" runat="server" Text="Re-record checked spots" CssClass="button" OnClick="OnRerecord" />
        <asp:LinkButton ID="m_btnRerecordUndo" runat="server" Text="Cancel Re-record checked spots" CssClass="button" OnClick="OnCancelRerecord" Visible="false"/>
    </div>

    <h3>Sent Production Files:</h3>
    <asp:Repeater ID="m_oRepeaterProduction" runat="server">
        <ItemTemplate>
            <div class="form-holder">
                <fieldset>
                    <legend>Sent <%#string.Format("{0:dddd, dd a\\t h:mm tt}", DataBinder.Eval(Container.DataItem, "CreatedDateTime")) %> by <%#string.Format("{0} {1}", MemberProtect.User.GetDataItem((Guid)DataBinder.Eval(Container.DataItem, "MPUserID"), "FirstName"), MemberProtect.User.GetDataItem((Guid)DataBinder.Eval(Container.DataItem, "MPUserID"), "LastName")) %></legend>
                    <div class="group">
                        <label>Files:</label>
                        <span class="output">
                        <asp:Repeater ID="m_oRepeaterProductionFiles" runat="server">
                            <ItemTemplate>
                                <a href="download.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IARequestProductionFileID") %>&type=requestproduction"><%#DataBinder.Eval(Container.DataItem, "Filename") %></a><br />
                            </ItemTemplate>
                        </asp:Repeater>
                        </span>
                    </div>
                    <div class="group">
                        <label>Notes for Client:</label>
                        <div class="inputgroup">
                            <span class="output"><%#DataBinder.Eval(Container.DataItem, "Notes") %></span>
                        </div>
                    </div>
                    <div class="button-row">
                        <asp:LinkButton ID="m_btnDelete" runat="server" Text="Delete" OnClientClick="return ConfirmUser('Are you sure you want to delete this entire package?');" OnClick="OnDeletePakcage" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "IARequestProductionID") %>' CssClass="button" />
                    </div>
                </fieldset>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    
    <div class="form-holder">
        <fieldset>
            <legend>Send Production File(s):</legend>
            <asp:ValidationSummary ID="m_oValidationSummary" runat="server" ValidationGroup="Production" HeaderText="Please fix the following to continue:" />
            <div class="group">
                <telerik:RadGrid ID="m_grdTalentFiles" runat="server" AllowSorting="True" AllowMultiRowSelection="true" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSourceTalentFiles" onitemdatabound="OnItemDataBoundTalentFiles" onitemcommand="OnItemCommandTalentFiles" AllowPaging="false" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
                    <MasterTableView NoMasterRecordsText="No Talent Files"></MasterTableView>
                    <ClientSettings Selecting-UseClientSelectColumnOnly="True">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowSelecting="OnRowSelecting" />
                    </ClientSettings>
                    
                    <HeaderContextMenu EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </HeaderContextMenu>

                    <MasterTableView Name="Master">
                        <RowIndicatorColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>

                        <ExpandCollapseColumn>
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="IASpotFileID" SortOrder="Ascending" />
                        </SortExpressions>

                        <Columns>
                            <telerik:GridBoundColumn DataField="IASpotFileID" UniqueName="IASpotFileID" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Filename" UniqueName="Filename" Visible="False"></telerik:GridBoundColumn>
                            <telerik:GridClientSelectColumn UniqueName="Send"></telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="Sent" UniqueName="Sent"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Files from talent(s):" UniqueName="NewFilename">
                                <ItemTemplate>
                                    <asp:RequiredFieldValidator ID="m_reqNewFilename" runat="server" ValidationGroup="Production" ControlToValidate="m_txtNewFilename" ErrorMessage="Filename is required.">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="m_expNewFilename" runat="server" ValidationGroup="Production" ControlToValidate="m_txtNewFilename" ErrorMessage="Filename must contain an extension" ValidationExpression="^(.)*\.(.){3}$">*</asp:RegularExpressionValidator>
                                    <telerik:RadTextBox ID="m_txtNewFilename" runat="server" MaxLength="75" Columns="55"></telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>

                    <FilterMenu EnableTheming="True">
                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                    </FilterMenu>
                </telerik:RadGrid>
            </div>

            <div class="group">
                <asp:Label ID="m_lblFinishedFile" runat="server" Text="<span class=required>Produced File:</span>" AssociatedControlID="m_oUpload"></asp:Label>
                <div class="inputgroup">
                    <telerik:RadUpload ID="m_oUpload" runat="server" ControlObjectsVisibility="AddButton,RemoveButtons" OnClientFileSelected="OnFileSelected"></telerik:RadUpload>
                </div>
            </div>

            <div class="group">
                <asp:Label ID="m_lblNotes" runat="server" AssociatedControlID="m_txtNotes">
                    <span>Notes for Client:</span>
                </asp:Label>
                <telerik:RadTextBox ID="m_txtNotes" runat="server" Columns="50" TextMode="MultiLine" Rows="5" MaxLength="2000"></telerik:RadTextBox>
            </div>
                
            <div class="button-row">
                <asp:LinkButton ID="m_btnSend" runat="server" Text="Send" ValidationGroup="Production" OnClick="OnSend" CssClass="button" />
            </div>
        </fieldset>
    </div>

    <div class="button-row">
        <asp:LinkButton ID="m_btnFinished" runat="server" Text="Completed, Send to Billing" CssClass="button primaryAction" OnClientClick="return OnSaveJobDetails();" OnClick="OnFinished" />
        <asp:LinkButton ID="m_back" runat="server" Text="Back" OnClick="OnBack" CssClass="button" />
    </div>
</div>
<div class="sidebar">
    <speedyspots:SpeedySpotsTabs id="m_oTabs" runat="server" DefaultView="QualityControl" DefaultTab="Job" ShowRequestDetails="true" ShowRequestNotes="true" ShowJobDetails="true" />
</div>
</asp:Content>