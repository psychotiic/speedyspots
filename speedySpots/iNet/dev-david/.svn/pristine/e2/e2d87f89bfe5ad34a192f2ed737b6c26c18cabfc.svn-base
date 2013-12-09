using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SpeedySpots.API.Interfaces
{
    [Serializable]
    public sealed class Customer : ISerializable
    {
        private int                                 m_iSMSID = 0;
        private string                              m_sName = string.Empty;
        private string                              m_sShippingAddressLine1 = string.Empty;
        private string                              m_sShippingAddressLine2 = string.Empty;
        private string                              m_sShippingCity = string.Empty;
        private string                              m_sShippingState = string.Empty;
        private string                              m_sShippingCountry = string.Empty;
        private string                              m_sShippingZip = string.Empty;
        private string                              m_sBillingAddressLine1 = string.Empty;
        private string                              m_sBillingAddressLine2 = string.Empty;
        private string                              m_sBillingCity = string.Empty;
        private string                              m_sBillingState = string.Empty;
        private string                              m_sBillingCountry = string.Empty;
        private string                              m_sBillingZip = string.Empty;
        private string                              m_sBillingName = string.Empty;
        private string                              m_sBillingPhone = string.Empty;
        private string                              m_sContact = string.Empty;
        private string                              m_sPhone = string.Empty;
        private string                              m_sFax = string.Empty;
        private string                              m_sInvoiceEmails = string.Empty;

        public Customer()
        {

        }

        public Customer(SerializationInfo info, StreamingContext context)
        {
            m_iSMSID = info.GetInt32("SMSID");
            m_sName = info.GetString("Name");

            m_sShippingAddressLine1 = info.GetString("ShippingAddressLine1");
            m_sShippingAddressLine2 = info.GetString("ShippingAddressLine2");
            m_sShippingCity = info.GetString("ShippingCity");
            m_sShippingState = info.GetString("ShippingState");
            m_sShippingCountry = info.GetString("ShippingCountry");
            m_sShippingZip = info.GetString("ShippingZip");
            m_sBillingAddressLine1 = info.GetString("BillingAddressLine1");
            m_sBillingAddressLine2 = info.GetString("BillingAddressLine2");
            m_sBillingCity = info.GetString("BillingCity");
            m_sBillingState = info.GetString("BillingState");
            m_sBillingCountry = info.GetString("BillingCountry");
            m_sBillingZip = info.GetString("BillingZip");
            m_sBillingName = info.GetString("BillingName");
            m_sBillingPhone = info.GetString("BillingPhone");
            m_sContact = info.GetString("Contact");
            m_sPhone = info.GetString("Phone");
            m_sFax = info.GetString("Fax");
            m_sInvoiceEmails = info.GetString("InvoiceEmails");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SMSID", m_iSMSID);
            info.AddValue("Name", m_sName);
            info.AddValue("ShippingAddressLine1", m_sShippingAddressLine1);
            info.AddValue("ShippingAddressLine2", m_sShippingAddressLine2);
            info.AddValue("ShippingCity", m_sShippingCity);
            info.AddValue("ShippingState", m_sShippingState);
            info.AddValue("ShippingCountry", m_sShippingCountry);
            info.AddValue("ShippingZip", m_sShippingZip);
            info.AddValue("BillingAddressLine1", m_sBillingAddressLine1);
            info.AddValue("BillingAddressLine2", m_sBillingAddressLine2);
            info.AddValue("BillingCity", m_sBillingCity);
            info.AddValue("BillingState", m_sBillingState);
            info.AddValue("BillingCountry", m_sBillingCountry);
            info.AddValue("BillingZip", m_sBillingZip);
            info.AddValue("BillingName", m_sBillingName);
            info.AddValue("BillingPhone", m_sBillingPhone);
            info.AddValue("Contact", m_sContact);
            info.AddValue("Phone", m_sPhone);
            info.AddValue("Fax", m_sFax);
            info.AddValue("InvoiceEmails", m_sInvoiceEmails);
        }

        #region Public Properties
        public int SMSID
        {
            set { m_iSMSID = value; }
            get { return m_iSMSID; }
        }

        public string Name
        {
            set { m_sName = value; }
            get { return m_sName; }
        }

        public string ShippingAddressLine1
        {
            set { m_sShippingAddressLine1 = value; }
            get { return m_sShippingAddressLine1; }
        }

        public string ShippingAddressLine2
        {
            set { m_sShippingAddressLine2 = value; }
            get { return m_sShippingAddressLine2; }
        }

        public string ShippingCity
        {
            set { m_sShippingCity = value; }
            get { return m_sShippingCity; }
        }

        public string ShippingState
        {
            set { m_sShippingState = value; }
            get { return m_sShippingState; }
        }

        public string ShippingCountry
        {
            set { m_sShippingCountry = value; }
            get { return m_sShippingCountry; }
        }

        public string ShippingZip
        {
            set { m_sShippingZip = value; }
            get { return m_sShippingZip; }
        }

        public string BillingAddressLine1
        {
            set { m_sBillingAddressLine1 = value; }
            get { return m_sBillingAddressLine1; }
        }

        public string BillingAddressLine2
        {
            set { m_sBillingAddressLine2 = value; }
            get { return m_sBillingAddressLine2; }
        }

        public string BillingCity
        {
            set { m_sBillingCity = value; }
            get { return m_sBillingCity; }
        }

        public string BillingState
        {
            set { m_sBillingState = value; }
            get { return m_sBillingState; }
        }

        public string BillingCountry
        {
            set { m_sBillingCountry = value; }
            get { return m_sBillingCountry; }
        }

        public string BillingZip
        {
            set { m_sBillingZip = value; }
            get { return m_sBillingZip; }
        }

        public string BillingName
        {
            set { m_sBillingName = value; }
            get { return m_sBillingName; }
        }

        public string BillingPhone
        {
            set { m_sBillingPhone = value; }
            get { return m_sBillingPhone; }
        }

        public string Contact
        {
            set { m_sContact = value; }
            get { return m_sContact; }
        }

        public string Phone
        {
            set { m_sPhone = value; }
            get { return m_sPhone; }
        }
        
        public string Fax
        {
            set { m_sFax = value; }
            get { return m_sFax; }
        }
        
        public string InvoiceEmails
        {
            set { m_sInvoiceEmails = value; }
            get { return m_sInvoiceEmails; }
        }
        #endregion
    }
}
