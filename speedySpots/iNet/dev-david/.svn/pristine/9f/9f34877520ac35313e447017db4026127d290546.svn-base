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
    public partial class producer_talent_availability : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();

                m_cboTalent.DataSource = DataAccess.fn_Producer_GetAllTalent().OrderBy(row => row.UserName);
                m_cboTalent.DataValueField = "MPUserID";
                m_cboTalent.DataTextField = "UserName";
                m_cboTalent.DataBind();

                m_cboTalentFilter.DataSource = DataAccess.fn_Producer_GetAllTalent().OrderBy(row => row.UserName);
                m_cboTalentFilter.DataValueField = "MPUserID";
                m_cboTalentFilter.DataTextField = "UserName";
                m_cboTalentFilter.DataBind();

                m_cboTalentFilter.Items.Insert(0, new RadComboBoxItem("-- All --", string.Empty));
                m_cboTalentFilter.SelectedIndex = 0;

                m_cboStatusFilter.SelectedValue = "Pending";
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            if(Session["IATalentUnavailabilityID"] != null)
            {
                IATalentUnavailability oIATalentUnavailability = DataAccess.IATalentUnavailabilities.SingleOrDefault(row => row.IATalentUnavailabilityID == (int)Session["IATalentUnavailabilityID"]);
                if(oIATalentUnavailability != null)
                {
                    oIATalentUnavailability.MPUserIDProducer = MemberProtect.CurrentUser.UserID;
                    oIATalentUnavailability.Notes = m_txtNotes.Text;
                    oIATalentUnavailability.FromDateTime = m_dtFrom.SelectedDate.Value;
                    oIATalentUnavailability.ToDateTime = m_dtTo.SelectedDate.Value;
                    DataAccess.SubmitChanges();

                    OnClear(this, new EventArgs());

                    Session["IATalentUnavailabilityID"] = null;
                    SetMessage("Unavailability has been saved.", InetSolution.Web.MessageTone.Positive);
                }
            }
            else
            {
                IATalentUnavailability oIATalentUnavailability = new IATalentUnavailability();
                oIATalentUnavailability.MPUserID = MemberProtect.Utility.ValidateGuid(m_cboTalent.SelectedValue);
                oIATalentUnavailability.MPUserIDProducer = MemberProtect.CurrentUser.UserID;
                oIATalentUnavailability.Status = "Approved";
                oIATalentUnavailability.Notes = m_txtNotes.Text;
                oIATalentUnavailability.FromDateTime = m_dtFrom.SelectedDate.Value;
                oIATalentUnavailability.ToDateTime = m_dtTo.SelectedDate.Value;
                oIATalentUnavailability.CreatedDateTime = DateTime.Now;
                DataAccess.IATalentUnavailabilities.InsertOnSubmit(oIATalentUnavailability);
                DataAccess.SubmitChanges();

                OnClear(this, new EventArgs());

                SetMessage("Your unavailability has been submitted.", InetSolution.Web.MessageTone.Positive);
            }

            m_grdList.Rebind();
        }

        protected void OnFilter(object sender, EventArgs e)
        {
            m_grdList.Rebind();
        }

        protected void OnClear(object sender, EventArgs e)
        {
            Session["IATalentUnavailabilityID"] = null;

            m_cboTalent.Enabled = true;
            m_dtFrom.SelectedDate = null;
            m_dtTo.SelectedDate = null;
            m_txtNotes.Text = string.Empty;
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_Producer_GetAllTalentUnavailabilityResult> oResults = DataAccess.fn_Producer_GetAllTalentUnavailability();

            if(m_dtFromFilter.SelectedDate.HasValue || m_dtToFilter.SelectedDate.HasValue || m_cboTalentFilter.SelectedValue != string.Empty || m_cboStatusFilter.SelectedValue != string.Empty)
            {
                // Apply filters
                if(m_dtFromFilter.SelectedDate.HasValue)
                {
                    oResults = oResults.Where(row => row.FromDateTime >= new DateTime(m_dtFromFilter.SelectedDate.Value.Year, m_dtFromFilter.SelectedDate.Value.Month, m_dtFromFilter.SelectedDate.Value.Day, 0, 0, 0, 0) && row.FromDateTime <= new DateTime(m_dtFromFilter.SelectedDate.Value.Year, m_dtFromFilter.SelectedDate.Value.Month, m_dtFromFilter.SelectedDate.Value.Day, 23, 59, 59, 999));
                }

                if(m_dtToFilter.SelectedDate.HasValue)
                {
                    oResults = oResults.Where(row => row.ToDateTime >= new DateTime(m_dtToFilter.SelectedDate.Value.Year, m_dtToFilter.SelectedDate.Value.Month, m_dtToFilter.SelectedDate.Value.Day, 0, 0, 0, 0) && row.FromDateTime <= new DateTime(m_dtToFilter.SelectedDate.Value.Year, m_dtToFilter.SelectedDate.Value.Month, m_dtToFilter.SelectedDate.Value.Day, 23, 59, 59, 999));
                }

                if(m_cboTalentFilter.SelectedValue != string.Empty)
                {
                    oResults = oResults.Where(row => row.MPUserID == MemberProtect.Utility.ValidateGuid(m_cboTalentFilter.SelectedValue));
                }

                if(m_cboStatusFilter.SelectedValue != string.Empty)
                {
                    oResults = oResults.Where(row => row.Status == m_cboStatusFilter.SelectedValue);
                }
            }

            m_grdList.DataSource = oResults;
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if(oDataItem["Status"].Text == "Approved" || oDataItem["Status"].Text == "Denied")
                {
                    oDataItem["Approve"].Controls.Clear();
                    oDataItem["Deny"].Controls.Clear();
                }
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;
                int iIATalentUnavailabilityID = MemberProtect.Utility.ValidateInteger(oDataItem["IATalentUnavailabilityID"].Text);

                IATalentUnavailability oIATalentUnavailability = DataAccess.IATalentUnavailabilities.SingleOrDefault(row => row.IATalentUnavailabilityID == iIATalentUnavailabilityID);
                if(oIATalentUnavailability != null)
                {
                    if(e.CommandName == "View")
                    {
                        Session["IATalentUnavailabilityID"] = oIATalentUnavailability.IATalentUnavailabilityID;

                        m_cboTalent.Enabled = false;
                        m_cboTalent.SelectedValue = MemberProtect.Utility.FormatGuid(oIATalentUnavailability.MPUserID);
                        m_dtFrom.SelectedDate = oIATalentUnavailability.FromDateTime;
                        m_dtTo.SelectedDate = oIATalentUnavailability.ToDateTime;
                        m_txtNotes.Text = oIATalentUnavailability.Notes;

                        m_grdList.Rebind();
                    }
                    else if(e.CommandName == "Approve")
                    {
                        oIATalentUnavailability.Status = "Approved";
                        DataAccess.SubmitChanges();

                        m_grdList.Rebind();
                        SetMessage("Unavailability has been approved.", InetSolution.Web.MessageTone.Positive);
                    }
                    else if(e.CommandName == "Deny")
                    {
                        oIATalentUnavailability.Status = "Denied";
                        DataAccess.SubmitChanges();

                        m_grdList.Rebind();
                        SetMessage("Unavailability has been denied.", InetSolution.Web.MessageTone.Positive);
                    }
                    else if(e.CommandName == "Delete")
                    {
                        DataAccess.IATalentUnavailabilities.DeleteOnSubmit(oIATalentUnavailability);
                        DataAccess.SubmitChanges();

                        SetMessage("Unavailability has been deleted.", InetSolution.Web.MessageTone.Positive);
                    }
                }
            }
        }
        #endregion

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);

            return oAccessControl;
        }
        #endregion
    }
}