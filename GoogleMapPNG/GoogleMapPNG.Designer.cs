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
            this.textBoxCenterLat = new System.Windows.Forms.TextBox();
            this.lblCenterLat = new System.Windows.Forms.Label();
            this.textBoxCenterLng = new System.Windows.Forms.TextBox();
            this.lblCenterLng = new System.Windows.Forms.Label();
            this.textBoxZoom = new System.Windows.Forms.TextBox();
            this.lblZoom = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.butCreatePNG = new System.Windows.Forms.Button();
            this.webBrowserPNG = new System.Windows.Forms.WebBrowser();
            this.textBoxXSize = new System.Windows.Forms.TextBox();
            this.lblXSize = new System.Windows.Forms.Label();
            this.textBoxYSize = new System.Windows.Forms.TextBox();
            this.lblYSize = new System.Windows.Forms.Label();
            this.textBoxLanguage = new System.Windows.Forms.TextBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            this.textBoxShowLng = new System.Windows.Forms.TextBox();
            this.lblShowLng = new System.Windows.Forms.Label();
            this.textBoxShowLat = new System.Windows.Forms.TextBox();
            this.lblShowLat = new System.Windows.Forms.Label();
            this.butShowPoint = new System.Windows.Forms.Button();
            this.textBoxSubsector = new System.Windows.Forms.TextBox();
            this.lblSubsector = new System.Windows.Forms.Label();
            this.butSubsector = new System.Windows.Forms.Button();
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
            this.textBoxMapType.Location = new System.Drawing.Point(69, 35);
            this.textBoxMapType.Name = "textBoxMapType";
            this.textBoxMapType.Size = new System.Drawing.Size(88, 20);
            this.textBoxMapType.TabIndex = 8;
            this.textBoxMapType.Text = "hybrid";
            // 
            // lblMapType
            // 
            this.lblMapType.AutoSize = true;
            this.lblMapType.Location = new System.Drawing.Point(4, 39);
            this.lblMapType.Name = "lblMapType";
            this.lblMapType.Size = new System.Drawing.Size(58, 13);
            this.lblMapType.TabIndex = 7;
            this.lblMapType.Text = "Map Type:";
            // 
            // textBoxCenterLat
            // 
            this.textBoxCenterLat.Location = new System.Drawing.Point(69, 61);
            this.textBoxCenterLat.Name = "textBoxCenterLat";
            this.textBoxCenterLat.Size = new System.Drawing.Size(88, 20);
            this.textBoxCenterLat.TabIndex = 10;
            this.textBoxCenterLat.Text = "46.095";
            // 
            // lblCenterLat
            // 
            this.lblCenterLat.AutoSize = true;
            this.lblCenterLat.Location = new System.Drawing.Point(3, 65);
            this.lblCenterLat.Name = "lblCenterLat";
            this.lblCenterLat.Size = new System.Drawing.Size(59, 13);
            this.lblCenterLat.TabIndex = 9;
            this.lblCenterLat.Text = "Center Lat:";
            // 
            // textBoxCenterLng
            // 
            this.textBoxCenterLng.Location = new System.Drawing.Point(69, 87);
            this.textBoxCenterLng.Name = "textBoxCenterLng";
            this.textBoxCenterLng.Size = new System.Drawing.Size(88, 20);
            this.textBoxCenterLng.TabIndex = 12;
            this.textBoxCenterLng.Text = "-64.725";
            // 
            // lblCenterLng
            // 
            this.lblCenterLng.AutoSize = true;
            this.lblCenterLng.Location = new System.Drawing.Point(0, 91);
            this.lblCenterLng.Name = "lblCenterLng";
            this.lblCenterLng.Size = new System.Drawing.Size(62, 13);
            this.lblCenterLng.TabIndex = 11;
            this.lblCenterLng.Text = "Center Lng:";
            // 
            // textBoxZoom
            // 
            this.textBoxZoom.Location = new System.Drawing.Point(69, 113);
            this.textBoxZoom.Name = "textBoxZoom";
            this.textBoxZoom.Size = new System.Drawing.Size(88, 20);
            this.textBoxZoom.TabIndex = 14;
            this.textBoxZoom.Text = "16";
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(25, 117);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(37, 13);
            this.lblZoom.TabIndex = 13;
            this.lblZoom.Text = "Zoom:";
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
            // butCreatePNG
            // 
            this.butCreatePNG.Location = new System.Drawing.Point(175, 39);
            this.butCreatePNG.Name = "butCreatePNG";
            this.butCreatePNG.Size = new System.Drawing.Size(94, 23);
            this.butCreatePNG.TabIndex = 20;
            this.butCreatePNG.Text = "Create PNG";
            this.butCreatePNG.UseVisualStyleBackColor = true;
            this.butCreatePNG.Click += new System.EventHandler(this.butCreatePNG_Click);
            // 
            // webBrowserPNG
            // 
            this.webBrowserPNG.Location = new System.Drawing.Point(175, 91);
            this.webBrowserPNG.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPNG.Name = "webBrowserPNG";
            this.webBrowserPNG.Size = new System.Drawing.Size(1354, 859);
            this.webBrowserPNG.TabIndex = 27;
            // 
            // textBoxXSize
            // 
            this.textBoxXSize.Location = new System.Drawing.Point(69, 144);
            this.textBoxXSize.Name = "textBoxXSize";
            this.textBoxXSize.Size = new System.Drawing.Size(88, 20);
            this.textBoxXSize.TabIndex = 29;
            this.textBoxXSize.Text = "640";
            // 
            // lblXSize
            // 
            this.lblXSize.AutoSize = true;
            this.lblXSize.Location = new System.Drawing.Point(22, 148);
            this.lblXSize.Name = "lblXSize";
            this.lblXSize.Size = new System.Drawing.Size(40, 13);
            this.lblXSize.TabIndex = 28;
            this.lblXSize.Text = "X Size:";
            // 
            // textBoxYSize
            // 
            this.textBoxYSize.Location = new System.Drawing.Point(69, 175);
            this.textBoxYSize.Name = "textBoxYSize";
            this.textBoxYSize.Size = new System.Drawing.Size(88, 20);
            this.textBoxYSize.TabIndex = 31;
            this.textBoxYSize.Text = "640";
            // 
            // lblYSize
            // 
            this.lblYSize.AutoSize = true;
            this.lblYSize.Location = new System.Drawing.Point(22, 179);
            this.lblYSize.Name = "lblYSize";
            this.lblYSize.Size = new System.Drawing.Size(40, 13);
            this.lblYSize.TabIndex = 30;
            this.lblYSize.Text = "Y Size:";
            // 
            // textBoxLanguage
            // 
            this.textBoxLanguage.Location = new System.Drawing.Point(69, 206);
            this.textBoxLanguage.Name = "textBoxLanguage";
            this.textBoxLanguage.Size = new System.Drawing.Size(88, 20);
            this.textBoxLanguage.TabIndex = 33;
            this.textBoxLanguage.Text = "en";
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(4, 210);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(58, 13);
            this.lblLanguage.TabIndex = 32;
            this.lblLanguage.Text = "Language:";
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.Location = new System.Drawing.Point(355, 13);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.Size = new System.Drawing.Size(781, 49);
            this.richTextBoxStatus.TabIndex = 34;
            this.richTextBoxStatus.Text = "";
            // 
            // textBoxShowLng
            // 
            this.textBoxShowLng.Location = new System.Drawing.Point(70, 317);
            this.textBoxShowLng.Name = "textBoxShowLng";
            this.textBoxShowLng.Size = new System.Drawing.Size(88, 20);
            this.textBoxShowLng.TabIndex = 38;
            this.textBoxShowLng.Text = "-64.715004";
            // 
            // lblShowLng
            // 
            this.lblShowLng.AutoSize = true;
            this.lblShowLng.Location = new System.Drawing.Point(1, 321);
            this.lblShowLng.Name = "lblShowLng";
            this.lblShowLng.Size = new System.Drawing.Size(58, 13);
            this.lblShowLng.TabIndex = 37;
            this.lblShowLng.Text = "Show Lng:";
            // 
            // textBoxShowLat
            // 
            this.textBoxShowLat.Location = new System.Drawing.Point(70, 291);
            this.textBoxShowLat.Name = "textBoxShowLat";
            this.textBoxShowLat.Size = new System.Drawing.Size(88, 20);
            this.textBoxShowLat.TabIndex = 36;
            this.textBoxShowLat.Text = "46.086980";
            // 
            // lblShowLat
            // 
            this.lblShowLat.AutoSize = true;
            this.lblShowLat.Location = new System.Drawing.Point(4, 295);
            this.lblShowLat.Name = "lblShowLat";
            this.lblShowLat.Size = new System.Drawing.Size(55, 13);
            this.lblShowLat.TabIndex = 35;
            this.lblShowLat.Text = "Show Lat:";
            // 
            // butShowPoint
            // 
            this.butShowPoint.Location = new System.Drawing.Point(28, 358);
            this.butShowPoint.Name = "butShowPoint";
            this.butShowPoint.Size = new System.Drawing.Size(94, 23);
            this.butShowPoint.TabIndex = 20;
            this.butShowPoint.Text = "Show Point";
            this.butShowPoint.UseVisualStyleBackColor = true;
            this.butShowPoint.Click += new System.EventHandler(this.butShowPoint_Click);
            // 
            // textBoxSubsector
            // 
            this.textBoxSubsector.Location = new System.Drawing.Point(73, 425);
            this.textBoxSubsector.Name = "textBoxSubsector";
            this.textBoxSubsector.Size = new System.Drawing.Size(88, 20);
            this.textBoxSubsector.TabIndex = 40;
            this.textBoxSubsector.Text = "NB-06-020-002";
            // 
            // lblSubsector
            // 
            this.lblSubsector.AutoSize = true;
            this.lblSubsector.Location = new System.Drawing.Point(4, 429);
            this.lblSubsector.Name = "lblSubsector";
            this.lblSubsector.Size = new System.Drawing.Size(58, 13);
            this.lblSubsector.TabIndex = 39;
            this.lblSubsector.Text = "Subsector:";
            // 
            // butSubsector
            // 
            this.butSubsector.Location = new System.Drawing.Point(28, 468);
            this.butSubsector.Name = "butSubsector";
            this.butSubsector.Size = new System.Drawing.Size(94, 23);
            this.butSubsector.TabIndex = 20;
            this.butSubsector.Text = "Get Subsector";
            this.butSubsector.UseVisualStyleBackColor = true;
            this.butSubsector.Click += new System.EventHandler(this.butSubsector_Click);
            // 
            // GoogleMapPNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1541, 962);
            this.Controls.Add(this.textBoxSubsector);
            this.Controls.Add(this.lblSubsector);
            this.Controls.Add(this.textBoxShowLng);
            this.Controls.Add(this.lblShowLng);
            this.Controls.Add(this.textBoxShowLat);
            this.Controls.Add(this.lblShowLat);
            this.Controls.Add(this.richTextBoxStatus);
            this.Controls.Add(this.textBoxLanguage);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.textBoxYSize);
            this.Controls.Add(this.lblYSize);
            this.Controls.Add(this.textBoxXSize);
            this.Controls.Add(this.lblXSize);
            this.Controls.Add(this.webBrowserPNG);
            this.Controls.Add(this.butSubsector);
            this.Controls.Add(this.butShowPoint);
            this.Controls.Add(this.butCreatePNG);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.textBoxZoom);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.textBoxCenterLng);
            this.Controls.Add(this.lblCenterLng);
            this.Controls.Add(this.textBoxCenterLat);
            this.Controls.Add(this.lblCenterLat);
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
        private System.Windows.Forms.TextBox textBoxCenterLat;
        private System.Windows.Forms.Label lblCenterLat;
        private System.Windows.Forms.TextBox textBoxCenterLng;
        private System.Windows.Forms.Label lblCenterLng;
        private System.Windows.Forms.TextBox textBoxZoom;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button butCreatePNG;
        private System.Windows.Forms.WebBrowser webBrowserPNG;
        private System.Windows.Forms.TextBox textBoxXSize;
        private System.Windows.Forms.Label lblXSize;
        private System.Windows.Forms.TextBox textBoxYSize;
        private System.Windows.Forms.Label lblYSize;
        private System.Windows.Forms.TextBox textBoxLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.RichTextBox richTextBoxStatus;
        private System.Windows.Forms.TextBox textBoxShowLng;
        private System.Windows.Forms.Label lblShowLng;
        private System.Windows.Forms.TextBox textBoxShowLat;
        private System.Windows.Forms.Label lblShowLat;
        private System.Windows.Forms.Button butShowPoint;
        private System.Windows.Forms.TextBox textBoxSubsector;
        private System.Windows.Forms.Label lblSubsector;
        private System.Windows.Forms.Button butSubsector;
    }
}

