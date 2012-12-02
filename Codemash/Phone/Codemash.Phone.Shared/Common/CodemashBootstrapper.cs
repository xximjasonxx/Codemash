using System;
using Caliburn.Micro;
using Ninject;

namespace Codemash.Phone.Shared.Common
{
    public abstract class CodemashBootstrapper : PhoneBootstrapper
    {
        public IKernel NinjectContainer { get; protected set; }

        protected override object GetInstance(Type service, string key)
        {
            return service == null ? null : NinjectContainer.Get(service);
        }

        protected override System.Collections.Generic.IEnumerable<object> GetAllInstances(Type service)
        {
            return NinjectContainer.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            NinjectContainer.Inject(instance);
        }
    }
}
