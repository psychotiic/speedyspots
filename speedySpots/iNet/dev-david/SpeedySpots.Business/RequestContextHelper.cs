using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using InetSolution.MemberProtect;
using SpeedySpots.DataAccess;
using log4net;

namespace SpeedySpots.Business
{
    public static class RequestContextHelper
    {
        private const string MPInstanceKey = "MemberProtectInstances";
        private const string UrlPathsKey = "UrlPaths";
        private static readonly ILog log = LogManager.GetLogger(typeof(RequestContextHelper));

        public static DataAccessDataContext CurrentDB()
        {
            const string itemKey = "CurrentDB";
            if (HttpContext.Current.Items[itemKey] == null)
            {
                DataAccessDataContext currentDB = new DataAccessDataContext(Settings.ConnectionString);
                HttpContext.Current.Items[itemKey] = currentDB;
                return currentDB;
            }

            return (DataAccessDataContext)HttpContext.Current.Items[itemKey];
        }

        public static void Init(MemberProtect app)
        {
            HttpContext.Current.Items[MPInstanceKey] = app;
        }

        public static string GetRootUrl(string pageName)
        {           
            if (HttpContext.Current.Request.Url.IsDefaultPort)
            {
                return string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, pageName);
            }
            else
            {
                return string.Format("{0}://{1}:{2}{3}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port, pageName);
            }
        }

        public static MemberProtect MemberProtect
        {
            get
            {
                if (HttpContext.Current.Items[MPInstanceKey] == null)
                {
                    // We lost or context copy of MP for this request, fire up a new one and log it
                    MemberProtectSettings oSettings = new MemberProtectSettings(Business.Settings.ConnectionString, HttpContext.Current.Session.SessionID, HttpContext.Current.Request.UserAgent, HttpContext.Current.Request.UserHostAddress);
                    HttpContext.Current.Items[MPInstanceKey] = new MemberProtect(oSettings);
                    log.Error("Lost httpContext instances of MemberProtect");
                }

                return (MemberProtect)HttpContext.Current.Items[MPInstanceKey];
            }
        }

    }
}