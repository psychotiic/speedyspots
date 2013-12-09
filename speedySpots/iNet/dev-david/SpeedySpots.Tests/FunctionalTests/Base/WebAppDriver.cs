namespace SpeedySpots.Tests.FunctionalTests.Base
{
   using System;
   using OpenQA.Selenium;
   using OpenQA.Selenium.IE;
   using OpenQA.Selenium.Support.PageObjects;
   using OpenQA.Selenium.Support.UI;

   public class WebAppDriver
   {
      private readonly IWebDriver _driver;
      private readonly string _baseUrl;

      public WebAppDriver(string baseUrl)
      {
         _driver = new InternetExplorerDriver(new InternetExplorerOptions {IgnoreZoomLevel = true});
         _baseUrl = baseUrl;
      }

      public void NavigateTo(string pageName)
      {
         _driver.Navigate().GoToUrl(string.Format("{0}{1}", _baseUrl, pageName));
      }

      public void Quit()
      {
         _driver.Quit();
      }

      public string Url
      {
         get { return _driver.Url; }
      }

      public void InitElements(object page)
      {
         PageFactory.InitElements(_driver, page);
      }

      public TPageType NewPage<TPageType>()
         where TPageType : PageBase
      {
         return (TPageType) Activator.CreateInstance(typeof (TPageType), this);
      }

      public IWebDriver InternalDriver
      {
         get { return _driver; }
      }

      public void WaitForElement(By by)
      {
         var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
         wait.Until(c =>
         {
            try
            {
               return c.FindElement(@by).Displayed;
            }
            catch (Exception)
            {
               return false;
            }
         });
      }
   }
}