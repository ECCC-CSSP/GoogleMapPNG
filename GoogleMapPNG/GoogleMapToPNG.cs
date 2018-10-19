using CSSPEnumsDLL.Enums;
using CSSPModelsDLL.Models;
using CSSPDBDLL.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapPNG
{
    public partial class GoogleMapToPNG
    {
        #region Variables
        #endregion Variables

        #region Properties
        private string DirName { get; set; }
        private string FileNameNW { get; set; }
        private string FileNameNE { get; set; }
        private string FileNameSW { get; set; }
        private string FileNameSE { get; set; }
        private string FileNameFull { get; set; }
        private string FileNameInset { get; set; }
        private string FileNameInsetFinal { get; set; }
        public string FileNameExtra { get; set; }
        private int GoogleImageWidth { get; set; }
        private int GoogleImageHeight { get; set; }
        private int GoogleLogoHeight { get; set; }
        private LanguageEnum LanguageRequest { get; set; }
        private string MapType { get; set; }
        private int Zoom { get; set; }
        private bool JumpGoogleLoading { get; set; } = false;
        private IPrincipal user { get; set; }
        #endregion Properties

        #region Constructors
        public GoogleMapToPNG()
        {
            LoadDefaults();
            user = new GenericPrincipal(new GenericIdentity("Charles.LeBlanc2@canada.ca", "Forms"), null);

        }

        #endregion Constructors

        #region Functions public
        public bool CreateSubsectorGoogleMapPNGForPolSourceSites(int SubsectorTVItemID, string MapType)
        {
            this.MapType = MapType;
            TVItemService tvItemService = new TVItemService(LanguageRequest, user);
            MapInfoPointService mapInfoPointService = new MapInfoPointService(LanguageRequest, user);

            TVItemModel tvItemModelSubsector = tvItemService.GetTVItemModelWithTVItemIDDB(SubsectorTVItemID);
            if (!string.IsNullOrWhiteSpace(tvItemModelSubsector.Error))
            {
                // need to set error
                return false;
            }

            List<MapInfoPointModel> mapInfoPointModelSubsectorList = mapInfoPointService.GetMapInfoPointModelListWithTVItemIDAndTVTypeAndMapInfoDrawTypeDB(SubsectorTVItemID, TVTypeEnum.Subsector, MapInfoDrawTypeEnum.Polygon);
            if (mapInfoPointModelSubsectorList.Count == 0)
            {
                // need to set the error
                return false;
            }

            double MaxLat = mapInfoPointModelSubsectorList.Select(c => c.Lat).Max();
            double MinLat = mapInfoPointModelSubsectorList.Select(c => c.Lat).Min();
            double MaxLng = mapInfoPointModelSubsectorList.Select(c => c.Lng).Max();
            double MinLng = mapInfoPointModelSubsectorList.Select(c => c.Lng).Min();


            List<MapInfoPointModel> mapInfoPointModelPolSourceSiteList = mapInfoPointService.GetMapInfoPointModelListWithParentIDAndTVTypeAndMapInfoDrawTypeDB(SubsectorTVItemID, TVTypeEnum.PolSourceSite, MapInfoDrawTypeEnum.Point);
            List<TVItemModel> tvItemModelPolSourceSiteList = tvItemService.GetChildrenTVItemModelListWithTVItemIDAndTVTypeDB(SubsectorTVItemID, TVTypeEnum.PolSourceSite).Where(c => c.IsActive == true).ToList();

            mapInfoPointModelPolSourceSiteList = (from c in mapInfoPointModelPolSourceSiteList
                                                  from t in tvItemModelPolSourceSiteList // all active sites
                                                  where c.TVItemID == t.TVItemID
                                                  select c).ToList();

            if (mapInfoPointModelPolSourceSiteList.Count > 0)
            {
                MaxLat = mapInfoPointModelPolSourceSiteList.Select(c => c.Lat).Max();
                MinLat = mapInfoPointModelPolSourceSiteList.Select(c => c.Lat).Min();
                MaxLng = mapInfoPointModelPolSourceSiteList.Select(c => c.Lng).Max();
                MinLng = mapInfoPointModelPolSourceSiteList.Select(c => c.Lng).Min();
            }

            CoordMap coordMap = GetMapCoordinateWhileGettingGooglePNG(MinLat, MaxLat, MinLng, MaxLng);

            if (!DrawSubsectorPolSourceSites(coordMap, tvItemModelSubsector, mapInfoPointModelSubsectorList, mapInfoPointModelPolSourceSiteList, tvItemModelPolSourceSiteList))
            {
                return false;
            }

            return true;
        }
        public bool CreateSubsectorGoogleMapPNGForMWQMSites(int SubsectorTVItemID, string MapType)
        {
            this.MapType = MapType;
            IPrincipal user = new GenericPrincipal(new GenericIdentity("Charles.LeBlanc2@canada.ca", "Forms"), null);

            TVItemService tvItemService = new TVItemService(LanguageRequest, user);
            MapInfoService mapInfoService = new MapInfoService(LanguageRequest, user);
            MapInfoPointService mapInfoPointService = new MapInfoPointService(LanguageRequest, user);

            TVItemModel tvItemModelSubsector = tvItemService.GetTVItemModelWithTVItemIDDB(SubsectorTVItemID);
            if (!string.IsNullOrWhiteSpace(tvItemModelSubsector.Error))
            {
                // need to set error
                return false;
            }

            List<MapInfoPointModel> mapInfoPointModelSubsectorList = mapInfoPointService.GetMapInfoPointModelListWithTVItemIDAndTVTypeAndMapInfoDrawTypeDB(SubsectorTVItemID, TVTypeEnum.Subsector, MapInfoDrawTypeEnum.Polygon);
            if (mapInfoPointModelSubsectorList.Count == 0)
            {
                // need to set the error
                return false;
            }

            double MaxLat = mapInfoPointModelSubsectorList.Select(c => c.Lat).Max();
            double MinLat = mapInfoPointModelSubsectorList.Select(c => c.Lat).Min();
            double MaxLng = mapInfoPointModelSubsectorList.Select(c => c.Lng).Max();
            double MinLng = mapInfoPointModelSubsectorList.Select(c => c.Lng).Min();

            List<MapInfoPointModel> mapInfoPointModelMWQMSiteList = mapInfoPointService.GetMapInfoPointModelListWithParentIDAndTVTypeAndMapInfoDrawTypeDB(SubsectorTVItemID, TVTypeEnum.MWQMSite, MapInfoDrawTypeEnum.Point);
            List<TVItemModel> tvItemModelMWQMSiteList = tvItemService.GetChildrenTVItemModelListWithTVItemIDAndTVTypeDB(SubsectorTVItemID, TVTypeEnum.MWQMSite).Where(c => c.IsActive == true).ToList();

            mapInfoPointModelMWQMSiteList = (from c in mapInfoPointModelMWQMSiteList
                                             from t in tvItemModelMWQMSiteList // all active sites
                                             where c.TVItemID == t.TVItemID
                                             select c).ToList();


            if (mapInfoPointModelMWQMSiteList.Count > 0)
            {
                MaxLat = mapInfoPointModelMWQMSiteList.Select(c => c.Lat).Max();
                MinLat = mapInfoPointModelMWQMSiteList.Select(c => c.Lat).Min();
                MaxLng = mapInfoPointModelMWQMSiteList.Select(c => c.Lng).Max();
                MinLng = mapInfoPointModelMWQMSiteList.Select(c => c.Lng).Min();
            }

            CoordMap coordMap = GetMapCoordinateWhileGettingGooglePNG(MinLat, MaxLat, MinLng, MaxLng);

            if (!DrawSubsectorMWQMSites(coordMap, tvItemModelSubsector, mapInfoPointModelSubsectorList, mapInfoPointModelMWQMSiteList, tvItemModelMWQMSiteList))
            {
                return false;
            }

            return true;
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
                //richTextBoxStatus.Text = ex.Message; // + ex.InnerException != null ? " Inner: " + ex.InnerException.Message : "";
            }

            return true;
        }
        private bool DrawHorizontalScale(Graphics g, CoordMap coordMap)
        {
            IPrincipal user = new GenericPrincipal(new GenericIdentity("Charles.LeBlanc2@canada.ca", "Forms"), null);
            MapInfoService mapInfoService = new MapInfoService(LanguageRequest, user);

            int MinLngX = (int)(GoogleImageWidth * 2 * 3 / 5);
            int MaxLngX = (int)(GoogleImageWidth * 2 * 4 / 5);

            g.DrawLine(new Pen(Color.LightBlue, 2), MinLngX, GoogleImageHeight * 2 - GoogleLogoHeight - 40, MaxLngX, GoogleImageHeight * 2 - GoogleLogoHeight - 40);

            double MinLng = coordMap.NorthEast.Lng - (coordMap.NorthEast.Lng - coordMap.SouthWest.Lng) / 5;
            double distLng = mapInfoService.CalculateDistance(coordMap.NorthEast.Lat * mapInfoService.d2r, coordMap.NorthEast.Lng * mapInfoService.d2r, coordMap.NorthEast.Lat * mapInfoService.d2r, MinLng * mapInfoService.d2r, mapInfoService.R) / 1000;


            string distText = distLng.ToString("F2") + " km";
            Font font = new Font("Arial", 10, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.LightBlue);

            SizeF sizeF = g.MeasureString(distText, font);

            g.DrawString(distText, font, brush, GoogleImageWidth * 2 * 4 / 5, GoogleImageHeight * 2 - GoogleLogoHeight - 60);

            font = new Font("Arial", 10, FontStyle.Regular);
            brush = new SolidBrush(Color.LightBlue);

            for (int i = 0; i < 10; i++)
            {
                if ((double)i > distLng)
                {
                    g.DrawLine(new Pen(Color.LightBlue, 1), MaxLngX, GoogleImageHeight * 2 - GoogleLogoHeight - 40 - 1, MaxLngX, GoogleImageHeight * 2 - GoogleLogoHeight - 40 + 10 - 1);
                    break;
                }
                g.DrawLine(new Pen(Color.LightBlue, 1), MinLngX + (int)(i / distLng * (MaxLngX - MinLngX)), GoogleImageHeight * 2 - GoogleLogoHeight - 40 - 1, MinLngX + (int)(i / distLng * (MaxLngX - MinLngX)), GoogleImageHeight * 2 - GoogleLogoHeight - 40 + 10 - 1);

                distText = i.ToString();

                sizeF = g.MeasureString(distText, font);

                g.DrawString(distText, font, brush, MinLngX + (int)(i / distLng * (MaxLngX - MinLngX)) - (sizeF.Width / 2), GoogleImageHeight * 2 - GoogleLogoHeight - 60);

            }


            return true;
        }
        private bool DrawImageBorder(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Black, 6.0f), 0, 0, GoogleImageWidth * 2, GoogleImageHeight * 2 - GoogleLogoHeight);

            return true;
        }
        private bool DrawLegendMWQMSites(Graphics g, CoordMap coordMap, List<MapInfoPointModel> mapInfoPointModelMWQMSiteList, List<TVItemModel> tvItemModelMWQMSiteList)
        {
            int StartingHeight = 100;
            int CurrentHeight = 0;
            int CenterOfLegend = 0;
            Brush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.LightGreen, 6.0f);
            Font font = new Font("Arial", 16, FontStyle.Bold);

            int LegendHeight = 0;
            int LegendWidth = 200;
            // Calculating Total legend height
            string LegendText = "Legend";
            SizeF sizeF = g.MeasureString(LegendText, font);
            LegendHeight = LegendHeight + (int)(sizeF.Height) + 10;

            font = new Font("Arial", 12, FontStyle.Bold);
            string ApprovedText = "Approved"; // Approved, Restricted, Conditional Approved, Conditionaly Restricted, Prohibited, Unclassified
            sizeF = g.MeasureString(ApprovedText, font);
            LegendHeight = LegendHeight + ((int)(sizeF.Height) + 5) * 12; // Passing, Fail, No Depuration, Colors (3)

            g.DrawRectangle(pen, GoogleImageWidth * 2 - LegendWidth, StartingHeight, LegendWidth - 5, LegendHeight);
            g.FillRectangle(brush, GoogleImageWidth * 2 - LegendWidth, StartingHeight, LegendWidth - 5, LegendHeight);

            CenterOfLegend = (GoogleImageWidth * 2) - LegendWidth / 2;
            brush = new SolidBrush(Color.LightBlue);

            // draw Legend title
            font = new Font("Arial", 16, FontStyle.Bold);
            sizeF = g.MeasureString(LegendText, font);
            CurrentHeight = StartingHeight + 10;
            g.DrawString(LegendText, font, brush, CenterOfLegend - (sizeF.Width / 2), CurrentHeight);

            CurrentHeight += 20;
            font = new Font("Arial", 8, FontStyle.Bold);
            brush = new SolidBrush(Color.Blue);
            List<string> ClassList = new List<string>()
            {
                "Approved", "Restricted", "Conditional Approved", "Conditionaly Restricted", "Prohibited", "Unclassified"
            };
            List<string> ClassInitList = new List<string>()
            {
                "A", "R", "CA", "CR", "P", "U"
            };

            sizeF = g.MeasureString(ClassInitList[2], font);
            int RectWidth = 24;
            int RectHeight = (int)(sizeF.Height) + 4;

            for (int i = 0, count = ClassList.Count; i < count; i++)
            {
                brush = new SolidBrush(Color.Black);
                sizeF = g.MeasureString(ClassList[i], font);
                CurrentHeight += (int)(sizeF.Height) + 10;
                g.DrawString(ClassList[i], font, brush, CenterOfLegend - LegendWidth / 2 + 50, CurrentHeight);

                switch (i)
                {
                    case 0: // Approved
                        {
                            brush = new SolidBrush(Color.Green);
                        }
                        break;
                    case 1: // Restricted
                        {
                            brush = new SolidBrush(Color.Red);
                        }
                        break;
                    case 2: // Conditional Approved
                        {
                            brush = new SolidBrush(Color.LightGreen);
                        }
                        break;
                    case 3: // Conditionaly Restricted
                        {
                            brush = new SolidBrush(Color.LightPink);
                        }
                        break;
                    case 4: // Prohibited
                        {
                            brush = new SolidBrush(Color.Black);
                        }
                        break;
                    case 5: // Unclassified
                        {
                            brush = new SolidBrush(Color.White);
                        }
                        break;
                    default:
                        break;
                }
                g.FillRectangle(brush, CenterOfLegend - LegendWidth / 2 + 20 - 2, CurrentHeight - 2, RectWidth, RectHeight);
                g.DrawRectangle(new Pen(Color.Black, 2.0f), CenterOfLegend - LegendWidth / 2 + 20 - 2, CurrentHeight - 2, RectWidth, RectHeight);

                if (i == 4)
                {
                    brush = new SolidBrush(Color.White);
                }
                else if (i == 5)
                {
                    brush = new SolidBrush(Color.Black);
                }
                else
                {
                    brush = new SolidBrush(Color.Black);
                }
                g.DrawString(ClassInitList[i], font, brush, CenterOfLegend - LegendWidth / 2 + 20, CurrentHeight);


            }

            CurrentHeight += 20;

            List<string> ColorList = new List<string>()
            {
                "Passed", "Failed", "No depuration"
            };

            List<string> FCCalcList = new List<string>() { "A", "B", "C", "D", "E", "F" };

            for (int i = 0, count = ColorList.Count; i < count; i++)
            {
                brush = new SolidBrush(Color.Black);
                sizeF = g.MeasureString(ColorList[i], font);
                CurrentHeight += (int)(sizeF.Height) + 10;
                g.DrawString(ColorList[i], font, brush, CenterOfLegend - LegendWidth / 2 + 50, CurrentHeight);

                g.DrawRectangle(new Pen(Color.Black, 2.0f), CenterOfLegend - LegendWidth / 2 + 20 - 2, CurrentHeight - 2, RectWidth, RectHeight);

                if (i == 0)
                {
                    brush = new SolidBrush(Color.FromArgb(0x44, 0xff, 0x44));
                    g.FillRectangle(brush, CenterOfLegend - LegendWidth / 2 + 20 - 2, CurrentHeight - 2, RectWidth, RectHeight);
                }
                else if (i == 1)
                {
                    brush = new SolidBrush(Color.FromArgb(0xff, 0x11, 0x11));
                    g.FillRectangle(brush, CenterOfLegend - LegendWidth / 2 + 20 - 2, CurrentHeight - 2, RectWidth, RectHeight);
                }
                else if (i == 2)
                {
                    brush = new SolidBrush(Color.FromArgb(0xaa, 0xaa, 0xff));
                    g.FillRectangle(brush, CenterOfLegend - LegendWidth / 2 + 20 - 2, CurrentHeight - 2, RectWidth, RectHeight);
                }
            }

            CurrentHeight += 40;

            string GoodLetterBad = "Good   A B C D E F   Bad";
            brush = new SolidBrush(Color.Black);
            sizeF = g.MeasureString(GoodLetterBad, font);
            g.DrawString(GoodLetterBad, font, brush, CenterOfLegend - (int)(sizeF.Width / 2), CurrentHeight);


            return true;
        }
        private bool DrawLegendPolSourceSites(Graphics g, CoordMap coordMap, List<MapInfoPointModel> mapInfoPointModelPolSourceSiteList, List<TVItemModel> tvItemModelPolSourceSiteList)
        {
            int StartingHeight = 100;
            int CurrentHeight = 0;
            int CenterOfLegend = 0;
            Brush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.LightGreen, 6.0f);
            Font font = new Font("Arial", 16, FontStyle.Bold);

            int LegendHeight = 0;
            int LegendWidth = 200;
            // Calculating Total legend height
            string LegendText = "Legend";
            SizeF sizeF = g.MeasureString(LegendText, font);
            LegendHeight = LegendHeight + (int)(sizeF.Height) + 10;

            font = new Font("Arial", 12, FontStyle.Bold);
            string ApprovedText = "Approved"; // Approved, Restricted, Conditional Approved, Conditionaly Restricted, Prohibited, Unclassified
            sizeF = g.MeasureString(ApprovedText, font);
            LegendHeight = LegendHeight + ((int)(sizeF.Height) + 5) * 12; // Passing, Fail, No Depuration, Colors (3)

            g.DrawRectangle(pen, GoogleImageWidth * 2 - LegendWidth, StartingHeight, LegendWidth - 5, LegendHeight);
            g.FillRectangle(brush, GoogleImageWidth * 2 - LegendWidth, StartingHeight, LegendWidth - 5, LegendHeight);

            CenterOfLegend = (GoogleImageWidth * 2) - LegendWidth / 2;
            brush = new SolidBrush(Color.LightBlue);

            // draw Legend title
            font = new Font("Arial", 16, FontStyle.Bold);
            sizeF = g.MeasureString(LegendText, font);
            CurrentHeight = StartingHeight + 10;
            g.DrawString(LegendText, font, brush, CenterOfLegend - (sizeF.Width / 2), CurrentHeight);

            CurrentHeight += 20;
            font = new Font("Arial", 8, FontStyle.Bold);
            brush = new SolidBrush(Color.Blue);
            List<string> MajorGroupList = new List<string>()
            {
                "Agriculture", "Forested", "Industry", "Marine", "Recreation", "Urban", "Other"
            };

            sizeF = g.MeasureString(MajorGroupList[0], font);
            int RectWidth = 24;
            int RectHeight = (int)(sizeF.Height) + 4;

            for (int i = 0, count = MajorGroupList.Count; i < count; i++)
            {
                brush = new SolidBrush(Color.Black);
                sizeF = g.MeasureString(MajorGroupList[i], font);
                CurrentHeight += (int)(sizeF.Height) + 10;
                g.DrawString(MajorGroupList[i], font, brush, CenterOfLegend - LegendWidth / 2 + 50, CurrentHeight);

                brush = new SolidBrush(Color.Green);
                int LatY = CurrentHeight + RectHeight / 2;
                int LngX = CenterOfLegend - LegendWidth / 2 + 20 + RectWidth / 2;
                int IconSize = 8;
                switch (i)
                {
                    case 0: // Agriculture
                        {
                            DrawAgricultureIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    case 1: // Forested
                        {
                            DrawForestedIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    case 2: // Industry
                        {
                            DrawIndustryIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    case 3: // Marine
                        {
                            DrawMarineIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    case 4: // Recreation
                        {
                            DrawRecreationIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    case 5: // Urban
                        {
                            DrawUrbanIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    case 6: // Other
                        {
                            DrawOtherIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, LatY, LngX);
                        }
                        break;
                    default:
                        break;
                }
            }

            CurrentHeight += 20;

            List<string> RiskList = new List<string>()
            {
                "High Risk", "Moderate Risk", "Low Risk"
            };

            for (int i = 0, count = RiskList.Count; i < count; i++)
            {
                sizeF = g.MeasureString(RiskList[i], font);
                CurrentHeight += (int)(sizeF.Height) + 10;

                if (i == 0)
                {
                    brush = new SolidBrush(Color.Red);
                }
                else if (i == 1)
                {
                    brush = new SolidBrush(Color.YellowGreen);
                }
                else if (i == 2)
                {
                    brush = new SolidBrush(Color.Green);
                }
                g.DrawString(RiskList[i], font, brush, CenterOfLegend - LegendWidth / 2 + 50, CurrentHeight);
            }

            return true;
        }
        private void DrawAgricultureIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            Point[] pointArr = new List<Point>()
                            {
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY + (width/2) },
                            }.ToArray();
            g.DrawPolygon(pen, pointArr);
            g.FillPolygon(brush, pointArr);
        }
        private void DrawForestedIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            Point[] pointArr = new List<Point>()
                            {
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX, Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX, Y = (int)LatY + (width/2)*2 },
                                new Point() { X = (int)LngX, Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX, Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                            }.ToArray();
            g.DrawPolygon(pen, pointArr);
            g.FillPolygon(brush, pointArr);
        }
        private void DrawIndustryIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            Point[] pointArr = new List<Point>()
                            {
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                            }.ToArray();
            g.DrawPolygon(pen, pointArr);
            g.FillPolygon(brush, pointArr);
        }
        private void DrawMarineIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            Point[] pointArr = new List<Point>()
                            {
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX, Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY },
                                new Point() { X = (int)LngX, Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                            }.ToArray();
            g.DrawPolygon(pen, pointArr);
            g.FillPolygon(brush, pointArr);
        }
        private void DrawRecreationIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            Point[] pointArr = new List<Point>()
                            {
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY },
                                new Point() { X = (int)LngX, Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                            }.ToArray();
            g.DrawPolygon(pen, pointArr);
            g.FillPolygon(brush, pointArr);
        }
        private void DrawUrbanIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            Point[] pointArr = new List<Point>()
                            {
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY - (width/3) },
                                new Point() { X = (int)LngX + (width/2), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY - (width/4) },
                                new Point() { X = (int)LngX + (width/3), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX, Y = (int)LatY - (width/2) },
                                new Point() { X = (int)LngX, Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY - width },
                                new Point() { X = (int)LngX - (width/3), Y = (int)LatY + (width/2) },
                                new Point() { X = (int)LngX - (width/2), Y = (int)LatY + (width/2) },
                            }.ToArray();
            g.DrawPolygon(pen, pointArr);
            g.FillPolygon(brush, pointArr);
        }
        private void DrawOtherIcon(Graphics g, Pen pen, SolidBrush brush, int width, int LatY, int LngX)
        {
            g.DrawEllipse(pen, (int)LngX - (width / 2), (int)LatY - (width / 2), width, width);
            g.FillEllipse(brush, (int)LngX - (width / 2), (int)LatY - (width / 2), width, width);
        }
        private bool DrawMWQMSitesPoints(Graphics g, CoordMap coordMap, List<MapInfoPointModel> mapInfoPointModelMWQMSiteList, List<TVItemModel> tvItemModelMWQMSiteList)
        {
            Brush brush = new SolidBrush(Color.LightGreen);
            Pen pen = new Pen(Color.LightGreen, 1.0f);
            Font font = new Font("Arial", 10, FontStyle.Regular);

            double TotalWidthLng = coordMap.NorthEast.Lng - coordMap.SouthWest.Lng;
            double TotalHeightLat = coordMap.NorthEast.Lat - coordMap.SouthWest.Lat;

            foreach (MapInfoPointModel mapInfoPointModel in mapInfoPointModelMWQMSiteList)
            {
                TVItemModel tvItemModel = tvItemModelMWQMSiteList.Where(c => c.TVItemID == mapInfoPointModel.TVItemID).FirstOrDefault();

                if (tvItemModel != null && tvItemModel.IsActive == true)
                {
                    double LngX = ((mapInfoPointModel.Lng - coordMap.SouthWest.Lng) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                    double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (coordMap.NorthEast.Lat - mapInfoPointModel.Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);

                    g.DrawRectangle(new Pen(Color.LightGreen, 1.0f), (int)LngX - 3, (int)LatY - 3, 6, 6);
                    g.FillRectangle(brush, (int)LngX - 3, (int)LatY - 3, 6, 6);

                    string TVText = tvItemModel.TVText;
                    while (true)
                    {
                        if (TVText.First().ToString() != "0")
                        {
                            break;
                        }
                        TVText = TVText.Substring(1);
                    }

                    g.DrawString(TVText, font, brush, (int)LngX, (int)LatY);

                }
            }

            return true;
        }
        private bool DrawNorthArrow(Graphics g, CoordMap coordMap)
        {
            string NorthText = "N";
            Font font = new Font("Arial", 16, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.LightBlue);

            SizeF sizeF = g.MeasureString(NorthText, font);

            g.MeasureString(NorthText, font);
            g.DrawString(NorthText, font, brush, GoogleImageWidth * 2 - 50 - (int)(sizeF.Width / 2), 30);

            Pen pen = new Pen(Color.LightBlue, 6);
            pen.StartCap = LineCap.NoAnchor;
            pen.EndCap = LineCap.RoundAnchor;
            g.DrawLine(pen, GoogleImageWidth * 2 - 50, 55, GoogleImageWidth * 2 - 50, 70);

            pen = new Pen(Color.LightBlue, 6);
            pen.StartCap = LineCap.ArrowAnchor;
            pen.EndCap = LineCap.NoAnchor;
            g.DrawLine(pen, GoogleImageWidth * 2 - 50, 15, GoogleImageWidth * 2 - 50, 30);

            return true;
        }
        private bool DrawPolSourceSitesPoints(Graphics g, CoordMap coordMap, List<MapInfoPointModel> mapInfoPointModelPolSourceSiteList, List<TVItemModel> tvItemModelPolSourceSiteList)
        {
            int IconSize = 8;
            if (tvItemModelPolSourceSiteList.Count > 0)
            {
                PolSourceSiteService polSourceSiteService = new PolSourceSiteService(LanguageRequest, user);
                PolSourceObservationService polSourceObservationService = new PolSourceObservationService(LanguageRequest, user);
                PolSourceObservationIssueService polSourceObservationIssueService = new PolSourceObservationIssueService(LanguageRequest, user);

                List<PolSourceSiteModel> polSourceSiteModelList = polSourceSiteService.GetPolSourceSiteModelListWithSubsectorTVItemIDDB(tvItemModelPolSourceSiteList[0].ParentID);
                List<PolSourceObservationModel> polSourceObservationModelList = polSourceObservationService.GetPolSourceObservationModelListWithSubsectorTVItemIDDB(tvItemModelPolSourceSiteList[0].ParentID);
                List<PolSourceObservationIssueModel> polSourceObservationIssueModelList = polSourceObservationIssueService.GetPolSourceObservationIssueModelListWithSubsectorTVItemIDDB(tvItemModelPolSourceSiteList[0].ParentID);

                Font font = new Font("Arial", 8, FontStyle.Regular);
                Brush brush = new SolidBrush(Color.LightGreen);

                double TotalWidthLng = coordMap.NorthEast.Lng - coordMap.SouthWest.Lng;
                double TotalHeightLat = coordMap.NorthEast.Lat - coordMap.SouthWest.Lat;

                int count = 0;
                foreach (MapInfoPointModel mapInfoPointModel in mapInfoPointModelPolSourceSiteList)
                {
                    TVItemModel tvItemModel = tvItemModelPolSourceSiteList.Where(c => c.TVItemID == mapInfoPointModel.TVItemID).FirstOrDefault();

                    if (tvItemModel != null && tvItemModel.IsActive == true)
                    {
                        PolSourceSiteModel polSourceSiteModel = polSourceSiteModelList.Where(c => c.PolSourceSiteTVItemID == tvItemModel.TVItemID).FirstOrDefault();

                        if (polSourceSiteModel != null)
                        {
                            PolSourceObservationModel polSourceObservationModelLastest = polSourceObservationModelList.Where(c => c.PolSourceSiteID == polSourceSiteModel.PolSourceSiteID).OrderByDescending(c => c.ObservationDate_Local).FirstOrDefault();

                            if (polSourceObservationModelLastest != null)
                            {
                                count += 1;
                                double LngX = ((mapInfoPointModel.Lng - coordMap.SouthWest.Lng) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                                double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (coordMap.NorthEast.Lat - mapInfoPointModel.Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);

                                List<PolSourceObservationIssueModel> polSourceObservationIssueModelListSelected = polSourceObservationIssueModelList.Where(c => c.PolSourceObservationID == polSourceObservationModelLastest.PolSourceObservationID).OrderBy(c => c.Ordinal).ToList();

                                if (polSourceObservationIssueModelListSelected.Count == 0)
                                {

                                    DrawOtherIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                }
                                else
                                {
                                    if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",10501,")) // Agriculture
                                    {
                                        if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",93001,")
                                            || polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92002,")) // High Risk
                                        {
                                            DrawAgricultureIcon(g, new Pen(Color.Red, 1.0f), new SolidBrush(Color.Red), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92001,")) // Moderate Risk
                                        {
                                            DrawAgricultureIcon(g, new Pen(Color.YellowGreen, 1.0f), new SolidBrush(Color.YellowGreen), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",91001,")) // Low Risk
                                        {
                                            DrawAgricultureIcon(g, new Pen(Color.Green, 1.0f), new SolidBrush(Color.Green), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else
                                        {
                                            DrawAgricultureIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                        }
                                    }
                                    else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",10502,")) // Forested
                                    {
                                        if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",93001,")
                                            || polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92002,")) // High Risk
                                        {
                                            DrawForestedIcon(g, new Pen(Color.Red, 1.0f), new SolidBrush(Color.Red), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92001,")) // Moderate Risk
                                        {
                                            DrawForestedIcon(g, new Pen(Color.YellowGreen, 1.0f), new SolidBrush(Color.YellowGreen), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",91001,")) // Low Risk
                                        {
                                            DrawForestedIcon(g, new Pen(Color.Green, 1.0f), new SolidBrush(Color.Green), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else
                                        {
                                            DrawForestedIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                        }
                                    }
                                    else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",10503,")) // Industry
                                    {
                                        if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",93001,")
                                            || polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92002,")) // High Risk
                                        {
                                            DrawIndustryIcon(g, new Pen(Color.Red, 1.0f), new SolidBrush(Color.Red), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92001,")) // Moderate Risk
                                        {
                                            DrawIndustryIcon(g, new Pen(Color.YellowGreen, 1.0f), new SolidBrush(Color.YellowGreen), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",91001,")) // Low Risk
                                        {
                                            DrawIndustryIcon(g, new Pen(Color.Green, 1.0f), new SolidBrush(Color.Green), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else
                                        {
                                            DrawIndustryIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                        }
                                    }
                                    else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",10504,")) // Marine
                                    {
                                        if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",93001,")
                                            || polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92002,")) // High Risk
                                        {
                                            DrawMarineIcon(g, new Pen(Color.Red, 1.0f), new SolidBrush(Color.Red), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92001,")) // Moderate Risk
                                        {
                                            DrawMarineIcon(g, new Pen(Color.YellowGreen, 1.0f), new SolidBrush(Color.YellowGreen), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",91001,")) // Low Risk
                                        {
                                            DrawMarineIcon(g, new Pen(Color.Green, 1.0f), new SolidBrush(Color.Green), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else
                                        {
                                            DrawMarineIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                        }
                                    }
                                    else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",10505,")) // Recreation
                                    {
                                        if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",93001,")
                                            || polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92002,")) // High Risk
                                        {
                                            DrawRecreationIcon(g, new Pen(Color.Red, 1.0f), new SolidBrush(Color.Red), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92001,")) // Moderate Risk
                                        {
                                            DrawRecreationIcon(g, new Pen(Color.YellowGreen, 1.0f), new SolidBrush(Color.YellowGreen), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",91001,")) // Low Risk
                                        {
                                            DrawRecreationIcon(g, new Pen(Color.Green, 1.0f), new SolidBrush(Color.Green), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else
                                        {
                                            DrawRecreationIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                        }
                                    }
                                    else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",10506,")) // Urban
                                    {
                                        if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",93001,")
                                            || polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92002,")) // High Risk
                                        {
                                            DrawUrbanIcon(g, new Pen(Color.Red, 1.0f), new SolidBrush(Color.Red), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",92001,")) // Moderate Risk
                                        {
                                            DrawUrbanIcon(g, new Pen(Color.YellowGreen, 1.0f), new SolidBrush(Color.YellowGreen), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else if (polSourceObservationIssueModelListSelected[0].ObservationInfo.Contains(",91001,")) // Low Risk
                                        {
                                            DrawUrbanIcon(g, new Pen(Color.Green, 1.0f), new SolidBrush(Color.Green), IconSize, (int)LatY, (int)LngX);
                                        }
                                        else
                                        {
                                            DrawUrbanIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                        }
                                    }
                                    else
                                    {
                                        DrawOtherIcon(g, new Pen(Color.Black, 1.0f), new SolidBrush(Color.Black), IconSize, (int)LatY, (int)LngX);
                                    }
                                }
                                g.DrawString(count.ToString(), font, brush, new Point((int)(LngX + 2), (int)(LatY + 5)));
                            }
                        }
                    }
                }
            }

            return true;
        }
        private bool DrawSubsectorMWQMSites(CoordMap coordMap, TVItemModel tvItemModelSubsector, List<MapInfoPointModel> mapInfoPointModelSubsectorList, List<MapInfoPointModel> mapInfoPointModelMWQMSiteList, List<TVItemModel> tvItemModelMWQMSiteList)
        {
            if (!GetInset(coordMap.NorthEast, coordMap.SouthWest))
            {
                return false;
            }

            using (Bitmap targetAll = new Bitmap(DirName + FileNameFull))
            {
                using (Graphics g = Graphics.FromImage(targetAll))
                {
                    using (Bitmap targetImg = new Bitmap(DirName + FileNameInsetFinal))
                    {
                        g.DrawImage(targetImg, new Point(0, 0));
                    }

                    if (!DrawImageBorder(g))
                    {
                        return false;
                    }

                    if (!DrawSubsectorPolygon(g, coordMap, mapInfoPointModelSubsectorList))
                    {
                        return false;
                    }

                    if (!DrawTitles(g, tvItemModelSubsector.TVText, "Marine Water Quality Monitoring Sites"))
                    {
                        return false;
                    }

                    //if (!DrawLegendMWQMSites(g, coordMap, mapInfoPointModelMWQMSiteList, tvItemModelMWQMSiteList))
                    //{
                    //    return false;
                    //}

                    if (!DrawNorthArrow(g, coordMap))
                    {
                        return false;
                    }

                    if (!DrawHorizontalScale(g, coordMap))
                    {
                        return false;
                    }

                    if (!DrawVerticalScale(g, coordMap))
                    {
                        return false;
                    }

                    if (!DrawMWQMSitesPoints(g, coordMap, mapInfoPointModelMWQMSiteList, tvItemModelMWQMSiteList))
                    {
                        return false;
                    }

                }

                targetAll.Save(DirName + FileNameFull.Replace(".png", "Annotated.png"), ImageFormat.Png);
            }

            return true;
        }
        private bool DrawSubsectorPolygon(Graphics g, CoordMap coordMap, List<MapInfoPointModel> mapInfoPointModelSubsectorList)
        {
            List<Point> polygonPointList = new List<Point>();

            double TotalWidthLng = coordMap.NorthEast.Lng - coordMap.SouthWest.Lng;
            double TotalHeightLat = coordMap.NorthEast.Lat - coordMap.SouthWest.Lat;

            foreach (MapInfoPointModel mapInfoPointModel in mapInfoPointModelSubsectorList)
            {
                double LngX = ((mapInfoPointModel.Lng - coordMap.SouthWest.Lng) / TotalWidthLng) * GoogleImageWidth * 2.0D;
                double LatY = ((GoogleImageHeight * 2) - GoogleLogoHeight) - ((TotalHeightLat - (coordMap.NorthEast.Lat - mapInfoPointModel.Lat)) / TotalHeightLat) * ((GoogleImageHeight * 2) - GoogleLogoHeight);
                polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });
            }

            g.DrawPolygon(new Pen(Color.Orange, 2.0f), polygonPointList.ToArray());

            return true;
        }
        private bool DrawSubsectorPolSourceSites(CoordMap coordMap, TVItemModel tvItemModelSubsector, List<MapInfoPointModel> mapInfoPointModelSubsectorList, List<MapInfoPointModel> mapInfoPointModelPolSourceSiteList, List<TVItemModel> tvItemModelPolSourceSiteList)
        {
            if (!GetInset(coordMap.NorthEast, coordMap.SouthWest))
            {
                return false;
            }

            using (Bitmap targetAll = new Bitmap(DirName + FileNameFull))
            {
                using (Graphics g = Graphics.FromImage(targetAll))
                {
                    using (Bitmap targetImg = new Bitmap(DirName + FileNameInsetFinal))
                    {
                        g.DrawImage(targetImg, new Point(0, 0));
                    }

                    if (!DrawImageBorder(g))
                    {
                        return false;
                    }

                    if (!DrawSubsectorPolygon(g, coordMap, mapInfoPointModelSubsectorList))
                    {
                        return false;
                    }

                    if (!DrawTitles(g, tvItemModelSubsector.TVText, "Pollution Source Site"))
                    {
                        return false;
                    }

                    if (!DrawLegendPolSourceSites(g, coordMap, mapInfoPointModelPolSourceSiteList, tvItemModelPolSourceSiteList))
                    {
                        return false;
                    }

                    if (!DrawNorthArrow(g, coordMap))
                    {
                        return false;
                    }

                    if (!DrawHorizontalScale(g, coordMap))
                    {
                        return false;
                    }

                    if (!DrawVerticalScale(g, coordMap))
                    {
                        return false;
                    }

                    if (!DrawPolSourceSitesPoints(g, coordMap, mapInfoPointModelPolSourceSiteList, tvItemModelPolSourceSiteList))
                    {
                        return false;
                    }

                }

                targetAll.Save(DirName + FileNameFull.Replace(".png", "Annotated.png"), ImageFormat.Png);
            }

            return true;
        }
        private bool DrawTitles(Graphics g, string FirstTitle, string SecondTitle)
        {
            float FirstTitleHeight = 0;

            // First Title
            Font font = new Font("Arial", 20, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.LightBlue);

            SizeF sizeFInit = g.MeasureString(FirstTitle, font);
            SizeF sizeF = g.MeasureString(FirstTitle, font);
            while (true)
            {
                sizeF = g.MeasureString(FirstTitle, font);
                if (sizeF.Width > (GoogleImageWidth * 2 - 200 - 200 - 100)) // 200 is the Inset and Legend width
                {
                    FirstTitle = FirstTitle.Substring(0, FirstTitle.Length - 2);
                }
                else
                {
                    break;
                }
            }
            if (sizeFInit.Width != sizeF.Width)
            {
                FirstTitle = FirstTitle + "...";
            }
            g.DrawString(FirstTitle, font, brush, new Point(GoogleImageWidth - (int)(sizeF.Width / 2), 3));

            FirstTitleHeight = sizeF.Height;

            // Second Title
            font = new Font("Arial", 16, FontStyle.Bold);
            brush = new SolidBrush(Color.LightBlue);
            sizeFInit = g.MeasureString(SecondTitle, font);
            sizeF = g.MeasureString(SecondTitle, font);
            while (true)
            {
                sizeF = g.MeasureString(SecondTitle, font);
                if (sizeF.Width > (GoogleImageWidth * 2 - 200 - 200 - 100)) // 200 is the Inset and Legend width
                {
                    SecondTitle = SecondTitle.Substring(0, SecondTitle.Length - 2);
                }
                else
                {
                    break;
                }
            }
            if (sizeFInit.Width != sizeF.Width)
            {
                SecondTitle = SecondTitle + "...";
            }
            g.DrawString(SecondTitle, font, brush, new Point(GoogleImageWidth - (int)(sizeF.Width / 2), (int)((3.0f * 2.0f) + FirstTitleHeight)));

            return true;
        }
        private bool DrawVerticalScale(Graphics g, CoordMap coordMap)
        {
            IPrincipal user = new GenericPrincipal(new GenericIdentity("Charles.LeBlanc2@canada.ca", "Forms"), null);
            MapInfoService mapInfoService = new MapInfoService(LanguageRequest, user);

            int MinLatY = (GoogleImageHeight * 2 - GoogleLogoHeight) - 60;
            int MaxLatY = ((GoogleImageHeight * 2 - GoogleLogoHeight) * 4 / 5) - 60;

            g.DrawLine(new Pen(Color.LightBlue, 2), 40, MinLatY, 40, MaxLatY);

            double MinLat = coordMap.NorthEast.Lat - (coordMap.NorthEast.Lat - coordMap.SouthWest.Lat) / 5;
            double distLat = mapInfoService.CalculateDistance(MinLat * mapInfoService.d2r, coordMap.NorthEast.Lng * mapInfoService.d2r, coordMap.NorthEast.Lat * mapInfoService.d2r, coordMap.NorthEast.Lng * mapInfoService.d2r, mapInfoService.R) / 1000;

            string distText = distLat.ToString("F2") + " km";
            Font font = new Font("Arial", 10, FontStyle.Regular);
            Brush brush = new SolidBrush(Color.LightBlue);

            SizeF sizeF = g.MeasureString(distText, font);

            g.DrawString(distText, font, brush, 45, ((GoogleImageHeight * 2 - GoogleLogoHeight) * 4 / 5) - 60 - sizeF.Height);

            for (int i = 0; i < 100; i++)
            {
                if ((double)i > distLat)
                {
                    g.DrawLine(new Pen(Color.LightBlue, 1), 40 - 1, MaxLatY, 40 - 1 - 10, MaxLatY);
                    break;
                }
                g.DrawLine(new Pen(Color.LightBlue, 1), 40 - 1, MinLatY + (int)(i / distLat * (MaxLatY - MinLatY)), 40 - 1 - 10, MinLatY + (int)(i / distLat * (MaxLatY - MinLatY)));

                distText = i.ToString();
                font = new Font("Arial", 10, FontStyle.Regular);
                brush = new SolidBrush(Color.LightBlue);

                sizeF = g.MeasureString(distText, font);

                g.DrawString(distText, font, brush, 45, MinLatY + (int)(i / distLat * (MaxLatY - MinLatY)) - (sizeF.Height / 2));
            }


            return true;
        }
        private bool GetInset(Coord CoordNE, Coord CoordSW)
        {
            MapInfoService mapInfoService = new MapInfoService(LanguageRequest, user);

            if (JumpGoogleLoading)
            {
                return true;
            }

            double Lat = (CoordNE.Lat + CoordSW.Lat) / 2;
            double Lng = (CoordNE.Lng + CoordSW.Lng) / 2;

            GoogleLogoHeight = 24;
            int InsetZoom = 6;
            int InsetWidth = 200;
            int InsetHeight = 200;

            using (WebClient client = new WebClient())
            {
                try
                {
                    string url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ Lat.ToString("F6") },{ Lng.ToString("F6") }&zoom={ InsetZoom.ToString() }&size={ InsetWidth.ToString() }x{ InsetHeight.ToString() }&language={ LanguageRequest.ToString() }";
                    client.DownloadFile(url, DirName + FileNameInset);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            Coord coord = new Coord() { Lat = (float)Lat, Lng = (float)Lng, Ordinal = 0 };
            CoordMap coordMapTemp = mapInfoService.GetBounds(coord, InsetZoom, InsetWidth, InsetHeight);

            Rectangle cropRect = new Rectangle(0, 0, InsetWidth, InsetHeight);
            using (Bitmap srcNewInset = new Bitmap(InsetWidth, InsetHeight))
            {
                using (Graphics g = Graphics.FromImage(srcNewInset))
                {
                    using (Bitmap srcInset = new Bitmap(DirName + FileNameInset))
                    {
                        g.DrawImage(srcInset, new Rectangle(0, 0, InsetWidth, InsetHeight), cropRect, GraphicsUnit.Pixel);
                    }

                    double LngX = 0.0D;
                    double LatY = 0.0D;
                    double TotalWidthLng = coordMapTemp.NorthEast.Lng - coordMapTemp.SouthWest.Lng;
                    double TotalHeightLat = coordMapTemp.NorthEast.Lat - coordMapTemp.SouthWest.Lat;

                    List<Point> polygonPointList = new List<Point>();
                    // point #1
                    LngX = ((CoordSW.Lng - coordMapTemp.SouthWest.Lng) / TotalWidthLng) * InsetWidth;
                    LatY = InsetHeight - ((TotalHeightLat - (coordMapTemp.NorthEast.Lat - CoordSW.Lat)) / TotalHeightLat) * InsetHeight;
                    polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });
                    // point #2
                    LngX = ((CoordNE.Lng - coordMapTemp.SouthWest.Lng) / TotalWidthLng) * InsetWidth;
                    LatY = InsetHeight - ((TotalHeightLat - (coordMapTemp.NorthEast.Lat - CoordSW.Lat)) / TotalHeightLat) * InsetHeight;
                    polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });
                    // point #3
                    LngX = ((CoordNE.Lng - coordMapTemp.SouthWest.Lng) / TotalWidthLng) * InsetWidth;
                    LatY = InsetHeight - ((TotalHeightLat - (coordMapTemp.NorthEast.Lat - CoordNE.Lat)) / TotalHeightLat) * InsetHeight;
                    polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });
                    // point #4
                    LngX = ((CoordSW.Lng - coordMapTemp.SouthWest.Lng) / TotalWidthLng) * InsetWidth;
                    LatY = InsetHeight - ((TotalHeightLat - (coordMapTemp.NorthEast.Lat - CoordNE.Lat)) / TotalHeightLat) * InsetHeight;
                    polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });
                    // point #5
                    LngX = ((CoordSW.Lng - coordMapTemp.SouthWest.Lng) / TotalWidthLng) * InsetWidth;
                    LatY = InsetHeight - ((TotalHeightLat - (coordMapTemp.NorthEast.Lat - CoordSW.Lat)) / TotalHeightLat) * InsetHeight;
                    polygonPointList.Add(new Point() { X = (int)LngX, Y = (int)LatY });

                    g.DrawPolygon(new Pen(Color.LightGreen, 1.0f), polygonPointList.ToArray());
                }

                srcNewInset.Save(DirName + FileNameInsetFinal, ImageFormat.Png);
            }


            cropRect = new Rectangle(0, 0, InsetWidth, InsetHeight - GoogleLogoHeight);

            using (Bitmap targetInsetFinal = new Bitmap(cropRect.Width, cropRect.Height))
            {
                using (Graphics g = Graphics.FromImage(targetInsetFinal))
                {
                    using (Bitmap srcInset = new Bitmap(DirName + FileNameInsetFinal))
                    {
                        g.DrawImage(srcInset, new Rectangle(0, 0, targetInsetFinal.Width, targetInsetFinal.Height), cropRect, GraphicsUnit.Pixel);

                        g.DrawRectangle(new Pen(Color.Black, 6.0f), cropRect);
                    }
                }
                targetInsetFinal.Save(DirName + FileNameInsetFinal, ImageFormat.Png);
            }

            return true;
        }
        private CoordMap GetMapCoordinateWhileGettingGooglePNG(double MinLat, double MaxLat, double MinLng, double MaxLng)
        {
            MapInfoService mapInfoService = new MapInfoService(LanguageRequest, user);

            CoordMap coordMap = new CoordMap();

            double ExtraLat = (MaxLat - MinLat) * 0.05D;
            double ExtraLng = (MaxLng - MinLng) * 0.05D;

            Coord coordNE = new Coord() { Lat = (float)(MaxLat + ExtraLat), Lng = (float)(MaxLng + ExtraLng), Ordinal = 0 };
            Coord coordSW = new Coord() { Lat = (float)(MinLat - ExtraLat), Lng = (float)(MinLng - ExtraLng), Ordinal = 0 };

            double CenterLat = coordNE.Lat - (coordNE.Lat - coordSW.Lat) / 4;
            double CenterLng = coordSW.Lng + (coordNE.Lng - coordSW.Lng) / 4;

            double deltaLng = 0.0D;
            double deltaLat = 0.0D;

            bool Found = false;
            bool ZoomWasReduced = false;
            while (!Found)
            {
                // calculate the center of the uper left block
                Coord coord = new Coord() { Lat = (float)CenterLat, Lng = (float)CenterLng, Ordinal = 0 };
                CoordMap coordMapTemp = mapInfoService.GetBounds(coord, Zoom, GoogleImageWidth, GoogleImageHeight);
                deltaLng = Math.Abs((CenterLng - coordMapTemp.SouthWest.Lng) * 2);
                deltaLat = Math.Abs((CenterLat - coordMapTemp.SouthWest.Lat) * 2) - (Math.Abs((CenterLat - coordMapTemp.SouthWest.Lat) * 2) * GoogleLogoHeight / GoogleImageHeight);

                coordMap = mapInfoService.GetBounds(new Coord() { Lat = (float)CenterLat, Lng = (float)CenterLng, Ordinal = 0 }, Zoom, GoogleImageWidth, GoogleImageHeight);
                CoordMap NewcoordMapTemp = mapInfoService.GetBounds(new Coord() { Lat = (float)CenterLat, Lng = (float)(CenterLng + deltaLng), Ordinal = 0 }, Zoom, GoogleImageWidth, GoogleImageHeight);

                coordMap.NorthEast.Lng = NewcoordMapTemp.NorthEast.Lng;
                coordMap.NorthEast.Lat = NewcoordMapTemp.NorthEast.Lat;

                NewcoordMapTemp = mapInfoService.GetBounds(new Coord() { Lat = (float)(CenterLat - deltaLat), Lng = (float)CenterLng, Ordinal = 0 }, Zoom, GoogleImageWidth, GoogleImageHeight);

                coordMap.SouthWest.Lng = NewcoordMapTemp.SouthWest.Lng;
                coordMap.SouthWest.Lat = NewcoordMapTemp.SouthWest.Lat;

                Found = true;
                if (coordMap.NorthEast.Lng < (double)coordNE.Lng)
                {
                    Zoom -= 1;
                    ZoomWasReduced = true;
                    Found = false;
                }
                if (coordMap.NorthEast.Lat < (double)coordNE.Lat)
                {
                    Zoom -= 1;
                    ZoomWasReduced = true;
                    Found = false;
                }
                if (coordMap.SouthWest.Lng > (double)coordSW.Lng)
                {
                    Zoom -= 1;
                    ZoomWasReduced = true;
                    Found = false;
                }
                if (coordMap.SouthWest.Lat > (double)coordSW.Lat)
                {
                    Zoom -= 1;
                    ZoomWasReduced = true;
                    Found = false;
                }
                if (Found && !ZoomWasReduced)
                {
                    if ((coordMap.NorthEast.Lng - coordMap.SouthWest.Lng) > (coordNE.Lng - coordSW.Lng) * 2)
                    {
                        Zoom += 1;
                        Found = false;
                    }
                    if ((coordMap.NorthEast.Lat - coordMap.SouthWest.Lat) > (coordNE.Lat - coordSW.Lat) * 2)
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

            if (!JumpGoogleLoading)
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        string url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ CenterLat.ToString("F6") },{ CenterLng.ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest.ToString() }";
                        client.DownloadFile(url, DirName + FileNameNW);

                        url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ CenterLat.ToString("F6") },{ (CenterLng + deltaLng).ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest.ToString() }";
                        client.DownloadFile(url, DirName + FileNameNE);

                        url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ (CenterLat - deltaLat).ToString("F6") },{ CenterLng.ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest.ToString() }";
                        client.DownloadFile(url, DirName + FileNameSW);

                        url = $@"https://maps.googleapis.com/maps/api/staticmap?maptype={ MapType }&center={ (CenterLat - deltaLat).ToString("F6") },{ (CenterLng + deltaLng).ToString("F6") }&zoom={ Zoom.ToString() }&size={ GoogleImageWidth.ToString() }x{ GoogleImageHeight.ToString() }&language={ LanguageRequest.ToString() }";
                        client.DownloadFile(url, DirName + FileNameSE);
                    }
                    catch (Exception ex)
                    {
                        return new CoordMap();
                    }
                }

                if (!CombineAllImageIntoOne())
                {
                    return new CoordMap();
                }

                if (!DeleteTempGoogleImageFiles())
                {
                    return new CoordMap();
                }
            }
            return coordMap;
        }
        private void LoadDefaults()
        {
            Random random = new Random();
            FileNameExtra = "";
            for (int i = 0; i < 10; i++)
            {
                FileNameExtra = FileNameExtra + (char)random.Next(97, 122);
            }

            if (JumpGoogleLoading)
            {
                FileNameExtra = "srqdyybrdq";
            }

            // defaults
            DirName = @"C:\CSSP Latest Code Old\GoogleMapPNG\GoogleMapPNG\";
            FileNameNW = $@"NW{ FileNameExtra }.png";
            FileNameNE = $@"NE{ FileNameExtra }.png";
            FileNameSW = $@"SW{ FileNameExtra }.png";
            FileNameSE = $@"SE{ FileNameExtra }.png";
            FileNameFull = $@"Full{ FileNameExtra }.png";
            FileNameInset = $@"Inset{ FileNameExtra }.png";
            FileNameInsetFinal = $@"InsetFinal{ FileNameExtra }.png";
            GoogleImageWidth = 640;
            GoogleImageHeight = 640;
            GoogleLogoHeight = 24;
            LanguageRequest = LanguageEnum.en;
            MapType = "hybrid"; // Can be roadmap (default), satellite, hybrid, terrain
            Zoom = 16;
        }
        #endregion Functions private


    }
}
