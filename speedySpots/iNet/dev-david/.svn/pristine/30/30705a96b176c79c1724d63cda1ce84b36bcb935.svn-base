<%@ Page Title="Speedy Spots :: Quick Invoice Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SpeedySpots.payments._default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
<link rel="stylesheet" type="text/css" media="print" href="../css/reset.css" />
<link rel="stylesheet" type="text/css" media="print" href="../css/sssms-print.css" />
<style type="text/css" media="screen">
    #ssl-code {
        float: right;
    }
</style>
<script language="javascript" type="text/javascript">
        //<![CDATA[
    var tl_loc0 = (window.location.protocol == "https:") ? "https://secure.comodo.net/trustlogo/javascript/trustlogo.js" :
    "http://www.trustlogo.com/trustlogo/javascript/trustlogo.js";
    document.writeln('<scr' + 'ipt language="JavaScript" src="' + tl_loc0 + '" type="text\/javascript">' + '<\/scr' + 'ipt>');
        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">

<script language="javascript" type="text/javascript">
$(document).ready(function()
{
   OnChangeAmount();
});

function OnChangeAmount()
{
    var fTotal = 0;
    var oAmounts = $(".invoice");
    for(var i=0; i<oAmounts.length; i++)
    {
        var sAmount = oAmounts[i].value.replace("$", "");
        sAmount = sAmount.replace(",", "");
        if(sAmount.length == 0)
        {
            sAmount = "0";
        }

        fTotal += parseFloat(sAmount);
    }

    $("#m_spanTotal1").html(fTotal);
    $("#m_spanTotal2").html(fTotal);
}

function OnValidatePayment(oControl)
{
    if(typeof(Page_ClientValidate) == 'function')
    {
        if(Page_ClientValidate('') == false)
        {
            return false;
        }
    }

    var btnPay = $("#m_btnPay");
    btnPay.html("Please wait...");
    btnPay.attr("disabled", "disabled");
    btnPay.addClass("disabled");

    var oInvoices = $(".Input-Invoice");
    var oAmounts = $(".Input-Amount");
    var sQueryString = "";

    sQueryString += "count=" + oInvoices.length + "&";
    for(var i=0; i<oInvoices.length; i++)
    {
        sQueryString += "invoice" + i + "=" + oInvoices[i].value + "&";
        sQueryString += "amount" + i + "=" + oAmounts[i].value.replace("$", "") + "&";
    }

    $.ajax({
        url: "<%=ResolveUrl("~/ajax-validate-payment.aspx") %>",
        data: sQueryString,
        dataType: "text",
        success: OnValidatePaymentSucceed,
        error: OnValidatePaymentError
    });

    return false;
}

function OnValidatePaymentSucceed(data, textStatus, jqXHR)
{
    if(data == "continue")
    {
        $("#<%=m_btnConfirm.ClientID %>").click();
    }
    else if(data == "confirm")
    {
        $("#m_divPayNow").css("visibility", "hidden");
        $("#m_divConfirmation").css("display", "block");
    }
    else
    {
        // Unexpected situation
        alert("Cross-Domain Issue:" + data);
    }
}

function OnValidatePaymentError(jqXHR, textStatus, errorThrown)
{
    alert(textStatus);
}

function OnCancel()
{
    $("#m_divPayNow").css("visibility", "visible");
    $("#m_divConfirmation").css("display", "none");

    var btnPay = $("#m_btnPay");
    btnPay.html("Pay Now");
    btnPay.attr("disabled", "");
    btnPay.removeClass("disabled");

    return false;
}
</script>

<div class="full">
<div id="ssl-code">
    <!--
    TrustLogo Html Builder Code:
    Shows the logo at URL https://www.speedyspots.com/images/tl_white.gif
    Logo type is  ("SC4")
    undefined
    //-->
    <script type="text/javascript">        TrustLogo("https://www.speedyspots.com/images/tl_white.gif", "SC4", "none");</script>
    <a href="http://www.instantssl.com" id="comodoTL">SSL</a>
</div>

<h2 style="font-size: 2em; margin-bottom: .5em;">Quick Invoice Payment</h2>

<div class="message">
    <p>Do not use this payment form to pay an estimate. Please pay the estimate through the link that was supplied.</p>
</div>

