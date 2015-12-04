using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Class representing opening hours of selected venue
    /// </summary>
    public class Hours
    {
        /// <summary>
        /// Opening hours for a set of days
        /// </summary>
        public List<TimeFrame> TimeFrames { get; set; }

        /// <summary>
        /// String representation of opening hours of selected venue
        /// </summary>
        /// <returns></returns>
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
