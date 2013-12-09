<%@ Page Title="Speedy Spots :: Send Message" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="messages-send.aspx.cs" Inherits="SpeedySpots.messages_send" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<link rel="stylesheet" type="text/css" href="css/jquery.flexbox.css" />
<script type="text/javascript" src="js/jquery.flexbox.min.js"></script>
<script type="text/javascript">
    $(function () {
        // Like Google Suggest (hide the arrow)
        $('#ffb2').flexbox("ajax-user-lookup.aspx", {
            resultTemplate: '<div class="resultcol1">{name}</div><div class="resultcol2">{Username}</div>',
            width: 500,
            minChars: 2,
            queryDelay: 400,
            showArrow: false,
            onSelect: function () {
                var hiddenVariable = $("#ffb2_hidden").val();
                var boolExists = false;
                var queue = $("#Master_m_oContent_m_hdnUsers");

                $("#name-holder span").each(function () {
                    if ($(this).is("#usr" + hiddenVariable)) {
                        boolExists = true;
                    }
                });

                if (boolExists == false) {
                    $("#name-holder").append("<span class=\"name\" id=\"usr" + hiddenVariable + "\">" + this.value + " <a href=\"#\" class=\"close\">x</a></span");
                    var items = queue.attr("value");
                    if (items == null)
                        items = [];
                    else
                        items = items.split(",");

                    items.push(hiddenVariable);
                    queue.attr("value", items.toString());
                }

                $('.name a').bind({
                    click: function () {
                        $(this).parent().remove();
                        var parentID = $(this).parent().attr("id");
                        parentID = parentID.substr(3, parentID.length - 3);

                        var itemValue = $("#Master_m_oContent_m_hdnUsers").val();
                        itemValue = itemValue.replace(',' + parentID, '');

                        $("#Master_m_oContent_m_hdnUsers").val(itemValue)
                    }
                });

                this.value = "";
            }
        });
    });
</script>
<style type="text/css">
#name-holder 
{
	margin-left: 25%;
	margin-top: -20px;
}
    
.name {
	margin-right: 10px;
	padding: 5px 10px;
	background: #ccc;
	-webkit-border-radius: 5px;
	-moz-border-radius: 5px;
	border-radius: 5px;
	font-size: .857142em;
}

.name a:link,
.name a:visited {
	text-decoration: none;
	color: red;
}

.resultcol1 { float:left; width:50%; }
.resultcol2 { float:left; width:50%; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<h2>Speedy Spots Messaging System</h2>

<div class="tab-holder group">
    <ul id="dashboard-tabs">
        <li><a href="staff-dashboard.aspx">Requests</a></li>
        <li><a href="staff-dashboard.aspx?filter=inproduction">In Production</a></li>
        <li><a href="staff-dashboard-completed.aspx">Completed</a></li>
        <li><a href="admin-dashboard.aspx">Admin</a></li>
        <li class="at"><a href="messages-inbox.aspx">Messages</a></li>
    </ul>
</div>

<div class="button-row action">
    <a href="messages-inbox.aspx" class="button">Back to Inbox</a>
</div>

    <div class="form-holder">
        <fieldset>
            <legend>Send Message:</legend>
            <div class="group">
                <label><span class="required">Display:</span></label>
                <div class="inputgroup">
                    <div class="note-holder">
                        <telerik:RadDateTimePicker runat="server" ID="m_dtStartDateTime" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                            <Calendar>
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                    <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDateTimePicker>
                        <p class="note">Start showing message</p>
                    </div>
                    <div class="note-holder">
                        <telerik:RadDateTimePicker runat="server" ID="m_dtEndDateTime" Calendar-ShowRowHeaders="False" TimeView-RenderDirection="Vertical" TimeView-Columns="4">
                            <Calendar>
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today" Date="" ItemStyle-CssClass="rcToday" />
                                    <telerik:RadCalendarDay IsWeekend="true" ItemStyle-CssClass="rcWeekend" />
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDateTimePicker>
                        <p class="note">Stop showing message</p>
                    </div>
                </div>
            </div>
            <div class="group">
                <label><span class="required">Send to a group:</span></label>
                <div class="inputgroup">
                    <asp:CheckBoxList ID="m_chkGroups" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical">
                        <asp:ListItem Text="Staff" Value="Staff"></asp:ListItem>
                        <asp:ListItem Text="Talent" Value="Talent"></asp:ListItem>
                        <asp:ListItem Text="Customers" Value="Customers"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="group">
                <label><span class="required">And/or specific user(s):</span></label>
                <div class="note-holder">
                    <div id="ffb2"></div>
                    <p class="note">Just start typing a name, username, or email address.</p>
                </div>
                <asp:HiddenField ID="m_hdnUsers" runat="server" Value="" />
            </div>
            <div class="group">
                <div id="name-holder"></div>
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="m_reqSubject" runat="server" ControlToValidate="m_txtSubject" ErrorMessage="Please enter the subject.">*</asp:RequiredFieldValidator>
                    <span class="required">Subject:</span>
                </label>
                <telerik:RadTextBox ID="m_txtSubject" runat="server" Columns="100" MaxLength="100"></telerik:RadTextBox>
            </div>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_txtMessage" ErrorMessage="Please enter the message.">*</asp:RequiredFieldValidator>
                    <span class="required">Message:</span>
                </label>
                <div class="inputgroup">
                    <telerik:RadEditor ID="m_txtMessage" runat="server" Width="621px" AutoResizeHeight="false" EditModes="Design">
                        <Tools>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="Bold" />
                            <telerik:EditorTool Name="Underline" />
                            <telerik:EditorTool Name="StrikeThrough" />
                        </telerik:EditorToolGroup>
                    </Tools>
                    </telerik:RadEditor>
                    </div>
            </div>
        </fieldset>
    </div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnSend" runat="server" Text="Send Message" CssClass="button primaryAction" OnClick="OnSend" />
    </div>
</asp:Content>
