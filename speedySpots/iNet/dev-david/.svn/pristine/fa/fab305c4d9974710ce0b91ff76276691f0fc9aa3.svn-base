using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
   using Business;

   public partial class pageBreakPreview : SiteBasePage
    {
        public bool IsComplete { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int iIASpotID = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
                    IASpot oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
                    if(oIASpot != null)
                    {
                        lblJob.Text = oIASpot.IAProductionOrder.IAJob.Name;
                        lblJobNumber.Text = oIASpot.IAProductionOrder.IAJob.JobIDForDisplay;
                        lblSpotTitle.Text = oIASpot.Title;
                        lblSpotLength.Text = oIASpot.Length;

                        if (oIASpot.IASpotFees.Count() > 0)
                        {
                            m_oRepeaterFees.DataSource = oIASpot.IASpotFees;
                            m_oRepeaterFees.DataBind();
                        }
                        else
                        {
                            m_divFees.Visible = false;
                        }

                        lblProducer.Text = ApplicationContext.GetJobProducers(oIASpot.IAProductionOrder.IAJobID);
                        lblDueDate.Text = oIASpot.DueDateTime.ToString("dddd, dd a\\t h:mm tt");
                        lblSent.Text = oIASpot.IAProductionOrder.IAJob.ProductionDateTime.ToString("dddd, dd a\\t h:mm tt");
                        lblProductionNotes.Text = oIASpot.ProductionNotes;
                        lblScript.Text = oIASpot.Script;

                        IsComplete = (oIASpot.IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Complete));
                    }
                }
            }
        }

        #region Virtual Methods
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