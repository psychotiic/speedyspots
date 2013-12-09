using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedySpots.Services.Models
{
    public class Result
    {
        private int                                 m_iCode = 0;
        private string                              m_sMessage = string.Empty;
        private object                              m_oData = null;

        public Result()
        {
        }

        public Result(string sMessage)
        {
            m_sMessage = sMessage;
        }

        public Result(int iCode, string sMessage)
        {
            m_iCode = iCode;
            m_sMessage = sMessage;
        }

        public Result(int iCode, object oData)
        {
            m_iCode = iCode;
            m_oData = oData;
        }

        public Result(int iCode, string sMessage, object oData)
        {
            m_iCode = iCode;
            m_sMessage = sMessage;
            m_oData = oData;
        }

        #region Public Properties
        public int Code
        {
            get { return m_iCode; }
            set { m_iCode = value; }
        }

        public string Message
        {
            get { return m_sMessage; }
            set { m_sMessage = value; }
        }

        public object Data
        {
            get { return m_oData; }
            set { m_oData = value; }
        }
        #endregion
    }
}