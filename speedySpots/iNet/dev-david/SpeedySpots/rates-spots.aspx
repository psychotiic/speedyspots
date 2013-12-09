<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rates-spots.aspx.cs" Inherits="SpeedySpots.rates_spots" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
table.rates 
{
    font-size: 14px;
    margin: 0 0 2.5%;
    width: 100%;
    border: 1px solid #ccc;
    border-right: 0;
    border-bottom: 0;
    background: #fff;
}

table.rates thead th 
{
    text-align: left;
    border-bottom: 1px solid #999;
}

table.rates th,
table.rates td 
{
    padding: 5px 10px;
    border-bottom: 1px solid #ccc;
}


table.rates td.right,
table.rates th.right 
{
    text-align: right;
    border-right: 1px solid #ccc;
}

.main 
{
    float: right;
    margin-left: .9%;
    margin-right: 0;
}

.sidebar 
{
    margin-left: 0;
    margin-right: .9%;
}

.sidebar h3 
{
    margin-top: 10%;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
    <h2>Spots Rate Card</h2>
    <p>All prices based on one read per spot. Additional reads billed accordingly. (All talents are required to send at least two reads with copy :10 &amp; under.)</p>
            <h3>Music, SFX &amp; fully produced spots:</h3>
		    <p>
			    Prices start at $15.00 for one :30 or :60 second music bed or sound 
			    effect.&nbsp;Please call for quote.</p>
            <h3>CD or Cassette:</h3>
			<p>Speedy Spots will transfer your audio file to CD or Cassette and deliver upon 
				request.&nbsp;A $10 charge is assessed per CD or Cassette plus shipping fees.</p>
            <h3>Narrations:</h3>
			<p>Copy over :65 is subject to individual determination of per finished minute 
				price.&nbsp; <a href="rates-narration.aspx">Click here to see the narration rate 
					card.</a></p>
            <h3>Listening Fee:</h3>
			<p>An additional fee may be assessed depending on the complexity of your requirements in regards to the file (for example: timing a read to video). Please keep audio files to 1mb or less.</p>
            <h3>Billing:</h3>
		    <p>Invoices are e-mailed after the end of each month with 30 day terms.&nbsp;Prompt 
			    payment is expected. (A service charge of 1 1/2% not to exceed 18% per year 
			    will be added to all past due balances).&nbsp;Please keep us updated with your 
			    current e-mail address for billing.&nbsp;Please call or e-mail us with any 
			    questions
		    </p>
		    <p>PH: 1-800-697-8819<br>
			    FAX: 734-475-4645<br>
			    E-mail: <a href="mailto:%20spots@speedyspots.com">spots@speedyspots.com</a></p>
</div>
<div class="sidebar">
    <div style="border: 1px solid #ccc; background: #f0f0f0; padding: 15px;">
        <h2 style="margin-top: 0; padding-top: 0;">Talent Rates</h2>
        <h3>Regular voice talent (male/female):</h3>
        <table class="rates">
            <thead>
                <tr>
                    <th>Seconds</th>
                    <th class="right">Price</th>
                </tr>
            </thead>
            <tr>
                <td>15 or less spot</td>
                <td class="right">$30</td>
            </tr>
            <tr>
                <td>15.1 to 35 spot</td>
                <td class="right">$35</td>
            </tr>
            <tr>
                <td>35.1 to 65 spot</td>
                <td class="right">$50</td>
            </tr>
        </table>
    
        <h3>Accented or Character voice talent:</h3>
        <table class="rates">
            <thead>
                <tr>
                    <th>Seconds</th>
                    <th class="right">Price</th>
                </tr>
            </thead>
            <tr>
                <td>1 to 35 spot</td>
                <td class="right">$45</td>
            </tr>
            <tr>
                <td>35.1 to 65 spot</td>
                <td class="right">$60</td>
            </tr>
        </table>
    
       <!-- <h3>Character voice talent:</h3>
		<table class="rates">
            <thead>
                <tr>
                    <th>Seconds</th>
                    <th class="right">Price</th>
                </tr>
            </thead>
            <tr>
                <td>1 to 35</td>
                <td class="right">$45</td>
            </tr>
            <tr>
                <td>35.1 to 65</td>
                <td class="right">$60</td>
            </tr>
        </table> -->
    
        <h3>Children voice talent:</h3>
        <table class="rates">
            <thead>
                <tr>
                    <th>Seconds</th>
                    <th class="right">Price</th>
                </tr>
            </thead>
            <tr>
                <td>1 to 35 spot</td>
                <td class="right">$40 to $100</td>
            </tr>
        </table>
        <p>* Please allow 1 to 4 days</p>
    
        <h3>Spanish Talent:</h3>
        <table class="rates">
            <thead>
                <tr>
                    <th>Seconds</th>
                    <th class="right">Price</th>
                </tr>
            </thead>
            <tr>
                <td>Voice Over 1 to 35</td>
                <td class="right">$45</td>
            </tr>
            <tr>
                <td>Voice Over 35.1 to 65</td>
                <td class="right">$60</td>
            </tr>
            <tr>
                <td>Character Voice Overs</td>
                <td class="right">+20%</td>
            </tr>
            <tr>
                <td>Translation 1 to 5</td>
                <td class="right">$20</td>
            </tr>
            <tr>
                <td>Translation 5.1 to 35</td>
                <td class="right">$45</td>
            </tr>
            <tr>
                <td>Translation 35.1 to 65</td>
                <td class="right">$60</td>
            </tr>
        </table>
		<p>** VO translations &amp; graphic translations are billed separately.</p>
    </div>
</div>
</asp:Content>