using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.ComponentModel;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;

namespace SpeedySpots.Controls.SpotControls
{
   using Business;

   public partial class TalentAssignment : SiteBaseControl
    {
        public enum PageMode
        {
            Edit,
            View,
        }

        private IAJob                               m_oIAJob = null;
        private IAProductionOrder                   m_oIAProductionOrder = null;

        [Browsable(true)]
        public event EventHandler                   AssignTalent = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["IAJobID"] != null)
            {
                m_oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == (int)Session["IAJobID"]);
            }

            if(Session["IAProductionOrderID"] != null)
            {
                m_oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == (int)Session["IAProductionOrderID"]);
            }

            if(!IsPostBack)
            {
                if(m_oIAProductionOrder == null)
                {
                    Mode = PageMode.Edit;
                    LoadTalentTypes();
                    OnChangedTalentType(this, new EventArgs());
                }
                else
                {
                    Mode = PageMode.View;
                }
            }
        }

        protected void OnChangedTalentType(object sender, EventArgs e)
        {
            m_radTalent.DataValueField = "MPUserID";
            m_radTalent.DataTextField = "FullName";
            m_radTalent.DataSource = Business.Services.TalentsService.GetTalentsInTalentType(MemberProtect.Utility.ValidateInteger(m_cboTalentType.SelectedValue), DataAccess);
            m_radTalent.DataBound += new EventHandler(OnDataBoundTalent);
            m_radTalent.DataBind();
        }

        protected void OnReassign(object sender, EventArgs e)
        {
            Mode = PageMode.Edit;
            LoadTalentTypes();
            OnChangedTalentType(this, new EventArgs());

            m_radTalent.SelectedValue = MemberProtect.Utility.FormatGuid(m_oIAProductionOrder.MPUserIDTalent);
        }

        protected void OnAssignTalent(object sender, EventArgs e)
        {
            if(m_radTalent.SelectedValue != string.Empty)
            {
                if(m_oIAProductionOrder == null)
                {
                    m_oIAProductionOrder = new IAProductionOrder();
                    m_oIAProductionOrder.IAJobID = m_oIAJob.IAJobID;
                    m_oIAProductionOrder.IAProductionOrderStatusID = ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Incomplete);
                    m_oIAProductionOrder.HasBeenViewedByTalent = false;
                    m_oIAProductionOrder.CompletedDateTime = DateTime.Now;
                    m_oIAProductionOrder.CreatedDateTime = DateTime.Now;
                    m_oIAProductionOrder.ProductionDateTime = DateTime.Now;
                    m_oIAProductionOrder.OnHoldDateTime = DateTime.Now;
                    m_oIAProductionOrder.IATalentTypeID = MemberProtect.Utility.ValidateInteger(m_cboTalentType.SelectedValue);
                    m_oIAProductionOrder.MPUserIDTalent = MemberProtect.Utility.ValidateGuid(m_radTalent.SelectedValue);
                    m_oIAProductionOrder.MPUserIDProducer = Guid.Empty;
                    m_oIAProductionOrder.MPUserIDOnHold = Guid.Empty;
                    m_oIAProductionOrder.Notes = string.Empty;
                    DataAccess.IAProductionOrders.InsertOnSubmit(m_oIAProductionOrder);
                    DataAccess.SubmitChanges();
                }
                else
                {
                    if(!IsProducerView)
                    {
                        if(Session["IASpotID"] != null)
                        {
                            IASpot oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == (int)Session["IASpotID"]);
                            if(oIASpot != null)
                            {
                                // Rules
                                // 1. If the new talent already has a PO in this job, move this spot to that PO
                                IAProductionOrder oIAProductionOrder = m_oIAJob.IAProductionOrders.FirstOrDefault(row => row.MPUserIDTalent == MemberProtect.Utility.ValidateGuid(m_radTalent.SelectedValue));
                                if(oIAProductionOrder != null)
                                {
                                    //oIASpot.IAProductionOrderID = oIAProductionOrder.IAProductionOrderID;

                                    // Remove from original Production Order
                                    oIASpot.IAProductionOrder.IASpots.Remove(oIASpot);

                                    // Assign to new Production Order
                                    oIAProductionOrder.IASpots.Add(oIASpot);

                                    DataAccess.SubmitChanges();
                                }
                                // 2. If the new talent doesn't have a PO in this job, create a new PO for the talent and move this spot into it
                                else
                                {
                                    // Create new PO
                                    oIAProductionOrder = new IAProductionOrder();
                                    oIAProductionOrder.IAJobID = m_oIAProductionOrder.IAJobID;
                                    oIAProductionOrder.IAProductionOrderStatusID = m_oIAProductionOrder.IAProductionOrderStatusID;
                                    oIAProductionOrder.IATalentTypeID = MemberProtect.Utility.ValidateInteger(m_cboTalentType.SelectedValue);
                                    oIAProductionOrder.MPUserIDTalent = MemberProtect.Utility.ValidateGuid(m_radTalent.SelectedValue);
                                    oIAProductionOrder.MPUserIDProducer = m_oIAProductionOrder.MPUserIDProducer;
                                    oIAProductionOrder.MPUserIDOnHold = m_oIAProductionOrder.MPUserIDOnHold;
                                    oIAProductionOrder.Notes = m_oIAProductionOrder.Notes;
                                    oIAProductionOrder.CreatedDateTime = DateTime.Now;
                                    oIAProductionOrder.CompletedDateTime = m_oIAProductionOrder.CompletedDateTime;
                                    oIAProductionOrder.ProductionDateTime = m_oIAProductionOrder.ProductionDateTime;
                                    oIAProductionOrder.OnHoldDateTime = m_oIAProductionOrder.OnHoldDateTime;
                                    DataAccess.IAProductionOrders.InsertOnSubmit(oIAProductionOrder);
                                    DataAccess.SubmitChanges();

                                    // Remove from original Production Order
                                    oIASpot.IAProductionOrder.IASpots.Remove(oIASpot);

                                    // Assign to new Production Order
                                    oIAProductionOrder.IASpots.Add(oIASpot);

                                    DataAccess.SubmitChanges();
                                }

                                // If the last spot was moved for a particular talent, we need to remove the now empty production order\
                                IAProductionOrder oOriginalIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == (int)Session["IAProductionOrderID"]);
                                if(oOriginalIAProductionOrder.IASpots.Count == 0)
                                {
                                    DataAccess.IAProductionOrders.DeleteOnSubmit(oOriginalIAProductionOrder);
                                    DataAccess.SubmitChanges();
                                }
                            }

                            Response.Redirect("~/quality-control-job-details.aspx");
                        }
                    }
                    else
                    {
                        m_oIAProductionOrder.IATalentTypeID = MemberProtect.Utility.ValidateInteger(m_cboTalentType.SelectedValue);
                        m_oIAProductionOrder.MPUserIDTalent = MemberProtect.Utility.ValidateGuid(m_radTalent.SelectedValue);
                        DataAccess.SubmitChanges();
                    }
                }

                Session["IAProductionOrderID"] = m_oIAProductionOrder.IAProductionOrderID;

                Mode = PageMode.View;

                if(AssignTalent != null)
                {
                    AssignTalent(this, new EventArgs());
                }
            }
        }

        #region Private Methods
        private void OnDataBoundTalent(object sender, EventArgs e)
        {
            foreach(ListItem oItem in m_radTalent.Items)
            {
                Guid oMPUserID = MemberProtect.Utility.ValidateGuid(oItem.Value);

                StringBuilder oSchedule = new StringBuilder();

                bool bIsUnavailable = false;
                if(!IsTalentAvailable(oMPUserID, m_oIAJob.DueDateTime))
                {
                    DateTime oStartOfToday = new DateTime(m_oIAJob.DueDateTime.Year, m_oIAJob.DueDateTime.Month, m_oIAJob.DueDateTime.Day, 0, 0, 0, 0);
                    DateTime oEndOfToday = new DateTime(m_oIAJob.DueDateTime.Year, m_oIAJob.DueDateTime.Month, m_oIAJob.DueDateTime.Day, 23, 59, 59, 999);

                    IQueryable<fn_Producer_GetUnavailabilityReportResult> oResults = DataAccess.fn_Producer_GetUnavailabilityReport(oStartOfToday, oEndOfToday).Where(row => row.MPUserID == oMPUserID);
                    foreach(fn_Producer_GetUnavailabilityReportResult oResult in oResults)
                    {
                        oSchedule.Append(string.Format("<div><strong>{0}</strong> is out at <strong>{1:M/dd/yyyy a\\t h:mm tt}</strong> and good for delivery <strong>{2:M/dd/yyyy a\\t h:mm tt}</strong>.</div><br/>", oResult.Talent, oResult.FromDateTime, oResult.ToDateTime));
                    }

                    bIsUnavailable = true;
                }

                IATalentSchedule oIATalentSchedule = Business.Services.TalentScheduleService.GetTalentSchedule(oMPUserID, DataAccess);
                if(oIATalentSchedule != null)
                {
                    oSchedule.Append("<table>");
                    oSchedule.Append("<thead>");
                    oSchedule.Append("<td><strong>Day</strong></td>");
                    oSchedule.Append("<td><strong>In</strong></td>");
                    oSchedule.Append("<td><strong>Out</strong></td>");
                    oSchedule.Append("</thead>");
                    oSchedule.Append("<tr>");
                    oSchedule.Append("<td>Monday</td>");
                    if(oIATalentSchedule.MondayInDateTime.Year > 1950 && oIATalentSchedule.MondayOutDateTime.Year > 1950)
                    {
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.MondayInDateTime));
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.MondayOutDateTime));
                    }
                    else
                    {
                        oSchedule.Append("<td>N/A</td>");
                        oSchedule.Append("<td>N/A</td>");
                    }
                    oSchedule.Append("</tr>");
                    oSchedule.Append("<tr>");
                    oSchedule.Append("<td>Tuesday</td>");
                    if(oIATalentSchedule.TuesdayInDateTime.Year > 1950 && oIATalentSchedule.TuesdayOutDateTime.Year > 1950)
                    {
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.TuesdayInDateTime));
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.TuesdayOutDateTime));
                    }
                    else
                    {
                        oSchedule.Append("<td>N/A</td>");
                        oSchedule.Append("<td>N/A</td>");
                    }
                    oSchedule.Append("</tr>");
                    oSchedule.Append("<tr>");
                    oSchedule.Append("<td>Wednesday</td>");
                    if(oIATalentSchedule.WednesdayInDateTime.Year > 1950 && oIATalentSchedule.WednesdayOutDateTime.Year > 1950)
                    {
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.WednesdayInDateTime));
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.WednesdayOutDateTime));
                    }
                    else
                    {
                        oSchedule.Append("<td>N/A</td>");
                        oSchedule.Append("<td>N/A</td>");
                    }
                    oSchedule.Append("</tr>");
                    oSchedule.Append("<tr>");
                    oSchedule.Append("<td>Thursday</td>");
                    if(oIATalentSchedule.ThursdayInDateTime.Year > 1950 && oIATalentSchedule.ThursdayOutDateTime.Year > 1950)
                    {
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.ThursdayInDateTime));
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.ThursdayOutDateTime));
                    }
                    else
                    {
                        oSchedule.Append("<td>N/A</td>");
                        oSchedule.Append("<td>N/A</td>");
                    }
                    oSchedule.Append("</tr>");
                    oSchedule.Append("<tr>");
                    oSchedule.Append("<td>Friday</td>");
                    if(oIATalentSchedule.FridayInDateTime.Year > 1950 && oIATalentSchedule.FridayOutDateTime.Year > 1950)
                    {
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.FridayInDateTime));
                        oSchedule.Append(string.Format("<td>{0:h:mm tt}</td>", oIATalentSchedule.FridayOutDateTime));
                    }
                    else
                    {
                        oSchedule.Append("<td>N/A</td>");
                        oSchedule.Append("<td>N/A</td>");
                    }
                    oSchedule.Append("</tr>");
                    oSchedule.Append("</table>");
                }

                if(oSchedule.Length == 0)
                {
                    oSchedule.Append("<div>Talent has no schedule and is therefore considered unavailable.</div>");
                }

                if(bIsUnavailable)
                {
                    oItem.Attributes["class"] = "unavailable";
                    oItem.Text = string.Format("<a class='tooltip' href='#'>{0} (Unavailable)<span class='classic'>{1}</span></a>", oItem.Text, oSchedule.ToString());
                }
                else
                {
                    oItem.Text = string.Format("<a class='tooltip' href='#'>{0} <span class='classic'>{1}</span></a>", oItem.Text, oSchedule.ToString());
                }
            }
        }

        private bool IsTalentAvailable(Guid oMPUserID, DateTime oDateTime)
        {
            // If talent has approved unavailability for the given date/time, then they are considered not available; return false
            List<IATalentUnavailability> oIATalentUnavailabilities = DataAccess.IATalentUnavailabilities.Where(row => row.MPUserID == oMPUserID && row.Status == "Approved").ToList();
            if(oIATalentUnavailabilities.Count > 0)
            {
                foreach(IATalentUnavailability oIATalentUnavailability in oIATalentUnavailabilities)
                {
                    if(oDateTime.CompareTo(oIATalentUnavailability.FromDateTime) >= 0 && oDateTime.CompareTo(oIATalentUnavailability.ToDateTime) <= 0)
                    {
                        return false;
                    }
                }
            }

            // If talent is scheduled to normally be available for the given date/time, then they are considered available; return true, otherewise, they aren't; return false
            IATalentSchedule oIATalentSchedule = Business.Services.TalentScheduleService.GetTalentSchedule(oMPUserID, DataAccess);
            if(oIATalentSchedule != null)
            {
                if(oDateTime.DayOfWeek == DayOfWeek.Monday)
                {
                    if(oIATalentSchedule.MondayInDateTime.Year > 1950 && oIATalentSchedule.MondayOutDateTime.Year > 1950)
                    {
                        if(oDateTime.CompareTo(new DateTime(1950, 1, 1, oIATalentSchedule.MondayInDateTime.Hour, oIATalentSchedule.MondayInDateTime.Minute, oIATalentSchedule.MondayInDateTime.Second, 0)) >= 0 && oDateTime.CompareTo(new DateTime(2050, 1, 1, oIATalentSchedule.MondayOutDateTime.Hour, oIATalentSchedule.MondayOutDateTime.Minute, oIATalentSchedule.MondayOutDateTime.Second, 0)) <= 0)
                        {
                            return true;
                        }
                    }
                }
                else if(oDateTime.DayOfWeek == DayOfWeek.Tuesday)
                {
                    if(oIATalentSchedule.TuesdayInDateTime.Year > 1950 && oIATalentSchedule.TuesdayOutDateTime.Year > 1950)
                    {
                        if(oDateTime.CompareTo(new DateTime(1950, 1, 1, oIATalentSchedule.TuesdayInDateTime.Hour, oIATalentSchedule.TuesdayInDateTime.Minute, oIATalentSchedule.TuesdayInDateTime.Second, 0)) >= 0 && oDateTime.CompareTo(new DateTime(2050, 1, 1, oIATalentSchedule.TuesdayOutDateTime.Hour, oIATalentSchedule.TuesdayOutDateTime.Minute, oIATalentSchedule.TuesdayOutDateTime.Second, 0)) <= 0)
                        {
                            return true;
                        }
                    }
                }
                else if(oDateTime.DayOfWeek == DayOfWeek.Wednesday)
                {
                    if(oIATalentSchedule.WednesdayInDateTime.Year > 1950 && oIATalentSchedule.WednesdayOutDateTime.Year > 1950)
                    {
                        if(oDateTime.CompareTo(new DateTime(1950, 1, 1, oIATalentSchedule.WednesdayInDateTime.Hour, oIATalentSchedule.WednesdayInDateTime.Minute, oIATalentSchedule.WednesdayInDateTime.Second, 0)) >= 0 && oDateTime.CompareTo(new DateTime(2050, 1, 1, oIATalentSchedule.WednesdayOutDateTime.Hour, oIATalentSchedule.WednesdayOutDateTime.Minute, oIATalentSchedule.WednesdayOutDateTime.Second, 0)) <= 0)
                        {
                            return true;
                        }
                    }
                }
                else if(oDateTime.DayOfWeek == DayOfWeek.Thursday)
                {
                    if(oIATalentSchedule.ThursdayInDateTime.Year > 1950 && oIATalentSchedule.ThursdayOutDateTime.Year > 1950)
                    {
                        if(oDateTime.CompareTo(new DateTime(1950, 1, 1, oIATalentSchedule.ThursdayInDateTime.Hour, oIATalentSchedule.ThursdayInDateTime.Minute, oIATalentSchedule.ThursdayInDateTime.Second, 0)) >= 0 && oDateTime.CompareTo(new DateTime(2050, 1, 1, oIATalentSchedule.ThursdayOutDateTime.Hour, oIATalentSchedule.ThursdayOutDateTime.Minute, oIATalentSchedule.ThursdayOutDateTime.Second, 0)) <= 0)
                        {
                            return true;
                        }
                    }
                }
                else if(oDateTime.DayOfWeek == DayOfWeek.Friday)
                {
                    if(oIATalentSchedule.FridayInDateTime.Year > 1950 && oIATalentSchedule.FridayOutDateTime.Year > 1950)
                    {
                        if(oDateTime.CompareTo(new DateTime(1950, 1, 1, oIATalentSchedule.FridayInDateTime.Hour, oIATalentSchedule.FridayInDateTime.Minute, oIATalentSchedule.FridayInDateTime.Second, 0)) >= 0 && oDateTime.CompareTo(new DateTime(2050, 1, 1, oIATalentSchedule.FridayOutDateTime.Hour, oIATalentSchedule.FridayOutDateTime.Minute, oIATalentSchedule.FridayOutDateTime.Second, 0)) <= 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void UpdateDisplay()
        {
            m_divEdit.Visible = false;
            m_divView.Visible = false;

            if(Mode == PageMode.View)
            {
                m_divView.Visible = true;
            }
            else
            {
                m_divEdit.Visible = true;
            }
        }

        private void LoadTalentTypes()
        {
            if (m_cboTalentType.Items != null && m_cboTalentType.Items.Count <= 0)
            {
                m_cboTalentType.DataValueField = "TalentTypeId";
                m_cboTalentType.DataTextField = "Name";
                m_cboTalentType.DataSource = Business.Services.TalentTypeService.GetTalentTypes(DataAccess);
                m_cboTalentType.DataBind();
            }

            if (m_oIAProductionOrder != null && m_oIAProductionOrder.IATalentTypeID > 0)
            {
                m_cboTalentType.SelectedValue = MemberProtect.Utility.FormatInteger(m_oIAProductionOrder.IATalentTypeID);
            }
        }
        #endregion

        #region Public Properties
        public IAJob IAJob
        {
            get { return m_oIAJob; }
        }

        public Guid SelectedTalent
        {
            get { return MemberProtect.Utility.ValidateGuid(m_radTalent.SelectedValue); }
        }

        public int SelectedTalentType
        {
            get { return MemberProtect.Utility.ValidateInteger(m_cboTalentType.SelectedValue); }
        }

        public bool IsNew
        {
            get { return m_oIAProductionOrder == null; }
        }

        public string TalentAssigned
        {
            get
            {
                if(m_oIAProductionOrder != null)
                {
                    return string.Format("{0} {1}", MemberProtect.User.GetDataItem(m_oIAProductionOrder.MPUserIDTalent, "FirstName"), MemberProtect.User.GetDataItem(m_oIAProductionOrder.MPUserIDTalent, "LastName"));
                }
                else
                {
                    return "<ERROR>";
                }
            }
        }

        public PageMode Mode
        {
            set
            {
                ViewState["Mode"] = value;
                UpdateDisplay();
            }

            get
            {
                if(ViewState["Mode"] != null)
                {
                    return (PageMode)ViewState["Mode"];
                }
                else
                {
                    return PageMode.View;
                }
            }
        }

        public bool IsProducerView
        {
            get { return ((SpotDetails)Parent).IsProducerView; }
        }
        #endregion
    }
}