namespace SpeedySpots.Tests.FunctionalTests
{
   using Base;
   using NUnit.Framework;
   using Pages;

   [TestFixture]
   public class LoginTests : TestBase
   {
      [Test]
      public void ShouldLoginWithValidCredentials()
      {
         var result = Start.LoginAsCustomer();
         Assert.IsAssignableFrom<UserDashboardPage>(result);
         Assert.IsTrue(result.Messages.Text.Contains("You've successfully logged in"));
      }

      [Test]
      public void ShouldReceiveErrorMessageWithInvalidCredentials()
      {
         var result = Start.SignInWithInvalidCredentials();
         Assert.IsTrue(result.Messages.Text.Contains("Username or password is incorrect"));
      }
   }
}