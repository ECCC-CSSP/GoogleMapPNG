namespace GoogleMapPNG
{
    partial class GoogleMapPNG
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
            this.lblURL = new System.Windows.Forms.Label();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.textBoxMapType = new System.Windows.Forms.TextBox();
            this.lblMapType = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.webBrowserPNG = new System.Windows.Forms.WebBrowser();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            this.textBoxSubsectorID = new System.Windows.Forms.TextBox();
            this.lblSubsectorID = new System.Windows.Forms.Label();
            this.butCreatePolSourceSitePNG = new System.Windows.Forms.Button();
            this.butCreateMWQMSitePNG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(30, 13);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(32, 13);
            this.lblURL.TabIndex = 5;
            this.lblURL.Text = "URL:";
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(69, 9);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(268, 20);
            this.textBoxURL.TabIndex = 6;
            this.textBoxURL.Text = "https://maps.googleapis.com/maps/api/staticmap";
            // 
            // textBoxMapType
            // 
            this.textBoxMapType.Location = new System.Drawing.Point(82, 35);
            this.textBoxMapType.Name = "textBoxMapType";
            this.textBoxMapType.Size = new System.Drawing.Size(79, 20);
            this.textBoxMapType.TabIndex = 8;
            this.textBoxMapType.Text = "hybrid";
            // 
            // lblMapType
            // 
            this.lblMapType.AutoSize = true;
            this.lblMapType.Location = new System.Drawing.Point(18, 39);
            this.lblMapType.Name = "lblMapType";
            this.lblMapType.Size = new System.Drawing.Size(58, 13);
            this.lblMapType.TabIndex = 7;
            this.lblMapType.Text = "Map Type:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(183, 69);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 13);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Idle";
            // 
            // webBrowserPNG
            // 
            this.webBrowserPNG.Location = new System.Drawing.Point(175, 91);
            this.webBrowserPNG.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPNG.Name = "webBrowserPNG";
            this.webBrowserPNG.Size = new System.Drawing.Size(1354, 859);
            this.webBrowserPNG.TabIndex = 27;
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.Location = new System.Drawing.Point(355, 13);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.Size = new System.Drawing.Size(781, 49);
            this.richTextBoxStatus.TabIndex = 34;
            this.richTextBoxStatus.Text = "";
            // 
            // textBoxSubsectorID
            // 
            this.textBoxSubsectorID.Location = new System.Drawing.Point(82, 72);
            this.textBoxSubsectorID.Name = "textBoxSubsectorID";
            this.textBoxSubsectorID.Size = new System.Drawing.Size(79, 20);
            this.textBoxSubsectorID.TabIndex = 40;
            this.textBoxSubsectorID.Text = "635";
            // 
            // lblSubsectorID
            // 
            this.lblSubsectorID.AutoSize = true;
            this.lblSubsectorID.Location = new System.Drawing.Point(4, 76);
            this.lblSubsectorID.Name = "lblSubsectorID";
            this.lblSubsectorID.Size = new System.Drawing.Size(72, 13);
            this.lblSubsectorID.TabIndex = 39;
            this.lblSubsectorID.Text = "Subsector ID:";
            // 
            // butCreatePolSourceSitePNG
            // 
            this.butCreatePolSourceSitePNG.Location = new System.Drawing.Point(7, 115);
            this.butCreatePolSourceSitePNG.Name = "butCreatePolSourceSitePNG";
            this.butCreatePolSourceSitePNG.Size = new System.Drawing.Size(154, 23);
            this.butCreatePolSourceSitePNG.TabIndex = 20;
            this.butCreatePolSourceSitePNG.Text = "Create Pol Source Site PNG";
            this.butCreatePolSourceSitePNG.UseVisualStyleBackColor = true;
            this.butCreatePolSourceSitePNG.Click += new System.EventHandler(this.butCreatePolSourceSitePNG_Click);
            // 
            // butCreateMWQMSitePNG
            // 
            this.butCreateMWQMSitePNG.Location = new System.Drawing.Point(7, 157);
            this.butCreateMWQMSitePNG.Name = "butCreateMWQMSitePNG";
            this.butCreateMWQMSitePNG.Size = new System.Drawing.Size(154, 23);
            this.butCreateMWQMSitePNG.TabIndex = 41;
            this.butCreateMWQMSitePNG.Text = "Create MWQM Site PNG";
            this.butCreateMWQMSitePNG.UseVisualStyleBackColor = true;
            this.butCreateMWQMSitePNG.Click += new System.EventHandler(this.butCreateMWQMSitePNG_Click);
            // 
            // GoogleMapPNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1541, 962);
            this.Controls.Add(this.butCreateMWQMSitePNG);
            this.Controls.Add(this.textBoxSubsectorID);
            this.Controls.Add(this.lblSubsectorID);
            this.Controls.Add(this.richTextBoxStatus);
            this.Controls.Add(this.webBrowserPNG);
            this.Controls.Add(this.butCreatePolSourceSitePNG);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.textBoxMapType);
            this.Controls.Add(this.lblMapType);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.lblURL);
            this.Name = "GoogleMapPNG";
            this.Text = "Google Map PNG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.TextBox textBoxMapType;
        private System.Windows.Forms.Label lblMapType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.WebBrowser webBrowserPNG;
        private System.Windows.Forms.RichTextBox richTextBoxStatus;
        private System.Windows.Forms.TextBox textBoxSubsectorID;
        private System.Windows.Forms.Label lblSubsectorID;
        private System.Windows.Forms.Button butCreatePolSourceSitePNG;
        private System.Windows.Forms.Button butCreateMWQMSitePNG;
    }
}

