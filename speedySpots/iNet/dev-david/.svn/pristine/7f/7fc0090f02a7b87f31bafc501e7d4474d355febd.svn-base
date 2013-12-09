namespace SpeedySpots.Tests.FunctionalTests
{
   using Base;
   using NUnit.Framework;

   [TestFixture]
   public class MyCreditCardsTests : TestBase
   {
      [Test]
      public void ShouldFailToAddCreditCardWithMissingData()
      {
         var creditCardPage = Start
            .LoginAsCustomer()
            .ClickOnMyCreditCards()
            .ClickOnAddNewCreditCard()
            .SaveCreditCard();

         Assert.IsTrue(creditCardPage.ValidationSummary.Text.Contains("Please fix the following issues to continue:"));
      }

      [Test]
      public void ShouldSupportFullCreditCardWorkflow()
      {
         var step1 = Start
            .LoginAsCustomer()
            .ClickOnMyCreditCards()
            .ClickOnAddNewCreditCard()
            .PopulateWithValidData()
            .SaveCreditCard();

         Assert.IsTrue(step1.Messages.Text.Contains("Credit card saved"));

         var step2 = step1.ClickOnMyCreditCards()
                          .ClickOnExistingCard();

         Assert.AreEqual(PageData.CardAlias, step2.AliasTextBox.Value());
         Assert.AreEqual(PageData.CardNumber.Substring(11, 4), step2.CardEndingInLabel.Text);
         //Assert.AreEqual(PageData.CardReceiptEmail, step2.ReceiptEmailTextBox.Value());

         var step3 = step2.ClickOnDeleteCreditCard();

         Assert.IsTrue(step3.Messages.Text.Contains("Credit card has been deleted"));
      }

      [Test]
      public void ShouldNotDisplayCardFromAnotherUser()
      {
         var step1 = Start
            .LoginAsCustomer()
            .ClickOnMyCreditCards()
            .NavigateToCardId(8);

         Assert.IsTrue(step1.Messages.Text.Contains("Credit card not found"));
      }
   }
}