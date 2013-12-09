using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpeedySpots.API.Quickbooks;
using nsoftware.InQB;

namespace SpeedySpots_Quickbook_Connector
{
   public partial class SyncInvoices : Form
   {
      private List<InvoiceRow> m_oRows = new List<InvoiceRow>();
      private Invoice m_oInvoice = new Invoice();
      private bool m_bLoading = false;
      private static Quickbooks m_oQuickbooks = null;
      private static Icon m_emailIcon;

      public SyncInvoices(Quickbooks oQuickbooks)
      {
         InitializeComponent();

         m_oQuickbooks = oQuickbooks;

         m_emailIcon = new Icon(this.GetType(), "Templates.email.ico");
      }

      private void OnLoad(object sender, EventArgs e)
      {
         // Make sure QB is running
         Objsearch oQuery = new Objsearch();

         try
         {
            oQuery.OpenQBConnection();

            m_bLoading = true;
            m_cboDays.Text = "0";
            m_bLoading = false;

            m_dtStart.Value = DateTime.Today.AddDays(-1);
            m_dtEnd.Value = DateTime.Today;

            OnChangedDays(this, new EventArgs());

            m_chkAllSync.Checked = true;
            m_chkAllEmail.Checked = true;
            OnAllSync(this, new EventArgs());
            OnAllEmail(this, new EventArgs());

            m_grdInvoices.Columns["Amount"].DefaultCellStyle.Format = "c";
         }
         catch (InQBObjsearchException oException)
         {
            if (oException.Code == 606)
            {
               MessageBox.Show("Please start Quickbooks and load your company information before running sync process.", "Quickbooks Error");
            }
            else
            {
               MessageBox.Show(oException.Message);
            }

            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
         }
         finally
         {
            try
            {
               oQuery.CloseQBConnection();
            }
            catch (Exception)
            {
            }
         }
      }

      private void OnChangedDays(object sender, EventArgs e)
      {
         if (!m_bLoading)
         {
            if (m_dtStart.Value > m_dtEnd.Value)
            {
               MessageBox.Show("The start date must come before the end date.", "Invoice Date Error");
               return;
            }

            ToggleControls(false);

            m_oRows = new List<InvoiceRow>();
            Objsearch oQuery = new Objsearch();

            try
            {
               oQuery.OpenQBConnection();
               m_oInvoice.OpenQBConnection();

               oQuery.QBXMLVersion = "5.0";
               oQuery.QueryType = ObjsearchQueryTypes.qtInvoiceSearch;
               oQuery.IterateResults = true;
               oQuery.MaxResults = 50;
               oQuery.SearchCriteria = new SearchCriteria();
               oQuery.SearchCriteria.TransactionDateStart = m_dtStart.Value.ToString("d");
               oQuery.SearchCriteria.TransactionDateEnd = m_dtEnd.Value.ToString("d");
               oQuery.Search();

               for (int i = 0; i < oQuery.Results.Count; i++)
               {
                  InsertRow(oQuery.Results[i].ResultId);
               }

               while (oQuery.RemainingResults > 0)
               {
                  oQuery.GetNextResults();

                  for (int i = 0; i < oQuery.Results.Count; i++)
                  {
                     InsertRow(oQuery.Results[i].ResultId);
                  }
               }

               m_oRows.Sort(0, m_oRows.Count, null);

               StringBuilder oData = new StringBuilder();
               foreach (InvoiceRow oRow in m_oRows)
               {
                  oData.Append(string.Format("{0}-{1}-{2}|", oRow.InvoiceNumber, oRow.JobID, oRow.SMSID));
               }

               string sData = oData.ToString();

               if (sData.Length > 2)
               {
                  sData = sData.Substring(0, sData.Length - 1);
               }

               MarkInvalidInvoices(sData);
            }
            finally
            {
               oQuery.CloseQBConnection();
               m_oInvoice.CloseQBConnection();

               ToggleControls(true);
            }
         }
      }

