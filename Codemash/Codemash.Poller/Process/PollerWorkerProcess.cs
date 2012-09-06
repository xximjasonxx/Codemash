using System.Threading.Tasks;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.Poller.Process
{
    public class PollerWorkerProcess
    {
        [Inject]
        public IMasterDataProvider MasterDataProvider { get; set; }

        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        public void Execute()
        {
            // perform the check for changes against the master Session data
            // since this can be a heavy operation - it will be spun off into its own thread
            // same with the speaker check
            // this will be hosted in the cloud, we should have multi core processing available to us
            var sessionTask = new Task(PerformSessionsCheck);
            var speakerTask = new Task(PerformSpeakersCheck);

            // start each task and then wait for it to complete
            sessionTask.Start();
            speakerTask.Start();

            // wait for the tasks to complete
            Task.WaitAll(new[] { sessionTask, speakerTask });
        }

        /// <summary>
        /// Check the Session master against the local sessions and record any differences
        /// </summary>
        private void PerformSessionsCheck()
        {
            // this loads all the data in the data store into repository
            SessionRepository.Load();
        }

        /// <summary>
        /// Check the Speaker master against the local speakers and record any differences
        /// </summary>
        private void PerformSpeakersCheck()
        {
            // this loads all the data in the data store into the repository
            SpeakerRepository.Load();
        }
    }
}
