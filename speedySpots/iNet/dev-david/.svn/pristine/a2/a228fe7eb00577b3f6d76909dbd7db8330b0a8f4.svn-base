<%@ Page Title="Speedy Spots ::Music Library" Language="C#" AutoEventWireup="true" CodeBehind="music.aspx.cs" Inherits="SpeedySpots.music" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
<title>Music beds from Speedy Spots!</title>
<link rel="stylesheet" type="text/css" media="screen, projection" href="css/screen.css" />
<link rel="stylesheet" type="text/css" media="screen, projection" href="css/sssms-forms.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js"></script>
    <script type="text/javascript">
        function setSamplePlayer(path, id) {
            swfobject.embedSWF("fla/player_mp3_mini.swf", id, "135", "20", "9.0.0", "expressInstall.swf", { mp3: path, slidercolor: "cce0f4" }, { bgcolor: "#66a3e0" }, "");
        }
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/stars/jquery.ui.stars.js"></script>
	<link rel="stylesheet" type="text/css" href="js/stars/jquery.ui.stars.css" />
    <script type="text/javascript">
        $(function () {
            $(".star-filter").children().not("select").hide();
            $(".star-filter").stars({
                inputType: "select"
            });

            $(".star-holder").children().not("select").hide();
            $(".star-holder").stars({
                inputType: "select",
                callback: function (ui, type, value) {
                    // Disable Stars while AJAX connection is active
                    ui.disable();

                    if (value == 0) {
                        ratingID = ui.options.id2val[0].split("_")[0];
                        value = ratingID + "_" + value;
                    }
                    $.post("music.aspx", { rate: value }, function (json) {
                        ui.enable();
                    }, "json");
                }
            });
        });

        function resetFilters() {
            document.ctl00.ddlGenre.selectedIndex = 0;
            document.ctl00.ddlTempo.selectedIndex = 0;
            document.ctl00.ddlLength.selectedIndex = 0;
            document.ctl00.ddlRating.selectedIndex = 0;
            $('.star-filter').stars('selectID', -1);

            event.preventDefault();
        }
	</script>
<style type="text/css">
body {
background: #fff;
}

body form 
{
	margin: auto 15px;
}
</style>
</head>
<body>
<form runat="server">
<telerik:RadScriptManager ID="m_oScriptManager" runat="server"></telerik:RadScriptManager>
    <h2>Music Library</h2>
    <div class="form-holder filter">
        <fieldset>
            <legend>Filter Library:</legend>
            <div class="group">
                <asp:Label ID="lblGenre" runat="server" Text="Genre:" AssociatedControlID="ddlGenre"></asp:Label>
                <div class="inputgroup">
                    <asp:DropDownList ID="ddlGenre" runat="server" />
                </div>
            </div>
            <div class="group">
                <asp:Label ID="lblTempo" runat="server" Text="Tempo:" AssociatedControlID="ddlTempo"></asp:Label>
                <div class="inputgroup">
                    <asp:DropDownList ID="ddlTempo" runat="server" />
                    <asp:Label ID="lblLength" runat="server" Text="Length:" AssociatedControlID="ddlLength"></asp:Label>
                    <asp:DropDownList ID="ddlLength" runat="server" /> seconds
                </div>
            </div>
            <div class="group">
                <label>Your Rating:</label>
                <div class="star-filter">
                    <asp:DropDownList ID="ddlRating" name="filter" runat="server">
                        <asp:ListItem Value="0" Text="Not rated" />
                        <asp:ListItem Value="1" Text="Not that bad" />
                        <asp:ListItem Value="2" Text="Good" />
                        <asp:ListItem Value="3" Text="Perfect" />
                    </asp:DropDownList>
                </div>
            </div>
        </fieldset>
        <div class="button-row action">
            <asp:LinkButton ID="m_btnFilter" runat="server" Text="Filter Library" CssClass="button primaryAction" OnClick="OnFilterLibrary" />
            <a href="#" class="button" onclick="resetFilters();">Reset</a>
        </div>
    </div>

    <h3>Available Tracks</h3>
    <div class="message">
        <p>These music samples are encoded at low quality. The music will be full broadcast quality when mixed with a voiceover.  All purchased music beds require mixing with a voiceover prior to sending finished production to you.  Music beds are not sold separately.</p>
    </div>
    <telerik:RadGrid ID="m_grdMusic" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="None" onneeddatasource="OnNeedDataSourceRequests" OnItemDataBound="onItemDataBound" AllowPaging="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" Skin="test" EnableEmbeddedSkins="false">
        <MasterTableView NoDetailRecordsText="No items matching">
            <Columns>
                <telerik:GridBoundColumn DataField="IAMusicID" UniqueName="IAMusicID" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="FileDisplay" Visible="true" HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlFile" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="IAMusicGroupName" UniqueName="GenreName" Visible="True" HeaderText="Genre"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IAMusicTempoName" UniqueName="Tempo" Visible="True" HeaderText="Tempo"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LengthBeds" UniqueName="LengthBeds" Visible="True" HeaderText="Length"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="Sample" Visible="true" HeaderText="Sample">
                    <ItemTemplate>
                        <asp:Literal ID="litPlayer" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="RatingStars" Visible="true" HeaderText="Rating" SortExpression="Rating">
                    <ItemTemplate>
                        <div class="star-holder">
                            <asp:DropDownList ID="ddlRating" name="rate" runat="server">
                                <asp:ListItem Value="1" Text="Not that bad" />
                                <asp:ListItem Value="2" Text="Good" />
                                <asp:ListItem Value="3" Text="Perfect" />  
                            </asp:DropDownList>
			                <input type="submit" value="Rate it!" />
                        </div>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</form>
</body>
</html>
