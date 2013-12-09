using System;

namespace InvoiceWindowsService
{

   // Top level class that activates all service code

   public class Controller
   {
      #region Class Level Variables

      string _errorInformation = string.Empty;
      EventTimer _timer;

      #endregion

      #region functions

      public void StartServiceProcess()
      {
         //-------------------------------
         // Get settings from app.config
         string pollingInterval = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.PollingInterval);
 
         // Convert to milliseconds
         double pollingIntervalNum = Convert.ToDouble(pollingInterval) * 60000;

         // Create timer object
         _timer = new EventTimer();
         _timer.PollingInterval = pollingIntervalNum;
         _timer.InvoicePath = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.InvoicePath);
         _timer.DBConnectstring = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.DBConnectionString);
         _timer.FromEmailAddress = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.InvoiceNoticeEmailFromAddress);
         _timer.ReportEmailTo = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.ReportEmailTo);
         _timer.ErrorNoticeTo = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.ErrorNoticeTo);
         _timer.InvoiceURL = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.InvoiceUrl);
         _timer.PaymentURL = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.PaymentUrl);
         _timer.PostmarkAPIKey = Utilities.ReturnConfigurationSetting(Utilities.ConfigurationKeys.PostMarkAPIKey);

         // Start timer
         _timer.StartTimer();
      }

      public void EndServiceProcess()
      {
         if (_timer != null)
         {
            _timer.EndTimer();
            _timer.DisposeTimer();
         }
      }

      #endregion
   }
}
