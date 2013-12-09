using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Newtonsoft.Json;

namespace SpeedySpots
{
    public partial class ajax_request_label : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsAuthenticated)
                {
                    if (Request.Form["requestid"] != null && Request.Form["labelid"] != null && Request.Form["action"] != null)
                    {
                        int iIARequestID = MemberProtect.Utility.ValidateInteger(Request.Form["requestid"]);
                        int iIALabelID = MemberProtect.Utility.ValidateInteger(Request.Form["labelid"]);
                        string sAction = Request.Form["action"];

                        if (sAction == "add")
                        {
                            IARequestLabel oIARequestLabel = new IARequestLabel();
                            oIARequestLabel.IARequestID = iIARequestID;
                            oIARequestLabel.IALabelID = iIALabelID;
                            DataAccess.IARequestLabels.InsertOnSubmit(oIARequestLabel);
                            DataAccess.SubmitChanges();
                        }
                        else if (sAction == "delete")
                        {
                            List<IARequestLabel> oIARequestLabels = DataAccess.IARequestLabels.Where(row => row.IARequestID == iIARequestID && row.IALabelID == iIALabelID).ToList();
                            if (oIARequestLabels.Count > 0)
                            {
                                for (int i = oIARequestLabels.Count - 1; i >= 0; i--)
                                {
                                    DataAccess.IARequestLabels.DeleteOnSubmit(oIARequestLabels[i]);
                                    DataAccess.SubmitChanges();
                                }
                            }
                        }

                        Response.Clear();
                        Response.StatusCode = 200;
                        Response.Write(ApplicationContext.DisplayProducerLabels(iIARequestID));
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