using System;
using System.Linq;
using System.Windows.Forms;

namespace SpeedySpots_Quickbook_Connector
{
    public partial class EmailTemplateEdit : Form
    {
        private InvoiceRow invoiceRow;
        private InvoiceEmail invoiceEmailTemplate = new InvoiceEmail();

        public EmailTemplateEdit(InvoiceRow invoiceRow)
        {
            InitializeComponent();

            this.invoiceRow = invoiceRow;
        }
  
        private void LoadDisplay()
        {
            EmailSubjectText.Text = invoiceRow.EmailSubject;
            EmailBodyText.Text = invoiceRow.EmailBody;
        }

        private void InitialDisplay()
        {
            CustomerNameLabel.Text = invoiceRow.Customer;
            InvoiceNumberLabel.Text = string.Format("Invoice #: {0}", invoiceRow.InvoiceNumber.ToString());

            InvoiceDateLabel.Text = invoiceRow.TransactionDate.ToShortDateString();
            InvoiceAmountLabel.Text = invoiceRow.Amount.ToString("c");

            EmailSubjectText.Text = invoiceEmailTemplate.EmailSubject;
            EmailBodyText.Text = invoiceEmailTemplate.EmailBody;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            invoiceRow.CustomEmail = true;
            invoiceRow.EmailSubject = EmailSubjectText.Text.Trim();
            invoiceRow.EmailBody = EmailBodyText.Text.Trim();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to remove this custom email from this invoice?", "Remove custom email", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                ResetEmailTemplate();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ResetEmailTemplate()
        {
            invoiceRow.CustomEmail = false;
            invoiceRow.EmailSubject = invoiceEmailTemplate.EmailSubject;
            invoiceRow.EmailBody = invoiceEmailTemplate.EmailBody;
        }

        private void EmailTemplateEdit_Load(object sender, EventArgs e)
        {
            if (invoiceRow.CustomEmail)
                LoadDisplay();
            else
                InitialDisplay();
        }
    }
}