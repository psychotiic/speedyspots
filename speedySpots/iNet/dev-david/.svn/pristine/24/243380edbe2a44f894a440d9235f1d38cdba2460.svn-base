using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SpeedySpots.API.Interfaces
{
    [Serializable]
    public sealed class Payment : ISerializable
    {
        private string                              m_sCreditCardFirstName = string.Empty;
        private string                              m_sCreditCardLastName = string.Empty;
        private string                              m_sCreditCardType = string.Empty;
        private string                              m_sAuthorizeNetID = string.Empty;
        private List<PaymentLineItem>               m_oLineItems = new List<PaymentLineItem>();

        public Payment()
        {

        }

        public Payment(SerializationInfo info,StreamingContext context)
        {
            m_sCreditCardFirstName = info.GetString("CreditCardFirstName");
            m_sCreditCardLastName = info.GetString("CreditCardLastName");
            m_sCreditCardType = info.GetString("CreditCardType");
            m_sAuthorizeNetID = info.GetString("AuthorizeNetID");
            m_oLineItems = (List<PaymentLineItem>)info.GetValue("LineItems", typeof(List<PaymentLineItem>));
            CreatedDateTime = DateTime.Parse(info.GetString("CreatedDateTime"));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CreditCardFirstName", m_sCreditCardFirstName);
            info.AddValue("CreditCardLastName", m_sCreditCardLastName);
            info.AddValue("CreditCardType", m_sCreditCardType);
            info.AddValue("AuthorizeNetID", m_sAuthorizeNetID);
            info.AddValue("CreatedDateTime", this.CreatedDateTime);
            info.AddValue("LineItems", m_oLineItems);
        }

        #region Public Properties
        public string CreditCardFirstName
        {
            set { m_sCreditCardFirstName = value; }
            get { return m_sCreditCardFirstName; }
        }

        public string CreditCardLastName
        {
            set { m_sCreditCardLastName = value; }
            get { return m_sCreditCardLastName; }
        }

        public string CreditCardType
        {
            set { m_sCreditCardType = value; }
            get { return m_sCreditCardType; }
        }

        public string AuthorizeNetID
        {
            set { m_sAuthorizeNetID = value; }
            get { return m_sAuthorizeNetID; }
        }

        public List<PaymentLineItem> LineItems
        {
            get { return m_oLineItems; }
            set { m_oLineItems = value; }
        }

        public DateTime CreatedDateTime { get; set; }
        
        #endregion
    }

    [Serializable]
    public sealed class PaymentLineItem : ISerializable
    {
        private string                              m_sInvoiceID = string.Empty;
        private string                              m_sQBInvoiceID = string.Empty;
        private decimal                             m_fAmount = 0;

        public PaymentLineItem()
        {

        }

        public PaymentLineItem(SerializationInfo info, StreamingContext context)
        {
            m_sInvoiceID = info.GetString("InvoiceID");
            m_sQBInvoiceID = info.GetString("QBInvoiceID");
            m_fAmount = info.GetDecimal("Amount");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InvoiceID", m_sInvoiceID);
            info.AddValue("QBInvoiceID", m_sQBInvoiceID);
            info.AddValue("Amount", m_fAmount);
        }

        public string InvoiceID
        {
            set { m_sInvoiceID = value; }
            get { return m_sInvoiceID; }
        }

        public string QBInvoiceID
        {
            set { m_sQBInvoiceID = value; }
            get { return m_sQBInvoiceID; }
        }

        public decimal Amount
        {
            set { m_fAmount = value; }
            get { return m_fAmount; }
        }
    }
}
