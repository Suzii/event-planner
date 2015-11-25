using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace EventPlanner.Web.Helpers
{
    /// <summary>
    ///     Helper class with GetActualCity method which returns actual city where user is located by IP address.
    ///     When error raise it returns empty string.
    /// </summary>
    public static class LocationHelper
    {
        /// <returns>string of city located by IP address</returns>
        public static string GetCurrentCity(string ip)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    dynamic data = JObject.Parse(wc.DownloadString("https://freegeoip.net/json/"+ip));
                    return Encoding.UTF8.GetString(Encoding.Default.GetBytes(Convert.ToString(data.city)));
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}