<div id="m_divPayment" runat="server">
    <div class="form-holder">
    <fieldset>
        <legend>Payment/Invoice Information:</legend>
        
        <asp:Repeater ID="m_oRepeaterInvoices" runat="server">
            <ItemTemplate>
                <asp:HiddenField ID="m_txtID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
                <div class="group">
                    <label>Invoice (<%# Container.ItemIndex + 1 %>) #:</label>
                    <div class="inputgroup">
                        <asp:TextBox ID="m_txtNumber" runat="server" CssClass="Input-Invoice" MaxLength="20" Text='<%# DataBinder.Eval(Container.DataItem, "Number") %>' />
                        <asp:LinkButton ID="m_btnRemoveInvoice" runat="server" CausesValidation="false" Text="Remove Invoice" Visible='<%# Container.ItemIndex > 0 %>' CssClass="button delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' OnClick="OnRemoveInvoice" />
                    </div>
                </div>
                <div class="group">
                    <label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_txtAmount" ErrorMessage="Please enter the payment amount.">*</asp:RequiredFieldValidator>
                        <span class="required">Invoice (<%# Container.ItemIndex + 1 %>) Payment: </span>
                    </label>
                    <telerik:RadNumericTextBox CssClass="invoice Input-Amount" ID="m_txtAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' MinValue="0.00" MaxValue="99999.99" Type="Currency" ClientEvents-OnValueChanged="OnChangeAmount">
                        <NumberFormat AllowRounding="false" DecimalDigits="2" />
                    </telerik:RadNumericTextBox>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</div>
    <div class="button-row">
        <asp:LinkButton ID="m_btnAddInvoice" runat="server" CausesValidation="false" Text="Add Additional Invoice" CssClass="button" OnClick="OnAddInvoice" />
    </div>

    <div class="message positive">
        <p><strong>Payment Total: $<span id="m_spanTotal1">0.00</span></strong></p>
    </div>

    <div class="form-holder">
        <fieldset>
            <legend>Contact Information:</legend>
            <div class="group">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_txtCompany" ErrorMessage="Please enter the company name.">*</asp:RequiredFieldValidator>
                    <span class="required">Company Name:</span>
                </label>
                <asp:TextBox ID="m_txtCompany" runat="server" Columns="50" MaxLength="50" />
            </div>
            <div class="group hasnotes">
                <label>Special Instructions:</label>
                <div class="inputgroup">
                    <div class="note-holder">
                        <asp:TextBox ID="m_txtInstructions" runat="server" TextMode="MultiLine" Width="320" Height="75" />
                        <p class="note">Please do not include correspondence regarding scripts, talents or requests.<br />For those please use the <a href="/Pages/contact_us.aspx">contact us page</a>.</p>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>

    <div class="form-holder">
        <fieldset>
            <legend>Credit Card Information:</legend>
            
            <asp:Panel runat="server" ID="PaymentSourcePanel" Visible="False">
               <a name="paymentsource"></a>
               <div class="group">
                  <asp:Label ID="Label2" runat="server" AssociatedControlID="PaymentSourceCombo">
                     <asp:RequiredFieldValidator ID="PaymentSourceRequired" InitialValue="(please select)" ControlToValidate="PaymentSourceCombo" ErrorMessage="Please select a payment source." runat="server">*</asp:RequiredFieldValidator>
                     <span class="required">Payment Source:</span>
                  </asp:Label>
                  <div class="note-holder">
                     <telerik:RadComboBox ID="PaymentSourceCombo" CausesValidation="false" Width="250px" OnSelectedIndexChanged="PaymentSourceIndexChanged" AutoPostBack="true" LoadingMessage="Please wait" runat="server">
                     </telerik:RadComboBox>
                  </div>
               </div>
            </asp:Panel>

            <asp:Panel ID="OnetimePaymentSourcePanel" runat="server" Visible="false">
            <div class="group">
               <label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_txtEmail" ErrorMessage="Please enter the receipt email address.">*</asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="m_expContactEmail" runat="server" ControlToValidate="m_txtEmail" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4},?[\s]*)*$" ErrorMessage="Please enter a valid email address, or comma separated list.">*</asp:RegularExpressionValidator>
                  <span class="required">Receipt Email:</span>
               </label>
               <div class="note-holder">
                  <asp:TextBox ID="m_txtEmail" runat="server" Columns="50" MaxLength="255" />
                  <p class="note">Comma seperated list of email addresses to receive payment notifications.</p>
              </div>
            </div>
            <div class="group">
               <label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="m_cboCreditCardType" ErrorMessage="Please select your credit card type.">*</asp:RequiredFieldValidator>
                <span class="required">Card Type:</span></label>
                <telerik:RadComboBox ID="m_cboCreditCardType" runat="server" MaxHeight="200px">
                     <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                  </telerik:RadComboBox>
            </div>
            <div class="group hasnotes special">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_txtCreditCardNumber" ErrorMessage="Please enter the credit card number.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="m_txtCreditCardNumber" ValidationExpression="^(\d{15}|\d{16})$" ErrorMessage="Credit card number must be a valid 15 or 16 digit credit card number.">*</asp:RegularExpressionValidator>
                    <span class="required">Card Number:</span>
                </label>
                <div class="note-holder">
                    <asp:TextBox ID="m_txtCreditCardNumber" autocomplete="off" runat="server" Columns="16" MaxLength="16" />
                    <p class="note">No dashes or spaces.</p>
                </div>
            </div>
            <div class="group hasnotes">
                <label><span class="required">Expiration Date:</span></label>
                <div class="inputgroup">
                    <div class="note-holder">
                        <asp:DropDownList ID="m_cboCreditCardExpireMonth" runat="server" Width="50px" />
                        <p class="note">Month</p>
                    </div>
                    <div class="note-holder">
                        <asp:DropDownList ID="m_cboCreditCardExpireYear" runat="server" Width="65px" />
                        <p class="note">Year</p>
                    </div>
                </div>
            </div>
            <div class="group hasnotes special">
                <label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_txtCreditCardSecurityCode" ErrorMessage="Please enter the credit card CVC code.">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="m_txtCreditCardSecurityCode" ValidationExpression="^(\d{3}|\d{4})$" ErrorMessage="CVC code must be 3 or 4 digits.">*</asp:RegularExpressionValidator>
                    <span class="required">CVC:</span>
                </label>
                <div class="note-holder">
                    <asp:TextBox ID="m_txtCreditCardSecurityCode" runat="server" autocomplete="off" Columns="3" MaxLength="4" />
                    <p class="note">3 digit code found on the back of your card.  4 digits on front of card for American Express</p>
                </div>
            </div>
            <div class="group">
                <label>
                   <span>Name on Card:</span>
                </label>
               <div class="inputgroup">
                     <div class="note-holder">
                        <asp:TextBox ID="m_txtFirstName" runat="server" MaxLength="20" />
                        <p class="note">First name</p>
                     </div>
                     <div class="note-holder">
                        <asp:TextBox ID="m_txtLastName" runat="server" MaxLength="20" />
                        <p class="note">Last name</p>
                     </div>
                  </div>
             </div>
             <div class="group">
               <asp:Label ID="Label1" runat="server" AssociatedControlID="CompanyNameText">
                  <span>Company name on card:</span>
               </asp:Label>
            
               <div class="note-holder">
                  <telerik:RadTextBox ID="CompanyNameText" runat="server" MaxLength="50">
                  </telerik:RadTextBox>
               </div>
             </div>

             <div class="group">
                <label>
                    <span class="required">Terms &amp; Conditions:</span>
                </label>
                <div class="inputgroup nospace checkbox">
                    <asp:CheckBox ID="m_chkAgree" runat="server" /> <label for="Master_m_oContent_m_chkAgree">I agree to the <a href="termsandconditions.aspx" target="_blank">Terms &amp; Conditions</a></label>
                </div>
            </div>
               
               <div class="group">
                  <label>&nbsp;</label>
                  <div class="inputgroup">
                     <asp:CheckBox ID="m_chkSavePaymentInformation" runat="server" Text="Store this credit card for future use" />
                  </div>
               </div>

            </asp:Panel>
            
            <asp:Panel ID="ExistingCardPaymentPanel" runat="server" Visible="false">
               <div class="group">
               <label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="EmailReceiptTextBox" ErrorMessage="Please enter the email.">*</asp:RequiredFieldValidator>
                  <span class="required">Receipt Email:</span>
               </label>
               <div class="note-holder">
                  <asp:TextBox ID="EmailReceiptTextBox" runat="server" Columns="50" MaxLength="255" />
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="EmailReceiptTextBox"  ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4},?[\s]*)*$" ErrorMessage="Please enter a valid email address, or comma separated list.">*</asp:RegularExpressionValidator>
                  <p class="note">Comma seperated list of email addresses to receive payment notifications.</p>
              </div>
            </div>
               <div class="group">
                  <asp:Label ID="Label4" runat="server" AssociatedControlID="CardTypeLabel">
                     <span>Credit Card Type:</span>
                  </asp:Label>
                  <asp:Label runat="server" ID="CardTypeLabel"></asp:Label>
               </div>
               <div class="group">
                  <asp:Label ID="LastFourLabel" AssociatedControlID="LastFourOfCardLabel" runat="server">Card ending in: </asp:Label>
                  <asp:Label ID="LastFourOfCardLabel" runat="server"></asp:Label>
               </div>
               <div class="group">
                  <asp:Label ID="Label6" runat="server" AssociatedControlID="MonthExpirationLabel">
                     <span>Expiration:</span>
                  </asp:Label>
                  <div class="inputgroup">
                     <asp:Label runat="server" ID="MonthExpirationLabel"></asp:Label>/<asp:Label runat="server" ID="YearExpirationLabel"></asp:Label>
                  </div>
               </div>
               <div class="group">
                  <asp:Label ID="Label9" runat="server" AssociatedControlID="FirstNameLabel">
                     <span>Name:</span>
                  </asp:Label>
                  <div class="inputgroup">
                     <div class="note-holder">
                           <asp:Label runat="server" ID="FirstNameLabel"></asp:Label>
                     </div>
                     <div class="note-holder">
                           <asp:Label runat="server" ID="LastNameLabel"></asp:Label>
                     </div>
                  </div>
               </div>
            </asp:Panel>

        </fieldset>
    </div>

    <div class="message positive">
        <p><strong>Payment Total: $<span id="m_spanTotal2">0.00</span></strong></p>
    </div>

    <div id="m_divPayNow" class="button-row">
        <button id="m_btnPay" class="button primaryAction" onclick="return OnValidatePayment(this);">Pay Now</button>
    </div>

    <div id="m_divConfirmation" class="confirmNotice" style="display: none">
        <p>The total amount for your entered invoices doesn't match your payment amount. Would you like to proceed?</p>
        <div class="button-row">
            <inet:InetButton ID="m_btnConfirm" runat="server" Text="Confirm" CssClass="button primaryAction" onclick="OnPay" />
            <button id="m_btnCancel" class="button" onclick="return OnCancel();">Cancel</button>
        </div>
    </div>
   
   <div class="message positive" id="YouCanSaveCardsDiv" runat="server" Visible="False">
      <p>Credit Card information can be saved, but you must be logged in to save it.  Call us at 734-475-9327 if you don't have a user login.</p>
    </div>

