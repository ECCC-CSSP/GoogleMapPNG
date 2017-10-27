using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleMapPNG
{
    public partial class GoogleMapPNG : Form
    {
        public GoogleMapPNG()
        {
            InitializeComponent();
        }

        public void HTMLScreenShot()
        {
            WebBrowser wb = new WebBrowser();
            wb.ScrollBarsEnabled = false;
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.Size = new Size(int.Parse(textBoxWidth.Text) + 20, int.Parse(textBoxHeight.Text) + 20);

            string url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ textBoxMapType.Text }&center={ textBoxCenterLat.Text },{ textBoxCenterLng.Text }&zoom={ textBoxZoom.Text }&size={ textBoxWidth.Text }x{ textBoxHeight.Text }";
            wb.Navigate(url);

        }

        void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;

            using (Bitmap bitmap = new Bitmap(wb.Width, wb.Height))
            {
                Rectangle bounds = new Rectangle(new Point(0, 0), wb.Size);
                wb.DrawToBitmap(bitmap, bounds);
                bitmap.Save(@"C:\Users\Charles\Desktop\WebsiteScreenshot.png");
            }
        }

        private void butCreatePNG_Click(object sender, EventArgs e)
        {
            lblDone.Text = "Working...";
            HTMLScreenShot();
            lblDone.Text = "Done...";
        }
    }
}
