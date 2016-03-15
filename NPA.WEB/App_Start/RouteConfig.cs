using System.Web.Mvc;
using System.Web.Routing;

namespace NPA.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("HomeCatchAllRoute", "Home/{*.}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional});

            routes.MapRoute("PersonsCatchAllRoute", "Persons/{*.}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}
