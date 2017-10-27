using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        public void HTMLScreenShot2()
        {
            string localFilename = @"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\WebsiteScreenshot.png";
            string url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ textBoxMapType.Text }&center={ textBoxCenterLat.Text },{ textBoxCenterLng.Text }&zoom={ textBoxZoom.Text }&size=640x640";
            using (WebClient client = new WebClient())
            {
                try
                {
                    //double factorLng = double.Parse(textBoxFactorLng.Text);
                    //double factorLat = double.Parse(textBoxFactorLat.Text);
                    //string markerCenter = $@"&markers=color:blue|label:|{ textBoxCenterLat.Text },{ textBoxCenterLng.Text }";
                    //double centerLat = double.Parse(textBoxCenterLat.Text);
                    //double centerLng = double.Parse(textBoxCenterLng.Text);
                    //string pathsSquareBorder = $@"&path=weight:1|color:green";
                    //pathsSquareBorder  = pathsSquareBorder + $"|{ (centerLat - factorLat).ToString("F6") },{ (centerLng - factorLng).ToString("F6") }";
                    //pathsSquareBorder = pathsSquareBorder + $"|{ (centerLat - factorLat).ToString("F6") },{ (centerLng + factorLng).ToString("F6") }";
                    //pathsSquareBorder = pathsSquareBorder + $"|{ (centerLat + factorLat).ToString("F6") },{ (centerLng + factorLng).ToString("F6") }";
                    //pathsSquareBorder = pathsSquareBorder + $"|{ (centerLat + factorLat).ToString("F6") },{ (centerLng - factorLng).ToString("F6") }";
                    //pathsSquareBorder = pathsSquareBorder + $"|{ (centerLat - factorLat).ToString("F6") },{ (centerLng - factorLng).ToString("F6") }";
                    //client.DownloadFile(url + markerCenter + pathsSquareBorder, localFilename);
                    client.DownloadFile(url, localFilename);

                    webBrowserPNG.Navigate(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");
                }
                catch (Exception ex)
                {
                    richTextBox1.Text = ex.Message; // + ex.InnerException != null ? " Inner: " + ex.InnerException.Message : "";
                }
            }
        }

        private void butCreatePNG2_Click(object sender, EventArgs e)
        {
            lblDone.Text = "Working...";
            HTMLScreenShot2();
            lblDone.Text = "Done...";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Coordinate coordinate = new Coordinate(double.Parse(textBoxCenterLng.Text), double.Parse(textBoxCenterLat.Text));
            MapCoordinates result = GoogleMapsAPI.GetBounds(coordinate, int.Parse(textBoxZoom.Text), 640, 640);
            richTextBox1.AppendText($"{textBoxCenterLat.Text},{textBoxCenterLng.Text}" + "\r\n");
            richTextBox1.AppendText($"SouthWest: {result.SouthWest.ToString()}" + "\r\n");
            richTextBox1.AppendText($"NorthEast: {result.NorthEast.ToString()}" + "\r\n");
            richTextBox1.AppendText($"Lng2: {((double.Parse(textBoxCenterLng.Text)) + ((double.Parse(textBoxCenterLng.Text) - result.SouthWest.Longitude)*2)).ToString()}" + "\r\n");

            //textBoxCenterLng.Text = result.NorthEast.Longitude.ToString("F6");
            //textBoxCenterLat.Text = result.NorthEast.Latitude.ToString("F6");
        }
    }
}
