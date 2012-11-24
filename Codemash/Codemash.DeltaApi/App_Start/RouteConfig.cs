using System.Web.Http;
using System.Web.Routing;

namespace Codemash.DeltaApi
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("Handlers/*.ashx");

            // Explicit Routes
            routes.MapHttpRoute(
                name: "UpdateChangesetRoute",
                routeTemplate: "api/Change/Update",
                defaults: new {controller = "Change", action = "Update"});

            routes.MapHttpRoute(
                name: "ChangesRoute",
                routeTemplate: "api/Change/{channel}",
                defaults: new {controller = "Change"});

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}