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
            this.butCreatePNG = new System.Windows.Forms.Button();
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
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblDone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butCreatePNG
            // 
            this.butCreatePNG.Location = new System.Drawing.Point(205, 39);
            this.butCreatePNG.Name = "butCreatePNG";
            this.butCreatePNG.Size = new System.Drawing.Size(94, 23);
            this.butCreatePNG.TabIndex = 3;
            this.butCreatePNG.Text = "Create PNG";
            this.butCreatePNG.UseVisualStyleBackColor = true;
            this.butCreatePNG.Click += new System.EventHandler(this.butCreatePNG_Click);
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
            this.textBoxZoom.Text = "14";
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
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(69, 165);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(88, 20);
            this.textBoxHeight.TabIndex = 16;
            this.textBoxHeight.Text = "1000";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(21, 172);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(41, 13);
            this.lblHeight.TabIndex = 15;
            this.lblHeight.Text = "Height:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(69, 139);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(88, 20);
            this.textBoxWidth.TabIndex = 18;
            this.textBoxWidth.Text = "1600";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(24, 143);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(38, 13);
            this.lblWidth.TabIndex = 17;
            this.lblWidth.Text = "Width:";
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(214, 69);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(0, 13);
            this.lblDone.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 723);
            this.Controls.Add(this.lblDone);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.lblHeight);
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
            this.Controls.Add(this.butCreatePNG);
            this.Name = "Form1";
            this.Text = "Google Map PNG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button butCreatePNG;
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
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblDone;
    }
}

