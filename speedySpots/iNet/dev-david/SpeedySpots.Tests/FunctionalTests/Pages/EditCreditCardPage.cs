namespace SpeedySpots.Tests.FunctionalTests.Pages
{
   using Base;
   using OpenQA.Selenium;
   using OpenQA.Selenium.Support.PageObjects;

   public class EditCreditCardPage : PageBase
   {
      [FindsBy(How = How.Id, Using = "Master_m_oContent_AliasTextBox")]
      public IWebElement AliasTextBox { get; set; }
      
      [FindsBy(How = How.Id, Using = "Master_m_oContent_CreditCardTypeCombo_Input")]
      public IWebElement CardTypeComboBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_CreditCardNumberTextBox")]
      public IWebElement CardNumberTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_LastFourOfCardLabel")]
      public IWebElement CardEndingInLabel { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_FirstNameTextBox")]
      public IWebElement FirstNameTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_LastNameTextBox")]
      public IWebElement LastNameTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_ExpirationMonthCombo_Input")]
      public IWebElement ExpirationMonthCombo { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_ExpirationYearCombo_Input")]
      public IWebElement ExpirationYearCombo { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_CVCTextBox")]
      public IWebElement CardVerificationCodeTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_Address1TextBox")]
      public IWebElement AddressTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_CityTextBox")]
      public IWebElement CityTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_StateComboBox_Input")]
      public IWebElement StateComboBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_ZipCode")]
      public IWebElement ZipCodeTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_ReceiptEmailTextBox")]
      public IWebElement ReceiptEmailTextBox { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_SubmitLinkButton")]
      public IWebElement SaveCreditCardButton { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_DeleteLinkButton")]
      public IWebElement DeleteCreditCardButton { get; set; }

      [FindsBy(How = How.LinkText, Using = "My Credit Cards")]
      public IWebElement MyCreditCardsLink { get; set; }
      
      public EditCreditCardPage(WebAppDriver driver) : base(driver, "user-edit-creditcard.aspx")
      {
      }

      public EditCreditCardPage PopulateWithValidData()
      {
         AliasTextBox.SendKeys(PageData.CardAlias);
         CardTypeComboBox.SendKeys(PageData.CardType);
         CardNumberTextBox.SendKeys(PageData.CardNumber);
         FirstNameTextBox.SendKeys(PageData.CardFirstName);
         LastNameTextBox.SendKeys(PageData.CardLastName);
         ExpirationMonthCombo.SendKeys(PageData.CardExpirationMonth);
         ExpirationYearCombo.SendKeys(PageData.CardExpirationYear);
         CardVerificationCodeTextBox.SendKeys(PageData.CardVerificationCode);
         AddressTextBox.SendKeys(PageData.CardAddress);
         CityTextBox.SendKeys(PageData.CardCity);
         StateComboBox.SendKeys(PageData.CardState);

         return this;
      }

      public EditCreditCardPage SaveCreditCard()
      {
         SaveCreditCardButton.Click();

         return FollowToPage<EditCreditCardPage>();
      }

      public MyCreditCardsPage ClickOnMyCreditCards()
      {
         MyCreditCardsLink.Click();

         return FollowToPage<MyCreditCardsPage>();
      }

      public MyCreditCardsPage ClickOnDeleteCreditCard()
      {
         DeleteCreditCardButton.Click();
         SwitchToAlert().Accept();

         WaitForElement(By.Id("Master_m_divMessage"));
         
         return FollowToPage<MyCreditCardsPage>();
      }
   }
}