</div>

<div id="m_divReceipt" runat="server">
    <h2>Quick Payment Received</h2>

    <div class="button-row action">
        <a href="javascript:window.print()" class="button primaryAction">Click to Print This Page</a>
    </div>

    <h3>Thank you for making a payment on SpeedySpots.com.</h3>
    
    <div class="form-holder receipt">
        <fieldset>
            <legend>Payment Details:</legend>
            <%=GetInvoiceDetails() %>
            <div class="group">
                <label>Payment Total:</label>
                <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(GetTotal().ToString("c")) %></span>
            </div>
            <div class="group">
                <label>Company Name:</label>
                <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(m_txtCompany.Text) %></span>
            </div>
            <div class="group">
                <label>First Name:</label>
                <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(CardProfile.FirstName) %></span>
            </div>
            <div class="group">
                <label>Last Name:</label>
                <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(CardProfile.LastName) %></span>
            </div>

            <div class="group">
                <label>Card:</label>
                <span class="output"><%=Microsoft.Security.Application.AntiXss.HtmlEncode(CardProfile.CardType) %></span>
            </div>
            <div class="group">
                <label>Card Number:</label>
                <span class="output">xxxx-xxxx-xxxx-<%=Microsoft.Security.Application.AntiXss.HtmlEncode(CardProfile.LastFourOfCardNumber) %></span>
            </div>
            <div class="group">
                <label>Paid On:</label>
                <span class="output"><%=string.Format("{0:MMMM dd, yyyy}", DateTime.Now) %> at <%=string.Format("{0:h:mm tt}", DateTime.Now)%></span>
            </div>
            <div class="group">
                <label>Special Instructions:</label>
                <span class="output"><%=m_txtInstructions.Text %></span>
            </div>
        </fieldset>
    </div>
    
    <p class="large">Thank you again for using SpeedySpots.com</p>
</div>
</div>
</asp:Content>
