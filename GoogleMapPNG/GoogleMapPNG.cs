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
        private string DirName { get; set; } = @"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\";
        private string FileNameNW { get; set; } = @"NorthWest.png";
        private string FileNameNE { get; set; } = @"NorthEast.png";
        private string FileNameSW { get; set; } = @"SouthWest.png";
        private string FileNameSE { get; set; } = @"SouthEast.png";
        private string FileNameFull { get; set; } = @"Full.png";
        private int GoogleImageWidth { get; set; }
        private int GoogleImageHeight { get; set; }
        private int GoogleLogoHeight { get; set; }
        private string LanguageRequest { get; set; }
        private string MapType { get; set; }
        private double CenterLat { get; set; }
        private double CenterLng { get; set; }
        private int Zoom { get; set; }
        private double deltaLng { get; set; }
        private double deltaLat { get; set; }
        private double NewCenterLng { get; set; }
        private double NewCenterLat { get; set; }
        private MapCoordinates NewMapCoordinates { get; set; }
        #endregion Properties

        #region Constructors
        public GoogleMapPNG()
        {
            InitializeComponent();
        }
        #endregion Constructors

        #region Events
        private void butCreatePNG_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Working...";
            CreatePNG();
            lblStatus.Text = "Done...";
        }
        private void butSubsector_Click(object sender, EventArgs e)
        {
            Subsector(textBoxSubsector.Text);
        }
        private void butShowPoint_Click(object sender, EventArgs e)
        {
            double Lat = double.Parse(textBoxShowLat.Text);
            double Lng = double.Parse(textBoxShowLng.Text);
            ShowPoint(Lat, Lng);
        }

        #endregion Events

        #region Functions public

        public void CreatePNG()
        {

            MapType = textBoxMapType.Text; // Can be roadmap (default), satellite, hybrid, terrain
            CenterLat = double.Parse(textBoxCenterLat.Text);
            CenterLng = double.Parse(textBoxCenterLng.Text);
            Zoom = int.Parse(textBoxZoom.Text);
            GoogleImageWidth = int.Parse(textBoxXSize.Text);
            GoogleImageHeight = int.Parse(textBoxYSize.Text);
            LanguageRequest = textBoxLanguage.Text;
            GoogleLogoHeight = 24;

            Coordinate coordinate = new Coordinate(CenterLng, CenterLat);
            MapCoordinates mapCoordinatesTemp = GoogleMapsAPI.GetBounds(coordinate, Zoom, GoogleImageWidth, GoogleImageHeight);
            deltaLng = Math.Abs((CenterLng - mapCoordinatesTemp.SouthWest.Longitude) * 2);
            deltaLat = Math.Abs((CenterLat - mapCoordinatesTemp.SouthWest.Latitude) * 2) - (Math.Abs((CenterLat - mapCoordinatesTemp.SouthWest.Latitude) * 2) * GoogleLogoHeight / GoogleImageHeight);

            NewMapCoordinates = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng, CenterLat), Zoom, GoogleImageWidth, GoogleImageHeight);
            MapCoordinates NewMapCoordinatesTemp = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng + deltaLng, CenterLat), Zoom, GoogleImageWidth, GoogleImageHeight);

            NewMapCoordinates.NorthEast.Longitude = NewMapCoordinatesTemp.NorthEast.Longitude;

            NewMapCoordinatesTemp = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng, CenterLat - deltaLat), Zoom, GoogleImageWidth, GoogleImageHeight);

            NewMapCoordinates.SouthWest.Latitude = NewMapCoordinatesTemp.SouthWest.Latitude;

            if (!GetGoogleImages())
            {
                return;
            }

            if (!CombineAllImageIntoOne())
            {
                return;
            }

            if (!DeleteTempGoogleImageFiles())
            {
                return;
            }

            if (!DrawObjects())
            {
                return;
            }

            webBrowserPNG.Navigate(@"about:empty");
            webBrowserPNG.Navigate(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");
        }
        #endregion Functions public

        #region Functions private
        private bool CombineAllImageIntoOne()
        {
            Rectangle cropRect = new Rectangle(0, 0, GoogleImageWidth, GoogleImageHeight - GoogleLogoHeight);

            using (Bitmap targetNW = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(targetNW))
                {
                    using (Bitmap srcNW = new Bitmap(DirName + FileNameNW))
                    {
                        g.DrawImage(srcNW, new Rectangle(0, 0, targetNW.Width, targetNW.Height), cropRect, GraphicsUnit.Pixel);
                    }
                }

                using (Bitmap targetNE = new Bitmap(cropRect.Width, cropRect.Height))
                {
                    using (Graphics g = Graphics.FromImage(targetNE))
                    {
                        using (Bitmap srcNE = new Bitmap(DirName + FileNameNE))
                        {
                            g.DrawImage(srcNE, new Rectangle(0, 0, targetNE.Width, targetNE.Height), cropRect, GraphicsUnit.Pixel);
                        }
                    }

                    using (Bitmap targetAll = new Bitmap(cropRect.Width * 2, cropRect.Height + GoogleImageHeight))
                    {
                        using (Graphics g = Graphics.FromImage(targetAll))
                        {
                            g.DrawImage(targetNW, new Point(0, 0));
                            g.DrawImage(targetNE, new Point(GoogleImageWidth, 0));
                            using (Bitmap srcSW = new Bitmap(DirName + FileNameSW))
                            {
                                g.DrawImage(srcSW, new Point(0, GoogleImageHeight - GoogleLogoHeight));
                            }
                            using (Bitmap srcSE = new Bitmap(DirName + FileNameSE))
                            {
                                g.DrawImage(srcSE, new Point(GoogleImageWidth, GoogleImageHeight - GoogleLogoHeight));
                            }

                            targetAll.Save(DirName + FileNameFull, ImageFormat.Png);
                        }
                    }
                }
            }
            return true;
        }
        private bool DeleteTempGoogleImageFiles()
        {
            try
            {
                FileInfo fi = new FileInfo(DirName + FileNameNW);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                fi = new FileInfo(DirName + FileNameNE);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                fi = new FileInfo(DirName + FileNameSW);
                if (fi.Exists)
                {
                    fi.Delete();
                }
                fi = new FileInfo(DirName + FileNameSE);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
            catch (Exception ex)
            {
                richTextBoxStatus.Text = ex.Message; // + ex.InnerException != null ? " Inner: " + ex.InnerException.Message : "";
            }

            return true;
        }
        private bool DrawObjects()
        {
            using (Bitmap targetAll = new Bitmap(DirName + FileNameFull))
            {
                using (Graphics g = Graphics.FromImage(targetAll))
                {
                    double TotalWidthLng = NewMapCoordinates.NorthEast.Longitude - NewMapCoordinates.SouthWest.Longitude;
                    double TotalHeightLat = NewMapCoordinates.NorthEast.Latitude - NewMapCoordinates.SouthWest.Latitude;
                    double centerX = (NewMapCoordinates.NorthEast.Longitude - NewCenterLng) / TotalWidthLng * GoogleImageWidth * 2.0D;
                    double centerY = (NewMapCoordinates.NorthEast.Latitude - NewCenterLat) / TotalHeightLat * (GoogleImageHeight * 2 - GoogleLogoHeight);
                    g.DrawEllipse(new Pen(Color.Red, 4.0f), (int)centerX - 125, (int)centerY - 125, 250, 250);
                    g.DrawEllipse(new Pen(Color.Green, 5.0f), GoogleImageWidth - 100, (int)((GoogleImageHeight * 2 - GoogleLogoHeight) / 2.0D) - 100, 200, 200);
                    g.DrawLine(new Pen(Color.Orange, 2.0f), 0, 0, GoogleImageWidth * 2, GoogleImageHeight * 2 - GoogleLogoHeight);
                    g.DrawLine(new Pen(Color.Blue, 2.0f), GoogleImageWidth * 2, 0, 0, GoogleImageHeight * 2 - GoogleLogoHeight);
                }

                targetAll.Save(DirName + FileNameFull.Replace(".png", "Annotated.png"), ImageFormat.Png);
            }

            return true;
        }
        private bool GetGoogleImages()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ CenterLat.ToString("F6") },{ CenterLng.ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest }";
                    lblStatus.Text = $"Getting { url }";
                    client.DownloadFile(url, DirName + FileNameNW);

                    url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ CenterLat.ToString("F6") },{ (CenterLng + deltaLng).ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest }";
                    lblStatus.Text = $"Getting { url }";
                    client.DownloadFile(url, DirName + FileNameNE);

                    url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ (CenterLat - deltaLat).ToString("F6") },{ CenterLng.ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest }";
                    lblStatus.Text = $"Getting { url }";
                    client.DownloadFile(url, DirName + FileNameSW);

                    url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ (CenterLat - deltaLat).ToString("F6") },{ (CenterLng + deltaLng).ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest }";
                    lblStatus.Text = $"Getting { url }";
                    client.DownloadFile(url, DirName + FileNameSE);
                }
                catch (Exception ex)
                {
                    richTextBoxStatus.Text = ex.Message; // + ex.InnerException != null ? " Inner: " + ex.InnerException.Message : "";
                }
            }

            return true;
        }
        private void ShowPoint(double Lat, double Lng)
        {
            CenterLat = double.Parse(textBoxCenterLat.Text);
            CenterLng = double.Parse(textBoxCenterLng.Text);
            Zoom = int.Parse(textBoxZoom.Text);
            GoogleImageWidth = int.Parse(textBoxXSize.Text);
            GoogleImageHeight = int.Parse(textBoxYSize.Text);
            LanguageRequest = textBoxLanguage.Text;
            GoogleLogoHeight = 24;

            Coordinate coordinate = new Coordinate(CenterLng, CenterLat);
            MapCoordinates mapCoordinatesTemp = GoogleMapsAPI.GetBounds(coordinate, Zoom, GoogleImageWidth, GoogleImageHeight);
            deltaLng = Math.Abs((CenterLng - mapCoordinatesTemp.SouthWest.Longitude) * 2);
            deltaLat = Math.Abs((CenterLat - mapCoordinatesTemp.SouthWest.Latitude) * 2) - (Math.Abs((CenterLat - mapCoordinatesTemp.SouthWest.Latitude) * 2) * GoogleLogoHeight / GoogleImageHeight);

            NewMapCoordinates = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng, CenterLat), Zoom, GoogleImageWidth, GoogleImageHeight);
            MapCoordinates NewMapCoordinatesTemp = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng + deltaLng, CenterLat), Zoom, GoogleImageWidth, GoogleImageHeight);

            NewMapCoordinates.NorthEast.Longitude = NewMapCoordinatesTemp.NorthEast.Longitude;
            NewMapCoordinates.NorthEast.Latitude = NewMapCoordinatesTemp.NorthEast.Latitude;

            NewMapCoordinatesTemp = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng, CenterLat - deltaLat), Zoom, GoogleImageWidth, GoogleImageHeight);

            NewMapCoordinates.SouthWest.Longitude = NewMapCoordinatesTemp.SouthWest.Longitude;
            NewMapCoordinates.SouthWest.Latitude = NewMapCoordinatesTemp.SouthWest.Latitude;

            richTextBoxStatus.Text = "SoutWest: " + NewMapCoordinates.SouthWest.ToString() + " NorthWest: " + NewMapCoordinates.NorthEast.ToString();

            using (Bitmap targetAll = new Bitmap(DirName + FileNameFull))
            {
                using (Graphics g = Graphics.FromImage(targetAll))
                {
                    double TotalWidthLng = NewMapCoordinates.NorthEast.Longitude - NewMapCoordinates.SouthWest.Longitude;
                    double TotalHeightLat = NewMapCoordinates.NorthEast.Latitude - NewMapCoordinates.SouthWest.Latitude;
                    double LngX = ((Lng - NewMapCoordinates.SouthWest.Longitude) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                    double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (NewMapCoordinates.NorthEast.Latitude - Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);
                    g.DrawEllipse(new Pen(Color.Blue, 3.0f), (int)LngX - 20, (int)LatY - 20, 40, 40);
                    List<Point> polygonPoint = new List<Point>();
                    polygonPoint.Add(new Point() { X = (int)LngX - 50, Y = (int)LatY - 50 });
                    polygonPoint.Add(new Point() { X = (int)LngX + 50, Y = (int)LatY - 50 });
                    polygonPoint.Add(new Point() { X = (int)LngX + 50, Y = (int)LatY + 50 });
                    polygonPoint.Add(new Point() { X = (int)LngX - 50, Y = (int)LatY + 50 });
                    polygonPoint.Add(new Point() { X = (int)LngX - 50, Y = (int)LatY - 50 });

                    g.DrawPolygon(new Pen(Color.Orange, 2.0f), polygonPoint.ToArray());
                }

                targetAll.Save(DirName + FileNameFull.Replace(".png", "Annotated.png"), ImageFormat.Png);
            }

            webBrowserPNG.Navigate(@"about:empty");
            webBrowserPNG.Navigate(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");
        }
        private void Subsector(string subsector)
        {
            using (CSSPWebToolsDBEntities db = new CSSPWebToolsDBEntities())
            {
             
                var objSubsector = (from c in db.TVItems
                                    from cl in db.TVItemLanguages
                                    where c.TVItemID == cl.TVItemID
                                    && cl.Language == (int)LanguageEnum.en
                                    && cl.TVText.StartsWith(subsector)
                                    && c.TVType == (int)TVTypeEnum.Subsector
                                    select new { c, cl }).FirstOrDefault();

                if (objSubsector == null)
                {
                    richTextBoxStatus.Text = "Subsector not found: " + textBoxSubsector.Text;
                    return;
                }

                TVItem tvItemSubsector = objSubsector.c;
                TVItemLanguage tvItemLanguageSubsector = objSubsector.cl;

                List<MapInfoPoint> mapInfoPointPolygon = (from mi in db.MapInfos
                                                          from mip in db.MapInfoPoints
                                                          where mi.MapInfoID == mip.MapInfoID
                                                          && mi.TVItemID == tvItemSubsector.TVItemID
                                                          && mi.MapInfoDrawType == (int)MapInfoDrawTypeEnum.Polygon
                                                          select mip).ToList();

                if (mapInfoPointPolygon.Count == 0)
                {
                    richTextBoxStatus.Text = "Could not find polygon for subsector: " + textBoxSubsector.Text;
                    return;
                }


                List<MapInfoPoint> mapInfoPointMWQMSiteList = (from mi in db.MapInfos
                                                               from mip in db.MapInfoPoints
                                                               from t in db.TVItems
                                                               where mi.TVItemID ==  t.TVItemID
                                                               && mi.MapInfoID == mip.MapInfoID
                                                               && mi.MapInfoDrawType == (int)MapInfoDrawTypeEnum.Point
                                                               && t.ParentID == tvItemSubsector.TVItemID
                                                               && t.TVType == (int)TVTypeEnum.MWQMSite
                                                               && mi.TVType == (int)TVTypeEnum.MWQMSite
                                                               && t.IsActive == true
                                                               select mip).ToList();

                double MaxLat = mapInfoPointPolygon.Select(c => c.Lat).Max();
                double MinLat = mapInfoPointPolygon.Select(c => c.Lat).Min();
                double MaxLng = mapInfoPointPolygon.Select(c => c.Lng).Max();
                double MinLng = mapInfoPointPolygon.Select(c => c.Lng).Min();

                if (mapInfoPointMWQMSiteList.Count > 0)
                {
                    MaxLat = mapInfoPointMWQMSiteList.Select(c => c.Lat).Max();
                    MinLat = mapInfoPointMWQMSiteList.Select(c => c.Lat).Min();
                    MaxLng = mapInfoPointMWQMSiteList.Select(c => c.Lng).Max();
                    MinLng = mapInfoPointMWQMSiteList.Select(c => c.Lng).Min();
                }

                List<MapInfoPoint> mapInfoPointPolSourceSiteList = (from mi in db.MapInfos
                                                               from mip in db.MapInfoPoints
                                                               from t in db.TVItems
                                                               where mi.TVItemID == t.TVItemID
                                                               && mi.MapInfoID == mip.MapInfoID
                                                               && mi.MapInfoDrawType == (int)MapInfoDrawTypeEnum.Point
                                                               && t.ParentID == tvItemSubsector.TVItemID
                                                               && t.TVType == (int)TVTypeEnum.PolSourceSite
                                                               && mi.TVType == (int)TVTypeEnum.PolSourceSite
                                                               && t.IsActive == true
                                                               select mip).ToList();

                if (mapInfoPointMWQMSiteList.Count > 0)
                {
                    MaxLat = mapInfoPointMWQMSiteList.Concat(mapInfoPointPolSourceSiteList).Select(c => c.Lat).Max();
                    MinLat = mapInfoPointMWQMSiteList.Concat(mapInfoPointPolSourceSiteList).Select(c => c.Lat).Min();
                    MaxLng = mapInfoPointMWQMSiteList.Concat(mapInfoPointPolSourceSiteList).Select(c => c.Lng).Max();
                    MinLng = mapInfoPointMWQMSiteList.Concat(mapInfoPointPolSourceSiteList).Select(c => c.Lng).Min();
                }

                double ExtraLat = (MaxLat - MinLat) * 0.05D;
                double ExtraLng = (MaxLng - MinLng) * 0.05D;

                Coord coordNE = new Coord() { Lat = (float)(MaxLat + ExtraLat), Lng = (float)(MaxLng + ExtraLng), Ordinal = 0 };
                Coord coordSW = new Coord() { Lat = (float)(MinLat - ExtraLat), Lng = (float)(MinLng - ExtraLng), Ordinal = 0 };

                MapType = textBoxMapType.Text; // Can be roadmap (default), satellite, hybrid, terrain
                Zoom = int.Parse(textBoxZoom.Text);
                GoogleImageWidth = int.Parse(textBoxXSize.Text);
                GoogleImageHeight = int.Parse(textBoxYSize.Text);
                LanguageRequest = textBoxLanguage.Text;
                GoogleLogoHeight = 24;
                CenterLat = coordNE.Lat - (coordNE.Lat - coordSW.Lat) / 4;
                CenterLng = coordSW.Lng + (coordNE.Lng - coordSW.Lng) / 4;

                bool Found = false;
                bool ZoomWasReduced = false;
                while (!Found)
                {
                    // calculate the center of the uper left block
                    Coordinate coordinate = new Coordinate(CenterLng, CenterLat);
                    MapCoordinates mapCoordinatesTemp = GoogleMapsAPI.GetBounds(coordinate, Zoom, GoogleImageWidth, GoogleImageHeight);
                    deltaLng = Math.Abs((CenterLng - mapCoordinatesTemp.SouthWest.Longitude) * 2);
                    deltaLat = Math.Abs((CenterLat - mapCoordinatesTemp.SouthWest.Latitude) * 2) - (Math.Abs((CenterLat - mapCoordinatesTemp.SouthWest.Latitude) * 2) * GoogleLogoHeight / GoogleImageHeight);

                    NewMapCoordinates = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng, CenterLat), Zoom, GoogleImageWidth, GoogleImageHeight);
                    MapCoordinates NewMapCoordinatesTemp = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng + deltaLng, CenterLat), Zoom, GoogleImageWidth, GoogleImageHeight);

                    NewMapCoordinates.NorthEast.Longitude = NewMapCoordinatesTemp.NorthEast.Longitude;
                    NewMapCoordinates.NorthEast.Latitude = NewMapCoordinatesTemp.NorthEast.Latitude;

                    NewMapCoordinatesTemp = GoogleMapsAPI.GetBounds(new Coordinate(CenterLng, CenterLat - deltaLat), Zoom, GoogleImageWidth, GoogleImageHeight);

                    NewMapCoordinates.SouthWest.Longitude = NewMapCoordinatesTemp.SouthWest.Longitude;
                    NewMapCoordinates.SouthWest.Latitude = NewMapCoordinatesTemp.SouthWest.Latitude;

                    Found = true;
                    if (NewMapCoordinates.NorthEast.Longitude < (double)coordNE.Lng)
                    {
                        Zoom -= 1;
                        ZoomWasReduced = true;
                        Found = false;
                    }
                    if (NewMapCoordinates.NorthEast.Latitude < (double)coordNE.Lat)
                    {
                        Zoom -= 1;
                        ZoomWasReduced = true;
                        Found = false;
                    }
                    if (NewMapCoordinates.SouthWest.Longitude > (double)coordSW.Lng)
                    {
                        Zoom -= 1;
                        ZoomWasReduced = true;
                        Found = false;
                    }
                    if (NewMapCoordinates.SouthWest.Latitude > (double)coordSW.Lat)
                    {
                        Zoom -= 1;
                        ZoomWasReduced = true;
                        Found = false;
                    }
                    if (Found && !ZoomWasReduced)
                    {
                        if ((NewMapCoordinates.NorthEast.Longitude - NewMapCoordinates.SouthWest.Longitude) > (coordNE.Lng - coordSW.Lng) * 2)
                        {
                            Zoom += 1;
                            Found = false;
                        }
                        if ((NewMapCoordinates.NorthEast.Latitude - NewMapCoordinates.SouthWest.Latitude) > (coordNE.Lat - coordSW.Lat) * 2)
                        {
                            Zoom += 1;
                            Found = false;
                        }
                    }
                    if (Found)
                    {
                        break;
                    }
                }

                if (!GetGoogleImages())
                {
                    return;
                }

                if (!CombineAllImageIntoOne())
                {
                    return;
                }

                if (!DeleteTempGoogleImageFiles())
                {
                    return;
                }

                using (Bitmap targetAll = new Bitmap(DirName + FileNameFull))
                {
                    List<Point> polygonPointList = new List<Point>();

                    using (Graphics g = Graphics.FromImage(targetAll))
                    {
                        double TotalWidthLng = NewMapCoordinates.NorthEast.Longitude - NewMapCoordinates.SouthWest.Longitude;
                        double TotalHeightLat = NewMapCoordinates.NorthEast.Latitude - NewMapCoordinates.SouthWest.Latitude;

                        foreach (MapInfoPoint mapInfoPoint in mapInfoPointPolygon)
                        {
                            double LngX = ((mapInfoPoint.Lng - NewMapCoordinates.SouthWest.Longitude) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                            double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (NewMapCoordinates.NorthEast.Latitude - mapInfoPoint.Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);
                            polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });
                        }

                        g.DrawPolygon(new Pen(Color.Orange, 2.0f), polygonPointList.ToArray());

                        foreach (MapInfoPoint mapInfoPoint in mapInfoPointMWQMSiteList)
                        {
                            double LngX = ((mapInfoPoint.Lng - NewMapCoordinates.SouthWest.Longitude) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                            double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (NewMapCoordinates.NorthEast.Latitude - mapInfoPoint.Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);

                            g.DrawEllipse(new Pen(Color.LightGreen, 1.0f), (int)LngX - 5, (int)LatY - 5, 10, 10);
                        }

                        foreach (MapInfoPoint mapInfoPoint in mapInfoPointPolSourceSiteList)
                        {
                            double LngX = ((mapInfoPoint.Lng - NewMapCoordinates.SouthWest.Longitude) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                            double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (NewMapCoordinates.NorthEast.Latitude - mapInfoPoint.Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);

                            g.DrawRectangle(new Pen(Color.LightPink, 1.0f), (int)LngX - 5, (int)LatY - 5, 10, 10);
                        }
                    }

                    targetAll.Save(DirName + FileNameFull.Replace(".png", "Annotated.png"), ImageFormat.Png);
                }

                webBrowserPNG.Navigate(@"about:empty");
                webBrowserPNG.Navigate(@"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\ShowGoogleMapPNG.html");
            }
        }
        #endregion Functions private

    }
}

