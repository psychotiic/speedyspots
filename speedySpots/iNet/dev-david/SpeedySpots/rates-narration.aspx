<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rates-narration.aspx.cs" Inherits="SpeedySpots.rates_narration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<style type="text/css">
table.rates 
{
    font-size: 14px;
    margin-bottom: 2.5%;
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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
<div class="main">
<h2>Narration Rates</h2>
                                    <h3>Listening Fee:</h3>
                                    <p>An additional fee may be assessed depending on the complexity of your requirements in regards to the file (for example: timing a read to video). Please keep audio files to 1mb or less.</p>
                                    <h3>CD or Cassette:</h3>
                                    <p> Speedy Spots will transfer your audio file to CD or Cassette and deliver upon request.&nbsp;A $10 charge is assessed per CD or Cassette plus shipping fees.</p>
                                    <h3>Billing:</h3>
                                    <p>Invoices are e-mailed after the end of each month with 30 day terms.&nbsp;Prompt payment is expected. (A service charge of 1 1/2% not to exceed 18% per year will be added to all past due balances).&nbsp;Please keep us updated with your current e-mail address for billing.&nbsp;Please call or e-mail us with any questions </p>
                                    <p>PH: 1-800-697-8819<br>
      FAX: 734-475-4645<br>
      E-mail: <a href="mailto:%20spots@speedyspots.com">spots@speedyspots.com</a></p>
</div>
<div class="sidebar">
<div style="border: 1px solid #ccc; background: #f0f0f0; padding: 15px;">
<h2 style="margin-top: 0; padding-top: 0;">Rate Card</h2>
<p>Rates are for regular voice talent. Please add 20% more for accents, characters, and Spanish VOs & translations.</p>
<table class="rates">
    <thead>
        <tr>
            <th>Minutes</th>
            <th class="right">Price</th>
            <th>Minutes</th>
            <th class="right">Price</th>
        </tr>
    </thead>
    <tr>
        <td>1.5</td>
        <td class="right">$75</td>
        <td>6</td>
        <td class="right">$280</td>
    </tr>
    <tr>
        <td>2</td>
        <td class="right">$100</td>
        <td>6.5</td>
        <td class="right">$300</td>
    </tr>
    <tr>
        <td>2.5</td>
        <td class="right">$125</td>
        <td>7</td>
        <td class="right">$320</td>
    </tr>
    <tr>
        <td>3</td>
        <td class="right">$150</td>
        <td>7.5</td>
        <td class="right">$340</td>
    </tr>
    <tr>
        <td>3.5</td>
        <td class="right">$175</td>
        <td>8</td>
        <td class="right">$360</td>
    </tr>
    <tr>
        <td>4</td>
        <td class="right">$200</td>
        <td>8.5</td>
        <td class="right">$380</td>
    </tr>
    <tr>
        <td>4.5</td>
        <td class="right">$220</td>
        <td>9</td>
        <td class="right">$400</td>
    </tr>
    <tr>
        <td>5</td>
        <td class="right">$240</td>
        <td>9.5</td>
        <td class="right">$420</td>
    </tr>
    <tr>
        <td>5.5</td>
        <td class="right">$260</td>
        <td>10</td>
        <td class="right">$440</td>
    </tr>
</table>
      <p>**Rates may be higher with extreme vocabulary </p>
</div>
</div>
</asp:Content>
