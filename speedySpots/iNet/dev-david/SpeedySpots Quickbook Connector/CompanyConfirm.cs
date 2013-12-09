using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpeedySpots_Quickbook_Connector
{
    public partial class CompanyConfirm : Form
    {
        static CompanyConfirm newCompanyConfirm;
        static bool markImported = false;

        public CompanyConfirm()
        {
            InitializeComponent();
        }

        public static bool ShowMessage(string message)
        {
            newCompanyConfirm = new CompanyConfirm();
            newCompanyConfirm.lblMessage.Text = message;
            newCompanyConfirm.ShowDialog();
            return markImported;
        }

        public void CompanyConfirm_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            markImported = (chkMarkImported.CheckState == CheckState.Checked);
            newCompanyConfirm.Dispose(); 
        }
    }
}