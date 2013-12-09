namespace SpeedySpots.Tests.FunctionalTests.Base
{
   using NUnit.Framework;
   using Pages;

   
   public class TestBase
   {
      private readonly WebAppDriver _driver;
      protected readonly string BaseUrl = "http://localhost:52483/";

      public TestBase()
      {
         _driver = new WebAppDriver(BaseUrl);
      }

      [TestFixtureSetUp]
      public void TestFixtureSetup()
      {
      }

      [SetUp]
      public virtual void Setup()
      {
      }

      [TestFixtureTearDown]
      public void TestFixtureTearDown()
      {
         _driver.Quit();
      }

      protected WebAppDriver Driver
      {
         get { return _driver; }
      }

      protected LoginPage Start
      {
         get
         {
            var page = new LoginPage(_driver);
            return page.Start();
         }
      }
   }
}