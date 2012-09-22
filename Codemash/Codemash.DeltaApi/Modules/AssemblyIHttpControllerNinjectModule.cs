using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using Codemash.DeltaApi.Core;
using Ninject.Modules;

namespace Codemash.DeltaApi.Modules
{
    public class AssemblyIHttpControllerNinjectModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Assembly thisAssembly = GetType().Assembly;
            var applicableTypes = thisAssembly.GetTypes()
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
        }

        #endregion
    }
}