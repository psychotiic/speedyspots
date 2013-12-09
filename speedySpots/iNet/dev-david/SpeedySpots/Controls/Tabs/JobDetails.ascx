<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobDetails.ascx.cs" Inherits="SpeedySpots.Controls.JobDetails" %>

<telerik:RadCodeBlock ID="m_oCodeBlock1" runat="server">
<script language="javascript" type="text/javascript">
function onJobDetailsRequestStart(sender, arguments) {
    if(arguments.EventTarget == "<%=m_btnSave.UniqueID %>") {
        arguments.EnableAjax = false;
    }
}

function OnSaveJobDetails() {
    if(ValidateProductionFees()) {
        var feesChanged = HasProductionFeesChanged();

        if(feesChanged) {
            alert("You have modified the production fees without saving.\nPlease click the Update Job Details button above before completing the job.");
            return false;
        }
    } else {
        return false;
    }

    return true;
}

function ValidateProductionFees() {
    if($("#<%=m_chkMusic.ClientID %>").attr('checked')) {
        var txtMusic = $("#<%=m_txtMusicQuantity.ClientID %>");

        if(txtMusic.val() == "" || txtMusic.val() == "0") {
            alert("Please specify a quantity for the additional music production.");
            return false;
        }
    }

    if($("#<%=m_chkSFX.ClientID %>").attr('checked')) {
        var txtSFX = $("#<%=m_txtSFXQuantity.ClientID %>");

        if(txtSFX.val() == "" || txtSFX.val() == "0") {
            alert("Please specify a quantity for the additional SFX production.");
            return false;
        }
    }

    if($("#<%=m_chkProduction.ClientID %>").attr('checked')) {
        var txtProduction = $("#<%=m_txtProductionQuantity.ClientID %>");

        if(txtProduction.val() == "" || txtProduction.val() == "0") {
            alert("Please specify a quantity for the additional production.");
            return false;
        }
    }

    if($("#<%=m_chkConvert.ClientID %>").attr('checked')) {
        var txtConvert = $("#<%=m_txtConvertQuantity.ClientID %>");

        if(txtConvert.val() == "" || txtConvert.val() == "0") {
            alert("Please specify a quantity for the additional convert production.");
            return false;
        }
    }

    return true;
}

function HasProductionFeesChanged() {
    var hasItemChanged = false;
    var isQCView = <%=(!IsProducerView).ToString().ToLower() %>;

    if (isQCView) {
        $('#productionFees').find('input[data-original]').each(function () {
            var currentItem = $(this);
            var originalVal = currentItem.attr('data-original');
            var currentVal = currentItem.val();

            if (currentItem.attr('type') == 'checkbox') {
                currentVal = (currentItem.is(':checked')) ? 'True' : 'False';
            } else {
                currentVal = parseFloat(currentVal.replace('$', ''));
                originalVal = parseFloat(originalVal);
            }

            if (originalVal != currentVal) {
                hasItemChanged = true;
                return true;
            }
        });
    }

    return hasItemChanged;
}
</script>
</telerik:RadCodeBlock>

