using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;

namespace SpeedySpots.Controls
{
    public partial class JobDetails : SiteBaseControl
    {
        private IAJob   m_oIAJob = null;
        private const string _orginalKey = "data-original";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["IAJobID"] != null)
            {
                m_oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == (int)Session["IAJobID"]);
            }

            if(m_oIAJob != null)
            {
                if(!IsPostBack)
                {
                    // Load information
                    m_chkMusic.Checked = m_oIAJob.IsMusic;
                    m_chkSFX.Checked = m_oIAJob.IsSFX;
                    m_chkProduction.Checked = m_oIAJob.IsProduction;
                    m_chkConvert.Checked = m_oIAJob.IsConvert;

                    m_divNotesEdit.Visible = false;
                    m_divNotesView.Visible = false;

                    m_divFilesEdit.Visible = false;
                    m_divFilesView.Visible = false;

                    m_spanMusic.Visible = false;
                    m_spanSFX.Visible = false;
                    m_spanProduction.Visible = false;
                    m_spanConvert.Visible = false;

                    if(IsProducerView)
                    {
                        m_divNotesEdit.Visible = true;
                        m_divFilesEdit.Visible = true;
                    }
                    if(!IsProducerView)
                    {
                        m_spanMusic.Visible = true;
                        m_spanSFX.Visible = true;
                        m_spanProduction.Visible = true;
                        m_spanConvert.Visible = true;

                        m_divNotesView.Visible = true;
                        m_divFilesView.Visible = true;

                        m_txtMusicQuantity.Text = MemberProtect.Utility.FormatInteger(m_oIAJob.QuantityMusic);
                        m_txtMusicPrice.Text = MemberProtect.Utility.FormatDecimal(m_oIAJob.PriceMusic);                      

                        m_txtSFXQuantity.Text = MemberProtect.Utility.FormatInteger(m_oIAJob.QuantitySFX);
                        m_txtSFXPrice.Text = MemberProtect.Utility.FormatDecimal(m_oIAJob.PriceSFX);

                        m_txtProductionQuantity.Text = MemberProtect.Utility.FormatInteger(m_oIAJob.QuantityProduction);
                        m_txtProductionPrice.Text = MemberProtect.Utility.FormatDecimal(m_oIAJob.PriceProduction);

                        m_txtConvertQuantity.Text = MemberProtect.Utility.FormatInteger(m_oIAJob.QuantityConvert);
                        m_txtConvertPrice.Text = MemberProtect.Utility.FormatDecimal(m_oIAJob.PriceConvert);
                        
                        m_divNotesForQC.InnerHtml = m_oIAJob.Notes;

                        SetDataOrginalsOffOfCurrentValue();

                        foreach(IAJobFile oIAJobFile in m_oIAJob.IAJobFiles)
                        {
                            m_divFiles.InnerHtml += string.Format("<a href='download.aspx?id={0}&type=job'>{1}</a><br />", oIAJobFile.IAJobFileID, oIAJobFile.Filename);
                        }
                    }

                    m_txtNotes.Text = m_oIAJob.Notes;
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            if(!IsProducerView)
            {
                m_oIAJob.QuantityMusic = MemberProtect.Utility.ValidateInteger(m_txtMusicQuantity.Text);
                m_oIAJob.QuantitySFX = MemberProtect.Utility.ValidateInteger(m_txtSFXQuantity.Text);
                m_oIAJob.QuantityProduction = MemberProtect.Utility.ValidateInteger(m_txtProductionQuantity.Text);
                m_oIAJob.QuantityConvert = MemberProtect.Utility.ValidateInteger(m_txtConvertQuantity.Text);
                m_oIAJob.PriceMusic = MemberProtect.Utility.ValidateDecimal(m_txtMusicPrice.Text);
                m_oIAJob.PriceSFX = MemberProtect.Utility.ValidateDecimal(m_txtSFXPrice.Text);
                m_oIAJob.PriceProduction = MemberProtect.Utility.ValidateDecimal(m_txtProductionPrice.Text);
                m_oIAJob.PriceConvert = MemberProtect.Utility.ValidateDecimal(m_txtConvertPrice.Text);
            }

            m_oIAJob.IsMusic = m_chkMusic.Checked;
            m_oIAJob.IsSFX = m_chkSFX.Checked;
            m_oIAJob.IsProduction = m_chkProduction.Checked;
            m_oIAJob.IsConvert = m_chkConvert.Checked;

            if(IsProducerView)
            {
                m_oIAJob.Notes = m_txtNotes.Text;
            }
            DataAccess.SubmitChanges();

            if (!IsProducerView)
            {
                SetDataOrginalsOffOfCurrentValue();
            }

            if(IsProducerView)
            {
                if(ProcessUploadedFiles())
                {
                    m_grdList.Rebind();
                }
            }
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if(m_oIAJob != null)
            {
                m_grdList.DataSource = DataAccess.fn_Producer_GetJobFiles(m_oIAJob.IAJobID);
            }
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                int iIAJobFileID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobFileID"].Text);
                IAJobFile oIAJobFile = DataAccess.IAJobFiles.SingleOrDefault(row => row.IAJobFileID == iIAJobFileID);
                
                if(oIAJobFile != null)
                {
                    string sFilename = oIAJobFile.Filename;
                    if(sFilename.Length > 18)
                    {
                        sFilename = string.Format("{0}&#8230;{1}", MemberProtect.Utility.Left(oIAJobFile.Filename, 6), MemberProtect.Utility.Right(oIAJobFile.Filename, 9));
                    }
                    oDataItem["Filename"].Text = string.Format("<a href='download.aspx?id={0}&type=job'>{1}</a>", oIAJobFile.IAJobFileID, sFilename);
                }
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                int iIAJobFileID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobFileID"].Text);
                IAJobFile oIAJobFile = DataAccess.IAJobFiles.SingleOrDefault(row => row.IAJobFileID == iIAJobFileID);

                if(oIAJobFile != null)
                {
                    if(e.CommandName == "Delete")
                    {
                        // Delete files first
                        ApplicationContext.DeleteFile(oIAJobFile.IAJobFileID, "JOB");

                        DataAccess.IAJobFiles.DeleteOnSubmit(oIAJobFile);
                        DataAccess.SubmitChanges();

                        m_grdList.Rebind();
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private bool ProcessUploadedFiles()
        {
            if(m_oUpload.UploadedFiles.Count > 0)
            {
                foreach(UploadedFile oFile in m_oUpload.UploadedFiles)
                {
                    // Create file record
                    IAJobFile oIAJobFile = new IAJobFile();
                    oIAJobFile.IAJobID = m_oIAJob.IAJobID;
                    oIAJobFile.Filename = oFile.GetName();
                    oIAJobFile.FilenameUnique = string.Format("{0}{1}", Guid.NewGuid(), oFile.GetExtension());
                    oIAJobFile.FileSize = oFile.ContentLength;
                    oIAJobFile.CreatedDateTime = DateTime.Now;
                    DataAccess.IAJobFiles.InsertOnSubmit(oIAJobFile);
                    DataAccess.SubmitChanges();

                    // Save physical file under a new name
                    oFile.SaveAs(string.Format("{0}{1}", ApplicationContext.UploadPath, oIAJobFile.FilenameUnique));
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetDataOrginalsOffOfCurrentValue()
        {
            m_chkMusic.InputAttributes.Add(_orginalKey, m_oIAJob.IsMusic.ToString());
            m_txtMusicQuantity.Attributes.Add(_orginalKey, m_txtMusicQuantity.Text);
            m_txtMusicPrice.Attributes.Add(_orginalKey, m_txtMusicPrice.Text);

            m_chkSFX.InputAttributes.Add(_orginalKey, m_chkSFX.Checked.ToString());
            m_txtSFXQuantity.Attributes.Add(_orginalKey, m_txtSFXQuantity.Text);
            m_txtSFXPrice.Attributes.Add(_orginalKey, m_txtSFXPrice.Text);

            m_chkProduction.Attributes.Add(_orginalKey, m_chkProduction.Checked.ToString());
            m_txtProductionQuantity.Attributes.Add(_orginalKey, m_txtProductionQuantity.Text);
            m_txtProductionPrice.Attributes.Add(_orginalKey, m_txtProductionPrice.Text);

            m_chkConvert.InputAttributes.Add(_orginalKey, m_chkConvert.Checked.ToString());
            m_txtConvertQuantity.Attributes.Add(_orginalKey, m_txtConvertQuantity.Text);
            m_txtConvertPrice.Attributes.Add(_orginalKey, m_txtConvertPrice.Text);
        }
        #endregion

        #region Public Properties
        public bool IsProducerView
        {
            get { return ((SpeedySpotsTabs)Parent).IsProducerView; }
        }

        public IAJob IAJob
        {
            get { return m_oIAJob; }
        }
        #endregion
    }
}