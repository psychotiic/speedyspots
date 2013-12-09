namespace SpeedySpots_Quickbook_Connector
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_btnSyncPaymentsAndCustomers = new System.Windows.Forms.Button();
            this.m_oProgressUpload = new System.Windows.Forms.ProgressBar();
            this.m_lblSyncTask = new System.Windows.Forms.Label();
            this.m_lblProgress = new System.Windows.Forms.Label();
            this.m_btnImportPayments = new System.Windows.Forms.Button();
            this.m_btnImportCustomers = new System.Windows.Forms.Button();
            this.m_btnImportInvoices = new System.Windows.Forms.Button();
            this.m_btnSyncInvoices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_btnSyncPaymentsAndCustomers
            // 
            this.m_btnSyncPaymentsAndCustomers.Location = new System.Drawing.Point(13, 52);
            this.m_btnSyncPaymentsAndCustomers.Name = "m_btnSyncPaymentsAndCustomers";
            this.m_btnSyncPaymentsAndCustomers.Size = new System.Drawing.Size(182, 23);
            this.m_btnSyncPaymentsAndCustomers.TabIndex = 0;
            this.m_btnSyncPaymentsAndCustomers.Text = "Download Payments && Customers";
            this.m_btnSyncPaymentsAndCustomers.UseVisualStyleBackColor = true;
            this.m_btnSyncPaymentsAndCustomers.Click += new System.EventHandler(this.OnSyncPaymentsAndCustomers);
            // 
            // m_oProgressUpload
            // 
            this.m_oProgressUpload.Location = new System.Drawing.Point(13, 23);
            this.m_oProgressUpload.Name = "m_oProgressUpload";
            this.m_oProgressUpload.Size = new System.Drawing.Size(321, 23);
            this.m_oProgressUpload.TabIndex = 1;
            // 
            // m_lblSyncTask
            // 
            this.m_lblSyncTask.AutoSize = true;
            this.m_lblSyncTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblSyncTask.Location = new System.Drawing.Point(9, 7);
            this.m_lblSyncTask.Name = "m_lblSyncTask";
            this.m_lblSyncTask.Size = new System.Drawing.Size(0, 13);
            this.m_lblSyncTask.TabIndex = 3;
            // 
            // m_lblProgress
            // 
            this.m_lblProgress.Location = new System.Drawing.Point(258, 7);
            this.m_lblProgress.Name = "m_lblProgress";
            this.m_lblProgress.Size = new System.Drawing.Size(76, 13);
            this.m_lblProgress.TabIndex = 5;
            this.m_lblProgress.Text = "0/0";
            this.m_lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_btnImportPayments
            // 
            this.m_btnImportPayments.Location = new System.Drawing.Point(122, 85);
            this.m_btnImportPayments.Name = "m_btnImportPayments";
            this.m_btnImportPayments.Size = new System.Drawing.Size(104, 23);
            this.m_btnImportPayments.TabIndex = 6;
            this.m_btnImportPayments.Text = "Import Payments";
            this.m_btnImportPayments.UseVisualStyleBackColor = true;
            this.m_btnImportPayments.Click += new System.EventHandler(this.OnTestImportPayments);
            // 
            // m_btnImportCustomers
            // 
            this.m_btnImportCustomers.Location = new System.Drawing.Point(233, 85);
            this.m_btnImportCustomers.Name = "m_btnImportCustomers";
            this.m_btnImportCustomers.Size = new System.Drawing.Size(101, 23);
            this.m_btnImportCustomers.TabIndex = 7;
            this.m_btnImportCustomers.Text = "Import Customers";
            this.m_btnImportCustomers.UseVisualStyleBackColor = true;
            this.m_btnImportCustomers.Click += new System.EventHandler(this.OnTestImportCustomers);
            // 
            // m_btnImportInvoices
            // 
            this.m_btnImportInvoices.Location = new System.Drawing.Point(12, 85);
            this.m_btnImportInvoices.Name = "m_btnImportInvoices";
            this.m_btnImportInvoices.Size = new System.Drawing.Size(104, 23);
            this.m_btnImportInvoices.TabIndex = 8;
            this.m_btnImportInvoices.Text = "Import Invoices";
            this.m_btnImportInvoices.UseVisualStyleBackColor = true;
            this.m_btnImportInvoices.Click += new System.EventHandler(this.OnTestImportInvoices);
            // 
            // m_btnSyncInvoices
            // 
            this.m_btnSyncInvoices.Location = new System.Drawing.Point(201, 52);
            this.m_btnSyncInvoices.Name = "m_btnSyncInvoices";
            this.m_btnSyncInvoices.Size = new System.Drawing.Size(133, 23);
            this.m_btnSyncInvoices.TabIndex = 9;
            this.m_btnSyncInvoices.Text = "Send Invoices";
            this.m_btnSyncInvoices.UseVisualStyleBackColor = true;
            this.m_btnSyncInvoices.Click += new System.EventHandler(this.OnSyncInvoices);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 116);
            this.Controls.Add(this.m_btnSyncInvoices);
            this.Controls.Add(this.m_btnImportInvoices);
            this.Controls.Add(this.m_btnImportCustomers);
            this.Controls.Add(this.m_btnImportPayments);
            this.Controls.Add(this.m_lblProgress);
            this.Controls.Add(this.m_lblSyncTask);
            this.Controls.Add(this.m_oProgressUpload);
            this.Controls.Add(this.m_btnSyncPaymentsAndCustomers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpeedySpots Quickbooks Connector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnSyncPaymentsAndCustomers;
        private System.Windows.Forms.ProgressBar m_oProgressUpload;
        private System.Windows.Forms.Label m_lblSyncTask;
        private System.Windows.Forms.Label m_lblProgress;
        private System.Windows.Forms.Button m_btnImportPayments;
        private System.Windows.Forms.Button m_btnImportCustomers;
        private System.Windows.Forms.Button m_btnImportInvoices;
        private System.Windows.Forms.Button m_btnSyncInvoices;
    }
}

