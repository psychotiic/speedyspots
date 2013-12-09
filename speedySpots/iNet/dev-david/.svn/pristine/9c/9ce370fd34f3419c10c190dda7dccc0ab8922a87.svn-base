namespace SpeedySpots.Tests.FunctionalTests
{
   using Base;
   using NUnit.Framework;
   using Pages;

   [TestFixture]
   public class UserDashboardTests : TestBase
   {
      [Test]
      public void ShouldNavigateToMyCreditCards()
      {
         var result = Start
            .LoginAsCustomer()
            .ClickOnMyCreditCards();
         Assert.IsAssignableFrom<MyCreditCardsPage>(result);
      }
   }
}