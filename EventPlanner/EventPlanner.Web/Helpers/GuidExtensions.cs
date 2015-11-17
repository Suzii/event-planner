using System;

namespace EventPlanner.Web.Helpers
{
    public static class GuidExtensions
    {
        public static string GetUniqueUrlParameter(this Guid? id)
        {
            if (!id.HasValue)
            {
                return string.Empty;
            }
            return id.Value.GetUniqueUrlParameter();
        }

        public static string GetUniqueUrlParameter(this Guid id)
        {
            var htmlFriendly = Convert.ToBase64String(id.ToByteArray());
            htmlFriendly = htmlFriendly.Replace("/", "_");
            htmlFriendly = htmlFriendly.Replace("+", "-");
            return htmlFriendly.Substring(0, 22);
        }
    }
}
