<%@ Page Title="Speedy Spots :: Edit Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="edit-request.aspx.cs" Inherits="SpeedySpots.edit_request" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script language="javascript" type="text/javascript">
    function OnRushOrder(sender, bDisplayNotes)
    {
        if(sender.checked)
        {
            if(bDisplayNotes)
            {
                document.getElementById("m_pRushOrderNotes").style.visibility = "visible";
            }
            else
            {
                document.getElementById("m_pRushOrderNotes").style.visibility = "hidden";
            }
        }
        else
        {
            document.getElementById("m_pRushOrderNotes").style.visibility = "hidden";
        }
    }
</script>
</telerik:RadCodeBlock>

<div class="full">
    <p class="breadcrumb"><asp:HyperLink ID="hlDashboardBreadcrumb" runat="server" Text="Dashboard" /> &raquo; <strong>Edit Request</strong></p>
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
    <%=ApplicationContext.GetOutOfOfficeNotification() %>
    </telerik:RadCodeBlock>
    <h2>Edit Request</h2>
    <div class="form-holder">
        <fieldset>
            <legend>Account Information:</legend>
            <div class="group">
                <label><span class="required">Username:</span></label>
                <asp:Label id="m_lblUserName" runat="server"></asp:Label>
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
                        <telerik:RadTextBox ID="m_txtContactPhone" runat="server" Columns="15"></telerik:RadTextBox>
                        <asp:RegularExpressionValidator ID="m_expContactPhone" runat="server" ControlToValidate="m_txtContactPhone" ValidationExpression="^\d{3}-\d{3}-\d{4}$" ErrorMessage="Contact Phone must be a valid phone number 123-123-1234.">*</asp:RegularExpressionValidator>
                        <p class="note">Example: 555-555-1212</p>
                    </div>
                    <div class="note-holder">
                        <asp:Label ID="m_lblPhoneExt" runat="server" Text="Ext:" AssociatedControlID="m_txtContactPhoneExtension"></asp:Label>
                        <telerik:RadTextBox ID="m_txtContactPhoneExtension" runat="server" Columns="5" MaxLength="5"></telerik:RadTextBox>
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
                <div class="note-holder">
                    <telerik:RadTextBox ID="m_txtNotificationEmails" Columns="100" runat="server" MaxLength="400"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="m_reqNotificationEmails" runat="server" ControlToValidate="m_txtNotificationEmails" ErrorMessage="Please enter at least one notification email address.">*</asp:RequiredFieldValidator>
                    <p class="note">Separate emails with commas.</p>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="form-holder">
        <fieldset>
            <legend>Scripts &amp; Production Elements:</legend>
            <div class="group hasnotes">
                <asp:Label ID="m_lblScriptFile" runat="server" Text="<span class=required>Upload Files:</span>" AssociatedControlID="m_oUpload"></asp:Label>
                <div class="inputgroup">
                    <div class="note-holder">
                        <telerik:RadUpload ID="m_oUpload" runat="server" InputSize="56" Width="750px" ControlObjectsVisibility="AddButton,RemoveButtons"></telerik:RadUpload>
                        <p class="note">Upload your script(s) and any other production elements.</p>
                    </div>
                </div>
            </div>

            <div class="group" id="divFilesList" runat="server">
                <label>Uploaded Files:</label>
                <span class="output">
                    <asp:Repeater ID="m_oRepeaterFiles" runat="server">
                        <ItemTemplate>
                            <a href="download.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IARequestFileID") %>&type=request"><%#DataBinder.Eval(Container.DataItem, "Filename") %></a><br />
                        </ItemTemplate>
                    </asp:Repeater>
                </span>
            </div>

            <div class="group">
                <p style="font-size: 1.25em; text-align: center; background: #ffc; padding-bottom: 5px; -webkit-border-radius: 5px; -moz-border-radius: 5px; border-radius: 5px;">If you upload your script in the field above, you do not need to type/paste it in the field below.</p>
            </div>

            <div class="group">
                <asp:Label ID="m_lblScript" runat="server" Text="Type/Paste Your Script:" AssociatedControlID="m_txtScript"></asp:Label>
                <div class="inputgroup">
                    <telerik:RadEditor ID="m_txtScript" runat="server" Width="621px" AutoResizeHeight="false" EditModes="Design">
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
                    <li>Additional production?<br />eg. Music &amp; Sound Effects</li>
                    <li>Spot title(s) if needed for invoice</li>
                    <li>Job #s if needed for invoice</li>
                </ul>
                <p><strong>Reminder:</strong> <a href="javascript:popUp('http://www.speedyspots.com/pages/talentShowcase.aspx');">Check Talent Availability (opens in a new window)</a> before submitting your spot.</p>
            </div>

            <div class="hasinlinenote">
                <asp:Label ID="m_lblNotesOnFile" runat="server" Text="<span>Production Notes:</span>" AssociatedControlID="m_txtProductionNotes"></asp:Label>
                <div class="note-holder">
                    <telerik:RadEditor ID="m_txtProductionNotes" runat="server" Width="60%" Height="250px" AutoResizeHeight="false" EditModes="Design">
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
                        <asp:RadioButton ID="m_rad24Hour" runat="server" GroupName="Rush" Text="Standard Delivery*" Checked onclick="OnRushOrder(this, false);" />
                        <asp:RadioButton ID="m_radSameDay" runat="server" GroupName="Rush" Text="Same Day" onclick="OnRushOrder(this, true);" />
                        <p>*All requests received by 7pm EST M-F will be delivered by 9am the next business morning.</p>
                        <p id="m_pRushOrderNotes" class="note">Please indicate your exact due date and time in the production notes above.</p>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>

    <div class="button-row">
        <asp:LinkButton ID="m_btnSubmit" runat="server" Text="Save My Request" CssClass="button primaryAction" OnClick="OnSubmit" />
    </div>
</div>
</asp:Content>
