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
    public partial class out_of_office : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_dtMondayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtMondayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtTuesdayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtTuesdayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtWednesdayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtWednesdayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtThursdayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtThursdayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtFridayIn.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                m_dtFridayOut.MinDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);

                m_txtMessage.Content = ApplicationContext.SiteProperites.ClosedMessage;
                m_chkClosedMessageDisplayAlways.Checked = ApplicationContext.SiteProperites.ClosedMessageDisplayAlways;

                if (!(ApplicationContext.SiteProperites.MondayInDateTime.Hour == 0 && ApplicationContext.SiteProperites.MondayOutDateTime.Hour == 0))
                {
                    m_dtMondayIn.SelectedDate = ApplicationContext.SiteProperites.MondayInDateTime;
                    m_dtMondayOut.SelectedDate = ApplicationContext.SiteProperites.MondayOutDateTime;
                }

                if (!(ApplicationContext.SiteProperites.TuesdayInDateTime.Hour == 0 && ApplicationContext.SiteProperites.TuesdayOutDateTime.Hour == 0))
                {
                    m_dtTuesdayIn.SelectedDate = ApplicationContext.SiteProperites.TuesdayInDateTime;
                    m_dtTuesdayOut.SelectedDate = ApplicationContext.SiteProperites.TuesdayOutDateTime;
                }

                if (!(ApplicationContext.SiteProperites.WednesdayInDateTime.Hour == 0 && ApplicationContext.SiteProperites.WednesdayOutDateTime.Hour == 0))
                {
                    m_dtWednesdayIn.SelectedDate = ApplicationContext.SiteProperites.WednesdayInDateTime;
                    m_dtWednesdayOut.SelectedDate = ApplicationContext.SiteProperites.WednesdayOutDateTime;
                }

                if (!(ApplicationContext.SiteProperites.ThursdayInDateTime.Hour == 0 && ApplicationContext.SiteProperites.ThursdayOutDateTime.Hour == 0))
                {
                    m_dtThursdayIn.SelectedDate = ApplicationContext.SiteProperites.ThursdayInDateTime;
                    m_dtThursdayOut.SelectedDate = ApplicationContext.SiteProperites.ThursdayOutDateTime;
                }

                if (!(ApplicationContext.SiteProperites.FridayInDateTime.Hour == 0 && ApplicationContext.SiteProperites.FridayOutDateTime.Hour == 0))
                {
                    m_dtFridayIn.SelectedDate = ApplicationContext.SiteProperites.FridayInDateTime;
                    m_dtFridayOut.SelectedDate = ApplicationContext.SiteProperites.FridayOutDateTime;
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            IAProperty oIAProperty = ApplicationContext.SiteProperites.GetSiteProperties(true);
            oIAProperty.ClosedMessage = m_txtMessage.Content;
            oIAProperty.ClosedMessageDisplayAlways = m_chkClosedMessageDisplayAlways.Checked;

            if(m_dtMondayIn.SelectedDate.HasValue && m_dtMondayOut.SelectedDate.HasValue)
            {
                oIAProperty.MondayInDateTime = new DateTime(1950, 1, 1, m_dtMondayIn.SelectedDate.Value.Hour, m_dtMondayIn.SelectedDate.Value.Minute, 0, 0);
                oIAProperty.MondayOutDateTime = new DateTime(1950, 1, 1, m_dtMondayOut.SelectedDate.Value.Hour, m_dtMondayOut.SelectedDate.Value.Minute, 0, 0);
            }
            else
            {
                oIAProperty.MondayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                oIAProperty.MondayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            }

            if(m_dtTuesdayIn.SelectedDate.HasValue && m_dtTuesdayOut.SelectedDate.HasValue)
            {
                oIAProperty.TuesdayInDateTime = new DateTime(1950, 1, 1, m_dtTuesdayIn.SelectedDate.Value.Hour, m_dtTuesdayIn.SelectedDate.Value.Minute, 0, 0);
                oIAProperty.TuesdayOutDateTime = new DateTime(1950, 1, 1, m_dtTuesdayOut.SelectedDate.Value.Hour, m_dtTuesdayOut.SelectedDate.Value.Minute, 0, 0);
            }
            else
            {
                oIAProperty.TuesdayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                oIAProperty.TuesdayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            }

            if(m_dtWednesdayIn.SelectedDate.HasValue && m_dtWednesdayOut.SelectedDate.HasValue)
            {
                oIAProperty.WednesdayInDateTime = new DateTime(1950, 1, 1, m_dtWednesdayIn.SelectedDate.Value.Hour, m_dtWednesdayIn.SelectedDate.Value.Minute, 0, 0);
                oIAProperty.WednesdayOutDateTime = new DateTime(1950, 1, 1, m_dtWednesdayOut.SelectedDate.Value.Hour, m_dtWednesdayOut.SelectedDate.Value.Minute, 0, 0);
            }
            else
            {
                oIAProperty.WednesdayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                oIAProperty.WednesdayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            }

            if(m_dtThursdayIn.SelectedDate.HasValue && m_dtThursdayOut.SelectedDate.HasValue)
            {
                oIAProperty.ThursdayInDateTime = new DateTime(1950, 1, 1, m_dtThursdayIn.SelectedDate.Value.Hour, m_dtThursdayIn.SelectedDate.Value.Minute, 0, 0);
                oIAProperty.ThursdayOutDateTime = new DateTime(1950, 1, 1, m_dtThursdayOut.SelectedDate.Value.Hour, m_dtThursdayOut.SelectedDate.Value.Minute, 0, 0);
            }
            else
            {
                oIAProperty.ThursdayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                oIAProperty.ThursdayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            }

            if(m_dtFridayIn.SelectedDate.HasValue && m_dtFridayOut.SelectedDate.HasValue)
            {
                oIAProperty.FridayInDateTime = new DateTime(1950, 1, 1, m_dtFridayIn.SelectedDate.Value.Hour, m_dtFridayIn.SelectedDate.Value.Minute, 0, 0);
                oIAProperty.FridayOutDateTime = new DateTime(1950, 1, 1, m_dtFridayOut.SelectedDate.Value.Hour, m_dtFridayOut.SelectedDate.Value.Minute, 0, 0);
            }
            else
            {
                oIAProperty.FridayInDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                oIAProperty.FridayOutDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
            }

            ApplicationContext.SiteProperites.UpdateSiteProperties(oIAProperty);
        }

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