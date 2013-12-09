using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class SpotFileTypeService
    {
        const string CacheItemKey = "System-SpotFileTypes";

        public static SpotFileType GetSpotTypeByName(string name, DataAccessDataContext context)
        {
            return GetSpotFileTypes(context).SingleOrDefault(s => s.Name.ToUpper() == name.ToUpper());
        }

        public static SpotFileType GetSpotTypeById(int id, DataAccessDataContext context)
        {
            return GetSpotFileTypes(context).SingleOrDefault(s => s.SpotFileTypeId == id);
        }

        public static List<SpotFileType> GetSpotFileTypes(DataAccessDataContext context)
        {
            List<SpotFileType> fees;

            if (HttpContext.Current.Cache[CacheItemKey] == null)
            {
                fees = (from n in context.IASpotFileTypes
                        orderby n.IASpotFileTypeID
                        select new SpotFileType
                        {
                            SpotFileTypeId = n.IASpotFileTypeID,
                            Name = n.Name
                        }).ToList();

                if (fees != null)
                {
                    HttpContext.Current.Cache.Add(CacheItemKey, fees, null, DateTime.Now.AddSeconds(14400), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                fees = (List<SpotFileType>)HttpContext.Current.Cache[CacheItemKey];
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