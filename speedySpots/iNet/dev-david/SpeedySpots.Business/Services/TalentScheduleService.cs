using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class TalentScheduleService
    {
        const string CacheItemKey = "TalentSchedules";

        public static IATalentSchedule GetTalentSchedule(Guid talentUserId, DataAccessDataContext context)
        {
            return GetTalentSchedules(context).Where(t => t.MPUserID == talentUserId).SingleOrDefault();
        }

        public static IList<IATalentSchedule> GetTalentSchedules(DataAccessDataContext context)
        {
            IList<IATalentSchedule> talentSchedules;

            if (HttpContext.Current.Cache[CacheItemKey] == null)
            {
                talentSchedules = context.IATalentSchedules.ToList();

                if (talentSchedules != null)
                {
                    HttpContext.Current.Cache.Add(CacheItemKey, talentSchedules, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                talentSchedules = (IList<IATalentSchedule>)HttpContext.Current.Cache[CacheItemKey];
            }

            return talentSchedules;
        }

        public static void InvalidateCache()
        {
            if (HttpContext.Current.Cache[CacheItemKey] != null)
            {
                HttpContext.Current.Cache.Remove(CacheItemKey);
            }
        }

    }
}