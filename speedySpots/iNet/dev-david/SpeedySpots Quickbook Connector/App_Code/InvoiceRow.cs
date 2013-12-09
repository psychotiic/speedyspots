using System;
using System.Linq;

namespace SpeedySpots_Quickbook_Connector
{
    public class InvoiceRow : IComparable
    {
        private string m_sID = string.Empty;
        private int m_iInvoiceNumber = 0;
        private string m_sCustomer = string.Empty;
        private DateTime m_oTransactionDate = DateTime.Today;
        private double m_fAmount = 0;
        private string m_sJobID = string.Empty;
        private string m_sSMSID = string.Empty;
        private bool m_bIsValid = false;
        private string m_sEmailSubject = string.Empty;
        private string m_sEmailBody = string.Empty;
        private bool m_bCustomEmail = false;

        public InvoiceRow(string sID, int iInvoiceNumber, string sCustomer, DateTime oTransactionDate, double fAmount, string sJobID, string sSMSID)
        {
            m_sID = sID;
            m_iInvoiceNumber = iInvoiceNumber;
            m_sCustomer = sCustomer;
            m_oTransactionDate = oTransactionDate;
            m_fAmount = fAmount;
            m_sJobID = sJobID;
            m_sSMSID = sSMSID;
        }

        public int CompareTo(object oObject)
        {
            if (oObject == null)
            {
                return 1;
            }

            InvoiceRow oOtherInvoiceRow = oObject as InvoiceRow;
            if (oOtherInvoiceRow != null)
            {
                return this.InvoiceNumber.CompareTo(oOtherInvoiceRow.InvoiceNumber);
            }
            else
            {
                throw new ArgumentException("Object is not an InvoiceRow");
            }
        }

        public int InvoiceNumber
        {
            get
            {
                return m_iInvoiceNumber;
            }
        }

        public string ID
        {
            get
            {
                return m_sID;
            }
        }

        public string Customer
        {
            get
            {
                return m_sCustomer;
            }
        }

        public DateTime TransactionDate
        {
            get
            {
                return m_oTransactionDate;
            }
        }

        public double Amount
        {
            get
            {
                return m_fAmount;
            }
        }

        public string JobID
        {
            get
            {
                return m_sJobID;
            }
        }

        public string SMSID
        {
            get
            {
                return m_sSMSID;
            }
        }

        public bool IsValid
        {
            get
            {
                return m_bIsValid;
            }
            set
            {
                m_bIsValid = value;
            }
        }

        public string EmailSubject
        {
            get
            {
                return m_sEmailSubject;
            }
            set
            {
                m_sEmailSubject = value;
            }
        }

        public string EmailBody
        {
            get
            {
                return m_sEmailBody;
            }
            set
            {
                m_sEmailBody = value;
            }
        }

        public bool CustomEmail
        {
            get { return m_bCustomEmail; }
            set { m_bCustomEmail = value; }
        }
    }
}