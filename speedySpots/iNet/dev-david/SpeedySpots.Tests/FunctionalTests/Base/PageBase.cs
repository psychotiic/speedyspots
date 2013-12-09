namespace SpeedySpots.Tests.FunctionalTests.Base
{
   using OpenQA.Selenium;
   using OpenQA.Selenium.Support.PageObjects;

   public class PageBase
   {
      private readonly WebAppDriver _driver;
      private readonly string _pageName;

      [FindsBy(How = How.Id, Using = "Master_m_divMessage")]
      public IWebElement Messages { get; set; }

      [FindsBy(How = How.Id, Using = "Master_m_oValidationSummary")]
      public IWebElement ValidationSummary { get; set; }

      public PageBase(WebAppDriver driver, string pageName)
      {
         _driver = driver;
         _pageName = pageName;
         _driver.InitElements(this);
      }

      protected TPageType StartWithPage<TPageType>()
         where TPageType : PageBase
      {
         _driver.NavigateTo(_pageName);

         if (!_driver.Url.Contains(_pageName))
            throw new StaleElementReferenceException(string.Format("This is not the {0} page", _pageName));

         return _driver.NewPage<TPageType>();
      }

      protected TPageType FollowToPage<TPageType>(string pageOverride)
         where TPageType : PageBase
      {
         _driver.NavigateTo(pageOverride);

         return _driver.NewPage<TPageType>();
      }

      protected TPageType FollowToPage<TPageType>()
         where TPageType : PageBase
      {
         return _driver.NewPage<TPageType>();
      }

      protected IAlert SwitchToAlert()
      {
         return _driver.InternalDriver.SwitchTo().Alert();
      }

      protected void WaitForElement(By by)
      {
         _driver.WaitForElement(by);
      }
   }
}