namespace SpeedySpots
{
   using System;
   using System.Configuration;
   using System.Web;
   using System.Web.UI;
   using DataAccess;
   using Elmah;
   using log4net.Config;

   public class Global : HttpApplication
   {
      protected void Application_Start(object sender, EventArgs e)
      {
         var oDataAccess = new DataAccessDataContext(ConfigurationManager.ConnectionStrings["MemberProtectConnectionString"].ConnectionString);

         // Maintenance Tasks
         // 1. Remove all companies/orgs for which they have no users
         oDataAccess.sp_System_PurgeCompanies();

         XmlConfigurator.Configure();
      }

      protected void Session_Start(object sender, EventArgs e)
      {
      }

      protected void Application_BeginRequest(object sender, EventArgs e)
      {
      }

      protected void Application_AuthenticateRequest(object sender, EventArgs e)
      {
      }

      protected void Application_Error(object sender, EventArgs e)
      {
      }

      protected void Session_End(object sender, EventArgs e)
      {
      }

      protected void Application_End(object sender, EventArgs e)
      {
      }

      public void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
      {
         Filter(e);
      }

      public void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
      {
         Filter(e);
      }

      private void Filter(ExceptionFilterEventArgs e)
      {
         var exception = e.Exception.GetBaseException();
         var httpException = exception as HttpException;

         if (HttpContext.Current.Request.IsLocal)
         {
            e.Dismiss();
            return;
         }

         // User hit stop/esc while the page was posting back
         if (exception is HttpException && exception.Message.ToLower().Contains("the client disconnected.") ||
             exception.Message.ToLower().Contains("the remote host closed the connection."))
         {
            e.Dismiss();
            return;
         }

         // User hit stop while posting back and thew this error
         if (exception is ViewStateException && exception.Message.ToLower().Contains("invalid viewstate. client ip:"))
         {
            e.Dismiss();
            return;
         }
      }
   }
}