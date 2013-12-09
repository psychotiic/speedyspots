namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Web.UI;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class create_job : SiteBasePage
   {
      private IARequest m_oIARequest;

      protected void Page_Load(object sender, EventArgs e)
      {
         // User must be a producer in order to visit this page
         if (!ApplicationContext.IsStaff)
         {
            Response.Redirect("~/Default.aspx");
         }

         m_oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == RequestID);

         if (m_oIARequest == null)
         {
            Response.Redirect("~/staff-dashboard.aspx");
         }

         UpdateLabels();

         if (IsPostBack) return;

         SetBreadcrumbs();
         MarkRequestAsViewed();
         CheckRequestStatusAgainstJobs();

         m_grdList.PageSize = ApplicationContext.GetGridPageSize();

         ApplicationContext.LoadLanguages(ref m_cboLanguage);

         // Load status filter options
         m_cboLabels.DataSource = LabelsService.GetLabels();
         m_cboLabels.DataValueField = "IALabelID";
         m_cboLabels.DataTextField = "Text";
         m_cboLabels.DataBind();

         m_cboLabels.Text = "Apply Labels";

         ConfigureEstimateButton();

         // Auto-select any labels already associated with this request
         foreach (var chkLabel in from oRequestLabel in m_oIARequest.IARequestLabels
                                  select m_cboLabels.FindItemByValue(oRequestLabel.IALabelID.ToString(), true)
                                  into oItem
                                  where oItem != null
                                  select oItem.FindControl("m_chkLabel") as CheckBox)
         {
            chkLabel.Checked = true;
         }

         m_lblJob.Text = "Create Job:";
         m_btnUpdate.Visible = false;
         m_txtJobName.Focus();
      }

      private void ConfigureEstimateButton()
      {
         hlEstimate.NavigateUrl = string.Format(hlEstimate.NavigateUrl, RequestID);

         hlEstimate.Text = IARequest.IARequestEstimates.Count == 0 ? "Create Estimate" : "Edit Estimate";

         if (m_oIARequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Completed))
         {
            hlEstimate.Visible = false;
         }
      }

      protected void OnCreateJob(object sender, EventArgs e)
      {
         if (!m_dtJobDueDateTime.SelectedDate.HasValue || m_dtJobDueDateTime.SelectedDate.Value.CompareTo(DateTime.Now) <= 0)
         {
            SetMessage("Please select a future due date for the job.", MessageTone.Negative);
            return;
         }

         var oIAJob = new IAJob
         {
            IARequestID = m_oIARequest.IARequestID,
            IAJobStatusID = ApplicationContext.GetJobStatusID(JobStatus.Incomplete),
            MPUserID = MemberProtect.CurrentUser.UserID,
            MPUserIDProducer = Guid.Empty,
            MPUserIDCompleted = Guid.Empty,
            Sequence = JobsService.GetNextJobSequenceNumberForRequest(m_oIARequest.IARequestID, DataAccess),
            Name = m_txtJobName.Text,
            Language = m_cboLanguage.SelectedValue,
            Notes = string.Empty,
            DueDateTime = m_dtJobDueDateTime.SelectedDate.Value,
            IsASAP = m_chkASAP.Checked,
            QuantityMusic = 0,
            QuantitySFX = 0,
            QuantityProduction = 0,
            QuantityConvert = 0,
            PriceMusic = 0,
            PriceSFX = 0,
            PriceProduction = 0,
            PriceConvert = 0,
            IsMusic = false,
            IsSFX = false,
            IsProduction = false,
            IsConvert = false,
            IsSentToProduction = false,
            MPUserIDCreated = MemberProtect.CurrentUser.UserID,
            CreatedDateTime = DateTime.Now,
            ProductionDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0),
            CompletedDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0)
         };
         DataAccess.IAJobs.InsertOnSubmit(oIAJob);

         // If request has an estimate and it has been approved, mark request as 'Processing', or no estimate exists
         if (m_oIARequest.IARequestEstimates.Any())
         {
            if (m_oIARequest.IARequestEstimates[0].IsApproved)
            {
               m_oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Processing);
            }
         }
         else
         {
            m_oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Processing);
         }

         ApplicationContext.RequestAssignStaff(m_oIARequest, MemberProtect.CurrentUser.UserID);
         DataAccess.SubmitChanges();

         m_oTabs.RequestDetailsReload();

         ShowMessage("Job created succesfully!", MessageTone.Positive);
      }

      protected void OnUpdateJob(object sender, EventArgs e)
      {
         var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == (int) Session["IAJobID"]);
         if (oIAJob == null) return;
         if ((oIAJob.Name != m_txtJobName.Text) && (oIAJob.DueDateTime != m_dtJobDueDateTime.SelectedDate.Value))
         {
            IARequest.CreateNote(MemberProtect.CurrentUser.UserID,
                                 string.Format(
                                               "{0} is now known as {1} and it's due date has changed from {2:M/dd/yyyy a\t h:mm tt} to {3:M/dd/yyyy a\t h:mm tt}",
                                               oIAJob.Name, m_txtJobName.Text, oIAJob.DueDateTime, m_dtJobDueDateTime.SelectedDate.Value));
         }
         else if (oIAJob.Name != m_txtJobName.Text)
         {
            IARequest.CreateNote(MemberProtect.CurrentUser.UserID, string.Format("{0} is now known as {1}", oIAJob.Name, m_txtJobName.Text));
         }
         else if (oIAJob.DueDateTime != m_dtJobDueDateTime.SelectedDate.Value)
         {
            IARequest.CreateNote(MemberProtect.CurrentUser.UserID,
                                 string.Format("{0} due date changed from {1:M/dd/yyyy a\t h:mm tt} to {2:M/dd/yyyy a\t h:mm tt}", oIAJob.Name, oIAJob.DueDateTime,
                                               m_dtJobDueDateTime.SelectedDate.Value));
         }

         oIAJob.Name = m_txtJobName.Text;
         oIAJob.Language = m_cboLanguage.SelectedValue;
         oIAJob.DueDateTime = m_dtJobDueDateTime.SelectedDate.Value;
         oIAJob.IsASAP = m_chkASAP.Checked;
         DataAccess.SubmitChanges();

         m_txtJobName.Text = string.Empty;
         m_dtJobDueDateTime.SelectedDate = null;
         m_chkASAP.Checked = false;
         m_lblJob.Text = "Create Job:";
         m_btnSubmit.Visible = true;
         m_btnUpdate.Visible = false;

         ShowMessage("Job updated.", MessageTone.Positive);
      }

      protected void OnCancelRequest(object sender, EventArgs e)
      {
         m_oIARequest.CreateNote(MemberProtect.CurrentUser.UserID, string.Format("Request Canceled: {0}", m_txtCancellationReason.Text));

         m_oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Canceled);
         DataAccess.SubmitChanges();

         var orgID = ApplicationContext.GetOrgID(MemberProtect.CurrentUser.UserID);
         EmailCommunicationService.UserCancelRequestNoticeSend(m_oIARequest.IARequestID, m_oIARequest.RequestIdForDisplay, orgID, m_txtCancellationReason.Text);

         m_oTabs.RequestDetailsReload();
         m_oTabs.RequestNotesReload();
      }

      protected void OnUnlockRequest(object sender, EventArgs e)
      {
         m_oIARequest.CreateNote(MemberProtect.CurrentUser.UserID, string.Format("Request Unlocked: {0}", m_txtUnlockReason.Text));
         m_oIARequest.MPUserIDLockedByStaff = MemberProtect.CurrentUser.UserID;
         m_oIARequest.IsLocked = false;
         DataAccess.SubmitChanges();
         m_grdList.Rebind();
         m_oTabs.RequestNotesReload();
      }

      protected void OnLockRequest(object sender, EventArgs e)
      {
         m_oIARequest.IsLocked = true;
         DataAccess.SubmitChanges();
         m_grdList.Rebind();
      }

      protected void OnRecut(object sender, EventArgs e)
      {
         var oIARequestProduction =
            DataAccess.IARequestProductions.SingleOrDefault(row => row.IARequestProductionID == MemberProtect.Utility.ValidateInteger(m_txtID.Value));

         if (oIARequestProduction == null) return;
         if (!ApplicationContext.Recut(oIARequestProduction, m_txtDescription.Text)) return;

         ShowMessage("Re-Cut submitted.", MessageTone.Positive);
         m_oTabs.RequestDetailsReload();
      }

      protected void OnPushRequestToProcessing(object sender, EventArgs e)
      {
         m_oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Processing);
         DataAccess.SubmitChanges();

         CheckRequestStatusAgainstJobs();
         m_oTabs.RequestDetailsReload();
      }

      protected void OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
      {
         if (!e.IsFromDetailTable)
         {
            m_grdList.DataSource = (from j in DataAccess.IAJobs
                                    where j.IARequestID == m_oIARequest.IARequestID
                                    select j).ToList();
         }
      }

      protected void OnDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
      {
         var oDataItem = e.DetailTableView.ParentItem;
         var iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
         e.DetailTableView.DataSource = DataAccess.fn_Producer_GetProductionOrders(iIAJobID);
      }

      protected void OnItemDataBound(object sender, GridItemEventArgs e)
      {
         if (!(e.Item is GridDataItem)) return;

         var oDataItem = e.Item as GridDataItem;

         if (oDataItem.OwnerTableView.Name != "Master") return;

         //m_iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["JobID"].Text);
         var oIAJob = (IAJob) e.Item.DataItem;
         if (oIAJob == null) return;

         oDataItem["JobID"].Text = oIAJob.JobIDForDisplay;

         if (oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
         {
            oDataItem["AddPo"].Controls[0].Visible = false;
            oDataItem["Delete"].Controls[0].Visible = false;
         }
         else
         {
            oDataItem["Reactivate"].Controls[0].Visible = false;
         }

         var bHasSpots = oIAJob.IAProductionOrders.Any(oIAProductionOrder => oIAProductionOrder.IASpots.Any());

         if (oIAJob.IsSentToProduction)
         {
            var oButton = oDataItem["Production"].Controls[0] as LinkButton;
            oButton.CommandName = "Withdraw";
            oButton.Text = "Withdraw";
            oButton.Attributes["onclick"] = "return ConfirmUser('Are you sure you want to withdraw this job from production?');";
         }

         if (!oIAJob.IsSentToProduction)
         {
            if (AllSpotsHaveBeenReviewed(oIAJob))
            {
               var oButton = oDataItem["Production"].Controls[0] as LinkButton;
               oButton.CommandName = "Production";
               oButton.Text = "To Production";

               if ((m_oIARequest.EstimateRequested == "Auto" || m_oIARequest.EstimateRequested == "Customer") &&
                   !m_oIARequest.IARequestEstimates.Any())
               {
                  oButton.Attributes["onclick"] =
                     "alert('This request needs an estimate. Please complete and send to customer before sending to production.'); return false;";
               }
               else if (m_oIARequest.IACustomerCreditCardID > 0 && !m_oIARequest.IARequestEstimates.Any())
               {
                  oButton.Attributes["onclick"] =
                     "alert('This request is pre-authorized for payment. Please complete estimate and charge to card before sending to production.'); return false;";
               }
               else
               {
                  if (m_oIARequest.IsLocked)
                  {
                     oButton.Attributes["onclick"] = "return ConfirmUser('Are you sure you want to send this job to production?');";
                  }
                  else
                  {
                     oButton.Attributes["onclick"] =
                        "return ConfirmUser('This request is currently UNLOCKED. Are you sure you want to send this job to production?');";
                  }
               }
            }
         }

         if (!bHasSpots)
         {
            oDataItem["Production"].Controls[0].Visible = false;
         }

         oDataItem["Recut"].Visible = false;
         if (!oIAJob.IARequestProductions.Any()) return;
         var oIARequestProduction = oIAJob.IARequestProductions.OrderByDescending(row => row.CreatedDateTime).First();
         if (oIARequestProduction == null) return;
         if (oIARequestProduction.HasRecutBeenRequested) return;
         oDataItem["Recut"].Visible = true;
         oDataItem["Recut"].CssClass = "recutWindow";
         oDataItem["Recut"].Attributes["onclick"] = string.Format("OnOpenRecut({0});", oIARequestProduction.IARequestProductionID);
      }

      protected void OnItemCommand(object source, GridCommandEventArgs e)
      {
         if (!(e.Item is GridDataItem)) return;

         var oDataItem = e.Item as GridDataItem;

         switch (e.Item.OwnerTableView.Name)
         {
            case "Master":
               switch (e.CommandName)
               {
                  case "View":
                     Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                     Response.Redirect(string.Format("~/create-job.aspx?rid={0}", RequestID));
                     break;
                  case "EditJob":
                  {
                     Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                     var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == (int) Session["IAJobID"]);
                     if (oIAJob != null)
                     {
                        m_txtJobName.Text = oIAJob.Name;
                        m_cboLanguage.SelectedValue = oIAJob.Language;
                        m_dtJobDueDateTime.SelectedDate = oIAJob.DueDateTime;
                        m_chkASAP.Checked = oIAJob.IsASAP;

                        m_lblJob.Text = "Edit Job:";
                        m_btnSubmit.Visible = false;
                        m_btnUpdate.Visible = true;

                        UpdateLabels();

                        ScriptManager.RegisterStartupScript(this, GetType(), "ScrollToBottom", "ScrollToBottom();", true);
                     }
                  }
                     break;
                  case "Reactivate":
                  {
                     var iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                     var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iIAJobID);
                     if (oIAJob != null)
                     {
                        oIAJob.IAJobStatusID = ApplicationContext.GetJobStatusID(JobStatus.CompleteNeedsProduction);
                        oIAJob.IARequest.CreateNote(MemberProtect.CurrentUser.UserID, string.Format("{0} Pulled from Billing", oIAJob.Name));
                        DataAccess.SubmitChanges();

                        SendReactiveEmail(oIAJob);

                        m_grdList.Rebind();
                     }
                  }
                     break;
                  case "Production":
                  {
                     var iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);

                     var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iIAJobID);
                     if (oIAJob != null)
                     {
                        oIAJob.IsSentToProduction = true;
                        oIAJob.MPUserIDProducer = MemberProtect.CurrentUser.UserID;
                        oIAJob.ProductionDateTime = DateTime.Now;

                        var sNote = string.Format("Sent '{0}' into production.", oIAJob.Name);
                        oIAJob.IARequest.CreateNote(MemberProtect.CurrentUser.UserID, sNote);
                        DataAccess.SubmitChanges();

                        var sStatus = ProcessSentToProduction(oIAJob.IARequestID);

                        foreach (var oIAProductionOrder in oIAJob.IAProductionOrders)
                        {
                           foreach (var oIASpot in oIAProductionOrder.IASpots)
                           {
                              oIASpot.IASpotStatusID = ApplicationContext.GetSpotStatusID(SpotStatus.Unviewed);
                              DataAccess.SubmitChanges();
                           }

                           var sASAP = string.Empty;
                           if (oIAProductionOrder.IAJob.IsASAP)
                           {
                              sASAP = " <span style='color: red;'>ASAP</span>";
                           }

                           // The 'Due Date/Time' is the earliest due date/time from all the spots no 'on hold' in the Job's PO's.
                           var oDueDateTime = oIAProductionOrder.IAJob.DueDateTime;
                           var oSpots =
                              oIAProductionOrder.IASpots.Where(row => row.IASpotStatusID != ApplicationContext.GetSpotStatusID(SpotStatus.OnHold))
                                                .OrderBy(row => row.DueDateTime)
                                                .Take(1)
                                                .ToList();
                           if (oSpots != null)
                           {
                              if (oSpots.Count > 0)
                              {
                                 oDueDateTime = oSpots[0].DueDateTime;
                              }
                           }

                           // Send an email to the assigned talent
                           var oSB = new StringBuilder();
                           oSB.AppendLine(string.Format("Hi {0},<br/><br/>", MemberProtect.User.GetDataItem(oIAProductionOrder.MPUserIDTalent, "FirstName")));
                           oSB.AppendLine(string.Format("You have been assigned a new production order for job: <a href='{0}?id={1}'>{2}</a><br/>",
                                                        ApplicationContext.GetRootUrl(Page, "talent-production-order-details.aspx"),
                                                        oIAProductionOrder.IAProductionOrderID, oIAProductionOrder.IAJob.Name));
                           oSB.AppendLine(string.Format("Job Number: {0}<br />", oIAProductionOrder.IAJob.JobIDForDisplay));
                           oSB.AppendLine(string.Format("Due Time: {0:h:mm tt MMM d}{1}", oDueDateTime, sASAP));

                           //Send notice to the talent
                           EmailCommunicationService.JobTalentNotificationSend(oSB, oIAProductionOrder.MPUserIDTalent);
                        }

                        var sMessage = "Job sent to production";
                        if (sStatus == "In Production")
                        {
                           sMessage += ", request is now in production.";
                        }
                        else
                        {
                           sMessage += ".";
                        }

                        RedirectMessage(string.Format("~/create-job.aspx?rid={0}", RequestID), sMessage, MessageTone.Positive);
                     }
                  }
                     break;
                  case "Withdraw":
                  {
                     var iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);

                     var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iIAJobID);
                     if (oIAJob != null)
                     {
                        // Mark all spots as being On Hold
                        var sNote = string.Empty;
                        foreach (var oIAProductionOrder in oIAJob.IAProductionOrders)
                        {
                           foreach (var oIASpot in oIAProductionOrder.IASpots)
                           {
                              oIASpot.IASpotStatusID = ApplicationContext.GetSpotStatusID(SpotStatus.OnHold);
                           }
                           oIAProductionOrder.MPUserIDOnHold = MemberProtect.CurrentUser.UserID;
                           oIAProductionOrder.OnHoldDateTime = DateTime.Now;
                           oIAProductionOrder.HasBeenViewedByTalent = false;
                        }

                        sNote = string.Format("Withdrew '{0}' from production.", oIAJob.Name);
                        oIAJob.IARequest.CreateNote(MemberProtect.CurrentUser.UserID, sNote);

                        oIAJob.IsSentToProduction = false;
                        oIAJob.MPUserIDProducer = Guid.Empty;
                        oIAJob.ProductionDateTime = new DateTime(1950, 1, 1, 0, 0, 0, 0);
                        DataAccess.SubmitChanges();

                        ProcessSentToProduction(oIAJob.IARequestID);

                        Response.Redirect(string.Format("~/create-job.aspx?rid={0}", RequestID));
                     }
                  }
                     break;
                  case "Delete":
                  {
                     var iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);

                     var oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iIAJobID);
                     if (oIAJob != null)
                     {
                        var iIARequestID = oIAJob.IARequestID;

                        foreach (var oIAJobFile in oIAJob.IAJobFiles)
                        {
                           ApplicationContext.DeleteFile(oIAJobFile.IAJobFileID, "JOB");
                        }

                        foreach (
                           var oIASpotFile in
                              oIAJob.IAProductionOrders.SelectMany(oIAProductionOrder => oIAProductionOrder.IASpots.SelectMany(oIASpot => oIASpot.IASpotFiles)))
                        {
                           ApplicationContext.DeleteFile(oIASpotFile.IASpotFileID, "SPOT");
                        }

                        DataAccess.IAJobs.DeleteOnSubmit(oIAJob);
                        DataAccess.SubmitChanges();

                        // Handle processing to 'In Production' or 'Processing'
                        ProcessSentToProduction(iIARequestID);

                        var responseMessage = "Job deleted";

                        // If there are no longer any jobs on this request aftering deleting this job, send Request back to 'Submitted' status
                        if (!m_oIARequest.IAJobs.Any())
                        {
                           // 1. Only if it's not in the 'Waiting Estimate Approval' state
                           if (m_oIARequest.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval))
                           {
                              m_oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Submitted);
                              DataAccess.SubmitChanges();

                              responseMessage += ", request status changed to Submitted.";
                           }
                        }
                        else if (m_oIARequest.IAJobs.Count() ==
                                 m_oIARequest.IAJobs.Count(j => j.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete)))
                        {
                           // If all the jobs in this request are marked as complete, then set the request completed now
                           m_oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Completed);
                           DataAccess.SubmitChanges();

                           responseMessage += ", request is now Complete.";
                        }

                        ShowMessage(responseMessage, MessageTone.Positive);
                     }
                  }
                     break;
                  case "AddProductionOrder":
                     Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                     Session["IAProductionOrderID"] = null;
                     Response.Redirect(string.Format("~/create-production-order.aspx?jid={0}", oDataItem["IAJobID"].Text));
                     break;
               }
               m_oTabs.RequestDetailsReload();
               break;
            case "Detail":
               switch (e.CommandName)
               {
                  case "View":
                     Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                     Response.Redirect(string.Format("~/create-production-order.aspx?jid={0}&poid={1}", oDataItem["IAJobID"].Text,
                                                     oDataItem["IAProductionOrderID"].Text));
                     break;
                  case "Duplicate":
                  {
                     var iIAProductionOrderID = MemberProtect.Utility.ValidateInteger(oDataItem["IAProductionOrderID"].Text);

                     var oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == iIAProductionOrderID);
                     if (oIAProductionOrder == null) return;
                     ApplicationContext.DuplicateProductionOrder(oIAProductionOrder);

                     m_grdList.Rebind();
                  }
                     break;
               }
               break;
         }
      }

      private void SendReactiveEmail(IAJob oIAJob)
      {
         var fromAddress = MemberProtect.CurrentUser.Username;
         var fromName = MemberProtect.User.GetDataItem(MemberProtect.CurrentUser.UserID, "FirstName") + " " +
                        MemberProtect.User.GetDataItem(MemberProtect.CurrentUser.UserID, "LastName");
         var subject = string.Format("Job Reactivated - {0}", oIAJob.Name);
         var messageBody = string.Format("{0} has reactivated job {1} - {2} at {3}", fromName, oIAJob.JobIDForDisplay, oIAJob.Name, DateTime.Now.ToString("f"));

         EmailCommunicationService.JobReactivedBillingNotificationSend(messageBody, subject, fromAddress, fromName);
      }

      private void UpdateLabels()
      {
         m_divLabels.InnerHtml = ApplicationContext.DisplayProducerLabels(IARequest.IARequestID);
      }

      private string ProcessSentToProduction(int iIARequestID)
      {
         var sVerbose = string.Empty;
         var oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == iIARequestID);
         if (oIARequest == null) return sVerbose;

         // Check whether or not the Request should be 'In Production' or 'Processing'
         // 1. If there are jobs and all Jobs on the Request have been Sent to Production then Request is 'In Production'
         // 2. Otherwise Request is in 'Processing'
         if (oIARequest.IAJobs.Any() && oIARequest.IAJobs.Count(row => row.IsSentToProduction) == oIARequest.IAJobs.Count())
         {
            oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.InProduction);
            sVerbose = "In Production";
         }
         else
         {
            oIARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Processing);
            sVerbose = "Processing";
         }
         DataAccess.SubmitChanges();

         return sVerbose;
      }

      private bool AllSpotsHaveBeenReviewed(IAJob oIAJob)
      {
         var iTotalSpots = 0;
         var iTotalReviewedSpots = 0;

         foreach (var oIASpot in oIAJob.IAProductionOrders.SelectMany(oIAProductionOrder => oIAProductionOrder.IASpots))
         {
            iTotalSpots++;
            if (oIASpot.IsReviewed)
            {
               iTotalReviewedSpots++;
            }
         }

         return iTotalSpots == iTotalReviewedSpots;
      }

      private void SetBreadcrumbs()
      {
         if (Request.QueryString["s"] != null && Request.QueryString["s"].ToLower() == "c")
         {
            hlDashboard.NavigateUrl = "~/staff-dashboard-completed.aspx";
         }
         else
         {
            hlDashboard.NavigateUrl = "~/staff-dashboard.aspx";
         }
      }

      private void MarkRequestAsViewed()
      {
         if (m_oIARequest == null || m_oIARequest.HasBeenViewedByProducer) return;

         m_oIARequest.HasBeenViewedByProducer = true;
         DataAccess.SubmitChanges();
      }

      private void CheckRequestStatusAgainstJobs()
      {
         if (m_oIARequest.IAJobs.Any() && m_oIARequest.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Submitted))
         {
            // We've had a recut and need to allow staff to push this back int processing
            lbPushToProcessing.Visible = true;
         }
         else
         {
            lbPushToProcessing.Visible = false;
         }
      }

      protected IARequest IARequest
      {
         get { return m_oIARequest; }
      }

      protected int ActivityInterval
      {
         get { return ApplicationContext.SiteProperites.ActivityInterval; }
      }

      protected int RequestID
      {
         get
         {
            var requestID = 0;
            if (Request.QueryString["rid"] != null)
            {
               requestID = MemberProtect.Utility.ValidateInteger(Request.QueryString["rid"]);
            }

            return requestID;
         }
      }

      public override List<AccessControl> GetAccessControl()
      {
         return new List<AccessControl> {AccessControl.Admin, AccessControl.Staff};
      }
   }
}