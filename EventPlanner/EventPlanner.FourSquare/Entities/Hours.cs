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
        public List<TimeFrame> TimeFrames { get; set; }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            foreach (TimeFrame tf in TimeFrames)
            {
                res.Append(tf);
                res.Append(Environment.NewLine);
            }
            return res.ToString();
        }
    }
}
