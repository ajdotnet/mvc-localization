using System.Web.Mvc;
using System.Web.Routing;

namespace MyStocks.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // change the current culture
            var route = routes.MapRoute(
                name: "SetPreferredCulture",
                url: "SetPreferredCulture/{culture}",
                defaults: new { controller = "Culture", action = "SetPreferredCulture", culture = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}