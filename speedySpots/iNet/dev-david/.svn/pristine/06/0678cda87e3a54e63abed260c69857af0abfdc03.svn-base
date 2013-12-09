<%@ Page Title="Speedy Spots :: Create Estimate" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="create-estimate.aspx.cs" Inherits="SpeedySpots.create_estimate" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
   <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
      <AjaxSettings>
         <telerik:AjaxSetting AjaxControlID="m_cboEmailTemplate">
            <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="m_cboEmailTemplate" />
               <telerik:AjaxUpdatedControl ControlID="m_txtBody" />
            </UpdatedControls>
         </telerik:AjaxSetting>
      </AjaxSettings>
   </telerik:RadAjaxManagerProxy>

   <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
      <script language="javascript" type="text/javascript">
         $(function() {
            var approvalList = $('#<%= m_radCharge.ClientID %>');
            var divCharge = $("#<%=m_divCharge.ClientID %>");
            var divPaymentSource = $("#<%=paymentSourceGroup.ClientID%>");
            var reqCharge = document.getElementById("<%=m_reqCharge.ClientID %>");
            var button = $('#<%= m_btnSubmit.ClientID %>');

            approvalList.click(function(el) {
               if (el.toElement.value === 'RequiresPayment') {
                  ValidatorEnable(reqCharge, true);
                  divCharge.show();
                  divPaymentSource.hide();
                  button.text('Send Estimate');
               } else if (el.toElement.value === 'PrePay') {
                  ValidatorEnable(reqCharge, true);
                  divCharge.show();
                  divPaymentSource.show();
                  button.text('Pay Now');
               } else {
                  ValidatorEnable(reqCharge, false);
                  divCharge.hide();
                  divPaymentSource.hide();
                  button.text('Send Estimate');
               }
            });
         });
      </script>
   </telerik:RadCodeBlock>

   <div class="main">
      <p class="breadcrumb">
         <a href="Default.aspx">Dashboard</a> &raquo;
         <asp:HyperLink ID="hlBreadcrumbRequest" runat="server" NavigateUrl="~/create-job.aspx?rid={0}" Text="Request" />
         &raquo; <strong>Create Estimate</strong>
      </p>

      <telerik:RadCodeBlock ID="m_oCodeBlocks" runat="server">
         <h2><%=MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(IARequest.MPUserID)) %> Request No: <%=IARequest.RequestIdForDisplay %></h2>
         <p><strong>Status:</strong> <%=IARequest.IARequestStatus.Name %></p>
      </telerik:RadCodeBlock>

      <div class="form-holder">
         <fieldset>
            <legend>Estimate Details:</legend>
            <div class="group">
               <asp:Label ID="m_lblRecipientEmail" runat="server" AssociatedControlID="m_txtRecipientEmail">
                  <asp:RequiredFieldValidator ID="m_reqRecipientEmail" runat="server" ControlToValidate="m_txtRecipientEmail" ErrorMessage="Please enter the recipient's email address.">*</asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="m_expRecipientEmail" runat="server" ControlToValidate="m_txtRecipientEmail" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4})$" ErrorMessage="Recipient email must be a valid email address.">*</asp:RegularExpressionValidator>
                  <span class="required">To:</span>
               </asp:Label>
               <telerik:RadTextBox ID="m_txtRecipientEmail" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
            <div class="group">
               <asp:Label ID="m_lblCC" runat="server" AssociatedControlID="m_txtRecipientCCEmails">
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="m_txtRecipientCCEmails" ValidationExpression="^.{0,200}$" ErrorMessage="CC recipients may contain up to 200 characters.">*</asp:RegularExpressionValidator>
                  <span class="required">CC:</span>
               </asp:Label>
               <div class="note-holder">
                  <telerik:RadTextBox ID="m_txtRecipientCCEmails" runat="server" Columns="50" MaxLength="200"></telerik:RadTextBox>
                  <p class="note">Separate emails with commas.</p>
               </div>
            </div>
            <div class="group">
               <asp:Label ID="m_lblSubject" runat="server" AssociatedControlID="m_txtSubject">
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_txtSubject" ErrorMessage="Please enter the subject for the email.">*</asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="m_txtSubject" ValidationExpression="^.{0,50}$" ErrorMessage="Subject may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                  <span class="required">Subject:</span>
               </asp:Label>
               <telerik:RadTextBox ID="m_txtSubject" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
            </div>
            <div class="group">
               <asp:Label ID="Label1" runat="server" AssociatedControlID="m_cboEmailTemplate">
                <span class="required">Email Template:</span>
               </asp:Label>
               <asp:DropDownList ID="m_cboEmailTemplate" runat="server" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="OnChangedEmailTemplate" />
            </div>
         </fieldset>

         <fieldset>
            <telerik:RadEditor ID="m_txtBody" runat="server" Width="100%" EditModes="Design">
               <Tools>
                  <telerik:EditorToolGroup>
                     <telerik:EditorTool Name="ToggleScreenMode" />
                     <telerik:EditorTool Name="PasteStrip" />
                  </telerik:EditorToolGroup>
               </Tools>
            </telerik:RadEditor>
         </fieldset>

         <fieldset>
            <legend>Credit Card Information:</legend>
            <div class="group">
               <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                  <% if (MemberProtect.Utility.YesNoToBool(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(IARequest.MPUserID), "IsPayFirst"))) %>
                  <% { %>
                  <label><span>&nbsp;</span></label>
                  <div class="inputgroup">
                     <strong><i>(NOTE: This company is marked as Pay First)</i></strong>
                  </div>
                  <% } %>
               </telerik:RadCodeBlock>
               <label><span class="required">Approval Action:</span></label>
               <div class="inputgroup">
                  <asp:RadioButtonList ID="m_radCharge" runat="server" RepeatLayout="Flow">
                     <asp:ListItem Value="RequiresPayment" Selected="True" Text="Require payment before beginning production" />
                     <asp:ListItem Value="RequiresApproval" Text="Requires customer approval" />
                     <asp:ListItem Value="JustInformation" Text="Just for customer information" />
                     <asp:ListItem Value="PrePay" Text="Immediately charge customer" />
                  </asp:RadioButtonList>
               </div>
            </div>
            
            <div id="paymentSourceGroup" runat="server" class="group" style="display: none;">
               <asp:Label ID="Label2" runat="server" AssociatedControlID="PaymentSourceCombo" Text="&nbsp;"></asp:Label>
               <div class="inputgroup nospace">
                  <div class="inputgroup nospace">
                     <telerik:RadComboBox ID="PaymentSourceCombo" Width="250px" LoadingMessage="Please wait" runat="server">
                     </telerik:RadComboBox>
                     <p id="paymentSourceNote" class="note" runat="server"></p>
                  </div>
               </div>
            </div>

            <div id="m_divCharge" runat="server" class="group">
               <asp:Label ID="m_lblAmount" runat="server" AssociatedControlID="m_txtCharge">
                  <asp:RequiredFieldValidator ID="m_reqCharge" runat="server" ControlToValidate="m_txtCharge" ErrorMessage="Please enter the charge.">*</asp:RequiredFieldValidator>
                  <span class="required">Amount to Charge:</span>
               </asp:Label>
               <div class="note-holder">
                  <telerik:RadNumericTextBox ID="m_txtCharge" runat="server" MaxValue="99999" Type="Currency"></telerik:RadNumericTextBox>
                  <p class="note">This is the minimum amount required to be paid by the customer.</p>
               </div>
            </div>
         </fieldset>
      </div>

      <div class="button-row">
         <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Send Estimate" CssClass="button primaryAction" OnClick="OnSendEstimate" />
      </div>
   </div>

   <div class="sidebar">
      <speedyspots:SpeedySpotsTabs ID="m_oTabs" runat="server" DefaultView="Producer" ShowRequestDetails="true" ShowRequestNotes="true" />
   </div>
</asp:Content>
