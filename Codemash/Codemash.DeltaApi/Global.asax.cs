using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Codemash.DeltaApi.Core;
using Codemash.DeltaApi.Dependency;

namespace Codemash.DeltaApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var kernel = new WebContainer(GetType().Assembly);
            kernel.Settings.ActivationCacheDisabled = true;
            GlobalConfiguration.Configuration.DependencyResolver = new CodemashNinjectDependencyResolver(kernel);
        }
    }
}