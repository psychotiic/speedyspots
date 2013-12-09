<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Talent-Tabs.ascx.cs" Inherits="SpeedySpots.Controls.Tabs.Talent_Tabs" %>
<div class="order-list">
    <div class="tab-holder group">
        <ul id="dashboard-tabs">
            <li id="liPending" runat="server"><a href="talent-dashboard.aspx">Pending</a></li>
            <li id="liToQC" runat="server"><a href="talent-production-orders-to-qc.aspx">Sent to QC</a></li>
            <li id="liCompleted" runat="server"><a href="talent-production-orders-completed.aspx">Completed</a></li>
            <li id="liMessages" runat="server"><a href="messages-inbox.aspx">Messages</a></li>
        </ul>
    </div>
</div>