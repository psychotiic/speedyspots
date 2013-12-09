using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net.Mail;
using System.Text;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots.Controls
{
   using Business;
   using Business.Models;

   public partial class SpotDetails : SiteBaseControl
    {
        private IAJob                               m_oIAJob = null;
        private IAProductionOrder                   m_oIAProductionOrder = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["jid"] != null)
            {
                Session["IAJobID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["jid"]);
            }

            if(Request.QueryString["poid"] != null)
            {
                Session["IAProductionOrderID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["poid"]);
            }

            if(Session["IAJobID"] != null)
            {
                m_oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == (int)Session["IAJobID"]);
            }

            if(Session["IAProductionOrderID"] != null)
            {
                m_oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == (int)Session["IAProductionOrderID"]);
            }
            else
            {
                // Quality Control cannot create new PO's, so if no PO is found, return them to their dashboard
                if(!IsProducerView)
                {
                    Response.Redirect("~/staff-dashboard.aspx?filter=inproduction");
                }
            }

            LoadVisibility();

            if(!IsPostBack)
            {
                SetupBreadcrumbsAndBackLinks();

                RefreshSpotList();
                RefreshFeeList();

                SetupTalentSelectOptions();

                if(!IsProducerView)
                {
                    if(Request.QueryString["sid"] != null)
                    {
                        Session["IASpotID"] = MemberProtect.Utility.ValidateInteger(Request.QueryString["sid"]);
                    }

                    if(Session["IASpotID"] != null)
                    {
                        IASpotID = (int)Session["IASpotID"];

                        IASpot oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == IASpotID);
                        if(oIASpot != null)
                        {
                            LoadSpotForm(oIASpot);                        
                        }
                    }
                }
            }
        }

        private void SetupBreadcrumbsAndBackLinks()
        {
            if (IsProducerView)
            {
                m_lnkDashboard.NavigateUrl = "~/staff-dashboard.aspx";
                m_lnkJobs.NavigateUrl = string.Format("~/create-job.aspx?rid={0}", m_oIAJob.IARequestID.ToString());

                m_lnkCancelProductionOrder.NavigateUrl = string.Format("~/create-job.aspx?rid={0}", m_oIAJob.IARequestID.ToString());
            }
            else
            {
                m_lnkDashboard.NavigateUrl = "~/staff-dashboard.aspx?filter=inproduction";
                m_lnkJobs.NavigateUrl = string.Format("~/quality-control-job-details.aspx?jid={0}", m_oIAJob.IAJobID);

                m_lnkCancelProductionOrder.NavigateUrl = string.Format("~/quality-control-job-details.aspx?jid={0}", m_oIAJob.IAJobID);
            }
        }

        private void SetupTalentSelectOptions()
        {
            m_cboSpotType.DataValueField = "SpotTypeId";
            m_cboSpotType.DataTextField = "Name";
            m_cboSpotType.DataSource = Business.Services.SpotTypeService.GetSpotTypes(DataAccess);
            m_cboSpotType.DataBind();
        }

        #region Production Order Section
        protected void OnTalentAssigned(object sender, EventArgs e)
        {
            if(Session["IAProductionOrderID"] != null)
            {
                m_oIAProductionOrder = DataAccess.IAProductionOrders.SingleOrDefault(row => row.IAProductionOrderID == (int)Session["IAProductionOrderID"]);

                LoadVisibility();
            }
        }

        protected void OnAddFee(object sender, EventArgs e)
        {
            SaveFees();

            if(IASpotID > 0)
            {
                IASpotFee oIASpotFee = new IASpotFee();
                oIASpotFee.IASpotID = IASpotID;
                oIASpotFee.IASpotFeeTypeID = Business.Services.SpotFeeTypeService.GetSpotFeeTypes(DataAccess).First().SpotFeeTypeId;
                oIASpotFee.LengthActual = string.Empty;
                oIASpotFee.Fee = 0;

                DataAccess.IASpotFees.InsertOnSubmit(oIASpotFee);
                DataAccess.SubmitChanges();
            }
            else
            {
                CreateNewFee();
            }

            RefreshFeeList();
        }

        #endregion

        #region Spot Section
        private void SaveFees()
        {
            foreach(RepeaterItem oItem in m_listFees.Items)
            {
                RadNumericTextBox txtFee = oItem.FindControl("m_txtFee") as RadNumericTextBox;
                DropDownList cboFeeType = oItem.FindControl("m_cboSpotFeeType") as DropDownList;
                HiddenField txtID = oItem.FindControl("m_txtID") as HiddenField;

                if(IASpotID > 0)
                {
                    IASpotFee oIASpotFee = DataAccess.IASpotFees.SingleOrDefault(row => row.IASpotFeeID == MemberProtect.Utility.ValidateInteger(txtID.Value));
                    if(oIASpotFee != null)
                    {
                        oIASpotFee.Fee = MemberProtect.Utility.ValidateDecimal(txtFee.Text);
                        oIASpotFee.IASpotFeeTypeID = MemberProtect.Utility.ValidateInteger(cboFeeType.SelectedValue);
                        DataAccess.SubmitChanges();
                    }
                }
                else
                {
                    SpotFee oIASpotFee = SpotFees.SingleOrDefault(row => row.IASpotFeeID == MemberProtect.Utility.ValidateInteger(txtID.Value));
                    if(oIASpotFee != null)
                    {
                        oIASpotFee.Fee = MemberProtect.Utility.ValidateDecimal(txtFee.Text);
                        oIASpotFee.IASpotFeeTypeID = MemberProtect.Utility.ValidateInteger(cboFeeType.SelectedValue);
                    }
                }
            }
        }

        protected void OnSaveSpot(object sender, EventArgs e)
        {
            IASpot oIASpot = SaveSpotForm();

            if(!IsProducerView)
            {
                LoadSpotForm(oIASpot);
            }
            else
            {
                ClearSpotForm();
                LoadVisibility();
            }

            m_divMessage.Visible = true;
        }

        protected void OnSavePreviewSpot(object sender, EventArgs e)
        {
            // Save the spot
            IASpot oIASpot = SaveSpotForm();

            // Re-load spot information
            LoadSpotForm(oIASpot);

            // Send javascript directive to load preview on refresh, in a new page
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintPreview", string.Format("OpenPreview({0});", oIASpot.IASpotID), true);
        }
        #endregion

        protected void OnDelete(object sender, EventArgs e)
        {
            int requestId = m_oIAProductionOrder.IAJob.IARequestID;

            foreach(IASpot oIASpot in m_oIAProductionOrder.IASpots)
            {
                foreach(IASpotFile oIASpotFile in oIASpot.IASpotFiles)
                {
                    ApplicationContext.DeleteFile(oIASpotFile.IASpotFileID, "SPOT");
                }
            }

            DataAccess.IAProductionOrders.DeleteOnSubmit(m_oIAProductionOrder);
            DataAccess.SubmitChanges();

            if(IsProducerView)
            {
                RedirectMessage(string.Format("~/create-job.aspx?rid={0}", requestId), "Production order has been deleted.", InetSolution.Web.MessageTone.Positive);
            }
            else
            {
                RedirectMessage(string.Format("~/quality-control-job-details.aspx?jid={0}", m_oIAJob.IAJobID), "Production order has been deleted.", InetSolution.Web.MessageTone.Positive);
            }
        }

        #region List (Spots)
        private void OnListSpotsItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Quality Control cannot duplicate a spot
                if(!IsProducerView)
                {
                    LinkButton btnDuplicate = e.Item.FindControl("m_btnDuplicate") as LinkButton;
                    btnDuplicate.Visible = false;
                }

                if(m_oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
                {
                    LinkButton btnDuplicate = e.Item.FindControl("m_btnDuplicate") as LinkButton;
                    LinkButton btnDelete = e.Item.FindControl("m_btnDelete") as LinkButton;

                    btnDuplicate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        protected void OnSpotCommand(object sender, EventArgs e)
        {
            LinkButton oButton = sender as LinkButton;

            int iIASpotID = MemberProtect.Utility.ValidateInteger(oButton.CommandArgument);
            IASpot oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == iIASpotID);
            if(oIASpot != null)
            {
                if(oButton.CommandName == "View")
                {
                    LoadSpotForm(oIASpot);
                }
                else if(oButton.CommandName == "Duplicate")
                {
                    IASpot oDuplicateSpot = ApplicationContext.DuplicateSpot(oIASpot);
                    
                    ClearSpotForm();
                    LoadSpotForm(oDuplicateSpot);

                    RefreshSpotList();
                }
                else if(oButton.CommandName == "Delete")
                {
                    // Delete files first
                    foreach(IASpotFile oIASpotFile in oIASpot.IASpotFiles)
                    {
                        ApplicationContext.DeleteFile(oIASpotFile.IASpotFileID, "SPOT");
                    }

                    DataAccess.IASpots.DeleteOnSubmit(oIASpot);
                    DataAccess.SubmitChanges();

                    RefreshSpotList();
                }
            }
        }

        private void RefreshSpotList()
        {
            m_listSpots.Visible = false;

            if(IsProducerView)
            {
                if(m_oIAProductionOrder != null)
                {
                    m_listSpots.ItemCreated += new RepeaterItemEventHandler(OnListSpotsItemCreated);
                    m_listSpots.DataSource = DataAccess.fn_Producer_GetSpots(m_oIAProductionOrder.IAProductionOrderID);
                    m_listSpots.DataBind();

                    if(m_listSpots.Items.Count > 0)
                    {
                        m_listSpots.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region List (Fees)
        private void OnListFeeItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList cboSpotFeeType = e.Item.FindControl("m_cboSpotFeeType") as DropDownList;
                RadNumericTextBox txtFee = e.Item.FindControl("m_txtFee") as RadNumericTextBox;

                cboSpotFeeType.DataValueField = "SpotFeeTypeId";
                cboSpotFeeType.DataTextField = "Name";
                cboSpotFeeType.DataSource = Business.Services.SpotFeeTypeService.GetSpotFeeTypes(DataAccess);
                cboSpotFeeType.DataBind();

                if(IASpotID > 0)
                {
                    fn_Producer_GetSpotFeesResult oResult = e.Item.DataItem as fn_Producer_GetSpotFeesResult;
                    IASpotFee oIASpotFee = DataAccess.IASpotFees.SingleOrDefault(row => row.IASpotFeeID == oResult.IASPotFeeID);
                    if(oIASpotFee != null)
                    {
                        txtFee.Text = MemberProtect.Utility.FormatDecimal(oIASpotFee.Fee);
                        cboSpotFeeType.SelectedValue = MemberProtect.Utility.FormatInteger(oIASpotFee.IASpotFeeTypeID);
                    }

                    if(m_oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
                    {
                        LinkButton btnDelete = e.Item.FindControl("m_btnDelete") as LinkButton;

                        btnDelete.Visible = false;
                    }
                }
                else
                {
                    SpotFee oSpotFee = e.Item.DataItem as SpotFee;
                    if(oSpotFee != null)
                    {
                        txtFee.Text = MemberProtect.Utility.FormatDecimal(oSpotFee.Fee);
                        cboSpotFeeType.SelectedValue = MemberProtect.Utility.FormatInteger(oSpotFee.IASpotFeeTypeID);
                    }
                }
            }
            else if(e.Item.ItemType == ListItemType.Footer)
            {
                if(m_oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
                {
                    LinkButton btnAdd = e.Item.FindControl("m_btnAdd") as LinkButton;
                    btnAdd.Visible = false;
                }
            }
        }

        protected void OnFeeCommand(object sender, EventArgs e)
        {
            SaveFees();

            LinkButton oButton = sender as LinkButton;

            int iIASpotFeeID = MemberProtect.Utility.ValidateInteger(oButton.CommandArgument);

            if(IASpotID > 0)
            {
                IASpotFee oIASpotFee = DataAccess.IASpotFees.SingleOrDefault(row => row.IASpotFeeID == iIASpotFeeID);
                if(oIASpotFee != null)
                {
                    if(oButton.CommandName == "Delete")
                    {
                        DataAccess.IASpotFees.DeleteOnSubmit(oIASpotFee);
                        DataAccess.SubmitChanges();

                        RefreshFeeList();
                    }
                }
            }
            else
            {
                SpotFee oSpotFee = SpotFees.SingleOrDefault(row => row.IASpotFeeID == iIASpotFeeID);
                if(oSpotFee != null)
                {
                    if(oButton.CommandName == "Delete")
                    {
                        SpotFees.Remove(oSpotFee);

                        RefreshFeeList();
                    }
                }
            }
        }

        private void RefreshFeeList()
        {
            if(IASpotID > 0)
            {
                m_listFees.ItemCreated += new RepeaterItemEventHandler(OnListFeeItemCreated);
                m_listFees.DataSource = DataAccess.fn_Producer_GetSpotFees(IASpotID);
                m_listFees.DataBind();
            }
            else
            {
                if(SpotFees.Count == 0)
                {
                    CreateNewFee();
                }

                m_listFees.ItemCreated += new RepeaterItemEventHandler(OnListFeeItemCreated);
                m_listFees.DataSource = SpotFees;
                m_listFees.DataBind();
            }
        }
        #endregion

        #region List (Files)
        private void OnListFilesItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(m_oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
                {
                    LinkButton btnDelete = e.Item.FindControl("m_btnDelete") as LinkButton;

                    btnDelete.Visible = false;
                }
            }
        }

        protected void OnFilesCommand(object sender, EventArgs e)
        {
            LinkButton oButton = sender as LinkButton;

            int iIASpotFileID = MemberProtect.Utility.ValidateInteger(oButton.CommandArgument);
            IASpotFile oIASpotFile = DataAccess.IASpotFiles.SingleOrDefault(row => row.IASpotFileID == iIASpotFileID);
            if(oIASpotFile != null)
            {
                if(oButton.CommandName == "Delete")
                {
                    if(ApplicationContext.DeleteFile(iIASpotFileID, "SPOT"))
                    {
                        // Delete file record in database
                        DataAccess.IASpotFiles.DeleteOnSubmit(oIASpotFile);
                        DataAccess.SubmitChanges();

                        RefreshFilesList();
                    }
                }
            }
        }

        private void RefreshFilesList()
        {
            m_listFiles.Visible = false;

            if(DataAccess.fn_Producer_GetSpotFiles(IASpotID, ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Production)).Any())
            {
                m_listFiles.Visible = true;
                m_listFiles.ItemCreated += new RepeaterItemEventHandler(OnListFilesItemCreated);
                m_listFiles.DataSource = DataAccess.fn_Producer_GetSpotFiles(IASpotID, ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Production));
                m_listFiles.DataBind();
            }
            else
            {
                m_listFiles.DataSource = null;
                m_listFiles.DataBind();
            }
        }
        #endregion

        #region Private Methods
        private void ClearSpotForm()
        {
            IASpotID = 0;

            m_txtPurchaseOrderNumber.Text = string.Empty;
            m_dtDueDateTime.SelectedDate = null;
            m_chkASAP.Checked = false;
            m_txtLength.Text = string.Empty;
            m_txtTitle.Text = string.Empty;
            m_cboSpotType.SelectedIndex = 0;
            m_txtProductionNotes.Content = string.Empty;
            m_txtScript.Content = string.Empty;

            LoadVisibility();
        }

        private IASpot SaveSpotForm()
        {
            IASpot oIASpot = new IASpot();

            if(IASpotID == 0)
            {
                if(IsProducerView)
                {
                    oIASpot = new IASpot();
                    oIASpot.IAProductionOrderID = m_oIAProductionOrder.IAProductionOrderID;
                    oIASpot.IASpotStatusID = ApplicationContext.GetSpotStatusID(SpotStatus.OnHold);
                    oIASpot.LengthActual = string.Empty;
                    oIASpot.CreatedDateTime = DateTime.Now;
                    oIASpot.CompletedDateTime = DateTime.Now;

                    m_oIAProductionOrder.IASpots.Add(oIASpot);
                }
            }
            else
            {
                oIASpot = DataAccess.IASpots.SingleOrDefault(row => row.IASpotID == IASpotID);
            }

            if(oIASpot != null)
            {
                oIASpot.PurchaseOrderNumber = m_txtPurchaseOrderNumber.Text;
                oIASpot.DueDateTime = m_dtDueDateTime.SelectedDate.Value;
                oIASpot.IsAsap = m_chkASAP.Checked;
                oIASpot.Length = m_txtLength.Text;
                oIASpot.Title = m_txtTitle.Text;
                oIASpot.IASpotTypeID = MemberProtect.Utility.ValidateInteger(m_cboSpotType.SelectedValue);
                oIASpot.ProductionNotes = StripNewlineCharchters(m_txtProductionNotes.Content);
                oIASpot.Script = StripNewlineCharchters(m_txtScript.Content);
                oIASpot.IsReviewed = true;
                DataAccess.SubmitChanges();

                // Save spot fees in memory
                if(IASpotID == 0)
                {
                    foreach(RepeaterItem oItem in m_listFees.Items)
                    {
                        RadNumericTextBox txtFee = oItem.FindControl("m_txtFee") as RadNumericTextBox;
                        DropDownList cboFeeType = oItem.FindControl("m_cboSpotFeeType") as DropDownList;

                        IASpotFee oIASpotFee = new IASpotFee();
                        oIASpotFee.IASpotID = oIASpot.IASpotID;
                        oIASpotFee.IASpotFeeTypeID = MemberProtect.Utility.ValidateInteger(cboFeeType.SelectedValue);
                        oIASpotFee.Fee = MemberProtect.Utility.ValidateDecimal(txtFee.Text);
                        oIASpotFee.LengthActual = string.Empty;
                        oIASpot.IASpotFees.Add(oIASpotFee);
                        DataAccess.SubmitChanges();
                    }

                    // Clear out fees to reset the information for a new Spot
                    m_listFees.DataSource = null;
                    m_listFees.DataBind();

                    SpotFees = new List<SpotFee>();
                    SpotFeesCount = 0;
                    CreateNewFee();
                }

                SaveFees();
                
                IASpotID = 0;

                ProcessUploadedProducerFiles(oIASpot.IASpotID);
                RefreshFilesList();
                RefreshSpotList();
                RefreshFeeList();
            }
            else
            {
                throw new ApplicationException(string.Format("Spot '{0}' cannot be found.", IASpotID));
            }

            return oIASpot;
        }

        private void LoadSpotForm(IASpot oIASpot)
        {
            if(oIASpot != null)
            {
                IASpotID = oIASpot.IASpotID;

                m_txtPurchaseOrderNumber.Text = oIASpot.PurchaseOrderNumber;
                m_dtDueDateTime.SelectedDate = oIASpot.DueDateTime;
                m_chkASAP.Checked = oIASpot.IsAsap;
                m_txtLength.Text = oIASpot.Length;
                m_txtTitle.Text = oIASpot.Title;
                m_cboSpotType.SelectedValue = MemberProtect.Utility.FormatInteger(oIASpot.IASpotTypeID);
                m_txtProductionNotes.Content = oIASpot.ProductionNotes;
                m_txtScript.Content = oIASpot.Script;

                RefreshSpotList();
                RefreshFeeList();
                RefreshFilesList();

                LoadVisibility();
            }
        }

        private void LoadFeeForm(IASpotFee oIASpotFee)
        {
            if(oIASpotFee != null)
            {
                IASpotFeeID = oIASpotFee.IASpotFeeID;

                LoadVisibility();
            }
        }

        private void LoadVisibility()
        {
            m_divMessage.Visible = false;
            m_divSpots.Visible = false;
            m_btnDelete.Visible = false;
            m_listSpots.Visible = false;

            if(m_oIAProductionOrder == null)
            {
                m_divSpotButtons.Visible = false;
            }
            else
            {
                m_divSpots.Visible = true;

                if(IsProducerView)
                {
                    m_btnDelete.Visible = true;
                    m_listSpots.Visible = true;
                }

                m_divSpotButtons.Visible = true;
            }

            if(m_oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
            {
                m_divMainButtons.Visible = false;

                m_btnSaveSpot.OnClientClick = "return ConfirmUser('Are you sure you want to save these changes? This spot is in production or completed and your changes could create billing issues.');";
            }
        }

        private bool ProcessUploadedProducerFiles(int iIASpotID)
        {
            if(m_oUploadProducer.UploadedFiles.Count > 0)
            {
                foreach(UploadedFile oFile in m_oUploadProducer.UploadedFiles)
                {
                    // Create file record
                    IASpotFile oIASpotFile = new IASpotFile();
                    oIASpotFile.IASpotID = iIASpotID;
                    oIASpotFile.IASpotFileTypeID = ApplicationContext.GetSpotFileTypeID(SpotFileTypes.Production);
                    oIASpotFile.Filename = oFile.GetName();
                    oIASpotFile.FilenameUnique = string.Format("{0}{1}", Guid.NewGuid(), oFile.GetExtension());
                    oIASpotFile.FileSize = oFile.ContentLength;
                    oIASpotFile.IsDeletable = true;
                    oIASpotFile.CreatedDateTime = DateTime.Now;
                    DataAccess.IASpotFiles.InsertOnSubmit(oIASpotFile);
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

        private void CreateNewFee()
        {
            SpotFeesCount++;

            SpotFee oSpotFee = new SpotFee();
            oSpotFee.IASpotFeeID = SpotFeesCount;
            oSpotFee.IASpotFeeTypeID = Business.Services.SpotFeeTypeService.GetSpotFeeTypes(DataAccess).First().SpotFeeTypeId;
            oSpotFee.Fee = 0;

            List<SpotFee> oSpotFees = SpotFees;
            oSpotFees.Add(oSpotFee);

            SpotFees = oSpotFees;
        }
        #endregion

        #region Private Properties
        private int IASpotID
        {
            get
            {
                if(ViewState["IASpotID"] == null)
                {
                    ViewState["IASpotID"] = 0;
                    return (int)ViewState["IASpotID"];
                }
                else
                {
                    return (int)ViewState["IASpotID"];
                }
            }

            set { ViewState["IASpotID"] = value; }
        }

        private int IASpotFeeID
        {
            get
            {
                if(ViewState["IASpotFeeID"] == null)
                {
                    ViewState["IASpotFeeID"] = 0;
                    return (int)ViewState["IASpotFeeID"];
                }
                else
                {
                    return (int)ViewState["IASpotFeeID"];
                }
            }

            set { ViewState["IASpotFeeID"] = value; }
        }

        private int IASpotLengthID
        {
            get
            {
                if(ViewState["IASpotLengthID"] == null)
                {
                    ViewState["IASpotLengthID"] = 0;
                    return (int)ViewState["IASpotLengthID"];
                }
                else
                {
                    return (int)ViewState["IASpotLengthID"];
                }
            }

            set { ViewState["IASpotLengthID"] = value; }
        }

        private List<SpotFee> SpotFees
        {
            get
            {
                if(ViewState["SpotFees"] == null)
                {
                    ViewState["SpotFees"] = new List<SpotFee>();
                    return (List<SpotFee>)ViewState["SpotFees"];
                }
                else
                {
                    return (List<SpotFee>)ViewState["SpotFees"];
                }
            }

            set { ViewState["SpotFees"] = value; }
        }

        private int SpotFeesCount
        {
            get
            {
                if(ViewState["SpotFeesCount"] == null)
                {
                    ViewState["SpotFeesCount"] = 0;
                    return (int)ViewState["SpotFeesCount"];
                }
                else
                {
                    return (int)ViewState["SpotFeesCount"];
                }
            }

            set { ViewState["SpotFeesCount"] = value; }
        }
        #endregion

        #region Public Properties
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

        public IAJob IAJob
        {
            get { return m_oIAJob; }
        }

        public IAProductionOrder IAProductionOrder
        {
            get { return m_oIAProductionOrder; }
        }
        #endregion
    }
}