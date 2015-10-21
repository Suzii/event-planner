using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.FourSquare.Entities
{
    public class Hours
    {
        /// <summary>
        /// Opening hours for a set of days
        /// </summary>
        [JsonProperty ("timeframes")]
        public List<TimeFrame> TimeFrames { get; set; }

        public override string ToString()
        {
            string res = "";
            foreach(TimeFrame tf in TimeFrames)
            {
                res += tf + "\n";
            }
            return res;
        }
    }
}
