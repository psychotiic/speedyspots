<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TalentAssignment.ascx.cs" Inherits="SpeedySpots.Controls.SpotControls.TalentAssignment" %>
<%@ Import Namespace="SpeedySpots.Business" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script language="javascript" type="text/javascript">
function OnRequestStart(sender, eventArgs)
{
    // We force a regular post-back if the record is new and the assignment button has been pressed,
    // this is to ensure the spot detail control shows up after saving.
    if("<%=IsNew %>" == "True")
    {
        if(eventArgs.get_eventTarget() == "<%=m_btnAssignTalent.UniqueID %>")
        {
            eventArgs.set_enableAjax(false);
        }
    }
}
</script>
</telerik:RadCodeBlock>

<telerik:RadAjaxPanel ID="m_oAjaxPanel" runat="server" LoadingPanelID="m_pnlLoading" ClientEvents-OnRequestStart="OnRequestStart" EnablePageHeadUpdate="false">
    <telerik:RadCodeBlock ID="m_oCodeBlock" runat="server">
        <div id="m_divEdit" runat="server" class="form-holder">
            <fieldset>
                <legend>Talent Information:</legend>
                <div class="group">
                    <asp:Label ID="m_lblTalentType" runat="server" Text="<span class='required'>Talent Type:</span>" AssociatedControlID="m_cboTalentType"></asp:Label>
                    <asp:DropDownList ID="m_cboTalentType" runat="server" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="OnChangedTalentType" Width="160px" />                    
                </div>
                <div class="group">
                    <asp:Label ID="m_lblTalent" runat="server" AssociatedControlID="m_radTalent">
                        <span class="required">Talent Name:</span>
                    </asp:Label>
                    <div class="inputgroup">
                        <asp:RadioButtonList ID="m_radTalent" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow"></asp:RadioButtonList>
                    </div>
                </div>
            </fieldset>
            <div class="button-row">
                <asp:LinkButton ID="m_btnAssignTalent" runat="server" Text="Assign Talent" CausesValidation="false" CssClass="button" OnClick="OnAssignTalent" />
            </div>
        </div>
        <div id="m_divView" runat="server">
            <h2>Production order for <%=TalentAssigned %>
            <% if(IAJob.IAJobStatusID != ApplicationContext.GetJobStatusID(JobStatus.Complete)) %>
            <% { %>
             - <asp:LinkButton id="m_btnReassign" runat="server" CausesValidation="false" OnClick="OnReassign" Text="Reassign" />
             <% } %>
            </h2>
        </div>
    </telerik:RadCodeBlock>
</telerik:RadAjaxPanel>