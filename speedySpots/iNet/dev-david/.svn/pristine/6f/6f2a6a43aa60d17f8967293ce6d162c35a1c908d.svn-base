using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;
using System.Transactions;

namespace SpeedySpots
{
    public enum ItemType
    {
        Request,
        Job,
    }

    public partial class ajax_update_activity : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsAuthenticated)
                {
                    if (Request.Form["userid"] != null && Request.Form["itemtype"] != null && Request.Form["itemid"] != null)
                    {
                        Guid oMPUserID = MemberProtect.Utility.ValidateGuid(Request.Form["userid"]);
                        string sItemType = Request.Form["itemtype"];
                        int iItemID = MemberProtect.Utility.ValidateInteger(Request.Form["itemid"]);

                        UpdateActivity(oMPUserID, sItemType, iItemID);

                        Response.Clear();
                        Response.StatusCode = 200;
                        Response.Write(DisplayActivity(oMPUserID, sItemType, iItemID));
                    }
                }
            }
            catch(Exception oException)
            {
                Response.Clear();
                Response.StatusCode = 500;
                Response.Write(oException.Message + System.Environment.NewLine + oException.StackTrace);
                Response.End();
            }
        }

        #region Private Methods
        private void UpdateActivity(Guid oMPUserID, string sItemType, int iItemID)
        {
            int iActivityInterval = ApplicationContext.SiteProperites.ActivityInterval;
            DateTime oTooOldDateTime = DateTime.Now.Subtract(new TimeSpan(0, 0, 0, iActivityInterval, 0));
            IAUserActivity oIAUserActivity = null;

            TransactionOptions oTransactionOptions = new TransactionOptions();
            oTransactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;

            using(TransactionScope oTransaction = new TransactionScope(TransactionScopeOption.Required, oTransactionOptions))
            {
                // Reading this with NOLOCK hint since it's quite frequent
                oIAUserActivity = DataAccess.IAUserActivities.Where(row => row.MPUserID == oMPUserID && row.ItemType == sItemType && row.ItemID == iItemID).SingleOrDefault();
            }

            if(oIAUserActivity == null)
            {
                oIAUserActivity = new IAUserActivity();
                oIAUserActivity.MPUserID = oMPUserID;
                oIAUserActivity.ItemType = sItemType;
                oIAUserActivity.ItemID = iItemID;

                DataAccess.IAUserActivities.InsertOnSubmit(oIAUserActivity);
            }

            oIAUserActivity.CreatedDateTime = DateTime.Now;
            DataAccess.SubmitChanges();

            // Delete any old records that are older than now - interval
            DataAccess.sp_System_DeleteUserActivityOlderThan(oTooOldDateTime, sItemType);
        }


        private string DisplayActivity(Guid oMPUserID, string sItemType, int iItemID)
        {
            int iActivityInterval = ApplicationContext.SiteProperites.ActivityInterval;
            DateTime oTooOldDateTime = DateTime.Now.Subtract(new TimeSpan(0, 0, 0, iActivityInterval, 0));
            
            string sUsersViewing = string.Empty;
            List<IAUserActivity> oIAUserActivities = DataAccess.IAUserActivities.Where(row => row.CreatedDateTime > oTooOldDateTime && row.MPUserID != oMPUserID && row.ItemType == sItemType && row.ItemID == iItemID).ToList();
            foreach(IAUserActivity oIAUserActivity in oIAUserActivities)
            {
                sUsersViewing += string.Format("<div class=\"user\"><span>{0} {1}</span></div>", MemberProtect.User.GetDataItem(oIAUserActivity.MPUserID, "FirstName"), MemberProtect.User.GetDataItem(oIAUserActivity.MPUserID, "LastName"));
            }

            return sUsersViewing;
        }

        private string GetItemType(ItemType oItemType)
        {
            if(oItemType == ItemType.Request)
            {
                return "Request";
            }
            else if(oItemType == ItemType.Job)
            {
                return "Job";
            }
            else
            {
                throw new ArgumentException(string.Format("ItemType '{0}' is invalid.", oItemType.ToString()));
            }
        }
        #endregion

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }
}