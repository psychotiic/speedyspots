using System;
using System.IO;
using System.Linq;
using log4net;

namespace SpeedySpots_Quickbook_Connector
{
    public class InvoiceEmail
    {
        private static string emailTemplate;
        private static readonly ILog logger = log4net.LogManager.GetLogger(typeof(InvoiceEmail));

        public InvoiceEmail()
        {
            try
            {
                var textReader = File.OpenText(Environment.CurrentDirectory + @"\templates\invoice_email.txt");
                emailTemplate = textReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                emailTemplate = ex.Message;
                logger.ErrorFormat("Unable to read invoice_email.txt template - {0}", ex.Message);
            }
        }

        public string EmailSubject
        {
            get { return "Your Invoice Is Ready"; }
        }

        public string EmailBody
        {
            get { return emailTemplate; }
        }
    }
}
