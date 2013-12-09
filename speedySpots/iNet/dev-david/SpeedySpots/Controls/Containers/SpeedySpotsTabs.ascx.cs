using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeedySpots.Controls
{
    public partial class SpeedySpotsTabs : SiteBaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Public Properties
        public bool ShowRequestDetails
        {
            get
            {
                if(ViewState["ShowRequestDetails"] == null)
                {
                    ViewState["ShowRequestDetails"] = false;
                    return (bool)ViewState["ShowRequestDetails"];
                }
                else
                {
                    return (bool)ViewState["ShowRequestDetails"];
                }
            }
            set { ViewState["ShowRequestDetails"] = value; }
        }

        public bool ShowRequestNotes
        {
            get
            {
                if(ViewState["ShowRequestNotes"] == null)
                {
                    ViewState["ShowRequestNotes"] = false;
                    return (bool)ViewState["ShowRequestNotes"];
                }
                else
                {
                    return (bool)ViewState["ShowRequestNotes"];
                }
            }
            set { ViewState["ShowRequestNotes"] = value; }
        }

        public bool ShowJobDetails
        {
            get
            {
                if(ViewState["ShowJobDetails"] == null)
                {
                    ViewState["ShowJobDetails"] = false;
                    return (bool)ViewState["ShowJobDetails"];
                }
                else
                {
                    return (bool)ViewState["ShowJobDetails"];
                }
            }
            set { ViewState["ShowJobDetails"] = value; }
        }

        public string DefaultTab
        {
            get
            {
                if(ViewState["DefaultTab"] == null)
                {
                    ViewState["DefaultTab"] = "Request";
                    return (string)ViewState["DefaultTab"];
                }
                else
                {
                    return (string)ViewState["DefaultTab"];
                }
            }
            set { ViewState["DefaultTab"] = value; }
        }

        public string DefaultView
        {
            get
            {
                if(ViewState["DefaultView"] == null)
                {
                    ViewState["DefaultView"] = "Producer";
                    return (string)ViewState["DefaultView"];
                }
                else
                {
                    return (string)ViewState["DefaultView"];
                }
            }
            set { ViewState["DefaultView"] = value; }
        }

        public bool IsProducerView
        {
            get
            {
                if(DefaultView == "Producer")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void RequestNotesReload()
        {
            m_oRequestNotes.LoadNotes();
        }

        public void RequestDetailsReload()
        {
            m_oRequestDetails.ReloadDetails();
        }

        #endregion
    }
}