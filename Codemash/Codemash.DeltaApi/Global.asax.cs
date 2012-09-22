using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Codemash.DeltaApi.Core;
using Codemash.DeltaApi.Dependency;

namespace Codemash.DeltaApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var kernel = new WebContainer();
            kernel.Settings.ActivationCacheDisabled = true;
            GlobalConfiguration.Configuration.DependencyResolver = new CodemashNinjectDependencyResolver(kernel);
        }
    }
}