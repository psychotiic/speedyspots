using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class talent_my_availability : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            IATalentUnavailability oIATalentUnavailability = new IATalentUnavailability();
            oIATalentUnavailability.MPUserID = MemberProtect.CurrentUser.UserID;
            oIATalentUnavailability.MPUserIDProducer = Guid.Empty;
            oIATalentUnavailability.Status = "Pending";
            oIATalentUnavailability.Notes = m_txtNotes.Text;
            oIATalentUnavailability.FromDateTime = m_dtFrom.SelectedDate.Value;
            oIATalentUnavailability.ToDateTime = m_dtTo.SelectedDate.Value;
            oIATalentUnavailability.CreatedDateTime = DateTime.Now;
            DataAccess.IATalentUnavailabilities.InsertOnSubmit(oIATalentUnavailability);
            DataAccess.SubmitChanges();

            ShowMessage("Your unavailability has been submitted.", InetSolution.Web.MessageTone.Positive);
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            m_grdList.DataSource = DataAccess.fn_Talent_GetUnavailability(MemberProtect.CurrentUser.UserID);
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if(oDataItem["Status"].Text == "Approved")
                {
                    oDataItem["Delete"].Controls.Clear();
                }
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                if(e.CommandName == "Delete")
                {
                    GridDataItem oDataItem = e.Item as GridDataItem;
                    int iIATalentUnavailabilityID = MemberProtect.Utility.ValidateInteger(oDataItem["IATalentUnavailabilityID"].Text);

                    IATalentUnavailability oIATalentUnavailability = DataAccess.IATalentUnavailabilities.SingleOrDefault(row => row.IATalentUnavailabilityID == iIATalentUnavailabilityID);
                    if(oIATalentUnavailability != null)
                    {
                        DataAccess.IATalentUnavailabilities.DeleteOnSubmit(oIATalentUnavailability);
                        DataAccess.SubmitChanges();

                        ShowMessage("Unavailability has been deleted.", InetSolution.Web.MessageTone.Positive);
                    }
                }
            }
        }
        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Talent);

            return oAccessControl;
        }
        #endregion
    }
}