using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeedySpots.API.Interfaces;
using System.Runtime.Serialization;

namespace SpeedySpots.API.Objects
{
    public class PaymentResult : BaseResult
    {
        private List<Payment> m_oPayments = new List<Payment>();

        #region Public Properties
        public List<Payment> Payments
        {
            get { return m_oPayments; }
            set { m_oPayments = value; }
        }
        #endregion
    }
}
