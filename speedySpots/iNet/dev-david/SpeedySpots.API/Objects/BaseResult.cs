using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SpeedySpots.API
{
    [Serializable]
    public class BaseResult : ISerializable
    {
        private bool                                m_bIsSuccessfull = true;
        private string                              m_sErrorMessage = string.Empty;

        public BaseResult()
        {

        }

        public BaseResult(SerializationInfo info, StreamingContext context)
        {
            m_bIsSuccessfull = info.GetBoolean("IsSuccessfull");
            m_sErrorMessage = info.GetString("ErrorMessage");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsSuccessfull", m_bIsSuccessfull);
            info.AddValue("ErrorMessage", m_sErrorMessage);
        }

        #region Public Properties
        public bool IsSuccessfull
        {
            get { return m_bIsSuccessfull; }
            set { m_bIsSuccessfull = value; }
        }

        public string ErrorMessage
        {
            get { return m_sErrorMessage; }
            set { m_sErrorMessage = value; }
        }
        #endregion
    }
}
