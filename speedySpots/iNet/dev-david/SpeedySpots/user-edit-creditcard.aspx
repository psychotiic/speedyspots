<%@ Page Title="Speedy Spots :: Credit Card" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="user-edit-creditcard.aspx.cs" Inherits="SpeedySpots.user_edit_creditcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_oHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="m_oContent" runat="server">
   <p class="breadcrumb">      
      <a href="user-dashboard.aspx">Dashboard</a> &raquo;
      <a href="user-creditcards.aspx">My Credit Cards</a>
   </p>
   <asp:HiddenField ID="CreditCardId" runat="server" />
   <div id="m_divCreditCard" runat="server" class="form-holder">
      <fieldset>
         <legend>Credit Card Information</legend>
         <p>Required fields are <span class="required">bold and underlined</span>.</p>
         <br />

         <div class="group">
            <asp:Label ID="AliasLabel" AssociatedControlID="AliasTextBox" runat="server">Alias:</asp:Label>
            <telerik:RadTextBox ID="AliasTextBox" runat="server"></telerik:RadTextBox>
         </div>

         <div class="group">
            <asp:Label ID="Label8" runat="server" AssociatedControlID="CreditCardTypeCombo">
               <asp:RequiredFieldValidator ID="CreditCardTypeRequiredFieldValidator" ControlToValidate="CreditCardTypeCombo" ErrorMessage="Please select the card type." runat="server">*</asp:RequiredFieldValidator>
               <span class="required">Card type:</span>
            </asp:Label>
            <telerik:RadComboBox ID="CreditCardTypeCombo" runat="server" MaxHeight="200px">
               <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </telerik:RadComboBox>
         </div>

         <div class="group">
            <asp:Panel ID="CreditCardNumberEdit" runat="server" Visible="false">
                  <asp:Label ID="Label2" runat="server" AssociatedControlID="CreditCardNumberTextBox">
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="CreditCardNumberTextBox" ValidationExpression="^(\d{15}|\d{16})$" ErrorMessage="Credit Card Number must be a valid credit card number.">*</asp:RegularExpressionValidator>
                     <asp:RequiredFieldValidator ID="CreditCardNumberRequired" runat="server" ControlToValidate="CreditCardNumberTextBox" ErrorMessage="Please enter your credit card number.">*</asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="CardNumberServerSideValidator" ErrorMessage="Credit Card Number is not valid for this card type." ValidateEmptyText="True" OnServerValidate="ServerValidationOfCardNumber" runat="server">*</asp:CustomValidator>
                     <span class="required">Card number:</span>
                  </asp:Label>
                  <div class="note-holder">
                     <telerik:RadTextBox ID="CreditCardNumberTextBox" AutoCompleteType="Disabled" runat="server" Columns="20" MaxLength="16">
                     </telerik:RadTextBox>
                     <p class="note">Enter number without spaces, example: 1234123412341234</p>
                  </div>
            </asp:Panel>
            <asp:Panel ID="CreditCardNumberView" runat="server" Visible="false">
               <asp:Label ID="LastFourLabel" AssociatedControlID="LastFourOfCardLabel" runat="server">Card ending in: </asp:Label>
               <asp:Label ID="LastFourOfCardLabel" runat="server"></asp:Label>
            </asp:Panel>
         </div>
         
         <div class="group">
            <asp:Label ID="Label11" runat="server" AssociatedControlID="FirstNameTextBox">
               <span>Name on card:</span>
            </asp:Label>
            
            <div class="inputgroup">
               <div class="note-holder">
                  <telerik:RadTextBox ID="FirstNameTextBox" runat="server" MaxLength="20">
                  </telerik:RadTextBox>
                  <p class="note">First name<p>
               </div>
               <div class="note-holder">
                  <telerik:RadTextBox ID="LastNameTextBox" runat="server" MaxLength="20">
                  </telerik:RadTextBox>
                  <p class="note">Last name</p>
               </div>
            </div>
         
            <asp:Label ID="Label1" runat="server" AssociatedControlID="CompanyName">
               <span>Company name on card:</span>
            </asp:Label>
            
            <div class="note-holder">
               <telerik:RadTextBox ID="CompanyName" runat="server" MaxLength="50">
               </telerik:RadTextBox>
            </div>
         </div>

         <div class="group">
            <asp:Label ID="Label9" runat="server" AssociatedControlID="expirationMonthCombo">
               <asp:CustomValidator ID="ExpirationCustomValidator" ErrorMessage="Please choose a valid credit card expiration date in the future." OnServerValidate="ServerValidationOfExpirationDate" runat="server">*</asp:CustomValidator>
               <span class="required">Expiration:</span>
            </asp:Label>
            <div class="inputgroup">
               <div class="note-holder">
                  <telerik:RadComboBox ID="ExpirationMonthCombo" runat="server" MaxHeight="200px" Width="75px">
                  </telerik:RadComboBox>
                  <p class="note">Month</p>
               </div>
               <div class="note-holder">
                  <telerik:RadComboBox ID="ExpirationYearCombo" runat="server" MaxHeight="200px" Width="75px">
                  </telerik:RadComboBox>
                  <p class="note">Year</p>
               </div>
            </div>
         </div>

         <asp:Panel ID="CVCEditPanel" runat="server" Visible="false">
            <div class="group">
               <asp:Label ID="CVCLabel" AssociatedControlID="CVCTextBox" runat="server">
                  <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Card Verification Code is not valid for this card type." OnServerValidate="ServerValidationOfCVC" ValidateEmptyText="True" runat="server">*</asp:CustomValidator>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="CVCTextBox" runat="server" ErrorMessage="Please enter the card verification code as it appears on the back of your credit card.">*</asp:RequiredFieldValidator>               
                  <span class="required">CVC:</span>
               </asp:Label>
               <div class="note-holder">
                  <telerik:RadTextBox ID="CVCTextBox" Columns="4" AutoCompleteType="Disabled" MaxLength="4" runat="server">
                  </telerik:RadTextBox>
                  <p class="note">3 digit code found on the back of your card.  4 digits on front of card for American Express</p>
               </div>
            </div>
         </asp:Panel>
         
         <div class="group">
            <asp:Label ID="Address1Label" AssociatedControlID="Address1TextBox" runat="server">Address:</asp:Label>
            <telerik:RadTextBox ID="Address1TextBox" runat="server"></telerik:RadTextBox>
         </div>

         <div class="group">
            <asp:Label ID="CityLabel" AssociatedControlID="CityTextBox" runat="server">City</asp:Label>
            <telerik:RadTextBox ID="CityTextBox" runat="server"></telerik:RadTextBox>
         </div>
         <div class="group">
            <asp:Label ID="StateLabel" AssociatedControlID="StateComboBox" runat="server">State:</asp:Label>
            <telerik:RadComboBox ID="StateComboBox" runat="server" Height="200px">
                <Items>
                    <telerik:RadComboBoxItem Selected="true" Value="" Text="" />
                    <telerik:RadComboBoxItem Value="N/A" Text="Not Applicable" />
                    <telerik:RadComboBoxItem Value="AL" Text="Alabama" />
                    <telerik:RadComboBoxItem Value="AK" Text="Alaska" />
                    <telerik:RadComboBoxItem Value="AZ" Text="Arizona" />
                    <telerik:RadComboBoxItem Value="AR" Text="Arkansas" />
                    <telerik:RadComboBoxItem Value="CA" Text="California" />
                    <telerik:RadComboBoxItem Value="CO" Text="Colorado" />
                    <telerik:RadComboBoxItem Value="CT" Text="Connecticut" />
                    <telerik:RadComboBoxItem Value="DE" Text="Delaware" />
                    <telerik:RadComboBoxItem Value="DC" Text="District of Columbia" />
                    <telerik:RadComboBoxItem Value="FL" Text="Florida" />
                    <telerik:RadComboBoxItem Value="GA" Text="Georgia" />
                    <telerik:RadComboBoxItem Value="HI" Text="Hawaii" />
                    <telerik:RadComboBoxItem Value="ID" Text="Idaho" />
                    <telerik:RadComboBoxItem Value="IL" Text="Illinois" />
                    <telerik:RadComboBoxItem Value="IN" Text="Indiana" />
                    <telerik:RadComboBoxItem Value="IA" Text="Iowa" />
                    <telerik:RadComboBoxItem Value="KS" Text="Kansas" />
                    <telerik:RadComboBoxItem Value="KY" Text="Kentucky" />
                    <telerik:RadComboBoxItem Value="LA" Text="Louisiana" />
                    <telerik:RadComboBoxItem Value="ME" Text="Maine" />
                    <telerik:RadComboBoxItem Value="MD" Text="Maryland" />
                    <telerik:RadComboBoxItem Value="MA" Text="Massachusetts" />
                    <telerik:RadComboBoxItem Value="MI" Text="Michigan" />
                    <telerik:RadComboBoxItem Value="MN" Text="Minnesota" />
                    <telerik:RadComboBoxItem Value="MS" Text="Mississippi" />
                    <telerik:RadComboBoxItem Value="MO" Text="Missouri" />
                    <telerik:RadComboBoxItem Value="MT" Text="Montana" />
                    <telerik:RadComboBoxItem Value="NE" Text="Nebraska" />
                    <telerik:RadComboBoxItem Value="NV" Text="Nevada" />
                    <telerik:RadComboBoxItem Value="NH" Text="New Hampshire" />
                    <telerik:RadComboBoxItem Value="NJ" Text="New Jersey" />
                    <telerik:RadComboBoxItem Value="NM" Text="New Mexico" />
                    <telerik:RadComboBoxItem Value="NY" Text="New York" />
                    <telerik:RadComboBoxItem Value="NC" Text="North Carolina" />
                    <telerik:RadComboBoxItem Value="ND" Text="North Dakota" />
                    <telerik:RadComboBoxItem Value="OH" Text="Ohio" />
                    <telerik:RadComboBoxItem Value="OK" Text="Oklahoma" />
                    <telerik:RadComboBoxItem Value="OR" Text="Oregon" /> 
                    <telerik:RadComboBoxItem Value="PA" Text="Pennsylvania" /> 
                    <telerik:RadComboBoxItem Value="RI" Text="Rhode Island" /> 
                    <telerik:RadComboBoxItem Value="SC" Text="South Carolina" /> 
                    <telerik:RadComboBoxItem Value="SD" Text="South Dakota" /> 
                    <telerik:RadComboBoxItem Value="TN" Text="Tennessee" /> 
                    <telerik:RadComboBoxItem Value="TX" Text="Texas" /> 
                    <telerik:RadComboBoxItem Value="UT" Text="Utah" /> 
                    <telerik:RadComboBoxItem Value="VT" Text="Vermont" /> 
                    <telerik:RadComboBoxItem Value="VA" Text="Virginia" /> 
                    <telerik:RadComboBoxItem Value="WA" Text="Washington" /> 
                    <telerik:RadComboBoxItem Value="WV" Text="West Virginia" /> 
                    <telerik:RadComboBoxItem Value="WI" Text="Wisconsin" /> 
                    <telerik:RadComboBoxItem Value="WY" Text="Wyoming" /> 
                </Items>
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>                
            </telerik:RadComboBox>
         </div>
         <div class="group">
            <asp:Label ID="ReceiptEmailLabel" AssociatedControlID="ReceiptEmailTextBox" runat="server">
               <asp:RegularExpressionValidator ID="ReceiptEmailValidator" runat="server" ControlToValidate="ReceiptEmailTextBox" ValidationExpression="^([a-zA-Z0-9]+[a-zA-Z0-9._%-]*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4},?[\s]*)*$" ErrorMessage="Please enter a valid email address, or comma separated list.">*</asp:RegularExpressionValidator>
               Receipt email:
            </asp:Label>
            <div class="note-holder">
               <telerik:RadTextBox ID="ReceiptEmailTextBox" runat="server" Columns="50" MaxLength="255"></telerik:RadTextBox>
               <p class="note">Comma seperated list of email addresses to receive payment notifications.</p>
            </div>
         </div>
      </fieldset>

      <div class="button-row">
         <asp:LinkButton ID="SubmitLinkButton" CausesValidation="true" Text="Save Credit Card" CssClass="button primaryAction" OnClick="OnCreditCardSave" runat="server" />
         <asp:LinkButton ID="DeleteLinkButton" CausesValidation="false" Text="Delete Credit Card" CssClass="button negativeAction" OnClientClick="return confirm('Are you sure you want to delete this credit card?');" OnClick="OnCreditCardDelete" Visible="false" runat="server" />
         <br/>
         <br/>
         <div class="note-holder">
            <p class="note">By clicking <strong>Save Credit Card</strong> you agree to the <a href="/payments/termsandconditions.aspx" target="_blank" onclick="window.open(this.href,'mypopup');">terms and conditions</a>.</p >

         </div>
      </div>
   </div>
</asp:Content>
