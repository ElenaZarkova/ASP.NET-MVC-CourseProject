using System.Web.Mvc;
using System.Web.Routing;

namespace CourseProject.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // TODO: fix route
            routes.MapRoute(
               name: "Rate",
               url: "book/rate",
               defaults: new { controller = "Book", action = "Rate" });

            routes.MapRoute(
               name: "Book",
               url: "book/{id}",
               defaults: new { controller = "Book", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
