using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using Codemash.Api.Data.Repositories;
using Codemash.Notification.Context;
using Codemash.Notification.Context.Impl;
using Codemash.Poller.Container;
using Codemash.Poller.Process;
using Codemash.Server.Core;
using Microsoft.WindowsAzure.ServiceRuntime;
using Ninject;
using Ninject.Parameters;

namespace Codemash.Poller
{
    public class PollerWorkerRole : RoleEntryPoint
    {
        private readonly PollerContainer _container;

        public PollerWorkerRole()
        {
            _container = new PollerContainer();
        }

        public override void Run()
        {
            try
            {
                Logger.Current.LogInformation("Process Start");

                ExecuteProcess();
                ExecuteNotificationCheck();

                Logger.Current.LogInformation("Process Complete");
            }
            catch (Exception ex)
            {
                Logger.Current.LogException(ex);
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Establish Security Parameters for Notifications
            ProvisionSecurity();

            return base.OnStart();
        }

        private void ExecuteProcess()
        {
            // create the session process
            var sessionProcess = _container.Get<ISynchronize>("Session", new IParameter[0]);
            var speakerProcess = _container.Get<ISynchronize>("Speaker", new IParameter[0]);

            using (var scope = new TransactionScope())
            {
                // synchronize speakrs (should go first since session has a dependancy here)
                Logger.Current.LogInformation("Synchronizing Speakers");
                var speakerChanges = speakerProcess.Synchronize();

                // now do the session data
                Logger.Current.LogInformation("Synchronizing Sessions");
                var sessionChanges = sessionProcess.Synchronize();

                // collect the changes and save them
                Logger.Current.LogInformation("Saving Delta Information");
                var allChanges = speakerChanges.Concat(sessionChanges).ToList();
                if (allChanges.Count > 0)
                {
                    var repository = _container.Get<IChangeRepository>();
                    repository.SaveRange(allChanges);
                }

                // complete the transaction
                Logger.Current.LogInformation("Completing the Procss");
                scope.Complete();
            }
        }

        private void ExecuteNotificationCheck()
        {
            var process = _container.Get<IProcess>();
            process.Execute();
        }

        private void ProvisionSecurity()
        {
            var securityContext = new Win8SecurityContext();
            securityContext.Provision(Config.PackageSecurityToken, Config.PackageSecret);

            _container.Bind<ISecurityContext>().ToConstant(securityContext).InSingletonScope();
        }
    }
}
