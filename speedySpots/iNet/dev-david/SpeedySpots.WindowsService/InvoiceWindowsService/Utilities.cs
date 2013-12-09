using System;
using System.Diagnostics;
using System.Configuration;

namespace InvoiceWindowsService
{
   static class Utilities
   {  
      public enum ConfigurationKeys
      {
         DBConnectionString,
         PollingInterval,
         InvoicePath,
         InvoiceNoticeEmailFromName,
         InvoiceNoticeEmailFromAddress,
         ReportEmailTo,
         ErrorNoticeTo,
         PostMarkAPIKey,
         InvoiceUrl,
         PaymentUrl
      }

      public enum DatabaseTables
      {
         Worker_InvoicePackage,
         Worker_InvoiceNoticeSent
      }

      // Write to event log
      static public void WriteEventLog(string source, string log, string eventInfo)
      {
         if (!EventLog.SourceExists(source))
         {
            EventLog.CreateEventSource(source, log);
         }

         EventLog.WriteEntry(source, eventInfo);
      }

      static public string ReturnConfigurationSetting(ConfigurationKeys sConfigName)
      {
         // Given a configuration file key, return the value from the App.Config File
         
         AppSettingsReader appSettings = new AppSettingsReader();
         string configValue = appSettings.GetValue(sConfigName.ToString(), typeof(string)).ToString();

         return configValue;
      }

      static public byte[] FileToByteArray(string _FileName)
      {
         byte[] _Buffer = null;

         try
         {
            // Open file for reading
            System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            // attach filestream to binary reader
            System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

            // get total byte length of the file
            long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

            // read entire file into buffer
            _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

            // close file reader
            _FileStream.Close();
            _FileStream.Dispose();
            _BinaryReader.Close();
         }
         catch (Exception ex)
         {
            string errorInfo = ex.ToString();
            // Error
            //Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
         }

         return _Buffer;
      }

   }
}
