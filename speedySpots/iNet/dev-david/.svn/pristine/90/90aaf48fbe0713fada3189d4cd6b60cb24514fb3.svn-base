namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Services;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class quality_control_job_details : SiteBasePage
   {
      private readonly List<string> m_oSentFilenames = new List<string>();
      private IAJob m_oIAJob;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request.QueryString["jid"] != null)
         {
            Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["jid"]);
         }

         if (Session["IAJobID"] != null)
         {
            m_oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == (int) Session["IAJobID"]);

            foreach (var oIARequestProduction in m_oIAJob.IARequestProductions)
            {
               foreach (var oIARequestProductionFile in oIARequestProduction.IARequestProductionFiles)
               {
                  if (!m_oSentFilenames.Contains(oIARequestProductionFile.Filename))
                  {
                     m_oSentFilenames.Add(oIARequestProductionFile.Filename);
                  }
               }
            }
         }

         if (!IsPostBack)
         {
            m_grdList.PageSize = ApplicationContext.GetGridPageSize();
            m_grdTalentFiles.PageSize = ApplicationContext.GetGridPageSize();
         }

         m_btnRerecordUndo.Visible = false;
         m_btnFinished.Visible = false;
         if (m_oIAJob == null)
         {
            Response.Redirect("~/Default.aspx");
         }
         else
         {
            m_oRepeaterProduction.ItemCreated += OnRepeaterProductionItemCreated;
            m_oRepeaterProduction.DataSource = m_oIAJob.IARequest.IARequestProductions.Where(row => row.IAJobID == m_oIAJob.IAJobID);
            m_oRepeaterProduction.DataBind();

            // Rules to show Finished button:                   
            var bFinishVisible = true;
            //1. All spots have had files delivered by the talent
            foreach (var oIAProductionOrder in m_oIAJob.IAProductionOrders)
            {
               foreach (var oIASpot in oIAProductionOrder.IASpots)
               {
                  if (oIASpot.IASpotFiles.Count(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent)) == 0)
                  {
                     bFinishVisible = false;
                     break;
                  }
               }
            }

            //2. At least one file delivery has been sent to the customer
            if (bFinishVisible)
            {
               if (DataAccess.IARequestProductions.Count(row => row.IAJobID == m_oIAJob.IAJobID) == 0)
               {
                  bFinishVisible = false;
               }
            }

            // 3. The actual time has been filled out by the QC for each spot
            if (bFinishVisible)
            {
               foreach (var oIAProductionOrder in m_oIAJob.IAProductionOrders)
               {
                  foreach (var oIASpot in oIAProductionOrder.IASpots)
                  {
                     // TODO for 6674 check each spot fee for actual length, except for type 6
                     foreach (var spotFee in oIASpot.IASpotFees)
                     {
                        if (spotFee.IASpotFeeTypeID != (int) SpotFeeTypes.ListeningFee && string.IsNullOrEmpty(spotFee.LengthActual))
                        {
                           bFinishVisible = false;
                           break;
                        }
                     }
                  }
               }
            }

            // MF 1/31/2012 - Undoing this for case https://inet.fogbugz.com/default.asp?6534 until that work around is addressed
            // MF 3/9/2012 - Re-enableing this for case https://inet.fogbugz.com/default.asp?6615
            // 4. The job hasn't been marked as complete
            if (bFinishVisible && m_oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
            {
               bFinishVisible = false;
            }

            m_btnFinished.Visible = bFinishVisible;
            if (!JobsService.AreAllProducitonOrdersComplete(m_oIAJob, DataAccess))
            {
               m_btnFinished.Attributes["onclick"] =
                  "return ConfirmUser('The talent has not marked this job as finished. Are you sure you want to complete it and send to billing?');";
            }
         }
      }

      protected void OnDeletePakcage(object sender, EventArgs e)
      {
         var btnDelete = sender as LinkButton;
         var iIARequestProductionID = MemberProtect.Utility.ValidateInteger(btnDelete.CommandArgument);

         var oIARequestProduction = DataAccess.IARequestProductions.SingleOrDefault(row => row.IARequestProductionID == iIARequestProductionID);
         if (oIARequestProduction != null)
         {
            // Send email to customer               
            var oSB = new StringBuilder();
            oSB.Append(string.Format("Hello {0},<br/>", MemberProtect.User.GetDataItem(oIARequestProduction.IARequest.MPUserID, "FirstName")));
            oSB.Append("<br/>");
            oSB.Append(
               string.Format(
                  "A recent set of files delivered for your request # {0} have been temporarily pulled down for additional work. We will contact you again once they have been re-posted.<br/>",
                  oIARequestProduction.IARequest.RequestIdForDisplay));
            oSB.Append(string.Format("<a href='{0}?rid={1}'>View Request</a><br/>", ApplicationContext.GetRootUrl(this, "order-details.aspx"),
                                     oIARequestProduction.IARequest.IARequestID));
            oSB.Append("<br/>");
            oSB.Append("Thank you,<br/>");
            oSB.Append("SpeedySpots.com");

            EmailCommunicationService.JobPackageDeleteNoticeToCustomer(oSB, oIARequestProduction.IARequest.MPUser.Username);

            // Send email to other notified
            oSB = new StringBuilder();
            oSB.AppendLine("Hello,<br/>");
            oSB.Append("<br/>");
            oSB.AppendLine(
               string.Format(
                  "A recent set of files delivered for {0} {1} of {2} have been temporarily pulled down for additional work. We will contact you again once they have been re-posted.<br/>",
                  MemberProtect.User.GetDataItem(oIARequestProduction.IARequest.MPUserID, "FirstName"),
                  MemberProtect.User.GetDataItem(oIARequestProduction.IARequest.MPUserID, "LastName"),
                  MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(oIARequestProduction.IARequest.MPUserID))));
            oSB.Append("<br/>");
            oSB.AppendLine("Thank you,<br/>");
            oSB.AppendLine("SpeedySpots.com");

            var sEmails = oIARequestProduction.IARequest.NotificationEmails.Split(',').ToList();
            EmailCommunicationService.JobPackageDeleteNoticeToJobContacts(oSB, sEmails, oIARequestProduction.IARequest.MPUser.Username);

            DataAccess.IARequestProductions.DeleteOnSubmit(oIARequestProduction);
            DataAccess.SubmitChanges();

            ShowMessage("File package deleted.", MessageTone.Positive);
         }
      }

      protected void OnRerecord(object sender, EventArgs e)
      {
         foreach (GridDataItem oDataItem in m_grdList.Items)
         {
            if (oDataItem.OwnerTableView.Name == "Detail")
            {
               if (oDataItem["Recut"] != null)
               {
                  var chkRecut = oDataItem["Recut"].Controls[0] as CheckBox;
                  if (chkRecut.Checked)
                  {
                     var iIASpotID = MemberProtect.Utility.ValidateInteger(oDataItem["IASpotID"].Text);

                     var oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
                     if (oIASpot != null)
                     {
                        SpotService.MarkSpotForReRecord(oIASpot, DataAccess);
                     }
                  }
               }
            }
         }

         m_grdList.Rebind();
      }

      protected void OnCancelRerecord(object sender, EventArgs e)
      {
         foreach (GridDataItem oDataItem in m_grdList.Items)
         {
            if (oDataItem.OwnerTableView.Name == "Detail")
            {
               if (oDataItem["Recut"] != null)
               {
                  var chkRecut = oDataItem["Recut"].Controls[0] as CheckBox;
                  if (chkRecut.Checked)
                  {
                     var iIASpotID = MemberProtect.Utility.ValidateInteger(oDataItem["IASpotID"].Text);

                     var oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
                     if (oIASpot != null)
                     {
                        SpotService.CancelSpotReRecordRequest(oIASpot, DataAccess);
                     }
                  }
               }
            }
         }

         m_grdList.Rebind();
      }

      protected void OnFinished(object sender, EventArgs e)
      {
         m_oIAJob.IAJobStatusID = ApplicationContext.GetJobStatusID(JobStatus.Complete);
         m_oIAJob.MPUserIDCompleted = MemberProtect.CurrentUser.UserID;
         m_oIAJob.CompletedDateTime = DateTime.Now;
         DataAccess.SubmitChanges();

         var oIARequestNote = new IARequestNote();
         oIARequestNote.IARequestID = m_oIAJob.IARequest.IARequestID;
         oIARequestNote.MPUserID = MemberProtect.CurrentUser.UserID;
         oIARequestNote.Note = string.Format("{0} marked as finished, ready for billing.", m_oIAJob.Name);
         oIARequestNote.CreatedDateTime = DateTime.Now;

         DataAccess.IARequestNotes.InsertOnSubmit(oIARequestNote);
         DataAccess.SubmitChanges();

         // Complete request once all jobs are finished
         if (DataAccess.IAJobs.Count(row => row.IARequestID == m_oIAJob.IARequestID) ==
             DataAccess.IAJobs.Count(row => row.IARequestID == m_oIAJob.IARequestID && row.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete)))
         {
            m_oIAJob.IARequest.IARequestStatusID = ApplicationContext.GetRequestStatusID(RequestStatus.Completed);
            DataAccess.SubmitChanges();
         }

         RedirectMessage("~/staff-dashboard.aspx?filter=inproduction", "The job has been completed.", MessageTone.Positive);
      }

      protected void OnSend(object sender, EventArgs e)
      {
         if (Save())
         {
            ShowMessage("Files have been sent to the customer", MessageTone.Positive);
         }
         else
         {
            m_grdList.Rebind();
         }
      }

      protected void OnBack(object sender, EventArgs e)
      {
         Response.Redirect("~/staff-dashboard.aspx?filter=inproduction");
      }

      #region Repeaters

      private void OnRepeaterProductionItemCreated(object sender, RepeaterItemEventArgs e)
      {
         var oIARequestProduction = ((IARequestProduction) e.Item.DataItem);

         if (oIARequestProduction.IARequestProductionFiles.Count() > 0)
         {
            var oRepeaterProductionFiles = e.Item.FindControl("m_oRepeaterProductionFiles") as Repeater;
            oRepeaterProductionFiles.DataSource = oIARequestProduction.IARequestProductionFiles;
            oRepeaterProductionFiles.DataBind();
         }
      }

      #endregion

      #region Grid (PO's & Spots)

      protected void OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
      {
         if (!e.IsFromDetailTable)
         {
            m_grdList.DataSource = DataAccess.fn_QualityControl_GetJobDetails_ProductionOrders(m_oIAJob.IAJobID);
         }
      }

      protected void OnDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
      {
         var oDataItem = e.DetailTableView.ParentItem;

         var iIAProductionOrderID = MemberProtect.Utility.ValidateInteger(oDataItem["IAProductionOrderID"].Text);

         e.DetailTableView.DataSource = DataAccess.IASpots.Where(row => row.IAProductionOrderID == iIAProductionOrderID);
      }

      protected void OnItemDataBound(object sender, GridItemEventArgs e)
      {
         if (e.Item is GridDataItem)
         {
            var oDataItem = e.Item as GridDataItem;

            if (oDataItem.OwnerTableView.Name == "Master")
            {
            }
            else if (oDataItem.OwnerTableView.Name == "Detail")
            {
               var iIASpotID = MemberProtect.Utility.ValidateInteger(oDataItem["IASpotID"].Text);
               var oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
               if (oIASpot != null)
               {
                  var sStatusClass = string.Empty;
                  if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.Finished))
                  {
                     sStatusClass = "Finished";
                  }
                  else if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.NeedsFix))
                  {
                     sStatusClass = "NeedsFix";
                  }
                  else if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.OnHold))
                  {
                     sStatusClass = "OnHold";
                  }
                  else if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.Unviewed))
                  {
                     sStatusClass = "Unviewed";
                  }
                  else if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.Viewed))
                  {
                     sStatusClass = "Viewed";
                  }

                  oDataItem["Status"].CssClass = sStatusClass;
                  if (sStatusClass == "NeedsFix")
                  {
                     oDataItem["Status"].ToolTip = "Needs Fix";
                     m_btnRerecordUndo.Visible = true;
                  }
                  else
                  {
                     if (sStatusClass == "OnHold")
                     {
                        oDataItem["Status"].ToolTip = "On Hold";
                     }
                     else
                     {
                        oDataItem["Status"].ToolTip = sStatusClass;
                     }
                  }

                  if (oDataItem["IsAsap"].Text == "True")
                  {
                     oDataItem["DueDateTime"].Text += " <span class='red'>ASAP</span>";

                     oDataItem.Style["background-color"] = "LemonChiffon";
                  }

                  // Fee(s)
                  var sFeeContent = string.Empty;
                  foreach (var oIASpotFee in oIASpot.IASpotFees)
                  {
                     sFeeContent += string.Format("{0:c}, ", oIASpotFee.Fee);
                  }

                  if (sFeeContent.Length > 0)
                  {
                     sFeeContent = MemberProtect.Utility.Left(sFeeContent, sFeeContent.Length - 2);
                  }

                  oDataItem["Fee"].Text = sFeeContent;

                  // File(s)
                  var sFileContent = string.Empty;
                  foreach (var oIASpotFile in oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent)))
                  {
                     var sFilename = oIASpotFile.Filename;
                     if (sFilename.Length > 18)
                     {
                        sFilename = string.Format("{0}&hellip;{1}", MemberProtect.Utility.Left(sFilename, 14), MemberProtect.Utility.Right(sFilename, 4));
                     }

                     sFileContent += string.Format("<p><a class='download' href='download.aspx?id={0}&type=spot'>{1}</a><br />{2:ddd, dd a\t h:mmt}</p> ",
                                                   oIASpotFile.IASpotFileID, sFilename, oIASpotFile.CreatedDateTime);
                  }

                  if (sFeeContent.Length > 0)
                  {
                     sFileContent = MemberProtect.Utility.Left(sFileContent, sFileContent.Length - 2);
                  }

                  oDataItem["File"].Text = sFileContent;

                  // Notes/Script
                  oDataItem["Notes"].Text = string.Format("<a href=\"javascript:popUp('view-notes-script.aspx?id={0}')\" class='spot-script'>Notes/Script</a>",
                                                          oIASpot.IASpotID);
               }
            }
         }
      }

      protected void OnItemCommand(object source, GridCommandEventArgs e)
      {
         if (e.Item is GridDataItem)
         {
            var oDataItem = e.Item as GridDataItem;

            if (e.Item.OwnerTableView.Name == "Master")
            {
            }
            else if (e.Item.OwnerTableView.Name == "Detail")
            {
               var oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == MemberProtect.Utility.ValidateInteger(oDataItem["IASpotID"].Text));

               if (oIASpot != null)
               {
                  if (e.CommandName == "View")
                  {
                     var iIASpotID = MemberProtect.Utility.ValidateInteger(oDataItem["IASpotID"].Text);
                     var iIAProductionOrderID = MemberProtect.Utility.ValidateInteger(oDataItem["IAProductionOrderID"].Text);
                     var oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == iIAProductionOrderID);
                     if (oIAProductionOrder != null)
                     {
                        Session["IARequestID"] = oIASpot.IAProductionOrder.IAJob.IARequestID;
                        Session["IAJobID"] = oIAProductionOrder.IAJobID;
                        Session["IAProductionOrderID"] = oIAProductionOrder.IAProductionOrderID;
                        Session["IASpotID"] = iIASpotID;

                        Response.Redirect(string.Format("~/quality-control-spot-details.aspx?sid={0}", iIASpotID));
                     }
                  }
               }
            }
         }
      }

      #endregion

      #region Grid (Talent Files)

      protected void OnNeedDataSourceTalentFiles(object source, GridNeedDataSourceEventArgs e)
      {
         m_grdTalentFiles.DataSource = DataAccess.fn_QualityControl_GetJobTalentFiles(m_oIAJob.IAJobID);
      }

      protected void OnItemDataBoundTalentFiles(object sender, GridItemEventArgs e)
      {
         if (e.Item is GridDataItem)
         {
            var oDataItem = e.Item as GridDataItem;
            oDataItem["Sent"].Text = string.Empty;

            var txtNewFilename = oDataItem["NewFilename"].FindControl("m_txtNewFilename") as RadTextBox;
            txtNewFilename.Text = oDataItem["Filename"].Text;

            // Mark any files that have previously been sent (via simple filename check)
            var iIASpotFileID = MemberProtect.Utility.ValidateInteger(oDataItem["IASpotFileID"].Text);
            var oIASpotFile = DataAccess.IASpotFiles.SingleOrDefault(row => row.IASpotFileID == iIASpotFileID);
            if (oIASpotFile != null)
            {
               foreach (var oIARequestProduction in m_oIAJob.IARequestProductions)
               {
                  foreach (var oIARequestProductionFile in oIARequestProduction.IARequestProductionFiles)
                  {
                     if (oIARequestProductionFile.Filename == txtNewFilename.Text)
                     {
                        oDataItem["Sent"].Text = "<img src='img/page_white_tick.png'/>";
                        break;
                     }
                  }
               }
            }
         }
      }

      protected void OnItemCommandTalentFiles(object source, GridCommandEventArgs e)
      {
         if (e.Item is GridDataItem)
         {
            var oDataItem = e.Item as GridDataItem;
         }
      }

      #endregion

      #region Private Methods

      private bool Save()
      {
         // Validate the user is trying to send something either existing talent files or uploaded files
         var bSendingSomething = false;
         foreach (GridDataItem oDataItem in m_grdTalentFiles.Items)
         {
            if (oDataItem["Send"] != null)
            {
               var chkSend = oDataItem["Send"].Controls[0] as CheckBox;
               if (chkSend.Checked)
               {
                  bSendingSomething = true;
                  break;
               }
            }
         }

         if (m_oUpload.UploadedFiles.Count > 0)
         {
            bSendingSomething = true;
         }

         if (!bSendingSomething)
         {
            SetMessage("Please select or upload files to send to the customer.", MessageTone.Negative);
            return false;
         }

         // Process
         var oIARequestProduction = new IARequestProduction();
         oIARequestProduction.IARequestID = m_oIAJob.IARequestID;
         oIARequestProduction.IAJobID = m_oIAJob.IAJobID;
         oIARequestProduction.MPUserID = MemberProtect.CurrentUser.UserID;
         oIARequestProduction.Notes = m_txtNotes.Text;
         oIARequestProduction.CreatedDateTime = DateTime.Now;
         oIARequestProduction.HasRecutBeenRequested = false;
         oIARequestProduction.RecutRequestDateTime = DateTime.Now;
         oIARequestProduction.RecutNotes = string.Empty;
         DataAccess.IARequestProductions.InsertOnSubmit(oIARequestProduction);
         DataAccess.SubmitChanges();

         foreach (GridDataItem oDataItem in m_grdTalentFiles.Items)
         {
            if (oDataItem["Send"] != null)
            {
               var chkSend = oDataItem["Send"].Controls[0] as CheckBox;
               if (chkSend.Checked)
               {
                  // Create a copy of the talent file and then attach it to a new request production
                  var iIASpotFileID = MemberProtect.Utility.ValidateInteger(oDataItem["IASpotFileID"].Text);
                  var oIASpotFile = DataAccess.IASpotFiles.SingleOrDefault(row => row.IASpotFileID == iIASpotFileID);
                  if (oIASpotFile != null)
                  {
                     var txtNewFilename = oDataItem["NewFilename"].FindControl("m_txtNewFilename") as RadTextBox;

                     var sExtension = string.Empty;
                     var iIndex = txtNewFilename.Text.LastIndexOf(".");
                     if (iIndex > 0)
                     {
                        sExtension = txtNewFilename.Text.Substring(iIndex, txtNewFilename.Text.Length - iIndex);
                     }

                     var sFilename = string.Format("{0}{1}", Guid.NewGuid(), sExtension);

                     // Copy files
                     File.Copy(string.Format("{0}{1}", ApplicationContext.UploadPath, oIASpotFile.FilenameUnique),
                               string.Format("{0}{1}", ApplicationContext.UploadPath, sFilename));
                     if (File.Exists(string.Format("{0}{1}", ApplicationContext.UploadPath, sFilename)))
                     {
                        var oIARequestProductionFile = new IARequestProductionFile();
                        oIARequestProductionFile.IARequestProductionID = oIARequestProduction.IARequestProductionID;
                        oIARequestProductionFile.Filename = txtNewFilename.Text;
                        oIARequestProductionFile.FilenameUnique = sFilename;
                        oIARequestProductionFile.FileSize = oIASpotFile.FileSize;
                        oIARequestProductionFile.CreatedDateTime = DateTime.Now;

                        oIARequestProduction.IARequestProductionFiles.Add(oIARequestProductionFile);
                        DataAccess.SubmitChanges();
                     }
                  }
               }
            }
         }

         ProcessUploadedFiles(oIARequestProduction);

         // Send email to customer
         var sName = string.Format("{0} {1}", MemberProtect.User.GetDataItem(oIARequestProduction.IARequest.MPUserID, "FirstName"),
                                   MemberProtect.User.GetDataItem(oIARequestProduction.IARequest.MPUserID, "LastName"));
         var sOrgName = MemberProtect.Organization.GetName(ApplicationContext.GetOrgID(oIARequestProduction.IARequest.MPUserID));
         var fileUrl = string.Empty;

         var oSB = new StringBuilder();
         oSB.AppendLine("<html><body>");
         oSB.AppendLine("Hello,<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine(string.Format("Files have been delivered for {0} of {1}.<br/>", sName, sOrgName));
         oSB.AppendLine(string.Format("Request #{0}<br/>", oIARequestProduction.IARequest.RequestIdForDisplay));
         oSB.AppendLine("<br/>");
         oSB.AppendLine("You may download the files by clicking the link(s) below:<br/>");
         foreach (var oIARequestProductionFile in oIARequestProduction.IARequestProductionFiles)
         {
            fileUrl = string.Format("{0}/{1}/{2}/{3}", ApplicationContext.GetRootUrl(this, "/delivery"),
                                    oIARequestProductionFile.IARequestProductionID.ToString().Replace("-", ""),
                                    oIARequestProductionFile.IARequestProductionFileID.ToString().Replace("-", ""), oIARequestProductionFile.Filename);
            oSB.AppendLine(string.Format("* {0} - <a href=\"{1}\">{1}</a><br />", oIARequestProductionFile.Filename, fileUrl));
         }
         oSB.AppendLine("<br/>");
         oSB.AppendLine(string.Format("NOTE: {0}", oIARequestProduction.Notes.Replace(Environment.NewLine, "<br/>")));
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine(string.Format("<a href='{0}?rid={1}'>View Request Details</a> [login required]<br/>",
                                      ApplicationContext.GetRootUrl(this, "order-details.aspx"), oIARequestProduction.IARequest.IARequestID));
         oSB.AppendLine("<br/>");
         oSB.AppendLine("<br/>");
         oSB.AppendLine("Thank you,<br/>");
         oSB.AppendLine("<a href='http://www.speedyspots.com'>SpeedySpots.com</a><br/>");
         oSB.AppendLine("<a href='mailto:spots@speedyspots.com'>spots@speedyspots.com</a><br/>");
         oSB.AppendLine("(734) 475-9327");
         oSB.AppendLine("</body></html>");

         var subject = string.Format("Files Delivered - #{0}", oIARequestProduction.IARequest.RequestIdForDisplay);
         var alsoContacts = oIARequestProduction.IARequest.NotificationEmails.Split(',').ToList();
         EmailCommunicationService.JobPackageDeliveryNoticeToJobContacts(oSB, subject, oIARequestProduction.IARequest.MPUser.Username, alsoContacts);

         return true;
      }

      private bool ProcessUploadedFiles(IARequestProduction oIARequestProduction)
      {
         if (m_oUpload.UploadedFiles.Count > 0)
         {
            foreach (UploadedFile oFile in (m_oUpload.UploadedFiles))
            {
               // Create file record
               var oIARequestProductionFile = new IARequestProductionFile();
               oIARequestProductionFile.IARequestProductionID = oIARequestProduction.IARequestProductionID;
               oIARequestProductionFile.Filename = oFile.GetName();
               oIARequestProductionFile.FilenameUnique = string.Format("{0}{1}", Guid.NewGuid(), oFile.GetExtension());
               oIARequestProductionFile.FileSize = oFile.ContentLength;
               oIARequestProductionFile.CreatedDateTime = DateTime.Now;
               oIARequestProduction.IARequestProductionFiles.Add(oIARequestProductionFile);
               DataAccess.SubmitChanges();

               // Save physical file under a new name
               oFile.SaveAs(string.Format("{0}{1}", ApplicationContext.UploadPath, oIARequestProductionFile.FilenameUnique));
            }

            return true;
         }
         else
         {
            return false;
         }
      }

      #endregion

      #region Public Properties

      public IAJob IAJob
      {
         get { return m_oIAJob; }
      }

      public List<string> SentFilenames
      {
         get { return m_oSentFilenames; }
      }

      public int ActivityInterval
      {
         get { return ApplicationContext.SiteProperites.ActivityInterval; }
      }

      #endregion

      #region Virtual Methods

      public override List<AccessControl> GetAccessControl()
      {
         var oAccessControl = new List<AccessControl>();

         oAccessControl.Add(AccessControl.Admin);
         oAccessControl.Add(AccessControl.Staff);

         return oAccessControl;
      }

      #endregion
   }
}