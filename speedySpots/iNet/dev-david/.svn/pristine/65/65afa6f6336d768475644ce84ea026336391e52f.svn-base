<%@ Page Title="Speedy Spots :: Spot to Record" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frm-talent-files.aspx.cs" Inherits="SpeedySpots.frm_talent_files" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
.red 
{
	color: Red;
	font-weight: bold;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div id="content">
    <p><a href="Default.aspx">Home</a> &raquo; Record a Spot</p>
    <fieldset class="output">
        <legend>Spot Details:</legend>
        <div class="group">
            <label class="required">Spot Name:</label>
            Peachtree Natural Foods-Osteo Protect
        </div>
        <div class="group">
            <label class="required">Talent:</label>
            Melissa
        </div>
        <div class="group">
            <label class="required">Your Fee:</label>
            $20 - <span class="red">includes listening fee, see below.</span>
        </div>
        <div class="group">
            <label class="required">Notes:</label>
            You have :20.5 seconds; read this like a crazy person who just escaped from an asylum.
        </div>
        <div class="group">
            <label class="required">Script:</label>
            <div class="inputgroup">
                <p><strike><strong>Cassidy:</strong> Osteoperosis?!?! Are you sure??</strike></p>
                <p><strong>Melissa:</strong> The results of your bone scan are clear!</p>
                <p><strike><strong>Cassidy:</strong> How? I've been taking a calcium supplement I got at the Supermarket!</strike></p>
                <p><strong>Melissa:</strong> Not only does the calcium supplement you picked not work, it can hurt you by causing heel spurs and kidney stones. Here's a prescription for your <strong><em>health food store</em></strong> to fill.</p>
                <p><strike><strong>Marty:</strong> Save 25% on Vita Logic's Osteo Protect, the number one doctor recommended calcium suppliment, when you shop at Peachtree Natural Foods. A recent study shows that Osteo Protect by Vita Logic actually reverses bone loss. Make no bones about it, protect your bones and your wallet. Shop at Peachtree Natrual Foods and save 25% on Osteo Protect.</strike></p>
            </div>
        </div>
        <div class="group">
            <label class="required">Length of read:</label>
            :20.5 seconds
        </div>
        <div class="group">
            <label class="required">Due Date/Time:</label>
            <span class="red">ASAP</span>
        </div>
    </fieldset>
    <div class="message negative">
        <p>It is requested that you listen to the following file(s):</p>
    </div>
    <fieldset class="output">
        <div class="group">
            <label class="required">Requested listenting:</label>
            <div class="inputgroup">
                <a href="#">PreviousRecording.mp3</a><br />
                <a href="#">BackgroundMusicForTiming.mp3</a>
            </div>
        </div>
    </fieldset>

    <div class="form-holder">
        <fieldset>
            <legend>Send us your finished recording(s):</legend>
            <div class="group">
                <asp:Label ID="m_lblFinishedFile" runat="server" Text="Recording:" AssociatedControlID="m_fileFinishedFile" CssClass="required"></asp:Label>
                <div class="inputgroup">
                    <telerik:RadUpload ID="m_fileFinishedFile" runat="server"></telerik:RadUpload>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="buttonrow">
        <asp:Button ID="m_btnSubmit" runat="server" Text="Finished" CssClass="primaryAction" />
        <asp:Button ID="m_back" runat="server" Text="Back" />
    </div>
</div>
</asp:Content>