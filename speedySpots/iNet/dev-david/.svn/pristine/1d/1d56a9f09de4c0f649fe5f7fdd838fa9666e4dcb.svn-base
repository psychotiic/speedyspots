using System.Timers;
using System.Data;
using SSInvoiceServiceLibrary;
using log4net;

namespace InvoiceWindowsService
{
   public class EventTimer
   {
      #region Class Level Variables

      // Class level variables
      private Timer _timer;
      private string _erroInformation = string.Empty;
      private string _invoicePath = string.Empty;
      private string _dbConnectstring = string.Empty;
      private double _pollingInterval;
      private string _zipFilePath = string.Empty;
      private string _fromEmailAddress = string.Empty;
      private string _reportEmailTo = string.Empty;
      private string _errorNoticeTo = string.Empty;
      private string _postmarkAPIKey = string.Empty;
      private string _invoiceURL = string.Empty;
      private string _paymentURL = string.Empty;
      private ILog _logger = log4net.LogManager.GetLogger("EventTimer");

      #endregion

      // Properties
      #region Properties

      public string ErrorInformation
      { get { return _erroInformation; } }

      public string InvoicePath { set; private get; }

      public string DBConnectstring
      { set { _dbConnectstring = value; } }

      public double PollingInterval
      { set { _pollingInterval = value; } }

      public string ZipFilePath
      { set { _zipFilePath = value; } }

      public string FromEmailAddress
      { set { _fromEmailAddress = value; } }

      public string ReportEmailTo
      { set { _reportEmailTo = value; } }

      public string ErrorNoticeTo
      { set { _errorNoticeTo = value; } }

      public string PostmarkAPIKey
      { set { _postmarkAPIKey = value; } }

      public string InvoiceURL
      { set { _invoiceURL = value; } }

      public string PaymentURL
      { set { _paymentURL = value; } }

      #endregion

      // Functions
      #region Functions

      public void StartTimer()
      {
         //_logger.Debug("Timer starting");
         if (_timer == null)
         {
            _timer = new Timer(_pollingInterval);
            _timer.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
         }

         _timer.Enabled = true;
         //_logger.Debug("Timer started");
      }

      public void EndTimer()
      {
         if (_timer != null)
         {
            _timer.Enabled = false;
         }
      }

      public void DisposeTimer()
      {
         _timer.Enabled = false;
         _timer.Close();
         _timer.Dispose();
      }

      private void OnTimerElapsed(object source, ElapsedEventArgs e)    
      {
         // Timer fires

         //_logger.Debug("Timer elapsed");

         // Turn off the timer
         EndTimer();

         // Process Invoices
         ProcessInvoicePackage();

         // Start timer
         StartTimer();
      }

      public bool ProcessInvoicePackage()
      {
         // Check the database to see if there are any invoices
         DataSet ds;
         bool importSuccessful = true;

         DBFunctions oFunctions = new DBFunctions(_dbConnectstring);
         ds = oFunctions.ReturnDataSetOfReadyInvoices();
         _erroInformation = oFunctions.ErrorInformation;

         if (_erroInformation.Length > 0)
         {
             // Error retrieving invoices
             _logger.ErrorFormat("Error looking up invoices: {0}", _erroInformation);
             importSuccessful = false;
         }
         else if (ds.Tables[0].Rows.Count == 0)
         {
            // There are no invoices
            importSuccessful = true;
         }
         else
         {
             _logger.DebugFormat("Processing Invoices: {0}", ds.Tables[0].Rows.Count);

            // Pass to invoice process to process
            InvoiceProcesses invProcess = new InvoiceProcesses();
            invProcess.ConnectString = _dbConnectstring;
            invProcess.InvoicePath = InvoicePath;
            invProcess.FromEmailAddress = _fromEmailAddress;
            invProcess.PaymentURL = _paymentURL;
            invProcess.InvoiceURL = _invoiceURL;
            invProcess.PostmarkAPI = _postmarkAPIKey;
            invProcess.ErrorNoticeEmail = _errorNoticeTo;
            invProcess.ReportEmailTo = _reportEmailTo;

            invProcess.ProcessInvoices(ds, _invoicePath);

         }
        
         ds.Dispose();

         return importSuccessful;
      }

      #endregion
   }
}
