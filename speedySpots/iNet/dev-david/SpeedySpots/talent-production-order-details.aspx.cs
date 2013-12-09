namespace SpeedySpots
{
   using System;
   using System.Collections.Generic;
   using System.Data.Linq;
   using System.Linq;
   using System.Web.UI.HtmlControls;
   using System.Web.UI.WebControls;
   using Business;
   using Business.Models;
   using DataAccess;
   using InetSolution.Web;
   using Objects;
   using Telerik.Web.UI;

   public partial class talent_production_order_details : SiteBasePage
   {
      private IAProductionOrder m_oIAProductionOrder;

      protected void Page_Load(object sender, EventArgs e)
      {
         if (Request.QueryString["id"] != null)
         {
            Session["IAProductionOrderID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]);
         }

         if (Session["IAProductionOrderID"] != null)
         {
            m_oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == (int) Session["IAProductionOrderID"]);
         }

         if (m_oIAProductionOrder != null)
         {
            // Talent should only be able to view PO's that are assigned to themselves
            if (m_oIAProductionOrder.MPUserIDTalent != MemberProtect.CurrentUser.UserID)
            {
               Response.Redirect(GetBackURL());
            }

            // If the Job has been put on hold (withdrawn), then the talent needs to be told that and redirected to their dashboard
            if (!m_oIAProductionOrder.IAJob.IsSentToProduction)
            {
               RedirectMessage("~/talent-dashboard.aspx", "This production order has been put on hold, you will be notified when it is ready.", MessageTone.Neutral);
            }

            m_oRepeater.ItemCreated += OnRepeaterItemCreated;

            m_oRepeater.DataSource = m_oIAProductionOrder.IASpots;
            m_oRepeater.DataBind();

            if (!m_oIAProductionOrder.HasBeenViewedByTalent)
            {
               m_oIAProductionOrder.HasBeenViewedByTalent = true;
               DataAccess.SubmitChanges();

               foreach (var oIASpot in m_oIAProductionOrder.IASpots)
               {
                  oIASpot.IASpotStatusID = ApplicationContext.GetSpotStatusID(SpotStatus.Viewed);
                  DataAccess.SubmitChanges();
               }
            }

            if (!IsPostBack)
            {
               m_txtNotes.Text = m_oIAProductionOrder.Notes;
               SetupBreadcrumbAndBackButton();
            }
         }
         else
         {
            Response.Redirect(GetBackURL());
         }
      }

      protected void OnTalentFilesCommand(object sender, RepeaterCommandEventArgs e)
      {
         if (e.CommandName == "Delete")
         {
            var iIASpotFileID = MemberProtect.Utility.ValidateInteger((string) e.CommandArgument);

            var oIASpotFile = DataAccess.IASpotFiles.SingleOrDefault(row => row.IASpotFileID == iIASpotFileID);
            if (oIASpotFile != null)
            {
               if (ApplicationContext.DeleteFile(oIASpotFile.IASpotFileID, "spot"))
               {
                  DataAccess.IASpotFiles.DeleteOnSubmit(oIASpotFile);
                  DataAccess.SubmitChanges();

                  m_oRepeater.DataSource = m_oIAProductionOrder.IASpots;
                  m_oRepeater.DataBind();
               }
            }
         }
      }

      protected void OnSave(object sender, EventArgs e)
      {
         Save();
      }

      protected void OnFinished(object sender, EventArgs e)
      {
         Save();

         // Refresh the recordset
         var iSpotsFinished = 0;
         foreach (var oIASpot in m_oIAProductionOrder.IASpots)
         {
            DataAccess.Refresh(RefreshMode.OverwriteCurrentValues, oIASpot);

            if (oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent)).Any())
            {
               iSpotsFinished++;
            }
         }

         // Only finish the PO when all the spots have had files uploaded by the talent
         if (iSpotsFinished == m_oIAProductionOrder.IASpots.Count())
         {
            // Mark the spots as finished
            foreach (var oIASpot in m_oIAProductionOrder.IASpots)
            {
               oIASpot.IASpotStatusID = ApplicationContext.GetSpotStatusID(SpotStatus.Finished);
               oIASpot.CompletedDateTime = DateTime.Now;
               DataAccess.SubmitChanges();
            }

            // Mark the PO as completed
            m_oIAProductionOrder.IAProductionOrderStatusID = ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Complete);
            m_oIAProductionOrder.CompletedDateTime = DateTime.Now;
            DataAccess.SubmitChanges();

            // Mark the job complete once all the PO's are complete
            var oIAJob = m_oIAProductionOrder.IAJob;
            if (oIAJob.IAProductionOrders.Count() ==
                oIAJob.IAProductionOrders.Count(row => row.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Complete)))
            {
               oIAJob.IAJobStatusID = ApplicationContext.GetJobStatusID(JobStatus.CompleteNeedsProduction);
               DataAccess.SubmitChanges();
            }

            RedirectMessage("~/talent-dashboard.aspx", "Production order saved.", MessageTone.Positive);
         }
         else
         {
            ShowMessage("You must upload at least one file for each spot before you can finish the production order.", MessageTone.Negative);
         }
      }

      #region Repeaters

      private void OnRepeaterItemCreated(object sender, RepeaterItemEventArgs e)
      {
         var oIASpot = ((IASpot) e.Item.DataItem);

         if (oIASpot.IASpotFees.Count() > 0)
         {
            var oRepeaterFees = e.Item.FindControl("m_oRepeaterFees") as Repeater;
            oRepeaterFees.DataSource = oIASpot.IASpotFees;
            oRepeaterFees.DataBind();
         }
         else
         {
            var oDivFees = e.Item.FindControl("m_divFees") as HtmlGenericControl;
            oDivFees.Visible = false;
         }

         if (oIASpot.IASpotFiles.Any(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Production)))
         {
            var oRepeaterProductionFiles = e.Item.FindControl("m_oRepeaterProductionFiles") as Repeater;
            oRepeaterProductionFiles.DataSource =
               oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Production));
            oRepeaterProductionFiles.DataBind();
         }
         else
         {
            var oDivProductionFiles = e.Item.FindControl("m_divProductionFiles") as HtmlGenericControl;
            oDivProductionFiles.Visible = false;
         }

         if (oIASpot.IASpotFiles.Any(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent)))
         {
            var oRepeaterTalentFiles = e.Item.FindControl("m_oRepeaterTalentFiles") as Repeater;
            oRepeaterTalentFiles.ItemCreated += OnTalentFilesCreated;
            oRepeaterTalentFiles.DataSource = oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent));
            oRepeaterTalentFiles.DataBind();
         }
         else
         {
            var oDivTalentFiles = e.Item.FindControl("m_divTalentFiles") as HtmlGenericControl;
            oDivTalentFiles.Visible = false;
         }

         var spanRecord = e.Item.FindControl("m_spanRecord") as HtmlGenericControl;
         spanRecord.Visible = false;

         if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.Finished))
         {
            var oDivUpload = e.Item.FindControl("m_divUpload") as HtmlGenericControl;
            oDivUpload.Visible = false;
         }
         else if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.NeedsFix))
         {
            spanRecord.Visible = true;
         }
      }

      private void OnTalentFilesCreated(object sender, RepeaterItemEventArgs e)
      {
         var oIASpotFile = ((IASpotFile) e.Item.DataItem);
         var spanDelete = e.Item.FindControl("m_spanDelete") as HtmlGenericControl;
         spanDelete.Visible = false;

         if (IAProductionOrder.IAProductionOrderStatusID == ApplicationContext.GetProductionOrderStatusID(ProductionOrderStatus.Incomplete) &&
             oIASpotFile.IsDeletable)
         {
            spanDelete.Visible = true;
         }
      }

      #endregion

      #region Public Methods

      public string ProcessPageBreak(int iIndex, int iTotal)
      {
         if (iIndex < (iTotal - 1))
         {
            return "<div class='page-break'></div>";
         }
         else
         {
            return string.Empty;
         }
      }

      public string GetProducers(int iIAJobID)
      {
         return ApplicationContext.GetJobProducers(iIAJobID);
      }

      #endregion

      #region Private Methods

      private void Save()
      {
         // Save Uploads
         foreach (RepeaterItem oItem in m_oRepeater.Items)
         {
            var oUpload = oItem.FindControl("m_oUpload") as RadUpload;

            var iIASpotID = MemberProtect.Utility.ValidateInteger(MemberProtect.Cryptography.Decrypt(oUpload.Attributes["Index"]));
            var oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
            if (oIASpot.IASpotID == iIASpotID)
            {
               ProcessUploadedFiles(oUpload, oIASpot);
            }
         }

         // Save Notes
         m_oIAProductionOrder.Notes = m_txtNotes.Text;
         DataAccess.SubmitChanges();

         m_oRepeater.DataSource = m_oIAProductionOrder.IASpots;
         m_oRepeater.DataBind();
      }

      private bool ProcessUploadedFiles(RadUpload oUpload, IASpot oIASpot)
      {
         if (oUpload.UploadedFiles.Count > 0)
         {
            if (oIASpot.IASpotStatusID == ApplicationContext.GetSpotStatusID(SpotStatus.NeedsFix))
            {
               // Mark all older files as 'old' by appending the word to their filenames.
               foreach (var oIASpotFIle in oIASpot.IASpotFiles.Where(row => row.IASpotFileTypeID == ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent)))
               {
                  var sNewFilename = string.Empty;
                  var sExtension = string.Empty;

                  var iIndex = oIASpotFIle.Filename.LastIndexOf(".");
                  if (iIndex > 0)
                  {
                     sExtension = oIASpotFIle.Filename.Substring(iIndex, oIASpotFIle.Filename.Length - iIndex);
                  }
                  sNewFilename = string.Format("{0}_old{1}", MemberProtect.Utility.Left(oIASpotFIle.Filename, iIndex), sExtension);
                  oIASpotFIle.Filename = sNewFilename;
                  DataAccess.SubmitChanges();
               }
            }

            foreach (UploadedFile oFile in oUpload.UploadedFiles)
            {
               // Create file record
               var oIASpotFile = new IASpotFile();
               oIASpotFile.IASpotFileTypeID = ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Talent);
               oIASpotFile.Filename = oFile.GetName();
               oIASpotFile.FilenameUnique = string.Format("{0}{1}", Guid.NewGuid(), oFile.GetExtension());
               oIASpotFile.FileSize = oFile.ContentLength;
               oIASpotFile.IsDeletable = true;
               oIASpotFile.CreatedDateTime = DateTime.Now;
               oIASpot.IASpotFiles.Add(oIASpotFile);
               DataAccess.SubmitChanges();

               // Save physical file under a new name
               oFile.SaveAs(string.Format("{0}{1}", ApplicationContext.UploadPath, oIASpotFile.FilenameUnique));
            }

            return true;
         }
         else
         {
            return false;
         }
      }

      private string GetBackURL()
      {
         string pageURL;
         var source = "D";

         if (!string.IsNullOrEmpty(Request.QueryString["s"]))
         {
            source = Request.QueryString["s"].ToUpper();
         }

         switch (source)
         {
            case "S":
               pageURL = "~/talent-production-orders-to-qc.aspx";
               break;
            case "C":
               pageURL = "~/talent-production-orders-completed.aspx";
               break;
            default:
               pageURL = "~/talent-dashboard.aspx";
               break;
         }

         return pageURL;
      }

      private void SetupBreadcrumbAndBackButton()
      {
         var source = "D";
         if (!string.IsNullOrEmpty(Request.QueryString["s"]))
         {
            source = Request.QueryString["s"].ToUpper();
         }

         switch (source)
         {
            case "S":
               hlBreadcrumb.Text = "Sent to QC";
               hlBreadcrumb.NavigateUrl = "~/talent-production-orders-to-qc.aspx";
               break;
            case "C":
               hlBreadcrumb.Text = "Completed";
               hlBreadcrumb.NavigateUrl = "~/talent-production-orders-completed.aspx";
               break;
            default:
               hlBreadcrumb.Text = "Pending";
               hlBreadcrumb.NavigateUrl = "~/talent-dashboard.aspx";
               break;
         }

         hlBack.NavigateUrl = hlBreadcrumb.NavigateUrl;
      }

      #endregion

      #region Public Properties

      public IAProductionOrder IAProductionOrder
      {
         get { return m_oIAProductionOrder; }
      }

      #endregion

      #region Virtual Methods

      public override List<AccessControl> GetAccessControl()
      {
         var oAccessControl = new List<AccessControl>();

         oAccessControl.Add(AccessControl.Talent);

         return oAccessControl;
      }

      #endregion
   }
}