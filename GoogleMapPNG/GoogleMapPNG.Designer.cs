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
            this.lblDone = new System.Windows.Forms.Label();
            this.butCreatePNG2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxFactorLng = new System.Windows.Forms.TextBox();
            this.lblFactorLng = new System.Windows.Forms.Label();
            this.textBoxFactorLat = new System.Windows.Forms.TextBox();
            this.lblFactorLat = new System.Windows.Forms.Label();
            this.webBrowserPNG = new System.Windows.Forms.WebBrowser();
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
            this.textBoxURL.Size = new System.Drawing.Size(582, 20);
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
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(214, 69);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(0, 13);
            this.lblDone.TabIndex = 19;
            // 
            // butCreatePNG2
            // 
            this.butCreatePNG2.Location = new System.Drawing.Point(175, 39);
            this.butCreatePNG2.Name = "butCreatePNG2";
            this.butCreatePNG2.Size = new System.Drawing.Size(94, 23);
            this.butCreatePNG2.TabIndex = 20;
            this.butCreatePNG2.Text = "Create PNG2";
            this.butCreatePNG2.UseVisualStyleBackColor = true;
            this.butCreatePNG2.Click += new System.EventHandler(this.butCreatePNG2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(377, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(351, 48);
            this.richTextBox1.TabIndex = 22;
            this.richTextBox1.Text = "";
            // 
            // textBoxFactorLng
            // 
            this.textBoxFactorLng.Location = new System.Drawing.Point(69, 140);
            this.textBoxFactorLng.Name = "textBoxFactorLng";
            this.textBoxFactorLng.Size = new System.Drawing.Size(88, 20);
            this.textBoxFactorLng.TabIndex = 24;
            this.textBoxFactorLng.Text = "0.004";
            // 
            // lblFactorLng
            // 
            this.lblFactorLng.AutoSize = true;
            this.lblFactorLng.Location = new System.Drawing.Point(1, 144);
            this.lblFactorLng.Name = "lblFactorLng";
            this.lblFactorLng.Size = new System.Drawing.Size(61, 13);
            this.lblFactorLng.TabIndex = 23;
            this.lblFactorLng.Text = "Factor Lng:";
            // 
            // textBoxFactorLat
            // 
            this.textBoxFactorLat.Location = new System.Drawing.Point(68, 169);
            this.textBoxFactorLat.Name = "textBoxFactorLat";
            this.textBoxFactorLat.Size = new System.Drawing.Size(88, 20);
            this.textBoxFactorLat.TabIndex = 26;
            this.textBoxFactorLat.Text = "0.004";
            // 
            // lblFactorLat
            // 
            this.lblFactorLat.AutoSize = true;
            this.lblFactorLat.Location = new System.Drawing.Point(0, 173);
            this.lblFactorLat.Name = "lblFactorLat";
            this.lblFactorLat.Size = new System.Drawing.Size(58, 13);
            this.lblFactorLat.TabIndex = 25;
            this.lblFactorLat.Text = "Factor Lat:";
            // 
            // webBrowserPNG
            // 
            this.webBrowserPNG.Location = new System.Drawing.Point(175, 91);
            this.webBrowserPNG.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPNG.Name = "webBrowserPNG";
            this.webBrowserPNG.Size = new System.Drawing.Size(832, 717);
            this.webBrowserPNG.TabIndex = 27;
            // 
            // GoogleMapPNG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 816);
            this.Controls.Add(this.webBrowserPNG);
            this.Controls.Add(this.textBoxFactorLat);
            this.Controls.Add(this.lblFactorLat);
            this.Controls.Add(this.textBoxFactorLng);
            this.Controls.Add(this.lblFactorLng);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.butCreatePNG2);
            this.Controls.Add(this.lblDone);
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
        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.Button butCreatePNG2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBoxFactorLng;
        private System.Windows.Forms.Label lblFactorLng;
        private System.Windows.Forms.TextBox textBoxFactorLat;
        private System.Windows.Forms.Label lblFactorLat;
        private System.Windows.Forms.WebBrowser webBrowserPNG;
    }
}

