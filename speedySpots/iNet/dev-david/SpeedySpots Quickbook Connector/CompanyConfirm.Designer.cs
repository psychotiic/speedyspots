namespace SpeedySpots_Quickbook_Connector
{
    partial class CompanyConfirm
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
            if (disposing && (components != null))
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
            this.btn_Ok = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.chkMarkImported = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(98, 88);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.SystemColors.Window;
            this.lblMessage.Location = new System.Drawing.Point(12, 11);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(260, 34);
            this.lblMessage.TabIndex = 1;
            // 
            // chkMarkImported
            // 
            this.chkMarkImported.AutoSize = true;
            this.chkMarkImported.Location = new System.Drawing.Point(12, 62);
            this.chkMarkImported.Name = "chkMarkImported";
            this.chkMarkImported.Size = new System.Drawing.Size(144, 17);
            this.chkMarkImported.TabIndex = 2;
            this.chkMarkImported.Text = "Mark as imported in SMS";
            this.chkMarkImported.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 53);
            this.panel1.TabIndex = 3;
            // 
            // CompanyConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 117);
            this.ControlBox = false;
            this.Controls.Add(this.chkMarkImported);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CompanyConfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Company Duplicate";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.CheckBox chkMarkImported;
        private System.Windows.Forms.Panel panel1;
    }
}