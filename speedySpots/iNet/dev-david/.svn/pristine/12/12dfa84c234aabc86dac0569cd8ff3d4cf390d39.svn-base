<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotDetails.ascx.cs" Inherits="SpeedySpots.Controls.SpotDetails" %>
<style type="text/css">
.pagebreak hr 
{
	display: none;
}

.pagebreak 
{
	display: block;
	height: 30px;
	background: url("img/bg-pagebreak.png") center center no-repeat;
}

.InsertPageBreak {
    background-position: -2045px center;
}
</style>

<script language="javascript" type="text/javascript">
function OpenPreview(id) {
    window.open('pageBreakPreview.aspx?id=' + id, 'windowname', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=1024,height=768');
}

function OnValidateFees() {
    var bResult = true;
    var sQuestion = "";

    $("span[id$=m_spanFee]").each(function (index) {
        var oValue = $(this).find("input[type=text]").val();
        var oType = $(this).find("select option:selected").text();

        if(oValue == "" || oValue == "$0.00") {
            sQuestion += "There is no fee listed for " + oType + ".\r\n";
        }
    });

    if(sQuestion != "") {
        return ConfirmUser(sQuestion + "\r\nAre you sure you want to proceed?");
    } else {
        return true;
    }
}
</script>

<div class="group">
    <p class="breadcrumb"><asp:HyperLink ID="m_lnkDashboard" runat="server" Text="Dashboard" /> &raquo; <asp:HyperLink ID="m_lnkJobs" runat="server" Text="Jobs" /> &raquo; PO & Spots</p>

        <div id="m_divMessage" class="message positive" runat="server">Spot saved.</div>

	    <div class="orderinput">
        <div class="requestheader">
            <telerik:RadCodeBlock ID="m_oCodeBlock2" runat="server">
                <h2><%=IAJob.Name %> Job ID: <%=IAJob.JobIDForDisplay %></h2>
            </telerik:RadCodeBlock>
        </div>

        <div id="m_divMainButtons" runat="server" class="button-row action">
            <asp:HyperLink ID="m_lnkCancelProductionOrder" runat="server" Text="Back to jobs" CssClass="button" />
            <asp:LinkButton ID="m_btnDelete" runat="server" Text="Delete Production Order" CausesValidation="false" CssClass="button negativeAction" OnClick="OnDelete" OnClientClick="return ConfirmUser('Are you sure you want to delete this production order?');" />
        </div>

        <speedyspots:TalentAssignment id="m_oTalentAssignment" runat="server"  OnAssignTalent="OnTalentAssigned" />

        <div id="m_divSpots" runat="server">
            <h3>Spots</h3>

            <div class="RadGrid_test nontel">
            <table width="100%">
            <asp:Repeater ID="m_listSpots" runat="server">
                <HeaderTemplate>
                    <thead>
                        <tr>
                            <th class="rgHeader">Title</th>
                            <th class="rgHeader">Due Date/Time</th>
                            <th class="rgHeader">&nbsp;</th>
                            <th class="rgHeader">&nbsp;</th>
                        </tr>
                    </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><asp:LinkButton ID="m_btnTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' CausesValidation="false" OnClick="OnSpotCommand" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IASpotID") %>'></asp:LinkButton></td>
                        <td><%# string.Format("{0:M/dd/yyyy a\\t h:mm tt}", DataBinder.Eval(Container.DataItem, "DueDateTime")) %></td>
                        <td><asp:LinkButton ID="m_btnDuplicate" runat="server" Text='Duplicate' CausesValidation="false" OnClick="OnSpotCommand" CommandName="Duplicate" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IASpotID") %>'></asp:LinkButton></td>
                        <td><asp:LinkButton ID="m_btnDelete" runat="server" Text='Delete' CausesValidation="false" OnClick="OnSpotCommand" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IASpotID") %>' OnClientClick="return ConfirmUser('Are you sure you want to delete this spot?');"></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>

            <div>
                <div class="form-holder">
                    <fieldset>
                        <legend>Spot Information:</legend>
                        <div class="group">
                            <asp:Label ID="m_lblPONumber" runat="server" AssociatedControlID="m_txtPurchaseOrderNumber">
                                <asp:RegularExpressionValidator ID="m_expPurchaseOrderNumber" runat="server" ControlToValidate="m_txtPurchaseOrderNumber" ValidationExpression="^.{0,50}$" ErrorMessage="Purchase order number may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                                Purchase Order #:
                            </asp:Label>
                            <telerik:RadTextBox ID="m_txtPurchaseOrderNumber" runat="server" Columns="50" MaxLength="50"></telerik:RadTextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblSpotDueDate" runat="server"  AssociatedControlID="m_dtDueDateTime">
                                <asp:RequiredFieldValidator ID="m_reqDueDateTime" runat="server" ControlToValidate="m_dtDueDateTime" ErrorMessage="Due date/time is required.">*</asp:RequiredFieldValidator>
                                <span class="required">Due Date/Time:</span>
                            </asp:Label>
                            <div class="inputgroup">
                                <telerik:RadDateTimePicker runat="server" ID="m_dtDueDateTime" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
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
                            <telerik:RadAjaxPanel ID="m_oAjaxPanel" runat="server" LoadingPanelID="m_pnlLoading" EnablePageHeadUpdate="false">
                                <asp:Repeater ID="m_listFees" runat="server">
                                <ItemTemplate>
                                    <span id="m_spanFee">
                                        <label>Talent Fee:</label>
                                        <telerik:RadNumericTextBox ID="m_txtFee" runat="server" Type="Currency" MinValue="0" MaxValue="9999.99"></telerik:RadNumericTextBox>
                                        For
                                        <asp:DropDownList ID="m_cboSpotFeeType" runat="server" Width="200px" />
                                        <asp:LinkButton ID="m_btnDelete" runat="server" CausesValidation="false" Text="Delete" OnClick="OnFeeCommand" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IASpotFeeID") %>' OnClientClick="return ConfirmUser('Are you sure you want to delete this talent fee?');"></asp:LinkButton>
                                        <asp:HiddenField ID="m_txtID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IASpotFeeID") %>' />
                                    </span>
                                </ItemTemplate>
                                <FooterTemplate>
                                <div class="button-row">
                                        <asp:LinkButton ID="m_btnAdd" runat="server" Text="Add Talent Fee" CausesValidation="false" OnClick="OnAddFee" />
                                </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            </telerik:RadAjaxPanel>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblLengthCount" runat="server" AssociatedControlID="m_txtLength">
                                <asp:RequiredFieldValidator ID="m_reqSpotLength" runat="server" ControlToValidate="m_txtLength" ErrorMessage="Spot length is required.">*</asp:RequiredFieldValidator>
                                <span class="required">Spot Length:</span>
                            </asp:Label>
                            <div class="inputgroup">
                                <telerik:RadTextbox ID="m_txtLength" runat="server" Columns="20" MaxLength="20"></telerik:RadTextbox>
                            </div>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblSpotTitle" runat="server" AssociatedControlID="m_txtTitle">
                                <asp:RequiredFieldValidator ID="m_reqSpotTitle" runat="server" ControlToValidate="m_txtTitle" ErrorMessage="Spot title is required.">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="m_expSpotTitle" runat="server" ControlToValidate="m_txtTitle"  ValidationExpression="^.{0,50}$" ErrorMessage="Spot title may contain up to 50 characters.">*</asp:RegularExpressionValidator>
                                <span class="required">Spot Title:</span>
                            </asp:Label>
                            <telerik:RadTextBox ID="m_txtTitle" Columns="50" runat="server" MaxLength="50"></telerik:RadTextBox>
                        </div>
                        <div class="group">
                            <asp:Label ID="m_lblSpotType" runat="server" AssociatedControlID="m_cboSpotType">
                                <asp:RequiredFieldValidator ID="m_reqSpotType" runat="server" ControlToValidate="m_cboSpotType" ErrorMessage="Spot type is required.">*</asp:RequiredFieldValidator>
                                <span class="required">Spot Type:</span>
                            </asp:Label>
                            <asp:DropDownList ID="m_cboSpotType" runat="server" Width="160px" />
                        </div>
                        <div class="group hasnotes">
                            <asp:Label ID="m_lblProductionNotes" runat="server" AssociatedControlID="m_txtProductionNotes">
                                <span>Production Notes:</span>
                            </asp:Label>
                            <div class="inputgroup">
                                <telerik:RadEditor ID="m_txtProductionNotes" runat="server" Width="100%" Height="200px" AutoResizeHeight="false" EditModes="Design,html" StripFormattingOptions="MSWordRemoveAll" OnClientPasteHtml="OnPaste" NewLineBr="false">
                                <RealFontSizes>
                                    <telerik:EditorRealFontSize Value="6pt" />
                                    <telerik:EditorRealFontSize Value="8pt" />
                                    <telerik:EditorRealFontSize Value="9pt" />
                                    <telerik:EditorRealFontSize Value="10pt" />
                                    <telerik:EditorRealFontSize Value="11pt" />
                                    <telerik:EditorRealFontSize Value="12pt" />
                                    <telerik:EditorRealFontSize Value="14pt" />
                                    <telerik:EditorRealFontSize Value="18pt" />
                                    <telerik:EditorRealFontSize Value="24pt" />
                                    <telerik:EditorRealFontSize Value="30pt" />
                                    <telerik:EditorRealFontSize Value="36pt" />
                                    <telerik:EditorRealFontSize Value="48pt" />
                                    <telerik:EditorRealFontSize Value="60pt" />
                                    <telerik:EditorRealFontSize Value="72pt" />
                                </RealFontSizes>
                                    <Tools>
                                        <telerik:EditorToolGroup>
                                            <telerik:EditorTool Name="Bold" />
                                            <telerik:EditorTool Name="Italic" />
                                            <telerik:EditorTool Name="Underline" />
                                            <telerik:EditorTool Name="StrikeThrough" />
                                            <telerik:EditorSeparator Visible="true" />
                                            <telerik:EditorTool Name="FontName" />
                                            <telerik:EditorTool Name="RealFontSize" />
                                            <telerik:EditorDropDown Name="LineSpacing" Text="LineSpacing" PopUpHeight="100px" PopupWidth="110px">  
                                             <telerik:EditorDropDownItem  Name ="Remove" value="" /> 
                                             <telerik:EditorDropDownItem  Name ="Single space" value="1em" /> 
                                             <telerik:EditorDropDownItem  Name ="1.5 Lines" value="1.5em" /> 
                                             <telerik:EditorDropDownItem  Name ="Double spacing" value="2em" /> 
                                            </telerik:EditorDropDown>
                                            <telerik:EditorTool Name="ForeColor" Text="Text color" />
                                            <telerik:EditorTool Name="BackColor" Text="Highlight" />
                                            <telerik:EditorSeparator Visible="true" />
                                            <telerik:EditorTool Name="ToggleScreenMode" />
                                        </telerik:EditorToolGroup>
                                        <telerik:EditorToolGroup>
                                            <telerik:EditorTool Name="Undo" />
                                            <telerik:EditorTool Name="Redo" />
                                        </telerik:EditorToolGroup>
                                    </Tools>
                                </telerik:RadEditor>
                            </div>
                        </div>

                        <h3>Script:</h3>

                            <telerik:RadEditor ID="m_txtScript" runat="server" Width="100%" AutoResizeHeight="False" EditModes="Design,html" StripFormattingOptions="MSWordRemoveAll" OnClientPasteHtml="OnPaste" NewLineBr="false">
                            <RealFontSizes>
                                <telerik:EditorRealFontSize Value="6pt" />
                                <telerik:EditorRealFontSize Value="8pt" />
                                <telerik:EditorRealFontSize Value="9pt" />
                                <telerik:EditorRealFontSize Value="10pt" />
                                <telerik:EditorRealFontSize Value="11pt" />
                                <telerik:EditorRealFontSize Value="12pt" />
                                <telerik:EditorRealFontSize Value="14pt" />
                                <telerik:EditorRealFontSize Value="18pt" />
                                <telerik:EditorRealFontSize Value="24pt" />
                                <telerik:EditorRealFontSize Value="30pt" />
                                <telerik:EditorRealFontSize Value="36pt" />
                                <telerik:EditorRealFontSize Value="48pt" />
                                <telerik:EditorRealFontSize Value="60pt" />
                                <telerik:EditorRealFontSize Value="72pt" />
                            </RealFontSizes>
                                <Tools>
                                    <telerik:EditorToolGroup>
                                        <telerik:EditorTool Name="Bold" />
                                        <telerik:EditorTool Name="Italic" />
                                        <telerik:EditorTool Name="Underline" />
                                        <telerik:EditorTool Name="StrikeThrough" />
                                        <telerik:EditorSeparator Visible="true" />
                                        <telerik:EditorTool Name="FontName" />
                                        <telerik:EditorTool Name="RealFontSize" />
                                        <telerik:EditorDropDown Name="LineSpacing" Text="LineSpacing" PopUpHeight="90px" PopupWidth="110px">  
                                             <telerik:EditorDropDownItem  Name ="Remove" value="" /> 
                                             <telerik:EditorDropDownItem  Name ="Single space" value="1em" /> 
                                             <telerik:EditorDropDownItem  Name ="1.5 Lines" value="1.5em" /> 
                                             <telerik:EditorDropDownItem  Name ="Double spacing" value="2em" /> 
                                        </telerik:EditorDropDown>
                                        <telerik:EditorTool Name="ForeColor" Text="Text color" />
                                        <telerik:EditorTool Name="BackColor" Text="Highlight" />
                                        <telerik:EditorSeparator Visible="true" />
                                        <telerik:EditorTool Name="JustifyLeft" />
                                        <telerik:EditorTool Name="JustifyCenter" />
                                        <telerik:EditorTool Name="JustifyRight" />
                                        <telerik:EditorSeparator Visible="true" />
                                        <telerik:EditorTool Name="InsertPageBreak" Text="Insert Page Break" />
                                        <telerik:EditorTool Name="Indent" />
                                        <telerik:EditorTool Name="Outdent" />
                                        <telerik:EditorSeparator Visible="true" />
                                        <telerik:EditorTool Name="ToggleScreenMode" />
                                    </telerik:EditorToolGroup>
                                    <telerik:EditorToolGroup>
                                        <telerik:EditorTool Name="Undo" />
                                        <telerik:EditorTool Name="Redo" />
                                    </telerik:EditorToolGroup>
                                </Tools>
                            </telerik:RadEditor>

                            <script type="text/javascript">
                                Telerik.Web.UI.Editor.CommandList["InsertPageBreak"] = function (commandName, editor, args)
                                {
                                    editor.pasteHtml('<div class="pagebreak"><hr /></div>');
                                };

                                Telerik.Web.UI.Editor.CommandList["LineSpacing"] = function (commandName, editor, args) {
                                    oValue = args.value;
                                    var selection = editor.getSelection();
                                    var htmlValue = selection.GetHtmlText();

                                    var oSelElem = editor.getSelection().getParentElement();
                                    if (oSelElem.tagName == "P") {
                                        oSelElem.style.lineHeight = oValue;
                                    } else {
                                        var newHtml = SetLineHeight(htmlValue, oValue);
                                        editor.set_html(SetLineHeight(htmlValue, oValue));
                                    }
                                };

                                function SetLineHeight(htmlValue, oHeightValue) {
                                    var _lineSpacingHelper = $('#LineSpacingHelper');
                                    _lineSpacingHelper.html(htmlValue);

                                    var doubleBlah = _lineSpacingHelper.find('p');

                                    _lineSpacingHelper.find('p').each(function () {
                                        if ($(this).html().length === 0) {
                                            $(this).remove();
                                        }
                                    });
                                    
                                    _lineSpacingHelper.find('p').attr('style', 'line-height:' + oHeightValue + ';');

                                    var newHtml = _lineSpacingHelper.html();
                                    _lineSpacingHelper.html('');

                                    return newHtml;
                                }

                                function OnPaste(sender, args) {
                                    args.set_value(CleanHtml(args.get_value()));
                                }
                            </script>
                            <div style="display:none;" id="LineSpacingHelper"></div>
                            <asp:RequiredFieldValidator ID="m_reqScript" runat="server" ControlToValidate="m_txtScript" ErrorMessage="Script is required.">*</asp:RequiredFieldValidator>
                    </fieldset>
                </div>
            </div>

            <div>
                <h1>Spot Files from Producer</h1>
                <table width="100%">
                <asp:Repeater ID="m_listFiles" runat="server">
                    <HeaderTemplate>
                        <thead>
                            <td><strong>Filename</strong></td>
                            <td>&nbsp;</td>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><a href='download.aspx?id=<%# DataBinder.Eval(Container.DataItem, "IASpotFileID") %>&type=spot'><%# DataBinder.Eval(Container.DataItem, "Filename") %></a></td>
                            <td><asp:LinkButton ID="m_btnDelete" runat="server" Text='Delete' CausesValidation="false" OnClick="OnFilesCommand" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IASpotFileID") %>' OnClientClick="return ConfirmUser('Are you sure you want to delete this file?');"></asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>

                <br />

                <div class="form-holder">
                    <fieldset>
                        <legend>Add Reference Files:</legend>
                        <div class="group">
                            <asp:Label ID="m_lblTalentFile" runat="server" Text="Files for Talent:" AssociatedControlID="m_oUploadProducer"></asp:Label>
                            <div class="inputgroup">
                                <telerik:RadUpload ID="m_oUploadProducer" runat="server" ControlObjectsVisibility="AddButton,RemoveButtons"></telerik:RadUpload>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>

            <div class="button-row" id="m_divSpotButtons" runat="server">
                <asp:LinkButton ID="m_btnSaveSpot" runat="server" Text="Save Spot" CssClass="button primaryAction" OnClientClick="return OnValidateFees();" OnClick="OnSaveSpot" />
                <asp:LinkButton ID="m_btnSavePreivewSpot" runat="server" Text="Save/Preview Spot" CssClass="button" OnClientClick="return OnValidateFees();" OnClick="OnSavePreviewSpot" />
            </div>
        </div>
    </div>
</div>