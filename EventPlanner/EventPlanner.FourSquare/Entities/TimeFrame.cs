using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.FourSquare.Entities
{
    public class TimeFrame
    {
        /// <summary>
        /// Set of days in a week (in string format)
        /// </summary>
        public string Days { get; set; }
        
        /// <summary>
        /// Opening time for a set of days in the week
        /// </summary>
        [JsonProperty("open")]
        public List<Time> Times { get; set; }
        
        public override string ToString()
        {
            StringBuilder res =  new StringBuilder(Days + "\n");
            foreach (Time t in Times)
            {
                res.Append(" ");
                res.Append(t);
            }
            return res.ToString();
        }
    }
}