<telerik:RadAjaxPanel ID="m_oAjaxPanel" runat="server" ClientEvents-OnRequestStart="onJobDetailsRequestStart" EnablePageHeadUpdate="false">
    <telerik:RadCodeBlock ID="m_oCodeBlock" runat="server">
    <% if(IAJob != null) %>
    <% { %>
        <fieldset>
            <legend>Details:</legend>
            <div class="group">
                <label class="stacked">Due Date:</label>
                <span class="output">
                    <%= string.Format("{0:M/dd/yyyy a\\t h:mm tt}", IAJob.DueDateTime)%>
                    <% if(IAJob.IsASAP) %>
                    <% { %>
                        <span style="color:Red">ASAP</span>
                    <% } %>
                </span>
            </div>
            <div class="group">
                <label class="stacked">Language:</label>
                <span class="output"><%=IAJob.Language %></span>
            </div>
        </fieldset>
    <% } %>
    </telerik:RadCodeBlock>
    <fieldset id="productionFees">
        <legend>Additonal Production:</legend>
        <div class="group sidebaroneoff">
            <div class="inputgroup">
                <asp:CheckBox ID="m_chkMusic" runat="server" Text="Music" />
                <span id="m_spanMusic" runat="server">
                    <telerik:RadNumericTextbox ID="m_txtMusicQuantity" runat="server" Width="20" MaxLength="2" MaxValue="99" Type="Number"><NumberFormat DecimalDigits="0" /></telerik:RadNumericTextbox> @ <telerik:RadNumericTextBox ID="m_txtMusicPrice" runat="server" Width="45"  MaxLength="6" MaxValue="9999.99" Type="Currency"></telerik:RadNumericTextBox>
                </span>
            </div>
        </div>
        <div class="group sidebaroneoff">
            <div class="inputgroup">
                <asp:CheckBox ID="m_chkSFX" runat="server" Text="SFX" />
                <span id="m_spanSFX" runat="server">
                    <telerik:RadNumericTextbox ID="m_txtSFXQuantity" runat="server" Width="20" MaxLength="2" MaxValue="99" Type="Number"><NumberFormat DecimalDigits="0" /></telerik:RadNumericTextbox> @ <telerik:RadNumericTextBox ID="m_txtSFXPrice" runat="server" Width="45" MaxLength="6" MaxValue="9999.99" Type="Currency"></telerik:RadNumericTextBox>
                </span>
            </div>
        </div>
        <div class="group sidebaroneoff">
            <div class="inputgroup">
                <asp:CheckBox ID="m_chkProduction" runat="server" Text="Production" />
                <span id="m_spanProduction" runat="server">
                    <telerik:RadNumericTextbox ID="m_txtProductionQuantity" runat="server" Width="20" MaxLength="2" MaxValue="99" Type="Number"><NumberFormat DecimalDigits="0" /></telerik:RadNumericTextbox> @ <telerik:RadNumericTextBox ID="m_txtProductionPrice" runat="server" Width="45" MaxLength="6" MaxValue="9999.99" Type="Currency"></telerik:RadNumericTextBox>
                </span>
            </div>
        </div>
        <div class="group sidebaroneoff">
            <div class="inputgroup">
                <asp:CheckBox ID="m_chkConvert" runat="server" Text="Convert" />
                <span id="m_spanConvert" runat="server">
                    <telerik:RadNumericTextbox ID="m_txtConvertQuantity" runat="server" Width="20" MaxLength="2" MaxValue="99" Type="Number"><NumberFormat DecimalDigits="0" /></telerik:RadNumericTextbox> @ <telerik:RadNumericTextBox ID="m_txtConvertPrice" runat="server" Width="45" MaxLength="6" MaxValue="9999.99" Type="Currency"></telerik:RadNumericTextBox>
                </span>
            </div>
        </div>
        <div id="m_divNotesEdit" runat="server" class="group sidebaroneoff">
            <asp:Label ID="m_lblNotes" runat="server" Text="Notes for QC:" AssociatedControlID="m_txtNotes" CssClass="stacked"></asp:Label>
            <telerik:RadTextBox ID="m_txtNotes" runat="server" Columns="35" TextMode="MultiLine" Rows="5"></telerik:RadTextBox>
        </div>
        <div id="m_divNotesView" runat="server" class="group sidebaroneoff">
            <label class="required stacked">Notes for QC:</label>
            <div id="m_divNotesForQC" runat="server" class="inputgroup"></div>
        </div>
        <div id="m_divFilesView" runat="server" class="group">
            <label class="required stacked">Files:</label>
            <div id="m_divFiles" runat="server" class="inputgroup"></div>
        </div>
    </fieldset>

    <div id="m_divFilesEdit" runat="server">
        <h3>Additional Files:</h3>
        <telerik:RadGrid ID="m_grdList" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" Width="247" onneeddatasource="OnNeedDataSource" onitemdatabound="OnItemDataBound" onitemcommand="OnItemCommand" Skin="test" EnableEmbeddedSkins="false">
            <MasterTableView NoMasterRecordsText="No files"></MasterTableView>
            <HeaderContextMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </HeaderContextMenu>

            <MasterTableView>
            <SortExpressions>
            <telerik:GridSortExpression FieldName="Filename" SortOrder="Ascending" />
            </SortExpressions>
                <Columns>
                    <telerik:GridBoundColumn DataField="IAJobFileID" UniqueName="IAJobFileID" Visible="False"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Filename" HeaderText="Filename" UniqueName="Filename"></telerik:GridBoundColumn>
                    <telerik:GridButtonColumn Text="Delete" CommandName="Delete" ConfirmText="Are you sure you want to delete this file?" UniqueName="Delete"></telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>

            <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
        </telerik:RadGrid>
        <fieldset>
            <div class="group">
                <asp:Label ID="m_lblProductionElement" runat="server" Text="File:" CssClass="stacked"></asp:Label>
                <telerik:RadUpload ID="m_oUpload" runat="server" MaxFileInputsCount="1" ControlObjectsVisibility="None"></telerik:RadUpload>
            </div>
        </fieldset>
    </div>
    <div class="button-row action">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="Update Job Details" CausesValidation="false" OnClientClick="return ValidateProductionFees();" OnClick="OnSave" CssClass="button" />
    </div>
</telerik:RadAjaxPanel>