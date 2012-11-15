using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Transactions;
using Codemash.Api.Data.Repositories;
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
            // spin off a thread which will fire every configurable minutes to check our source for changes
            // not using Timer here because we need the process to be blocking as each execution could be longer than others
            while (true)
            {
                try
                {
                    Trace.WriteLine("Process beginning");

                    ExecuteProcess();

                    Trace.WriteLine("Execution Complete");

                    // wait to check again
                    Thread.Sleep(TimeSpan.FromMinutes(Config.MinutesWaitTime));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;
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
                var speakerChanges = speakerProcess.Synchronize();

                // now do the session data
                var sessionChanges = sessionProcess.Synchronize();

                // collect the changes and save them
                var allChanges = speakerChanges.Concat(sessionChanges).ToList();
                if (allChanges.Count > 0)
                {
                    var repository = _container.Get<IChangeRepository>();
                    repository.SaveRange(allChanges);
                }

                // complete the transaction
                scope.Complete();
            }
        }
    }
}
