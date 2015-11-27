using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.Models.Models.Vote
{
    public class GeoJsonViewModel
    {
        public string type
        {
            get
            {
                return "FeatureCollection";
            }
        }

        public IList<Feature> features;
    }

    public class Feature
    {
        public string id { get; set; }

        public string type
        {
            get
            {
                return "Feature";
            }
        }
        public Geometry geometry { get; set; }

        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type
        {
            get
            {
                return "Point";
            }
            set { }
        }
        public double[] coordinates { get; set; }
    }

    public class Properties
    {
        public string name { get; set; }
        public string address { get; set; }
    }
}
