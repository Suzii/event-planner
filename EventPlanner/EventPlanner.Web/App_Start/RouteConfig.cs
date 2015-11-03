using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace EventPlanner.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "CreateEvent",
                url: "{controller}/{action}/{eventHash}",
                defaults: new { controller = "CreateEvent", action = "Index", eventHash = UrlParameter.Optional }
                );
        }
    }
}
