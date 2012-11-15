using System.Collections.Generic;
using System.Transactions;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.Poller.Process
{
    public class SpeakerSynchronize : ISynchronize
    {
        [Inject]
        public IMasterDataProvider MasterDataProvider { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

        #region Implementation of ISynchronize

        /// <summary>
        /// Execute whatever process is being implemeted
        /// </summary>
        public IList<Change> Synchronize()
        {
            // get the list of Speakers from the Master source
            var masterSpeakers = MasterDataProvider.GetAllSpeakers();

            // get the list of Speakers from the local source
            var localSpeakers = SpeakerRepository.GetAll();
            var initialCount = localSpeakers.Count;

            // save updated speaker data
            SpeakerRepository.SaveRange(masterSpeakers);

            if (initialCount > 0)
            {
                // find the differences between this list
                return masterSpeakers.Compare(localSpeakers);
            }

            return new List<Change>();
        }

        #endregion
    }
}
