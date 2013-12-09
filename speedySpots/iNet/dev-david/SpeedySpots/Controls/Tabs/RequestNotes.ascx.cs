using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots.Controls
{
    public partial class RequestNotes : SiteBaseControl
    {
        private IARequest                           m_oIARequest = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ApplicationContext.IsStaff)
            {
                if (Session["IARequestID"] != null)
                {
                    m_oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == (int)Session["IARequestID"]);
                }
                else
                {
                    throw new ApplicationException("Request cannot be found.");
                }


                if (!Page.IsPostBack)
                {
                    LoadNotes();

                    litJSCall.Text = string.Format("$find('{0}').ajaxRequestWithTarget('{1}', 'RefreshNotes');", m_oAjaxPanel.ClientID, m_oAjaxPanel.UniqueID);
                }
            }
        }

        public void LoadNotes()
        {
            rptNotes.DataSource = Business.Services.RequestsService.GetNotesForRequest(m_oIARequest.IARequestID, DataAccess);
            rptNotes.DataBind();
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            IARequestNote oIARequestNote = new IARequestNote();
            oIARequestNote.IARequestID = m_oIARequest.IARequestID;
            oIARequestNote.MPUserID = MemberProtect.CurrentUser.UserID;
            oIARequestNote.Note = m_txtNote.Text;
            oIARequestNote.CreatedDateTime = DateTime.Now;
            DataAccess.IARequestNotes.InsertOnSubmit(oIARequestNote);
            DataAccess.SubmitChanges();

            m_txtNote.Text = string.Empty;
            m_txtNote.Focus();

            LoadNotes();
        }
        

        protected void m_oAjaxPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            switch (e.Argument)
            {
                case "RefreshNotes":
                    LoadNotes();
                    break;
                default:
                    break;
            }
        }

        #region Public Properties
        public IARequest IARequest
        {
            get { return m_oIARequest; }
        }
        #endregion
    }
}