using System.Collections.Generic;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.Poller.Process
{
    public class SessionSynchronize : ISynchronize
    {
        [Inject]
        public IMasterDataProvider MasterDataProvider { get; set; }

        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public IList<Change> Synchronize()
        {
            // first step is to get the Session master data
            var masterSessionList = MasterDataProvider.GetAllSessions();

            // get the local session List for comparison
            var localSessionList = SessionRepository.GetAll();
            var initialCount = localSessionList.Count;

            // add the master session data into the respository
            SessionRepository.SaveRange(masterSessionList);

            if (initialCount > 0)
            {
                // perform the comparison
                return masterSessionList.Compare(localSessionList);
            }

            return new List<Change>();
        }
    }
}
