<%@ Page Title="Speedy Spots :: Add Production Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="create-production-order.aspx.cs" Inherits="SpeedySpots.create_production_order" ValidateRequest="false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<script type="text/javascript">
function OnClientCommandExecuting(editor, args) {
    if (args.get_commandName() == "ToggleScreenMode") {
        if (editor.isFullScreen()) {
            // return normal width
            $(".main").css("width", "68.6%");
            $("#feedback").css("display", "block");
        } else {
            // set full width
            $(".main").css("width", "100%");
            $("#feedback").css("display", "none");
        }
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <speedyspots:SpotDetails id="m_oSpotDetails" runat="server" DefaultView="Producer" />
</div>
<div class="sidebar">
    <speedyspots:SpeedySpotsTabs id="m_oTabs" runat="server" DefaultView="Producer" DefaultTab="Request" ShowRequestDetails="true" ShowRequestNotes="true" ShowJobDetails="true"/>
</div>
</asp:Content>