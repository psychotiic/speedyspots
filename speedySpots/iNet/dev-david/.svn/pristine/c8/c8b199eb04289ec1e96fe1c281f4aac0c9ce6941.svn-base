using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class ajax_send_feedback : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsAuthenticated)
                {
                    if (Request.Form["type"] != null && Request.Form["message"] != null && Request.Form["feeling"] != null && Request.Form["url"] != null)
                    {
                        string sType = Request.Form["type"];
                        string sMessage = Request.Form["message"];
                        string sFeeling = Request.Form["feeling"];
                        string sUrl = Request.Form["url"];

                        ApplicationContext.SubmitFeedback(sType, sFeeling, sMessage, sUrl);
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

        #region Overridden Methods
        public override bool IsProcessSSL()
        {
            return false;
        }

        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);
            oAccessControl.Add(AccessControl.Customer);
            oAccessControl.Add(AccessControl.Staff);
            oAccessControl.Add(AccessControl.Talent);

            return oAccessControl;
        }
        #endregion
    }
}