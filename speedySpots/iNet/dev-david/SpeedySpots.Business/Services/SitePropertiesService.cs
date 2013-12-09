using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;
using System.Web;

namespace SpeedySpots.Business.Services
{
    public class SitePropertiesService
    {
        private const string CacheItemKey = "IAProperites";

        public SitePropertiesService() {}

        private IAProperty GetSiteProperties()
        {
            return GetSiteProperties(false);
        }

        public IAProperty GetSiteProperties(bool forceDBRead)
        {
            IAProperty properties = null;

            if (HttpContext.Current.Cache[CacheItemKey] == null || forceDBRead)
            {
                properties = Business.RequestContextHelper.CurrentDB().IAProperties.SingleOrDefault();
                if (properties != null && HttpContext.Current.Cache[CacheItemKey] == null)
                {
                    HttpContext.Current.Cache.Add(CacheItemKey, properties, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                properties = (IAProperty)HttpContext.Current.Cache[CacheItemKey];
            }

            return properties;
        }

        public void FlushCache()
        {
            if (HttpContext.Current.Cache[CacheItemKey] != null)
            {
                HttpContext.Current.Cache.Remove(CacheItemKey);
            }
        }

        public void UpdateSiteProperties(IAProperty newProperties)
        {
            IAProperty properties = GetSiteProperties(true);
            properties.SiteName = newProperties.SiteName;
            properties.SiteDomain = newProperties.SiteDomain;
            properties.EmailNewAccount = newProperties.EmailNewAccount;
            properties.EmailAddressFrom = newProperties.EmailAddressFrom;
            properties.EmailAddressFromName = newProperties.EmailAddressFromName;
            properties.EmailBillings = newProperties.EmailBillings;
            properties.AuthorizeNetIsDebug = newProperties.AuthorizeNetIsDebug;
            properties.AuthorizeNetLoginID = newProperties.AuthorizeNetLoginID;
            properties.AuthorizeNetTransactionKey = newProperties.AuthorizeNetTransactionKey;
            properties.ClosedMessage = newProperties.ClosedMessage;
            properties.ClosedMessageDisplayAlways = newProperties.ClosedMessageDisplayAlways;
            properties.MondayInDateTime = newProperties.MondayInDateTime;
            properties.MondayOutDateTime = newProperties.MondayOutDateTime;
            properties.TuesdayInDateTime = newProperties.TuesdayInDateTime;
            properties.TuesdayOutDateTime = newProperties.TuesdayOutDateTime;
            properties.WednesdayInDateTime = newProperties.WednesdayInDateTime;
            properties.WednesdayOutDateTime = newProperties.WednesdayOutDateTime;
            properties.ThursdayInDateTime = newProperties.ThursdayInDateTime;
            properties.ThursdayOutDateTime = newProperties.ThursdayOutDateTime;
            properties.FridayInDateTime = newProperties.FridayInDateTime;
            properties.FridayOutDateTime = newProperties.FridayOutDateTime;
            properties.RecutRequestThreshold = newProperties.RecutRequestThreshold;
            properties.FeedbackEmailProblem = newProperties.FeedbackEmailProblem;
            properties.FeedbackEmailQuestion = newProperties.FeedbackEmailQuestion;
            properties.EmailSystemNotifications = newProperties.EmailSystemNotifications;
            properties.ActivityInterval = newProperties.ActivityInterval;
            properties.EmailEstimatePayment = newProperties.EmailEstimatePayment;
            properties.EnableCreditCardShadowCopy = newProperties.EnableCreditCardShadowCopy;
            Business.RequestContextHelper.CurrentDB().SubmitChanges();

            FlushCache();
            GetSiteProperties(true);
        }

        public string Sitename
        {
            get { return GetSiteProperties().SiteName; }
        }

        public string SiteDomain
        {
            get { return GetSiteProperties().SiteDomain; }
        }

        public string EmailNewAccount
        {
            get { return GetSiteProperties().EmailNewAccount; }
        }

        public string EmailAddressFrom
        {
            get { return GetSiteProperties().EmailAddressFrom; }
        }

        public string EmailAddressFromName
        {
            get { return GetSiteProperties().EmailAddressFromName; }
        }

        public string EmailBillings
        {
            get { return GetSiteProperties().EmailBillings; }
        }

        public bool AuthorizeNetIsDebug
        {
            get { return GetSiteProperties().AuthorizeNetIsDebug; }
        }

        public string AuthorizeNetLoginID
        {
            get { return GetSiteProperties().AuthorizeNetLoginID; }
        }

        public string AuthorizeNetTransactionKey
        {
            get { return GetSiteProperties().AuthorizeNetTransactionKey; }
        }

        public string ClosedMessage
        {
            get { return GetSiteProperties().ClosedMessage; }
        }

        public bool ClosedMessageDisplayAlways
        {
            get { return GetSiteProperties().ClosedMessageDisplayAlways; }
        }

        public DateTime MondayInDateTime
        {
            get { return GetSiteProperties().MondayInDateTime; }
        }

        public DateTime MondayOutDateTime
        {
            get { return GetSiteProperties().MondayOutDateTime; }
        }

        public DateTime TuesdayInDateTime
        {
            get { return GetSiteProperties().TuesdayInDateTime; }
        }

        public DateTime TuesdayOutDateTime
        {
            get { return GetSiteProperties().TuesdayOutDateTime; }
        }

        public DateTime WednesdayInDateTime
        {
            get { return GetSiteProperties().WednesdayInDateTime; }
        }

        public DateTime WednesdayOutDateTime
        {
            get { return GetSiteProperties().WednesdayOutDateTime; }
        }

        public DateTime ThursdayInDateTime
        {
            get { return GetSiteProperties().ThursdayInDateTime; }
        }

        public DateTime ThursdayOutDateTime
        {
            get { return GetSiteProperties().ThursdayOutDateTime; }
        }

        public DateTime FridayInDateTime
        {
            get { return GetSiteProperties().FridayInDateTime; }
        }

        public DateTime FridayOutDateTime
        {
            get { return GetSiteProperties().FridayOutDateTime; }
        }

        public int RecutRequestThreshold
        {
            get { return GetSiteProperties().RecutRequestThreshold; }
        }

        public string FeedbackEmailProblem
        {
            get { return GetSiteProperties().FeedbackEmailProblem; }
        }

        public string FeedbackEmailQuestion
        {
            get { return GetSiteProperties().FeedbackEmailQuestion; }
        }

        public string EmailSystemNotifications
        {
            get { return GetSiteProperties().EmailSystemNotifications; }
        }

        public int ActivityInterval
        {
            get { return GetSiteProperties().ActivityInterval; }
        }

        public string EmailEstimatePayment
        {
            get { return GetSiteProperties().EmailEstimatePayment; }
        }

        public bool EnableCreditCardShadowCopy
        {
           get { return GetSiteProperties().EnableCreditCardShadowCopy; }
        }
    }
}