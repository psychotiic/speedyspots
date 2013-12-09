using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Microsoft.Security.Application;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class edit_request : SiteBasePage
    {
        private IARequest                           m_oIARequest = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                Session["IARequestID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
            }
                
            if(Session["IARequestID"] != null)
            {
                m_oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == (int)Session["IARequestID"]);
            }

            if (m_oIARequest != null)
            {
                SetBreadCrumb();

                // Validate the request can be viewed by the current user
                if (!ApplicationContext.CanCurrentUserViewOrder(m_oIARequest))
                {
                    Response.Redirect("~/Default.aspx");
                }
            
                // Security passed, now ensure the request being edit is unlocked
                if(m_oIARequest.IsLocked)
                {
                    Response.Redirect(string.Format("~/order-details.aspx?rid={0}", m_oIARequest.IARequestID));
                }

                if(!IsPostBack)
                {
                    LoadCustomerInformation(MemberProtect.CurrentUser.UserID);

                    m_txtScript.Content = m_oIARequest.Script;
                    m_txtProductionNotes.Content = m_oIARequest.ProductionNotes;

                    if(m_oIARequest.IsRushOrder)
                    {
                        m_radSameDay.Checked = true;
                    }
                    else
                    {
                        m_rad24Hour.Checked = true;
                    }

                    if (m_oIARequest.IARequestFiles != null && m_oIARequest.IARequestFiles.Count > 0)
                    {
                        m_oRepeaterFiles.DataSource = m_oIARequest.IARequestFiles;
                        m_oRepeaterFiles.DataBind();
                    }
                    else
                    {
                        divFilesList.Visible = false;
                    }
                }
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // Validation
            if(m_radContactPhone.SelectedValue == "other")
            {
                if(m_txtContactPhone.Text == string.Empty)
                {
                    SetMessage("Please select or enter a contact phone number for this request.", InetSolution.Web.MessageTone.Negative);
                    return;
                }
            }

            Guid oMPUserID = MemberProtect.CurrentUser.UserID;

            if(m_radContactPhone.SelectedValue == "user")
            {
                m_oIARequest.ContactPhone = MemberProtect.User.GetDataItem(oMPUserID, "Phone");
                m_oIARequest.ContactPhoneExtension = MemberProtect.User.GetDataItem(oMPUserID, "PhoneExtension");
            }
            else if(m_radContactPhone.SelectedValue == "org")
            {
                m_oIARequest.ContactPhone = MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(oMPUserID), "Phone");
                m_oIARequest.ContactPhoneExtension = string.Empty;
            }
            else if(m_radContactPhone.SelectedValue == "other")
            {
                m_oIARequest.ContactPhone = m_txtContactPhone.Text;
                m_oIARequest.ContactPhoneExtension = m_txtContactPhoneExtension.Text;
            }

            m_oIARequest.Script = m_txtScript.Content;
            m_oIARequest.ProductionNotes = m_txtProductionNotes.Content;
            m_oIARequest.NotificationEmails = ApplicationContext.CleanAddressList(m_txtNotificationEmails.Text);
            m_oIARequest.IsRushOrder = m_radSameDay.Checked;
            m_oIARequest.IsLocked = true;
            m_oIARequest.CreateNote(MemberProtect.CurrentUser.UserID, "Request re-locked by customer.");
            DataAccess.SubmitChanges();

            // Validation Successful
            ProcessUploadedFiles(m_oIARequest.IARequestID);

            // Send System Notification
            string sSubject = string.Format("Request #{0} has been re-submitted", m_oIARequest.RequestIdForDisplay);

            StringBuilder oBody = new StringBuilder();
            oBody.Append(string.Format("{0} {1} of {2} has re-saved their request #{3} after being unlocked.</br>", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName"), MemberProtect.Organization.GetName(ApplicationContext.GetOrgID()), m_oIARequest.RequestIdForDisplay));
            oBody.Append("</br>");
            oBody.Append(string.Format("You can <a href='{0}?rid={1}'>view the request here</a>.</br>", ApplicationContext.GetRootUrl(this, "create-job.aspx"), m_oIARequest.IARequestID));

            Business.Services.EmailCommunicationService.SystemNotificationSend(sSubject, oBody.ToString());

            Session["IARequestID"] = m_oIARequest.IARequestID;
            Response.Redirect("~/user-dashboard.aspx");
        }

        #region Private Methods
        
        private void SetBreadCrumb()
        {
            if (Request.QueryString["s"] != null && Request.QueryString["s"].ToString().ToLower() == "a")
            {
                hlDashboardBreadcrumb.NavigateUrl = "~/user-requests.aspx";
                hlDashboardBreadcrumb.Text = "All Requests";
            }
            else if (ApplicationContext.IsCustomer && m_oIARequest.MPUserID != MemberProtect.CurrentUser.UserID)
            {
                hlDashboardBreadcrumb.NavigateUrl = "~/user-requests.aspx";
                hlDashboardBreadcrumb.Text = "All Requests";
            }
            else
            {
                hlDashboardBreadcrumb.NavigateUrl = "~/user-dashboard.aspx";
            }
        }

        private void LoadCustomerInformation(Guid oMPUserID)
        {
            m_lblUserName.Text = MemberProtect.User.GetUsername(oMPUserID);

            LoadContactPhoneInformation(oMPUserID);

            m_lblUserName.Text = AntiXss.HtmlEncode(string.Format("{0} {1}", MemberProtect.User.GetDataItem(oMPUserID, "FirstName"), MemberProtect.User.GetDataItem(oMPUserID, "LastName")));
            m_txtNotificationEmails.Text = m_oIARequest.NotificationEmails;
        }

        private void LoadContactPhoneInformation(Guid oMPUserID)
        {
            string sUserPhone = MemberProtect.User.GetDataItem(oMPUserID, "Phone");
            if(MemberProtect.User.GetDataItem(oMPUserID, "PhoneExtension") != string.Empty)
            {
                sUserPhone += string.Format(" x{0}", MemberProtect.User.GetDataItem(oMPUserID, "PhoneExtension"));
            }

            m_radContactPhone.Items.Clear();
            m_radContactPhone.Items.Add(new ListItem(sUserPhone, "user"));
            m_radContactPhone.Items.Add(new ListItem(MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(oMPUserID), "Phone"), "org"));
            m_radContactPhone.Items.Add(new ListItem("Other:", "other"));

            if(m_oIARequest.ContactPhone == MemberProtect.User.GetDataItem(oMPUserID, "Phone") && m_oIARequest.ContactPhoneExtension == MemberProtect.User.GetDataItem(oMPUserID, "PhoneExtension"))
            {
                m_radContactPhone.SelectedIndex = 0;
            }
            else if(m_oIARequest.ContactPhone == MemberProtect.Organization.GetDataItem(ApplicationContext.GetOrgID(oMPUserID), "Phone") && m_oIARequest.ContactPhoneExtension == string.Empty)
            {
                m_radContactPhone.SelectedIndex = 1;
            }
            else
            {
                m_radContactPhone.SelectedIndex = 2;
                m_txtContactPhone.Text = m_oIARequest.ContactPhone;
                m_txtContactPhoneExtension.Text = m_oIARequest.ContactPhoneExtension;
            }
        }

        private bool ProcessUploadedFiles(int iIARequestID)
        {
            if(m_oUpload.UploadedFiles.Count > 0)
            {
                // First, we must re-name the old existing files so it's clear they are old
                foreach(IARequestFile oIARequestFile in m_oIARequest.IARequestFiles)
                {
                    int iIndex = oIARequestFile.Filename.LastIndexOf('.');

                    if(iIndex >= 0)
                    {
                        oIARequestFile.Filename = oIARequestFile.Filename.Insert(iIndex, "_old");
                    }
                    else
                    {
                        oIARequestFile.Filename += "_old";
                    }

                    DataAccess.SubmitChanges();
                }

                foreach(UploadedFile oFile in m_oUpload.UploadedFiles)
                {
                    // Create file record
                    IARequestFile oIARequestFile = new IARequestFile();
                    oIARequestFile.IARequestID = iIARequestID;
                    oIARequestFile.Filename = oFile.GetName();
                    oIARequestFile.FilenameUnique = string.Format("{0}{1}", Guid.NewGuid(), oFile.GetExtension());
                    oIARequestFile.FileSize = oFile.ContentLength;
                    oIARequestFile.CreatedDateTime = DateTime.Now;
                    DataAccess.IARequestFiles.InsertOnSubmit(oIARequestFile);
                    DataAccess.SubmitChanges();

                    // Save physical file under a new name
                    oFile.SaveAs(string.Format("{0}{1}", ApplicationContext.UploadPath, oIARequestFile.FilenameUnique));
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Customer);

            return oAccessControl;
        }
        #endregion
    }
}
