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
    public partial class ajax_new_message : AjaxBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();

            StringBuilder oSB = new StringBuilder();
            StringWriter oSW = new StringWriter(oSB);

            using (JsonWriter oWriter = new JsonTextWriter(oSW))
            {
                oWriter.Formatting = Formatting.Indented;

                oWriter.WriteStartObject();

                if (IsAuthenticated)
                {
                    fn_Message_GetNewResult oResult = ApplicationContext.GetNewMessage();

                    string sHtml = string.Empty;
                    if (oResult != null)
                    {
                        sHtml = string.Format("<p><strong>New Message:</strong> <a href='messages-view.aspx?id={0}'>{1}</a> [<a href='#' id='ss_hide' class='hide'>hide</a>]</p>", oResult.IAMessageID, oResult.Subject);

                        oWriter.WritePropertyName("id");
                        oWriter.WriteValue(oResult.IAMessageID);
                        oWriter.WritePropertyName("html");
                        oWriter.WriteValue(sHtml);
                    }
                }
                else
                {
                    oWriter.WritePropertyName("Error");
                    oWriter.WriteValue("Unathenticated");
                }

                oWriter.WriteEndObject();
            }

            Response.ContentType = "application/json";
            Response.Write(oSB.ToString());
            Response.End();
        }

        #region Virtual Methods
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