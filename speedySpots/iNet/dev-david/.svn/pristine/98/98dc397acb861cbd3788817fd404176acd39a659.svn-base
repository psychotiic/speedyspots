using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class TalentsService
    {
        const string CacheItemKeyTalentList = "AllTalentList";
        const string CacheItemKeyTalentInTypeList = "TalentInTypeList";

        public static IList<Talent> GetAllTalents(DataAccessDataContext context)
        {
            IList<Talent> talents;

            if (HttpContext.Current.Cache[CacheItemKeyTalentList] == null)
            {
                talents = GetTalentsFromDB(context);
                HttpContext.Current.Cache.Add(CacheItemKeyTalentList, talents, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            else
            {
                talents = (IList<Talent>)HttpContext.Current.Cache[CacheItemKeyTalentList];
            }

            return talents;
        }

        private static IList<Talent> GetTalentsFromDB(DataAccessDataContext context)
        {
            var query = from t in context.MPUserDatas
                        where t.IsArchived == "N" && t.IsTalent == "Y"
                        orderby t.FirstName, t.LastName
                        select new Talent
                        {
                            MPUserId = t.MPUserID,
                            FirstName = t.FirstName,
                            LastName = t.LastName,
                            TalentTypes = (from tt in context.IATalentTypes
                                           join utt in context.IAUserTalentTypes on tt.IATalentTypeID equals utt.IATalentTypeID
                                           where utt.MPUserID == t.MPUserID
                                           select new TalentType
                                           {
                                               TalentTypeId = tt.IATalentTypeID,
                                               Name = tt.Name,
                                               SortOrder = tt.Sort
                                           }).ToList()
                        };

            return query.ToList();
        }

        public static void InvalidateListCache()
        {
            if (HttpContext.Current.Cache[CacheItemKeyTalentList] != null)
            {
                HttpContext.Current.Cache.Remove(CacheItemKeyTalentList);
            }
        }



        public static IList<Talent> GetTalentsInTalentType(int talentTypeId, DataAccessDataContext context)
        {
            Dictionary<int, IList<Talent>> talentsInTypes = GetTalentListsFromCache();
            IList<Talent> talentInType;

            if (talentsInTypes.ContainsKey(talentTypeId))
            {
                talentInType = talentsInTypes[talentTypeId];
            }
            else
            {
                talentInType = FindTalentsInTypeFromFullList(talentTypeId, context);
                talentsInTypes.Add(talentTypeId, talentInType);

                UpdateTalentListsInCache(talentsInTypes);
            }

            return talentInType;
        }

        private static Dictionary<int, IList<Talent>> GetTalentListsFromCache()
        {
            Dictionary<int, IList<Talent>> talentsInTypes = new Dictionary<int, IList<Talent>>();

            if (HttpContext.Current.Cache[CacheItemKeyTalentInTypeList] != null)
            {
                talentsInTypes = (Dictionary<int, IList<Talent>>)HttpContext.Current.Cache[CacheItemKeyTalentInTypeList];
            }           

            return talentsInTypes;
        }

        private static void UpdateTalentListsInCache(Dictionary<int, IList<Talent>> talentsInTypes)
        {
            if (HttpContext.Current.Cache[CacheItemKeyTalentInTypeList] == null)
            {
                HttpContext.Current.Cache.Add(CacheItemKeyTalentInTypeList, talentsInTypes, null, DateTime.Now.AddSeconds(3600), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            else
            {
                HttpContext.Current.Cache[CacheItemKeyTalentInTypeList] = talentsInTypes;
            }
        }

        private static IList<Talent> FindTalentsInTypeFromFullList(int talentTypeId, DataAccessDataContext context)
        {
            IList<Talent> talents = GetAllTalents(context);
            IList<Talent> talentInType = new List<Talent>();

            foreach (Talent talent in talents)
            {
                if (talent.TalentTypes != null)
                {
                    foreach (TalentType talentType in talent.TalentTypes)
                    {
                        if (talentType.TalentTypeId == talentTypeId)
                        {
                            talentInType.Add(talent);
                            break;
                        }
                    }
                }
            }

            return talentInType;
        }

        public static void InvalidateTalentInTypeListCache()
        {
            if (HttpContext.Current.Cache[CacheItemKeyTalentInTypeList] != null)
            {
                HttpContext.Current.Cache.Remove(CacheItemKeyTalentInTypeList);
            }
        }

    }
}