namespace SpeedySpots.Tests.FunctionalTests.Pages
{
   using Base;
   using OpenQA.Selenium;
   using OpenQA.Selenium.Support.PageObjects;

   public class MyCreditCardsPage : PageBase
   {
      [FindsBy(How = How.LinkText, Using = "Add New Credit Card")]
      public IWebElement AddNewCardLink { get; set; }

      [FindsBy(How = How.LinkText, Using = PageData.CardAlias)]
      public IWebElement ExistingCardLink { get; set; }

      public MyCreditCardsPage(WebAppDriver driver) : base(driver, "user-creditcards.aspx")
      {
      }

      public EditCreditCardPage ClickOnAddNewCreditCard()
      {
         AddNewCardLink.Click();

         return FollowToPage<EditCreditCardPage>();
      }

      public EditCreditCardPage ClickOnExistingCard()
      {
         ExistingCardLink.Click();

         return FollowToPage<EditCreditCardPage>();
      }

      public MyCreditCardsPage NavigateToCardId(int id)
      {
         return FollowToPage<MyCreditCardsPage>("user-edit-creditcard.aspx?id=" + id);
      }
   }
}