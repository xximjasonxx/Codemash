using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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

                    // create the session process
                    var sessionProcess = _container.Get<IProcess>("Session", new IParameter[0]);
                    var speakerProcess = _container.Get<IProcess>("Speaker", new IParameter[0]);

                    // handle all the speaker data
                    // since the session data will depend on speaker data, it is important
                    // that this goes first
                    speakerProcess.Execute();

                    // now do the session data
                    sessionProcess.Execute();

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
    }
}
