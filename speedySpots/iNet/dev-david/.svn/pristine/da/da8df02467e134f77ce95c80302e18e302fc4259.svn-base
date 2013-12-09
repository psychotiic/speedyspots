namespace SpeedySpots.Tests.FunctionalTests.Pages
{
   using Base;
   using OpenQA.Selenium;
   using OpenQA.Selenium.Support.PageObjects;

   public class UserDashboardPage : PageBase
   {
      [FindsBy(How = How.LinkText, Using = "My Credit Cards")]
      public IWebElement MyCreditCardsLink { get; set; }

      public UserDashboardPage(WebAppDriver driver) : base(driver, "user-dashboard.aspx")
      {
      }

      public MyCreditCardsPage ClickOnMyCreditCards()
      {
         MyCreditCardsLink.Click();

         return FollowToPage<MyCreditCardsPage>();
      }
   }
}