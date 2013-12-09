<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestNotes.ascx.cs" Inherits="SpeedySpots.Controls.RequestNotes" %>

<telerik:RadAjaxPanel ID="m_oAjaxPanel" runat="server" OnAjaxRequest="m_oAjaxPanel_AjaxRequest" EnablePageHeadUpdate="false">
    <fieldset>
        <legend>Request Notes:</legend>
        <asp:ValidationSummary ID="m_oValidationSummary" runat="server" ValidationGroup="Notes" DisplayMode="BulletList" HeaderText="Please fix the following issues to continue:" />

        <div id="m_divNotes" runat="server">
        <asp:Repeater ID="rptNotes" runat="server">
            <ItemTemplate>
                <dl class="notes group">
                    <dt><strong><%# Eval("FirstName") %> <%# Eval("LastName") %>:</strong><span class="date"><%#String.Format("{0:M/dd/yyyy @ h:mm tt}", DataBinder.Eval(Container.DataItem, "CreatedDateTime").ToString())%></span></dt>
                    <dd><p><%# Eval("Note") %></p></dd>
                </dl>
            </ItemTemplate>
        </asp:Repeater>
        </div>
        
        <div class="group">
            <asp:Label ID="m_lblNote" runat="server" AssociatedControlID="m_txtNote" CssClass="stacked">
                <asp:RequiredFieldValidator ID="m_reqNote" runat="server" ValidationGroup="Notes" ControlToValidate="m_txtNote" ErrorMessage="Please enter your notes.">*</asp:RequiredFieldValidator>
                Your Notes:
            </asp:Label>
            <telerik:RadTextBox ID="m_txtNote" runat="server" Columns="35" TextMode="MultiLine" Rows="5"></telerik:RadTextBox>
        </div>
    </fieldset>
    <div class="button-row action">
        <asp:linkButton ID="m_btnSubmit" runat="server" Text="Submit Notes" ValidationGroup="Notes" OnClick="OnSubmit" CssClass="button primaryAction" />
    </div>
</telerik:RadAjaxPanel>

<script type="text/javascript">
function refreshRequestNotes() {
    <asp:literal id="litJSCall" runat="server" />
}
</script>