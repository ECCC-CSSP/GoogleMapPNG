using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapPNG
{
    public static class GoogleMapsAPI
    {

        static GoogleMapsAPI()
        {
            OriginX = TileSize / 2;
            OriginY = TileSize / 2;
            PixelsPerLonDegree = TileSize / 360.0;
            PixelsPerLonRadian = TileSize / (2 * Math.PI);
        }

        public static int TileSize = 256;
        public static double OriginX, OriginY;
        public static double PixelsPerLonDegree;
        public static double PixelsPerLonRadian;

        public static double DegreesToRadians(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        public static double RadiansToDegrees(double rads)
        {
            return rads * 180.0 / Math.PI;
        }

        public static double Bound(double value, double min, double max)
        {
            value = Math.Min(value, max);
            return Math.Max(value, min);
        }

        //From Lat, Lon to World Coordinate X, Y. I'm being explicit in assigning to
        //X and Y properties.
        public static Coordinate Mercator(double latitude, double longitude)
        {
            double siny = Bound(Math.Sin(DegreesToRadians(latitude)), -.9999, .9999);

            Coordinate c = new Coordinate(0, 0);
            c.X = OriginX + longitude * PixelsPerLonDegree;
            c.Y = OriginY + .5 * Math.Log((1 + siny) / (1 - siny)) * -PixelsPerLonRadian;

            return c;
        }

        //From World Coordinate X, Y to Lat, Lon. I'm being explicit in assigning to
        //Latitude and Longitude properties.
        public static Coordinate InverseMercator(double x, double y)
        {
            Coordinate c = new Coordinate(0, 0);

            c.Longitude = (x - OriginX) / PixelsPerLonDegree;
            double latRadians = (y - OriginY) / -PixelsPerLonRadian;
            c.Latitude = RadiansToDegrees(Math.Atan(Math.Sinh(latRadians)));

            return c;
        }

        public static MapCoordinates GetBounds(Coordinate center, int zoom, int mapWidth, int mapHeight)
        {
            var scale = 1 << zoom;

            var centerWorld = Mercator(center.Latitude, center.Longitude);
            var centerPixel = new Coordinate(0, 0);
            centerPixel.X = centerWorld.X * scale;
            centerPixel.Y = centerWorld.Y * scale;

            var NEPixel = new Coordinate(0, 0);
            NEPixel.X = centerPixel.X + mapWidth / 2.0;
            NEPixel.Y = centerPixel.Y - mapHeight / 2.0;

            var SWPixel = new Coordinate(0, 0);
            SWPixel.X = centerPixel.X - mapWidth / 2.0;
            SWPixel.Y = centerPixel.Y + mapHeight / 2.0;

            var NEWorld = new Coordinate(0, 0);
            NEWorld.X = NEPixel.X / scale;
            NEWorld.Y = NEPixel.Y / scale;

            var SWWorld = new Coordinate(0, 0);
            SWWorld.X = SWPixel.X / scale;
            SWWorld.Y = SWPixel.Y / scale;

            var NELatLon = InverseMercator(NEWorld.X, NEWorld.Y);
            var SWLatLon = InverseMercator(SWWorld.X, SWWorld.Y);

            return new MapCoordinates() { NorthEast = NELatLon, SouthWest = SWLatLon };
        }
    }
    public class MapCoordinates
    {
        public Coordinate SouthWest { get; set; }
        public Coordinate NorthEast { get; set; }
    }

    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double Y { get { return Latitude; } set { Latitude = value; } }
        public double X { get { return Longitude; } set { Longitude = value; } }

        public Coordinate(double lng, double lat)
        {
            Latitude = lat;
            Longitude = lng;
        }

        public override string ToString()
        {
            return Math.Round(Y, 6).ToString() + ", " + Math.Round(X, 6).ToString();
        }
    }
}