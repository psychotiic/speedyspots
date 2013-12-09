using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetSolution.Web;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class messages_inbox : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff)
            {
                m_grdList.DataSource = DataAccess.fn_Message_GetAdminInbox();
            }
            else
            {
                m_grdList.DataSource = DataAccess.fn_Message_GetUserInbox(MemberProtect.CurrentUser.UserID).Where(row => DateTime.Now >= row.DisplayStartDateTime);
            }
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                DateTime oStart = DateTime.Today;
                DateTime oEnd = DateTime.Today;

                DateTime.TryParse(oDataItem["DisplayStartDateTime"].Text, out oStart);
                DateTime.TryParse(oDataItem["DisplayEndDateTime"].Text, out oEnd);

                oDataItem["DateRanges"].Text = string.Format("{0:M/dd/yyyy a\\t h:mm tt} - {1:M/dd/yyyy a\\t h:mm tt}", oStart, oEnd);
                oDataItem["Subject"].Text = string.Format("<a href='messages-view.aspx?id={0}'>{1}</a>", oDataItem["IAMessageID"].Text, oDataItem["Subject"].Text);

                if(!(ApplicationContext.IsAdmin && ApplicationContext.IsStaff))
                {
                    oDataItem["Delete"].Visible = false;
                }
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if(e.CommandName == "Delete")
                {
                    int iIAMessageID = MemberProtect.Utility.ValidateInteger(oDataItem["IAMessageID"].Text);

                    IAMessage oIAMessage = DataAccess.IAMessages.SingleOrDefault(row => row.IAMessageID == iIAMessageID);
                    if(oIAMessage != null)
                    {
                        DataAccess.IAMessages.DeleteOnSubmit(oIAMessage);
                        DataAccess.SubmitChanges();

                        m_grdList.Rebind();
                    }
                }
            }
        }
        #endregion

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