      private void MarkInvalidInvoices(string sData)
      {
         // validate with web service call
         List<string> sValidInvoices = m_oQuickbooks.ValidateInvoices(sData);

         // markup invalid rows
         foreach (string sInvoice in sValidInvoices)
         {
            InvoiceRow oRow = m_oRows.Find(row => row.InvoiceNumber == int.Parse(sInvoice));
            oRow.IsValid = true;
         }

         BindingList<InvoiceRow> oList = new BindingList<InvoiceRow>(m_oRows);
         m_grdInvoices.DataSource = m_oRows;

         foreach (DataGridViewRow oRow in m_grdInvoices.Rows)
         {
            DataGridViewTextBoxCell oIsValidColumn = oRow.Cells["IsValid"] as DataGridViewTextBoxCell;
            if ((bool)oIsValidColumn.Value != true)
            {
               oRow.Cells["Customer"].Style.ForeColor = Color.Red;
               oRow.Cells["Customer"].Style.SelectionForeColor = Color.Red;

               oRow.Cells["Date"].Style.ForeColor = Color.Red;
               oRow.Cells["Date"].Style.SelectionForeColor = Color.Red;

               oRow.Cells["Amount"].Style.ForeColor = Color.Red;
               oRow.Cells["Amount"].Style.SelectionForeColor = Color.Red;
            }
         }
      }

      private void ToggleControls(bool bToggle)
      {
         m_dtStart.Enabled = bToggle;
         m_dtEnd.Enabled = bToggle;
         m_cboDays.Enabled = bToggle;
         m_btnSendInvoices.Enabled = bToggle;
         m_btnCancel.Enabled = bToggle;
         m_grdInvoices.Enabled = bToggle;
         m_chkAllEmail.Enabled = bToggle;
         m_chkAllSync.Enabled = bToggle;
         m_btnRefresh.Enabled = bToggle;
      }

      private void InsertRow(string sInvoiceID)
      {
         double fDays = double.Parse(m_cboDays.Text);
         fDays = fDays * -1;

         m_oInvoice.Get(sInvoiceID);
         string sExportToSSMS = m_oInvoice.GetCustomField("Export to SSMS");
         string sSMSID = m_oInvoice.GetCustomField("SMSID");
         string sJobID = m_oInvoice.GetCustomField("JobID");

         DateTime invoiceExportDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
         DateTime fromDate = DateTime.Today.AddDays(fDays);
         DateTime toDate = DateTime.Today;

         if (DateTime.TryParse(sExportToSSMS, out invoiceExportDate))
         {
            if (invoiceExportDate >= fromDate && invoiceExportDate <= toDate)
            {
               DateTime oTransactionDate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
               DateTime.TryParse(m_oInvoice.TransactionDate, out oTransactionDate);

               int iInvoiceNumber = 0;
               int.TryParse(m_oInvoice.RefNumber, out iInvoiceNumber);

               double fAmount = 0;
               double.TryParse(m_oInvoice.BalanceRemaining, out fAmount);

               InvoiceRow oRow = new InvoiceRow(m_oInvoice.RefId, iInvoiceNumber, m_oInvoice.CustomerName, oTransactionDate, fAmount, sJobID, sSMSID);
               m_oRows.Add(oRow);
            }
         }
      }

      private void OnCancel(object sender, EventArgs e)
      {
         DialogResult = System.Windows.Forms.DialogResult.Cancel;
         Close();
      }

      private void OnSendInvoices(object sender, EventArgs e)
      {
         StringBuilder oInvalidInvoices = new StringBuilder();
         foreach (DataGridViewRow oRow in m_grdInvoices.Rows)
         {
            DataGridViewTextBoxCell oIsValidColumn = oRow.Cells["IsValid"] as DataGridViewTextBoxCell;
            DataGridViewLinkCell oInvoiceColumn = oRow.Cells["InvoiceNumber"] as DataGridViewLinkCell;
            DataGridViewCheckBoxCell oCheckboxColumn = oRow.Cells["IsSync"] as DataGridViewCheckBoxCell;
            if (!(bool)oIsValidColumn.Value && oCheckboxColumn.Value == oCheckboxColumn.TrueValue)
            {
                string invoiceNumber = oInvoiceColumn.Value.ToString();

               oInvalidInvoices.AppendLine(invoiceNumber);
            }
         }

         if (oInvalidInvoices.Length > 0)
         {
            MessageBox.Show("The following invoices are invalid, please fix or deselect them to continue: \r\n\r\n" + oInvalidInvoices.ToString(), "Invalid Invoices");
         }
         else
         {
            DialogResult = System.Windows.Forms.DialogResult.OK;
         }
      }

