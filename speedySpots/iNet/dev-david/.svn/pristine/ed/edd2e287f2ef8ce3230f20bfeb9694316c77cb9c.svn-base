<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpeedySpotsTabs.ascx.cs" Inherits="SpeedySpots.Controls.SpeedySpotsTabs" %>

<script language="javascript" type="text/javascript">
$(document).ready(
    function()
    {
        // Initially hide everything
        $('#divRequest').hide();
        $('#divNotes').hide();
        $('#divJob').hide();

        // Sanity check to always ensure at least Request tab displays
        var sDefaultTab = "<%=DefaultTab %>";
        if(sDefaultTab == "" || (sDefaultTab != "Request" && sDefaultTab != "Notes" && sDefaultTab != "Job"))
        {
            sDefaultTab = "Request";
        }

        // Display the default tab
        $("#div" + sDefaultTab).show();
        $("#tab" + sDefaultTab).addClass('at');
    }
 );

function OnRequestDetails()
{
    $('#tabRequest').addClass('at');
    $('#divRequest').show();

    $('#tabNotes').removeClass('at');
    $('#divNotes').hide();
    
    $('#tabJob').removeClass('at');
    $('#divJob').hide();
}

function OnRequestNotes()
{
    $('#tabRequest').removeClass('at');
    $('#divRequest').hide();

    $('#tabNotes').addClass('at');
    $('#divNotes').show();
    
    $('#tabJob').removeClass('at');
    $('#divJob').hide();
}

function OnJobDetails()
{
    $('#tabRequest').removeClass('at');
    $('#divRequest').hide();

    $('#tabNotes').removeClass('at');
    $('#divNotes').hide();

    $('#tabJob').addClass('at');
    $('#divJob').show();
}
</script>

<ul id="side-tabs" class="group">
<% if(ShowRequestDetails) %>
<% { %>
    <li id="tabRequest" class="selected"><a href="#" onclick="OnRequestDetails()">Request</a></li>
<% } %>
<% if(ShowRequestNotes) %>
<% { %>
    <li id="tabNotes"><a href="#" onclick="OnRequestNotes()">Notes</a></li>
<% } %>
<% if(ShowJobDetails) %>
<% { %>
    <li id="tabJob"><a href="#" onclick="OnJobDetails()">Job</a></li>
<% } %>
</ul>

<div id="divRequest" class="side-holder">
    <% if(ShowRequestDetails) %>
    <% { %>
    <speedyspots:RequestDetails ID="m_oRequestDetails" runat="server" />
    <% } %>
</div>

<div id="divNotes" class="form-holder requestnotes side-holder">
    <speedyspots:RequestNotes ID="m_oRequestNotes" runat="server" />
</div>

<div id="divJob" class="form-holder side-holder">
    <% if(ShowJobDetails) %>
    <% { %>
    <speedyspots:JobDetails ID="m_oJobDetails" runat="server" />
    <% } %>
</div>