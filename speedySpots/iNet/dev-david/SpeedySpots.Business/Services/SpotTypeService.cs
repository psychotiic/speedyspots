using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class SpotTypeService
    {
        const string CacheItemKey = "System-SpotTypes";

        public static SpotType GetSpotTypeByName(string name, DataAccessDataContext context)
        {
            return GetSpotTypes(context).SingleOrDefault(s => s.Name.ToUpper() == name.ToUpper());
        }

        public static SpotType GetSpotTypeById(int id, DataAccessDataContext context)
        {
            return GetSpotTypes(context).SingleOrDefault(s => s.SpotTypeId == id);
        }

        public static List<SpotType> GetSpotTypes(DataAccessDataContext context)
        {
            List<SpotType> spotTypes;

            if (HttpContext.Current.Cache[CacheItemKey] == null)
            {
                spotTypes = (from n in context.IASpotTypes
                               select new SpotType
                               {
                                   SpotTypeId = n.IASpotTypeID,
                                   Name = n.Name
                               }).ToList();

                if (spotTypes != null)
                {
                    HttpContext.Current.Cache.Add(CacheItemKey, spotTypes, null, DateTime.Now.AddSeconds(14400), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                }
            }
            else
            {
                spotTypes = (List<SpotType>)HttpContext.Current.Cache[CacheItemKey];
            }

            return spotTypes;
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