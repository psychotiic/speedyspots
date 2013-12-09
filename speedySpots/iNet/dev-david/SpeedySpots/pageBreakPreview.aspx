<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pageBreakPreview.aspx.cs" Inherits="SpeedySpots.pageBreakPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen, print" href="css/reset.css" />
    <style type="text/css" media="screen">
        body 
        {
            margin: 38px auto;
            width: 764px;
            font-size: 13pt !important;
        }
        
        .pagebreak hr 
{
	display: none;
}

.pagebreak 
{
	display: block;
	height: 30px;
	background: url("img/bg-pagebreak.png") center center no-repeat;
}

.button-row 
{
    display: block !important;
    text-align: center;
    padding: 9px 0;
	margin-bottom: 18px;
}

a.button {
	margin-right: 18px;
	padding-top: 3px;
	padding-right: 18px;
	padding-bottom: 3px;
	padding-left: 18px;
	font-size: 14px; /* 14 */
	line-height: 26px;
	color: #333;
	background: #c4c4c4 url("img/glass.png") repeat-x 0 50%;
	border: 1px solid #5e5e5e;
	border-radius: 13px;
	-moz-border-radius: 13px;
	-webkit-border-radius: 13px;
	text-decoration: none;
}

.button-row a.primaryAction {
	font-weight: bold;
	color: #060;
	background-color: #cce0cc;
	border-color: #7fb27f;
	-moz-box-shadow: 0 0 5px rgba(0,102,0,.8);
	-webkit-box-shadow: 0 0 5px rgba(0,102,0,.8);
}
    </style>
    <link rel="stylesheet" type="text/css" media="screen, print" href="css/sssms-print.css" />
</head>
<body>
<div class="button-row action">
        <a href="javascript:window.print()" class="button primaryAction">Click to Print This Page</a>
    </div>
    <form id="form1" runat="server">
<div id="wrapper">
    <div id="header">
        <div id="top">
            <img src="img/ss-logo-print.gif" height="72" width="339" alt="Speedy Spots" class="print" />
        </div>
    </div>
    <div id="body" class="group">
    <div class="full">
        <h2>Production Order</h2>
        <div class="hr"><hr /></div>
        
        <div class="form-holder group details">
            <fieldset class="half">
                <legend>Details:</legend>
                <div class="group">
                    <label>Job Title:</label>
                    <span class="output"><asp:Label id="lblJob" runat="server" /></span>
                </div>
                <div class="group">
                    <label>Spot:</label>
                    <span class="output"><asp:Label id="lblSpotTitle" runat="server" /></span>
                </div>
                <div class="group">
                    <label>Length:</label>
                    <div class="inputgroup">
                        <div class="inputgroup">
                            <span class="output"><asp:Label id="lblSpotLength" runat="server" /></span>
                        </div>
                    </div>
                </div>

                <div id="m_divFees" runat="server" class="group">
                    <label>Fees:</label>
                    <div class="inputgroup">
                        <span class="output">
                        <asp:Repeater ID="m_oRepeaterFees" runat="server">
                           <HeaderTemplate>
                              <table class="actualFees" border="0">
                                  <tbody>
                           </HeaderTemplate>
                            <ItemTemplate>
                                 <tr>
                                    <td nowrap><%#DataBinder.Eval(Container.DataItem, "IASpotFeeType.Name") %>:&nbsp;</td>
                                    <% if (IsComplete) %>
                                    <% { %>
                                    <td><%#DataBinder.Eval(Container.DataItem, "LengthActual") %></td>
                                    <td>&nbsp;@&nbsp;</td>
                                    <% } %>
                                    <td><%#DataBinder.Eval(Container.DataItem, "Fee", "{0:c}") %></td>
                                 </tr>
                            </ItemTemplate>
                           <FooterTemplate>
                                    </tbody>
                               </table>
                           </FooterTemplate>
                        </asp:Repeater>
                        </span>
                    </div>
                </div>
            </fieldset>

            <fieldset class="half">
                <legend>&nbsp;</legend>
                 <div class="group">
                    <label>Job #:</label>
                    <span class="output"><asp:Label id="lblJobNumber" runat="server" /></span>
                </div>
                 <div class="group">
                    <label>Producer(s):</label>
                    <span class="output"><asp:Label id="lblProducer" runat="server" /></span>
                </div>
                <div class="group">
                    <label>Sent:</label>
                    <span class="output"><asp:Label id="lblSent" runat="server" /></span>
                </div>
                <div class="group">
                    <label>Due:</label>
                    <span class="output"><asp:Label id="lblDueDate" runat="server" /></span>
                </div>
            </fieldset>
        </div>

        <h3>Production Notes:</h3>
        <div id="prodnotes">
            <asp:Label id="lblProductionNotes" runat="server" />
        </div> <!-- /prodnotes -->

        <h3 id="script">Script:</h3>
        <div class="script">
            <asp:Label id="lblScript" runat="server" />
        </div>
    </div>
</div>
</div>
    </form>
</body>
</html>
