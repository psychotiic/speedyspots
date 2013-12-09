<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestDetails.ascx.cs" Inherits="SpeedySpots.Controls.RequestDetails" %>

<div class="form-holder request">
<fieldset>
    <legend>Request Details:</legend>
    <div class="group">
        <label class="stacked">Request:</label>
        <span class="output"><asp:Literal ID="litIDForDisplay" runat="server" /></span>
    </div>
    
    <div id="divHasEstimate" runat="server" class="group">
        <label class="stacked">Estimate?</label>
        <asp:Literal ID="litEstimateDisplay" runat="server" />    
    </div>
    
    <div class="group">
        <label class="stacked">Status:</label>
        <asp:Literal ID="litStatus" runat="server" />
    </div>
    <div class="group">
        <label class="stacked">Submitted By:</label>
        <span class="output"><asp:Literal ID="litSubmittedBy" runat="server" /></span>
    </div>
    <div class="group">
        <label class="stacked">Contact Phone:</label>
        <span class="output"><asp:Literal ID="litContactPhone" runat="server" /></span>
    </div>
    <div class="group">
        <label class="stacked">Company:</label>
        <span class="output"><asp:Literal ID="litCompany" runat="server" /></span>
    </div>
    
    <telerik:RadAjaxPanel ID="m_oAjaxPanel" runat="server" RenderMode="Inline" EnableEmbeddedScripts="false" EnablePageHeadUpdate="false" EnableTheming="false">
        <div class="group">
            <label class="stacked">Notify:</label>
            <div id="divNotifyOutput" runat="server">
                <span class="output"><asp:Literal ID="litNotify" runat="server" /> <asp:LinkButton ID="hlNotifyEdit" runat="server" Text="[Edit]" OnClick="OnNotifyEdit" CausesValidation="false" /></span>
            </div>
            <div id="divNotifyEdit" runat="server">
                <asp:ValidationSummary id="valNotifySummary" runat="server" ValidationGroup="Notify" DisplayMode="List" />
                <asp:TextBox ID="txtNotifyEdit" runat="server" Width="250px" ValidationGroup="Notify" TextMode="MultiLine" Rows="5" />
                <asp:Button ID="btnNotifyUpdate" runat="server" Text="Update" OnClick="OnNotifyUpdate" ValidationGroup="Notify" />
                <asp:RequiredFieldValidator ID="rfvNotify" runat="server" ControlToValidate="txtNotifyEdit" ErrorMessage="Notify contacts are required" Text="* Required" Display="Dynamic"  ValidationGroup="Notify" />
                <asp:CustomValidator ID="cvNotify" runat="server" ControlToValidate="txtNotifyEdit" ValidateEmptyText="true" OnServerValidate="OnNotifyValidate" ErrorMessage="Notify contacts are required" Text="* Required" Display="Dynamic" ValidationGroup="Notify" />
            </div>
        </div>
    </telerik:RadAjaxPanel>
    
    <div id="divScript" runat="server" class="group">
        <label class="stacked">Script:</label>
        <span class="output"><asp:HyperLink ID="hlScript" runat="server" NavigateUrl="javascript:popUp('view-script.aspx?id={0}');">View Script</asp:HyperLink></span>
    </div>

    <div id="divProdNotes" runat="server" class="group">
        <label class="stacked">Prod. Notes:</label>
        <span class="output"><asp:HyperLink ID="hlProdNotes" runat="server" NavigateUrl="javascript:popUp('view-production-notes.aspx?id={0}');">View Notes</asp:HyperLink></span>
    </div>

    <div id="divFiles" runat="server">
        <div class="hr"><hr /></div>
        <div class="group">
            <label>File(s):</label>
            <div class="inputgroup">
                <asp:Repeater ID="rptFiles" runat="server">
                    <ItemTemplate>
                        <a href="download.aspx?id=<%# Eval("IARequestFileID")%>&type=request"><%# Eval("Filename")%></a><br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>    

    <div id="divEstimates" runat="server" class="group">
        <label class="stacked">Estimate:</label>
        <span class="output"><asp:HyperLink ID="hlEstimates" runat="server" NavigateUrl="javascript:popUp('view-estimate.aspx?id={0}');">View Estimate</asp:HyperLink></span>
    </div>

    <div class="group">
        <label class="stacked">Created:</label>
        <span class="output"><asp:Literal ID="litCreatedOn" runat="server" /></span>
    </div>
</fieldset>
</div>