      private void OnAllSync(object sender, EventArgs e)
      {
         foreach (DataGridViewRow oRow in m_grdInvoices.Rows)
         {
            DataGridViewCheckBoxCell oCheckboxColumn = oRow.Cells["IsSync"] as DataGridViewCheckBoxCell;
            if (m_chkAllSync.Checked)
            {
               oCheckboxColumn.Value = oCheckboxColumn.TrueValue;
            }
            else
            {
               oCheckboxColumn.Value = oCheckboxColumn.FalseValue;
            }
         }
      }

      private void OnAllEmail(object sender, EventArgs e)
      {
         foreach (DataGridViewRow oRow in m_grdInvoices.Rows)
         {
            DataGridViewCheckBoxCell oCheckboxColumn = oRow.Cells["IsEmail"] as DataGridViewCheckBoxCell;
            if (m_chkAllEmail.Checked)
            {
               oCheckboxColumn.Value = oCheckboxColumn.TrueValue;
            }
            else
            {
               oCheckboxColumn.Value = oCheckboxColumn.FalseValue;
            }
         }
      }

      public List<SyncInvoice> GetSelectedInvoices()
      {
         var oSyncInvoices = new List<SyncInvoice>();
         var emailTemplate = new InvoiceEmail();

         foreach (DataGridViewRow oRow in m_grdInvoices.Rows)
         {
            var dataItem = oRow.DataBoundItem as InvoiceRow;

            DataGridViewCheckBoxCell oCheckboxColumn = oRow.Cells["IsSync"] as DataGridViewCheckBoxCell;
            if (oCheckboxColumn.Value == oCheckboxColumn.TrueValue)
            {
               DataGridViewCell oInvoiceIDCell = oRow.Cells["ID"];
               DataGridViewCheckBoxCell oInvoiceEmailCell = oRow.Cells["IsEmail"] as DataGridViewCheckBoxCell;

               SyncInvoice oSyncInvoice = new SyncInvoice();
               oSyncInvoice.ID = (string)oInvoiceIDCell.Value;
               if (oInvoiceEmailCell.Value == oInvoiceEmailCell.TrueValue)
               {
                  oSyncInvoice.IsEmail = true;
                  if (dataItem != null)
                  {
                     if (dataItem.CustomEmail)
                     {
                        oSyncInvoice.EmailSubject = dataItem.EmailSubject;
                        oSyncInvoice.EmailBody = dataItem.EmailBody;
                     }
                     else
                     {
                        oSyncInvoice.EmailSubject = emailTemplate.EmailSubject;
                        oSyncInvoice.EmailBody = emailTemplate.EmailBody;
                     }
                  }
               }
               else
               {
                  oSyncInvoice.IsEmail = false;
               }

               oSyncInvoices.Add(oSyncInvoice);
            }
         }

         return oSyncInvoices;
      }

      private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
      {
         OnAllSync(this, new EventArgs());
         OnAllEmail(this, new EventArgs());

         SetEmailIndicator();
      }

      private void m_grdInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
      {
         var invoiceColumnIndex = m_grdInvoices.Columns["InvoiceNumber"].Index;

         if (e.ColumnIndex == invoiceColumnIndex)
         {
            // requires email checkbox
            var oRow = m_grdInvoices.Rows[e.RowIndex];
            var oCheckboxColumn = oRow.Cells["IsEmail"] as DataGridViewCheckBoxCell;
            if (oCheckboxColumn.Value != oCheckboxColumn.TrueValue)
            {
               MessageBox.Show("Personalized email not available for this invoice while Email is unchecked.");
               return;
            }

            var invoiceRow = m_grdInvoices.Rows[e.RowIndex].DataBoundItem as InvoiceRow;
            EmailTemplateEdit editForm = new EmailTemplateEdit(invoiceRow);
            var result = editForm.ShowDialog();
            if (result == DialogResult.OK)
               SetEmailIndicator();
         }
      }

      private void SetEmailIndicator()
      {
         var customEmailIndicatorIndex = m_grdInvoices.Columns["CustomEmailIndicator"].Index;

         foreach (DataGridViewRow row in m_grdInvoices.Rows)
         {
            var dataItem = row.DataBoundItem as InvoiceRow;
            var displayCell = row.Cells["CustomEmailIndicator"];

            if (dataItem != null && dataItem.CustomEmail)
            {
               displayCell.Value = m_emailIcon.ToBitmap();
            }
            else
            {
               displayCell.Value = new Bitmap(25, 25);
            }
         }
      }
   }
}