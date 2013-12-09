using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class TalentTypeService
    {
        const string CacheItemKey = "System-TalentTypes";

        public static List<TalentType> GetTalentTypes(DataAccessDataContext context)
        {
            List<TalentType> talentTypes;

            if (HttpContext.Current.Cache[CacheItemKey] == null)
            {
                talentTypes = (from n in context.IATalentTypes
                        orderby n.Sort
                        select new TalentType
                        {
                            TalentTypeId = n.IATalentTypeID,
                            Name = n.Name,
                            SortOrder = n.Sort
                        }).ToList();

                if (talentTypes != null)
                {
                    HttpContext.Current.Cache.Add(CacheItemKey, talentTypes, null, DateTime.Now.AddSeconds(14400), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                talentTypes = (List<TalentType>)HttpContext.Current.Cache[CacheItemKey];
            }

            return talentTypes;
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