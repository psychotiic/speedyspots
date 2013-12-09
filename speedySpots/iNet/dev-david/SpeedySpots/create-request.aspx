<%@ Page Title="Speedy Spots :: Create Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="create-request.aspx.cs" Inherits="SpeedySpots.create_request" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
   <link rel="stylesheet" type="text/css" href="css/jquery.flexbox.css" />
   <script type="text/javascript" src="js/jquery.flexbox.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

   <telerik:RadAjaxManagerProxy ID="m_oAjaxManagerProxy" runat="server">
      <AjaxSettings>
         <telerik:AjaxSetting AjaxControlID="m_oAjaxManagerProxy">
            <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="m_lblUserName" LoadingPanelID="m_pnlLoading" />
               <telerik:AjaxUpdatedControl ControlID="m_lblCompanyName" LoadingPanelID="m_pnlLoading" />
               <telerik:AjaxUpdatedControl ControlID="m_txtNotificationEmails" LoadingPanelID="m_pnlLoading" />
               <telerik:AjaxUpdatedControl ControlID="m_divBeginProduction" LoadingPanelID="m_pnlLoading" />
               <telerik:AjaxUpdatedControl ControlID="m_radContactPhone" LoadingPanelID="m_pnlLoading" />
               <telerik:AjaxUpdatedControl ControlID="paymentpreapprovalDiv" LoadingPanelID="m_pnlLoading" />
            </UpdatedControls>
         </telerik:AjaxSetting>
      </AjaxSettings>
   </telerik:RadAjaxManagerProxy>

   <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
      <script type="text/javascript">
         $(function () {
            // Like Google Suggest (hide the arrow)
            $('#ffb2').flexbox("ajax-customer-lookup.aspx", {
               showArrow: false,
               width: 600,
               queryDelay: 1000,
               minChars: 3,
               onSelect: function () {
                  var oAjaxManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
                  oAjaxManager.ajaxRequest($("#ffb2_hidden").val());
               }
            });

            OnRushOrder($("#<%=m_radSameDay.ClientID %>"));

            var sendEstimateRadio = $('#Master_m_oContent_m_radBeginProduction_1');
            if (sendEstimateRadio) {
               sendEstimateRadio.click(function () {
                  $('#<%= paymentpreapprovalDiv.ClientID %>').hide();
               });
            }

            var beingProductionRadio = $('#Master_m_oContent_m_radBeginProduction_0');
            if (beingProductionRadio) {
               beingProductionRadio.click(function () {
                  $('#<%= paymentpreapprovalDiv.ClientID %>').show();
               });
            }

            $('#<%= PaymentSourceCombo.ClientID%>').hide();
         });

         function paymentPreApprovalClicked(sender, args) {
            if (sender._checked) {
               $('#<%= PaymentSourceCombo.ClientID%>').show();
            } else {
               $('#<%= PaymentSourceCombo.ClientID%>').hide();
            }
         }

         function OnRushOrder(sender, bDisplayNotes) {
            if (sender.checked) {
               if (bDisplayNotes) {
                  $("#m_pRushOrderNotes").show();
                  return;
               }
            }
            $("#m_pRushOrderNotes").hide();
         }

         function OnPaste(sender, args) {
            args.set_value(CleanHtml(args.get_value()));
         }

         function FlipSubmitButton() {
            var isValid = Page_ClientValidate('');

            if (isValid) {
               var createButton = $('#<%=m_btnSubmit.ClientID %>');

               if ($.browser.msie && parseFloat(jQuery.browser.version) > 8.0) {
                  createButton.attr('disabled', 'disabled');
               }
               createButton.html('Sending....');
               createButton.removeClass('primaryAction');
            }

            return isValid;
         }
      </script>
   </telerik:RadCodeBlock>

   <div class="full">
      <p class="breadcrumb"><a href="user-dashboard.aspx">Dashboard</a> &raquo; <strong>Create New Request</strong></p>
      <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
         <%=ApplicationContext.GetOutOfOfficeNotification() %>
      </telerik:RadCodeBlock>
      <asp:HiddenField ID="m_hdnPageLoadTime" runat="server" />
      <h2>Create a Request</h2>
      <div class="form-holder">
         <fieldset>
            <legend>Account Information:</legend>
            <div id="m_divLookup" runat="server" class="group" Visible="false">
               <label><span class="required">Submitted By:</span></label>
               <div class="output">
                  <div id="ffb2"></div>
               </div>
            </div>
            <div class="group">
               <label><span class="required">Username:</span></label>
               <asp:Label ID="m_lblUserName" runat="server"></asp:Label>
            </div>
            <div id="m_divCompanyName" runat="server" class="group" Visible="false">
               <label><span class="required">Company Name:</span></label>
               <asp:Label ID="m_lblCompanyName" runat="server"></asp:Label>
            </div>
            <div class="group nospace hasnotes">
               <asp:Label ID="m_lblPhoneChoice" runat="server" Text="<span class=required>Contact Phone:</span>" AssociatedControlID="m_radContactPhone"></asp:Label>
               <div class="inputgroup">
                  <asp:RadioButtonList ID="m_radContactPhone" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow"></asp:RadioButtonList>
                  <asp:RequiredFieldValidator ID="m_reqContactPhone" runat="server" ControlToValidate="m_radContactPhone" ErrorMessage="Please select the contact phone number.">*</asp:RequiredFieldValidator>
               </div>
            </div>
            <div class="group">
               <label>Other Phone:</label>
               <div class="inputgroup nospace">
                  <div class="note-holder">
                     <telerik:RadTextBox ID="m_txtContactPhone" runat="server" MaxLength="13" Columns="15" Width="125px"></telerik:RadTextBox>
                     <asp:RegularExpressionValidator ID="m_expContactPhone" runat="server" ControlToValidate="m_txtContactPhone" ValidationExpression="^\d{3}-\d{3}-\d{4}$" ErrorMessage="Contact Phone must be a valid phone number 123-123-1234.">*</asp:RegularExpressionValidator>
                     <p class="note">Example: 555-555-1212</p>
                  </div>
                  <div class="note-holder">
                     <asp:Label ID="m_lblPhoneExt" runat="server" Text="Ext:" AssociatedControlID="m_txtContactPhoneExtension"></asp:Label>
                     <telerik:RadTextBox ID="m_txtContactPhoneExtension" runat="server" Columns="5" MaxLength="5" Width="65px"></telerik:RadTextBox>
                     <asp:RegularExpressionValidator ID="m_expContactPhoneExtension" runat="server" ControlToValidate="m_txtContactPhoneExtension" ValidationExpression="^\d{0,5}$" ErrorMessage="Contact Phone Extension may be up to 5 digits.">*</asp:RegularExpressionValidator>
                  </div>
               </div>
            </div>
         </fieldset>
      </div>

      <div class="form-holder">
         <fieldset>
            <legend>Spot Notification(s):</legend>
            <p>We will send a notification to the following account email addresses when the spot is complete.</p>
            <div class="group">
               <label><span class="required">Email(s):</span></label>
               <div class="inputgroup nospace">
                  <div class="note-holder">
                     <telerik:RadTextBox ID="m_txtNotificationEmails" Columns="100" runat="server" MaxLength="400" Width="675px"></telerik:RadTextBox>
                     <asp:RequiredFieldValidator ID="m_reqNotificationEmails" runat="server" ControlToValidate="m_txtNotificationEmails" ErrorMessage="Please enter at least one notification email address.">*</asp:RequiredFieldValidator>
                     <p class="note">Separate emails with commas.</p>
                  </div>
               </div>
            </div>
         </fieldset>
      </div>
      <div class="form-holder">
         <fieldset>
            <legend>Scripts &amp; Production Elements:</legend>
            <div class="group hasnotes" id="m_divNoIOSUpload" runat="server">
               <asp:Label ID="m_lblScriptFile" runat="server" Text="<span class=required>Upload Files:</span>" AssociatedControlID="m_oUpload"></asp:Label>
               <div class="inputgroup">
                  <div class="note-holder">
                     <telerik:RadUpload ID="m_oUpload" runat="server" InputSize="56" Width="750px" ControlObjectsVisibility="AddButton,RemoveButtons"></telerik:RadUpload>
                     <p class="note">Upload your script(s) and any other production elements.</p>
                  </div>
               </div>
            </div>

            <div class="group">
               <p style="font-size: 1.25em; text-align: center; background: #ffc; padding-bottom: 5px; -webkit-border-radius: 5px; -moz-border-radius: 5px; border-radius: 5px;">If you upload your script in the field above, you do not need to type/paste it in the field below.</p>
            </div>

            <div class="group">
               <asp:Label ID="m_lblScript" runat="server" Text="Type/Paste Your Script:" AssociatedControlID="m_txtScript"></asp:Label>
               <div class="inputgroup">
                  <asp:TextBox ID="m_txtIScript" runat="server" Width="100%" Rows="10" Columns="60" TextMode="MultiLine" Visible="false"></asp:TextBox>
                  <telerik:RadEditor ID="m_txtScript" runat="server" Width="100%" AutoResizeHeight="false" EditModes="Design" StripFormattingOptions="MSWordRemoveAll" OnClientPasteHtml="OnPaste">
                     <Tools>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Bold" />
                           <telerik:EditorTool Name="Italic" />
                           <telerik:EditorTool Name="Underline" />
                           <telerik:EditorTool Name="StrikeThrough" />
                           <telerik:EditorTool Name="ForeColor" Text="Text color" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Undo" />
                           <telerik:EditorTool Name="Redo" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Paste" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="ToggleScreenMode" />
                        </telerik:EditorToolGroup>
                     </Tools>
                  </telerik:RadEditor>
               </div>
            </div>

            <div class="inlinenote">
               <h3>Not sure what to include?</h3>
               <ul>
                  <li>Talent choice(s)</li>
                  <li>Length of read(s)</li>
                  <li>Due date & time</li>
                  <li>Type of read and pronunciation notes</li>
                  <li>Additional production?<br />
                     eg. Music &amp; Sound Effects</li>
                  <li>Spot title(s) if needed for invoice</li>
                  <li>Job #s if needed for invoice</li>
               </ul>
               <p><strong>Reminder:</strong> <a href="javascript:popUp('http://www.speedyspots.com/pages/talentShowcase.aspx');">Check Talent Availability (opens in a new window)</a> before submitting your spot.</p>
            </div>

            <div class="hasinlinenote">
               <asp:Label ID="m_lblNotesOnFile" runat="server" Text="<span>Production Notes:</span>" AssociatedControlID="m_txtProductionNotes"></asp:Label>
               <div class="note-holder">
                  <asp:TextBox ID="m_txtIProductionNotes" runat="server" Width="60%" Rows="10" Columns="60" TextMode="MultiLine" Visible="false"></asp:TextBox>
                  <telerik:RadEditor ID="m_txtProductionNotes" runat="server" Width="60%" Height="250px" AutoResizeHeight="false" EditModes="Design" StripFormattingOptions="MSWordRemoveAll" OnClientPasteHtml="OnPaste">
                     <Tools>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Bold" />
                           <telerik:EditorTool Name="Italic" />
                           <telerik:EditorTool Name="Underline" />
                           <telerik:EditorTool Name="StrikeThrough" />
                           <telerik:EditorTool Name="ForeColor" Text="Text color" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Undo" />
                           <telerik:EditorTool Name="Redo" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="Paste" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                           <telerik:EditorTool Name="ToggleScreenMode" />
                        </telerik:EditorToolGroup>
                     </Tools>
                  </telerik:RadEditor>
                  <p class="note"><a href="javascript:popUp('phonetic-tips.aspx');">Phonetic Tips!</a> | <a href="javascript:popUp('music.aspx');">Music library</a></p>
               </div>
            </div>

            <div class="group">
               <asp:Label ID="m_lblRushOrder" runat="server" Text="<span class=required>Needed By:</span>" AssociatedControlID="m_radSameDay" />
               <div class="inputgroup">
                  <div class="note-holder">
                     <asp:RadioButton ID="m_rad24Hour" runat="server" GroupName="Rush" Text="Standard Delivery*" Checked="true" onclick="OnRushOrder(this, false);" />
                     <asp:RadioButton ID="m_radSameDay" runat="server" GroupName="Rush" Text="Same Day" onclick="OnRushOrder(this, true);" />
                     <p>*All requests received by 7pm EST M-F will be delivered by 9am the next business morning.</p>
                     <p id="m_pRushOrderNotes" class="importantnote">Please indicate your exact due date and time in the production notes above.</p>
                  </div>
               </div>
            </div>

            <div id="m_divBeginProduction" runat="server" class="group">
               <asp:Label ID="m_lblBeginProduction" runat="server" Text="<span class=required>Begin Production:</span>" AssociatedControlID="m_radBeginProduction"></asp:Label>
               <div class="inputgroup nospace">
                  <asp:RadioButtonList ID="m_radBeginProduction" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                     <asp:ListItem Value="immediately" Selected="True">Begin production immediately.</asp:ListItem>
                     <asp:ListItem Value="estimate">Send an estimate to approve before beginning production.</asp:ListItem>
                  </asp:RadioButtonList>
                  <asp:RequiredFieldValidator ID="m_reqBeginProduction" runat="server" ControlToValidate="m_radBeginProduction" ErrorMessage="Please select how we should begin production.">*</asp:RequiredFieldValidator>
               </div>
            </div>

            <div id="paymentpreapprovalDiv" visible="false" runat="server" class="group">
               <asp:Label ID="PaymentPreApprovalLabel" AssociatedControlID="PaymentPreApprovalEstimateButton" Text="Payment Pre-Approval:" Class="required" runat="server"></asp:Label>
               <div class="inputgroup nospace">
                  <telerik:RadButton runat="server"
                     ID="PaymentPreApprovalEstimateButton"
                     AutoPostBack="False"
                     Value="prepay"
                     ToggleType="CheckBox"
                     ButtonType="ToggleButton"
                     Text="I authorize the use of the selected credit card for payment of this request."
                     OnClientClicked="paymentPreApprovalClicked">
                  </telerik:RadButton>
               </div>

               <asp:Label ID="Label2" runat="server" AssociatedControlID="PaymentSourceCombo" Text="&nbsp;"></asp:Label>
               <div class="inputgroup nospace">
                  <div class="inputgroup nospace">
                     <telerik:RadComboBox ID="PaymentSourceCombo" Width="250px" LoadingMessage="Please wait" runat="server">
                     </telerik:RadComboBox>
                  </div>
               </div>
            </div>

         </fieldset>
      </div>

      <p><strong>Important:</strong> Once you have submitted your request, you will only be able to cancel your request from the website until a producer starts working on it. You may still cancel your request after that point by contacting us by phone or email.</p>

      <div class="button-row">
         <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Submit My Request" CssClass="button primaryAction" OnClick="OnSubmit" OnClientClick="return FlipSubmitButton();" />
      </div>
   </div>
</asp:Content>
