using CSSPEnumsDLL.Enums;
using CSSPModelsDLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleMapPNG
{
    public partial class GoogleMapPNG : Form
    {
        #region Variables
        #endregion Variables

        #region Properties
        #endregion Properties

        #region Constructors
        public GoogleMapPNG()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Events
        private void butCreateMWQMSitePNG_Click(object sender, EventArgs e)
        {
            CreateSubsectorGoogleMapPNGForMWQMSites(int.Parse(textBoxSubsectorID.Text)); // 635 == Bouctouche river and harbour
        }
        private void butCreatePolSourceSitePNG_Click(object sender, EventArgs e)
        {
            CreateSubsectorGoogleMapPNGForPolSourceSites(int.Parse(textBoxSubsectorID.Text)); // 635 == Bouctouche river and harbour
        }
        #endregion Events

        #region Functions public

        private void CreateSubsectorGoogleMapPNGForMWQMSites(int SubsectorTVItemID)
        {
            GoogleMapToPNG googleMapToPNG = new GoogleMapToPNG();

            googleMapToPNG.CreateSubsectorGoogleMapPNGForMWQMSites(SubsectorTVItemID, textBoxMapType.Text);

            webBrowserPNG.Navigate(@"about:empty");

            CreateShowGoogleMapPNG("Full" + googleMapToPNG.FileNameExtra + "Annotated.png");

            webBrowserPNG.Navigate(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");
        }
        private void CreateSubsectorGoogleMapPNGForPolSourceSites(int SubsectorTVItemID)
        {
            GoogleMapToPNG googleMapToPNG = new GoogleMapToPNG();

            googleMapToPNG.CreateSubsectorGoogleMapPNGForPolSourceSites(SubsectorTVItemID, textBoxMapType.Text);

            webBrowserPNG.Navigate(@"about:empty");

            CreateShowGoogleMapPNG("Full" + googleMapToPNG.FileNameExtra + "Annotated.png");

            webBrowserPNG.Navigate(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");
        }
        #endregion Functions public

        #region Functions private
        public void CreateShowGoogleMapPNG(string FileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<!DOCTYPE html>");
            sb.AppendLine(@"");
            sb.AppendLine(@"<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">");
            sb.AppendLine(@"<head>");
            sb.AppendLine(@"    <meta charset=""utf-8"" />");
            sb.AppendLine(@"    <title></title>");
            sb.AppendLine(@"</head>");
            sb.AppendLine(@"<body>");
            sb.AppendLine(@"    <table cellpadding=""0"" cellspacing=""0"">");
            sb.AppendLine(@"        <tr>");
            sb.AppendLine($@"            <td><img src=""{ FileName }"" /></td>");
            sb.AppendLine(@"        </tr>");
            sb.AppendLine(@"    </table>");
            sb.AppendLine(@"</body>");
            sb.AppendLine(@"</html>");

            FileInfo fi = new FileInfo(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");

            StreamWriter sw = fi.CreateText();
            sw.Write(sb.ToString());
            sw.Close();

        }

        #endregion Functions private

    }
}

