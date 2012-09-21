using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using Codemash.Api.Data.Modules;
using Ninject;

namespace Codemash.DeltaApi.Core
{
    public class WebContainer : StandardKernel
    {
        public WebContainer(Assembly theAssembly)
        {
            var applicableTypes = theAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IHttpController)))
                .ToList();

            applicableTypes.ForEach(hc =>
            {
                if (!string.IsNullOrEmpty(hc.Name))
                {
                    var controllerName = hc.Name.AsControllerName();
                    if (controllerName != string.Empty)
                        Bind<IHttpController>().To(hc).Named(controllerName);
                }
            });

            // load the other modules
            var repositoryModule = new RepositoryNinjectModule();

            Load(new[]
                     {
                         repositoryModule
                     });
        }
    }
}