using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.Poller.Process
{
    public class SpeakerWorkerProcess : IProcess
    {
        [Inject]
        public IMasterDataProvider MasterDataProvider { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        #region Implementation of IProcess

        /// <summary>
        /// Execute whatever process is being implemeted
        /// </summary>
        public void Execute()
        {
            // get the list of Speakers from the Master source
            var masterSpeakers = MasterDataProvider.GetAllSpeakers();

            // get the list of Speakers from the local source
            var localSpeakers = SpeakerRepository.GetAll();

            // find the differences between this list
            var differences = masterSpeakers.Compare<Speaker, SpeakerChange>(localSpeakers);
            if (differences.Count > 0)
            {
                // differences exist
            }
        }

        #endregion
    }
}
