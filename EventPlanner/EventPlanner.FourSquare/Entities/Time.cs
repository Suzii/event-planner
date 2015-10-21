using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanner.FourSquare.Entities
{
    public class Time
    {
        /// <summary>
        /// Opening time.
        /// </summary>
        [JsonProperty("renderedTime")]
        public String RenderedTime { get; set; }

        public override string ToString()
        {
            return RenderedTime;
        }
    }
}
