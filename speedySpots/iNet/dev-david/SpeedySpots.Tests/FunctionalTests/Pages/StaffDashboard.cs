namespace SpeedySpots.Tests.FunctionalTests.Pages
{
   using Base;
   using OpenQA.Selenium;
   using OpenQA.Selenium.Support.PageObjects;

   public class StaffDashboard : PageBase
   {
      [FindsBy(How = How.LinkText, Using = "Create Request For Customer")]
      public IWebElement CreateRequestForCustomerLink { get; set; }

      public StaffDashboard(WebAppDriver driver) : base(driver, "staff-dashboard.aspx")
      {
      }
   }
}