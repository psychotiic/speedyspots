<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-notes-script.aspx.cs" Inherits="SpeedySpots.view_notes_script" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
<title><%=IASpot.Title %> - Script & Notes</title>
<link rel="stylesheet" type="text/css" media="screen, projection" href="css/sssms-forms.css" />
<link rel="stylesheet" type="text/css" media="print" href="css/sssms-print.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js"></script>
<script type="text/javascript" src="js/Placeholders.min.js"></script>
<style type="text/css">
body {
font-size: 100%;
font-family: "Trebuchet MS", Arial, Verdana, Helvetica, sans-serif;
}

.unavailabilities 
{
	margin: 0 5em;
}

#divReferenceFiles 
{
    float: right;
    width: 25%;
}

#divReferenceFiles ul,
#divReferenceFiles ul li 
{
    margin: 0;
    padding: 0;
    list-style: none;
}

#divReferenceFiles ul li 
{
    font-size: 12px;
    margin-bottom: 2em;
}

#divReferenceFiles ul li object
{
    display: block;
}

#divDetail 
{
    margin-right: 31%;
}

.group:after {
	content: ".";
	display: block;
	height: 0;
	clear: both;
	visibility: hidden;
}
table.actualFees thead tr th {
    text-align: left;
}
table.actualFees tbody tr td:nth-of-type(1) {
    text-align: right;
}
</style>
<script type="text/javascript">
    function OnClose() {
        window.close();
    }

    function OnPrint() {
        window.print();
    }

    function SetFocus() {

    }

    function setSamplePlayer(path, id) {
        swfobject.embedSWF("fla/player_mp3_mini.swf", id, "135", "20", "9.0.0", "expressInstall.swf", { mp3: path, slidercolor: "cce0f4" }, { bgcolor: "#66a3e0" }, "");
    }
</script>

</head>
<body onload="SetFocus()">
<form id="m_oForm" runat="server">
    <telerik:RadScriptManager ID="m_oScriptManager" runat="server"></telerik:RadScriptManager>
    <div id="m_divMessage" runat="server"></div>

    <h2>Notes/Script:</h2>
    <div class="button-row">
        <a href="javascript:void();" class="button primaryAction" onclick="OnPrint()">Print</a>
        <a href="javascript:void();" class="button" onclick="OnClose()">Close Window</a>
    </div>

    <div class="form-holder" id="divReferenceFiles" runat="server">
        <h3>Reference Files</h3>
        <asp:Repeater ID="rptProductionFiles" runat="server" OnItemDataBound="rptProductionFiles_ItemDataBound">
            <HeaderTemplate><ul></HeaderTemplate>
            <ItemTemplate>
                <li><asp:Literal ID="litPlayer" runat="server" /> <a href='download.aspx?id=<%#DataBinder.Eval(Container.DataItem, "IASpotFileID") %>&type=spot'><%#DataBinder.Eval(Container.DataItem, "Filename") %></a></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
    </div>

    <div class="form-holder" id="divDetail">
        <fieldset>
            <legend>Detail:</legend>
            <div class="group">
                <label>Requested Time:</label>
                <span class="output"><%=IASpot.Length %></span>
            </div>
            <div style="clear: both;">&nbsp;</div>
            <div class="group">
                <table id="actualFees" class="actualFees">
                    <thead>
                        <tr>
                            <th>&nbsp;</th>
                            <th width="25%">Fee</th>
                            <th>Actual Length</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="m_oTalentFees" runat="server" onitemdatabound="m_oTalentFees_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align: right;" nowrap><%# DataBinder.Eval(Container.DataItem, "IASpotFeeType.Name") %>:</td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="m_txtTalentFee" Width="80px" Type="Currency" MaxValue="9999.99" Value='<%# MemberProtect.Utility.ValidateDouble(DataBinder.Eval(Container.DataItem, "Fee").ToString()) %>' FeeID='<%#DataBinder.Eval(Container.DataItem, "IASpotFeeID") %>' EmptyMessage="$00.00" runat="server"></telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="m_txtActualTime" runat="server" MaxLength="5" Width="80px" />
                                        <asp:RequiredFieldValidator ID="m_reqActualTime" runat="server" ControlToValidate="m_txtActualTime" ErrorMessage="Actual time is required.">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="m_expActualTime" runat="server" ControlToValidate="m_txtActualTime" ValidationExpression="^\d{1,2}:\d{2}$" ErrorMessage="Actual time must be a valid time value (mm:ss).">*</asp:RegularExpressionValidator></label>                       
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>        
                    </tbody>
                </table>
                
            </div>

            <asp:ValidationSummary ID="m_oSummary" runat="server" DisplayMode="BulletList" HeaderText="Please fix the following issues to continue:" />

        </fieldset>
    </div>

    <div class="button-row">
        <asp:LinkButton ID="m_btnSave" runat="server" Text="Save" CssClass="button primaryAction" OnClick="OnSave" />
    </div>

    <span id="boxtitle"></span>
    <% if(IASpot.ProductionNotesForDisplay != string.Empty) %>
    <% { %>
        <h3>Production Notes:</h3>
        <p><%=IASpot.ProductionNotesForDisplay%></p>
    <% } %>
    
    <% if(IASpot.IAProductionOrder.NotesForDisplay != string.Empty) %>
    <% { %>    
        <h3>PO Notes From Talent:</h3>
        <p><%=IASpot.IAProductionOrder.NotesForDisplay %></p>
    <% } %>
</form>
        
<div class="script">
    <%=IASpot.Script %>
</div>
<script type="text/javascript">
    document.getElementById('m_oTalentFees_ctl00_m_txtTalentFee').focus();
    Placeholders.init({
        live: true,
        hideOnFocus: true
    });
</script>
</body>
</html>