using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class SpotFeeTypeService
    {
        const string CacheItemKey = "System-SpotFeeTypes";

        public static List<SpotFeeType> GetSpotFeeTypes(DataAccessDataContext context)
        {
            List<SpotFeeType> fees;

            if (HttpContext.Current.Cache[CacheItemKey] == null)
            {
                fees = (from n in context.IASpotFeeTypes
                        orderby n.IASpotFeeTypeID
                        select new SpotFeeType
                        {
                            SpotFeeTypeId = n.IASpotFeeTypeID,
                            Name = n.Name
                        }).ToList();

                if (fees != null)
                {
                    HttpContext.Current.Cache.Add(CacheItemKey, fees, null, DateTime.Now.AddSeconds(14400), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                fees = (List<SpotFeeType>)HttpContext.Current.Cache[CacheItemKey];
            }

            return fees;
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