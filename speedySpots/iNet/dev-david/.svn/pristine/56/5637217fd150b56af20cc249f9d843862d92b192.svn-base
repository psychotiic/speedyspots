using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SpeedySpots.API.Interfaces
{
    [Serializable]
    public sealed class Invoice : ISerializable
    {
        private string                              m_sInvoiceID = string.Empty;
        private int                                 m_iIAJobID = 0;
        private string                              m_sCustomerID = string.Empty;
        private string                              m_sInvoiceNumber = string.Empty;
        private DateTime                            m_oDueDateTime = new DateTime(1900, 1, 1, 0, 0, 0, 0);
        private decimal                             m_fAmount = 0;
        private string                              m_sFilename = string.Empty;
        private bool                                m_bIsEmail = false;
        private string                              m_sEmailSubject = string.Empty;
        private string                              m_sEmailBody = string.Empty;

        public Invoice()
        {
        }

        public Invoice(SerializationInfo info, StreamingContext context)
        {
            m_sInvoiceID = info.GetString("InvoiceID");
            m_iIAJobID = info.GetInt32("IAJobID");
            m_sCustomerID = info.GetString("CustomerID");
            m_sInvoiceNumber = info.GetString("InvoiceNumber");
            m_oDueDateTime = info.GetDateTime("DueDateTime");
            m_fAmount = info.GetDecimal("Amount");
            m_sFilename = info.GetString("Filename");
            m_bIsEmail = info.GetBoolean("IsEmail");
            m_sEmailSubject = info.GetString("EmailSubject");
            m_sEmailBody = info.GetString("EmailBody");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InvoiceID", m_sInvoiceID);
            info.AddValue("IAJobID", m_iIAJobID);
            info.AddValue("CustomerID", m_sCustomerID);
            info.AddValue("InvoiceNumber", m_sInvoiceNumber);
            info.AddValue("DueDateTime", m_oDueDateTime);
            info.AddValue("Amount", m_fAmount);
            info.AddValue("Filename", m_sFilename);
            info.AddValue("IsEmail", m_bIsEmail);
            info.AddValue("EmailSubject", m_sEmailSubject);
            info.AddValue("EmailBody", m_sEmailBody);
        }

        #region Public Properties
        public string InvoiceID
        {
            set { m_sInvoiceID = value; }
            get { return m_sInvoiceID; }
        }

        public int IAJobID
        {
            set { m_iIAJobID = value; }
            get { return m_iIAJobID; }
        }

        public string CustomerID
        {
            set { m_sCustomerID = value; }
            get { return m_sCustomerID; }
        }

        public string InvoiceNumber
        {
            set { m_sInvoiceNumber = value; }
            get { return m_sInvoiceNumber; }
        }

        public DateTime DueDateTime
        {
            set { m_oDueDateTime = value; }
            get { return m_oDueDateTime; }
        }

        public decimal Amount
        {
            set { m_fAmount = value; }
            get { return m_fAmount; }
        }

        public string Filename
        {
            set { m_sFilename = value; }
            get { return m_sFilename; }
        }

        public bool IsEmail
        {
            get { return m_bIsEmail; }
            set { m_bIsEmail = value; }
        }

        public string EmailSubject
        {
            get { return m_sEmailSubject; }
            set { m_sEmailSubject = value; }
        }

        public string EmailBody
        {
            get { return m_sEmailBody; }
            set { m_sEmailBody = value; }
        }
        #endregion
    }
}
