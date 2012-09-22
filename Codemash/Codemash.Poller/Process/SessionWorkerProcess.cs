using System.Transactions;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Provider;
using Codemash.Api.Data.Repositories;
using Ninject;

namespace Codemash.Poller.Process
{
    public class SessionWorkerProcess : IProcess
    {
        [Inject]
        public IMasterDataProvider MasterDataProvider { get; set; }

        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISessionChangeRepository SessionChangeRepository { get; set; }

        public void Execute()
        {
            // first step is to get the Session master data
            var masterSessionList = MasterDataProvider.GetAllSessions();

            // get the local session List for comparison
            var localSessionList = SessionRepository.GetAll();
            var initialCount = localSessionList.Count;

            using (var transaction = new TransactionScope())
            {
                // add the master session data into the respository
                SessionRepository.SaveRange(masterSessionList);

                if (initialCount > 0)
                {
                    // perform the comparison
                    var sessionDifferences = masterSessionList.Compare<Session, SessionChange>(localSessionList);
                    if (sessionDifferences.Count > 0)
                    {
                        SessionChangeRepository.SaveRange(sessionDifferences);
                    }
                }

                // commit the transaction
                transaction.Complete();
            }
        }
    }
}
