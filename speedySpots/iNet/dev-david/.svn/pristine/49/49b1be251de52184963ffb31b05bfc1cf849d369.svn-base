using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using SpeedySpots.DataAccess;

namespace SpeedySpots.Business.Services
{
    public static class LabelsService
    {

        public static IEnumerable<IALabel> GetLabels()
        {
            IEnumerable<IALabel> labels;
            string sCacheItemKey = "LabelsList";

            if (HttpContext.Current.Cache[sCacheItemKey] == null)
            {
                labels = RequestContextHelper.CurrentDB().IALabels.OrderBy(row => row.Text);
                if (HttpContext.Current.Cache[sCacheItemKey] == null)
                {
                    HttpContext.Current.Cache.Add(sCacheItemKey, labels, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                }
                else
                {
                    labels = (IEnumerable<IALabel>)HttpContext.Current.Cache[sCacheItemKey];
                }    
            }
            else
            {
                labels = (IEnumerable<IALabel>)HttpContext.Current.Cache[sCacheItemKey];
            }

            return labels;
        }

        public static IEnumerable<fn_Producer_GetLabelFilterResult> GetLabelsForFilterDropDown(DataAccessDataContext context)
        {
            IEnumerable<fn_Producer_GetLabelFilterResult> labels;
            string sCacheItemKey = "LabelsForFilterDropDown";

            if (HttpContext.Current.Cache[sCacheItemKey] == null)
            {
                labels = context.fn_Producer_GetLabelFilter().OrderBy(row => row.SortID).ThenBy(row => row.Text);
                HttpContext.Current.Cache.Add(sCacheItemKey, labels, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
            }
            else
            {
                labels = (IEnumerable<fn_Producer_GetLabelFilterResult>)HttpContext.Current.Cache[sCacheItemKey];
            }

            return labels;
        }

        public static IALabel GetLabel(int labelId)
        {
            IALabel label = GetLabels().Where(l => l.IALabelID == labelId).SingleOrDefault();

            return label;
        }


        public static void FlushLabelsCache()
        {
            HttpContext.Current.Cache.Remove("LabelsList");
            HttpContext.Current.Cache.Remove("LabelsForFilterDropDown");
        }
    }
}