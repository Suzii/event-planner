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
                url: "{controller}/{action}/{guid}",
                defaults: new { controller = "Home", action = "Index", guid = UrlParameter.Optional }
                //constraints: new { guid = new GuidRouteConstraint() }
            );

            routes.MapRoute(
                name: "CreateEvent",
                url: "{controller}/{action}/{eventHash}",
                defaults: new { controller = "CreateEvent", action = "Index", eventHash = UrlParameter.Optional }
                );
        }
    }
}
