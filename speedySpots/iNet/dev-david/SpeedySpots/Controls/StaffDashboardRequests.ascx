﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StaffDashboardRequests.ascx.cs" Inherits="SpeedySpots.Controls.StaffDashboardRequests" %>
<asp:Repeater ID="rptDashboard" runat="server" OnItemDataBound="rptDashboard_ItemDataBound"><ItemTemplate><asp:Literal id="litOutput" runat="server" /></ItemTemplate></asp:Repeater>
<tr style="display:none;"><td colspan="7" id="numbRecords" ><asp:Literal ID="litNumberOfRecords" runat="server" /></td></tr>