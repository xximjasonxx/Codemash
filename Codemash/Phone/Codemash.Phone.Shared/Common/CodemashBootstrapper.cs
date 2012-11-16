using System;
using Caliburn.Micro;
using Ninject;

namespace Codemash.Phone.Shared.Common
{
    public class CodemashBootstrapper : PhoneBootstrapper
    {
        public IKernel NinjectContainer { get; private set; }
        public PhoneClientType ClientTypeName { get; set; }

        protected override void Configure()
        {
            NinjectContainer = new CodemashContainer(RootFrame, ClientTypeName);
        }

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
