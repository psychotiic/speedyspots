namespace SpeedySpots.Business.Helpers
{
   using System.Linq;
   using System.Text;
   using System.Text.RegularExpressions;

   public static class Email
   {
      public static string CleanAddressList(string emailList)
      {
         var sb = new StringBuilder();
         emailList = emailList.Trim();
         emailList = emailList.Replace("<", "");
         emailList = emailList.Replace(">", "");
         emailList = emailList.Replace(";", ",");
         emailList = emailList.Replace("|", ",");
         emailList = emailList.Replace(" ", ",");
         emailList = emailList.Replace(",,", ",");
         emailList = emailList.Replace(",,", ",");
         emailList = emailList.Replace(",", ",");

         var emails = emailList.Split(',');

         foreach (var emailAddress in emails.Where(emailAddress => EmailAddress.Match(emailAddress).Success))
         {
            sb.Append(emailAddress);
            sb.Append(", ");
         }
         var cleanedEmailList = sb.ToString();
         if (cleanedEmailList.Length > 2)
         {
            cleanedEmailList = cleanedEmailList.Remove(cleanedEmailList.Length - 2, 2);
         }

         return cleanedEmailList;
      }

      public static Regex EmailAddress
      {
         get { return new Regex(@"^([A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4})$", RegexOptions.IgnoreCase); }
      }

      public static bool IsValidEmail(string emailAddress)
      {
         var emailMatch = EmailAddress.Match(emailAddress.Trim());
         return emailMatch.Success;
      }

      public static string ConvertMultiLineEmailsToCommaSeperated(string multilineEmails)
      {
         var emails = multilineEmails.Split('\n');
         var returnEmails = emails.Where(email => email != null && IsValidEmail(email))
                                  .Aggregate(string.Empty, (current, email) => current + (email.Trim() + ", "));
         returnEmails = returnEmails.EndsWith(", ") ? returnEmails.Remove(returnEmails.Length - 2) : returnEmails;
         return returnEmails;
      }
   }
}