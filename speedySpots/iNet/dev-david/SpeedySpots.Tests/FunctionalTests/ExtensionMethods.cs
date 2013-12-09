namespace SpeedySpots.Tests.FunctionalTests
{
   using OpenQA.Selenium;

   public static class ExtensionMethods
   {
      public static string Value(this IWebElement webElement)
      {
         return webElement.GetAttribute("value");
      }
   }
}