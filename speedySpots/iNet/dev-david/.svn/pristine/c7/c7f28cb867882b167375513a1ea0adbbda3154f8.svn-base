namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text.RegularExpressions;
   using System.Web.UI.WebControls;
   using Business.Models;
   using Business.Services;
   using InetSolution.Web;
   using Objects;

   public enum UserEditCreditCardMode
   {
      AddNew,
      EditExisting
   }

   public partial class user_edit_creditcard : SiteBasePage
   {
      protected UserEditCreditCardMode pageMode = UserEditCreditCardMode.AddNew;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            DisplayPage();
         }
      }

      protected void ServerValidationOfCardNumber(object source, ServerValidateEventArgs args)
      {
         try
         {
            var creditCardNumber = CreditCardNumberTextBox.Text.Trim();
            if (CreditCardTypeCombo.SelectedValue == "AmericanExpress")
            {
               var validationRegex = new Regex("^\\d{15}$");
               args.IsValid = validationRegex.Match(creditCardNumber).Success;
            }
            else
            {
               var validationRegex = new Regex("^\\d{16}$");
               args.IsValid = validationRegex.Match(creditCardNumber).Success;
            }
         }
         catch (Exception ex)
         {
            args.IsValid = false;
         }
      }

      protected void ServerValidationOfCVC(object source, ServerValidateEventArgs args)
      {
         try
         {
            var cvc = CVCTextBox.Text.Trim();
            if (CreditCardTypeCombo.SelectedValue == "AmericanExpress")
            {
               args.IsValid = (cvc.Length == 4);
            }
            else
            {
               args.IsValid = (cvc.Length == 3);
            }
         }
         catch (Exception ex)
         {
            args.IsValid = false;
         }
      }

      protected void ServerValidationOfExpirationDate(object source, ServerValidateEventArgs args)
      {
         try
         {
            var cardExpiresOnDate =
               new DateTime(MemberProtect.Utility.ValidateInteger(ExpirationYearCombo.SelectedValue),
                            MemberProtect.Utility.ValidateInteger(ExpirationMonthCombo.SelectedValue), 1)
                  .AddMonths(1).AddDays(-1);

            if (cardExpiresOnDate.CompareTo(DateTime.Today) > 0) return;

            args.IsValid = false;
         }
         catch (Exception)
         {
            args.IsValid = false;
         }
      }

      private void DisplayPage()
      {
         pageMode = UserEditCreditCardMode.AddNew;
         ApplicationContext.LoadCreditCardTypes(ref CreditCardTypeCombo);
         ApplicationContext.LoadCreditCardExpirationMonths(ref ExpirationMonthCombo);
         ApplicationContext.LoadCreditCardExpirationYears(ref ExpirationYearCombo);

         if (Request.QueryString["id"] != null)
         {
            pageMode = UserEditCreditCardMode.EditExisting;
            var vm = LoadExistingCreditCard();
            PoorMansUIBind(vm);
         }
         else
         {
            BindDefaultData();
         }

         SetDisplayMode();
      }

      private void BindDefaultData()
      {
         ReceiptEmailTextBox.Text = MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(), "EmailInvoice");
      }

      /// <summary>
      /// Save credit card details
      /// </summary>
      protected void OnCreditCardSave(object sender, EventArgs e)
      {
         if (!Page.IsValid) return;

         var service = GetCardService();
         var creditCardId = MemberProtect.Utility.ValidateInteger(CreditCardId.Value);
         var creditCardViewModel = service.GetCreditCard(creditCardId) ??
                                   service.CreateNew();

         creditCardViewModel.Alias = AliasTextBox.Text.Trim();
         creditCardViewModel.CardType = CreditCardTypeCombo.SelectedValue;
         if (pageMode == UserEditCreditCardMode.AddNew)
         {
            creditCardViewModel.CreditCardNumber = CreditCardNumberTextBox.Text.Trim();
            creditCardViewModel.CardVerificationCode = CVCTextBox.Text.Trim();
         }
         creditCardViewModel.FirstName = FirstNameTextBox.Text.Trim();
         creditCardViewModel.LastName = LastNameTextBox.Text.Trim();
         creditCardViewModel.CompanyName = CompanyName.Text.Trim();
         creditCardViewModel.ExpirationMonth = ExpirationMonthCombo.SelectedValue;
         creditCardViewModel.ExpirationYear = ExpirationYearCombo.SelectedValue;
         creditCardViewModel.Address = Address1TextBox.Text.Trim();
         creditCardViewModel.City = CityTextBox.Text.Trim();
         creditCardViewModel.State = StateComboBox.SelectedValue;
         creditCardViewModel.ReceiptEmailAddressCsv = ReceiptEmailTextBox.Text.Trim();

         var errors = service.SaveCreditCard(creditCardViewModel);
         if (errors.Any())
         {
            SetMessage(errors.Aggregate((msg, error) => msg + error + "<br />"), MessageTone.Negative);
            return;
         }

         RedirectMessage("~/user-creditcards.aspx", "Credit card saved.", MessageTone.Positive);
      }

      protected void OnCreditCardDelete(object sender, EventArgs e)
      {
         var service = GetCardService();
         var creditCardId = MemberProtect.Utility.ValidateInteger(CreditCardId.Value);
         var creditCardViewModel = service.GetCreditCard(creditCardId);

         if (creditCardViewModel == null)
         {
            RedirectMessage("~/user-creditcards.aspx", "Credit card not found.");
            return;
         }

         service.DeleteCreditCard(creditCardViewModel);
         RedirectMessage("~/user-creditcards.aspx", "Credit card has been deleted.");
      }

      private void SetDisplayMode()
      {
         if (pageMode == UserEditCreditCardMode.AddNew)
         {
            CVCEditPanel.Visible = true;
            CreditCardNumberEdit.Visible = true;
         }
         else
         {
            CreditCardNumberView.Visible = true;
            DeleteLinkButton.Visible = true;

            FirstNameTextBox.Enabled = false;
            LastNameTextBox.Enabled = false;
            CreditCardTypeCombo.Enabled = false;
            ExpirationMonthCombo.Enabled = false;
            ExpirationYearCombo.Enabled = false;
         }
      }

      private CreditCardViewModel LoadExistingCreditCard()
      {
         var service = GetCardService();
         var creditCardId = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
         return service.GetCreditCard(creditCardId);
      }

      private void PoorMansUIBind(CreditCardViewModel customerCreditCardViewModel)
      {
         if (customerCreditCardViewModel != null)
         {
            CreditCardId.Value = customerCreditCardViewModel.CreditCardId.ToString();
            AliasTextBox.Text = customerCreditCardViewModel.Alias;
            CreditCardTypeCombo.SelectedValue = customerCreditCardViewModel.CardType;
            LastFourOfCardLabel.Text = customerCreditCardViewModel.LastFourOfCardNumber;
            FirstNameTextBox.Text = customerCreditCardViewModel.FirstName;
            LastNameTextBox.Text = customerCreditCardViewModel.LastName;
            CompanyName.Text = customerCreditCardViewModel.CompanyName;
            ExpirationMonthCombo.SelectedValue = customerCreditCardViewModel.ExpirationMonth;
            ExpirationYearCombo.SelectedValue = customerCreditCardViewModel.ExpirationYear;
            Address1TextBox.Text = customerCreditCardViewModel.Address;
            CityTextBox.Text = customerCreditCardViewModel.City;
            StateComboBox.SelectedValue = customerCreditCardViewModel.State;
            ReceiptEmailTextBox.Text = customerCreditCardViewModel.ReceiptEmailAddressCsv;
         }
         else
         {
            RedirectMessage("~/user-creditcards.aspx", "Credit card not found.");
         }
      }

      /// <summary>
      /// Allow access to this page for the specified roles
      /// </summary>
      /// <returns></returns>
      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl>
         {
            AccessControl.Admin,
            AccessControl.Staff,
            AccessControl.Talent,
            AccessControl.Customer
         };
      }

      private CreditCardService GetCardService()
      {
         return new CreditCardService(MemberProtect.CurrentUser.UserID, MemberProtect, DataAccess, ApplicationContext.SiteProperites);
      }
   }
}