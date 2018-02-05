using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DI.Models
{
    public class KmlModel
    {
        

        public KmlModel()
        {
            this.Clickable = true;
            this.ScreenOverlays = true;
            this.Url = "http://gmaps-samples.googlecode.com/svn/trunk/ggeoxml/cta.kml";
        }

        public bool Clickable { get; set; }

        public bool PreserveViewport { get; set; }

        public bool ScreenOverlays { get; set; }

        public bool SuppressInfoWindows { get; set; }

        public string Url { get; set; }

        public int ZIndex { get; set; }


        /////////////////////////////////////////////////
        //public IEnumerable<RegionInfo> Locations { get; set; }

    }

    public class RegionInfo
    {
        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Title { get; set; }

        public int ZIndex { get; set; }

        public string ImagePath { get; set; }

        public string InfoWindowContent { get; set; }

        public double Population { get; set; }

        public string Address
        {
            get { return this.Title + ", Spain"; }
        }
    }
}