using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Configuration;

namespace SpeedySpots.Business
{
    public static class Settings
    {
        public static string ConnectionString
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MemberProtectConnectionString"].ConnectionString;
                return connectionString;
            }
        }

        public static string MusicPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["MusicPath"].ToString();
                path = path.EndsWith("/") ? path : path + "/";

                return path;
            }
        }

        public static string UploadPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["UploadPath"].ToString();

                if (!path.EndsWith("\\"))
                {
                    path += "\\";
                }

                return path;
            }
        }

        public static string UploadPathAsURL
        {
            get
            {
                string path = ConfigurationManager.AppSettings["UploadPathAsURL"];
                path = path.EndsWith("/") ? path : path + "/";

                return path;
            }
        }

        public static string InvoicePath
        {
            get
            {
                string path = ConfigurationManager.AppSettings["InvoicePath"].ToString();
                path = path.EndsWith("/") ? path : path + "/";

                return path;
            }
        }

        internal static Models.SmtpSettings GetSmtpSettings()
        {
            return GetSmtpSettings(false);
        }

        internal static Models.SmtpSettings GetSmtpSettings(bool forceDBRead)
        {
            Models.SmtpSettings settings;
            const string smtpCacheKey = "SmtpSettingsKey";

            if (HttpContext.Current.Cache[smtpCacheKey] == null || forceDBRead)
            {
                settings = RequestContextHelper.CurrentDB().ExecuteQuery<Models.SmtpSettings>("SELECT TOP 1 SmtpHost, SmtpPort, SmtpUsername, SmtpPassword FROM MPProperty").SingleOrDefault();
                if (settings != null && HttpContext.Current.Cache[smtpCacheKey] == null)
                {
                    HttpContext.Current.Cache.Add(smtpCacheKey, settings, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1), System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                settings = (Models.SmtpSettings)HttpContext.Current.Cache[smtpCacheKey];
            }

            return settings;
        }

        public static bool EnableWhosOn
        {
            get
            {
                bool enabled = false;

                if (ConfigurationManager.AppSettings["EnableWhosOn"] != null)
                {
                    bool.TryParse(ConfigurationManager.AppSettings["EnableWhosOn"].ToString(), out enabled);
                }
                
                return enabled;
            }
        }

    }
}
