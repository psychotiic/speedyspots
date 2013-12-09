namespace SpeedySpots.Tests.FunctionalTests.Pages
{
   using Base;
   using OpenQA.Selenium;
   using OpenQA.Selenium.Support.PageObjects;

   public class LoginPage : PageBase
   {
      [FindsBy(How = How.Id, Using = "Master_m_oContent_m_txtUsername")]
      public IWebElement Username { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_m_txtPassword")]
      public IWebElement Password { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oContent_m_btnLogin")]
      public IWebElement LoginButton { get; set; }

      public LoginPage(WebAppDriver driver) : base(driver, "login.aspx")
      {
      }

      public LoginPage SignInWithInvalidCredentials()
      {
         Username.SendKeys(PageData.InvalidUsername);
         Password.SendKeys(PageData.InvalidPassword);
         LoginButton.Click();

         return FollowToPage<LoginPage>();
      }

      public UserDashboardPage LoginAsCustomer()
      {
         Username.SendKeys(PageData.CustomerUsername);
         Password.SendKeys(PageData.CustomerPassword);
         LoginButton.Click();

         return FollowToPage<UserDashboardPage>();
      }
      
      public UserDashboardPage LoginAsQualityControl()
      {
         Username.SendKeys(PageData.QualityControlUsername);
         Password.SendKeys(PageData.QualityControlPassword);
         LoginButton.Click();

         return FollowToPage<UserDashboardPage>();
      }

      public LoginPage Start()
      {
         return StartWithPage<LoginPage>();
      }
   }
}