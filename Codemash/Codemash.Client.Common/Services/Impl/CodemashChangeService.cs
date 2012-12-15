using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Data;
using Codemash.Client.Data.Entities;
using Codemash.Client.Data.Repository;

namespace Codemash.Client.Common.Services.Impl
{
    public class CodemashChangeService : IChangeService
    {
        public ISessionRepository SessionRepository { get; private set; }
        public ISpeakerRepository SpeakerRepository { get; private set; }

        public CodemashChangeService(ISessionRepository sessionRepository, ISpeakerRepository speakerRepository)
        {
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
        }

        public void ApplyChanges(IEnumerable<Change> changeList)
        {
            // separate
            var speakerChanges = changeList.Where(c => string.Compare("speaker", c.EntityType, StringComparison.OrdinalIgnoreCase) == 0).ToList();
            var sessionChanges = changeList.Where(c => string.Compare("session", c.EntityType, StringComparison.OrdinalIgnoreCase) == 0).ToList();
            var updateClientChangeset = false;

            if (speakerChanges.Count > 0)
            {
                ApplySpeakerChanges(speakerChanges);
                SpeakerRepository.Save();
                updateClientChangeset = true;
            }

            if (sessionChanges.Count > 0)
            {
                ApplySessionChanges(sessionChanges);
                SessionRepository.Save();
                updateClientChangeset = true;
            }

            if (updateClientChangeset)
            {
                int changeSet = changeList.Max(c => c.Changeset);
                UpdateClientChangesetToLatest(changeSet);
            }
        }

        private void UpdateClientChangesetToLatest(int changeSet)
        {
            
        }

        private void ApplySessionChanges(IEnumerable<Change> sessionChanges)
        {
            foreach (var sessionChange in sessionChanges)
            {
                var session = SessionRepository.Get(sessionChange.EntityId);
                if (session != null)
                {
                    if (sessionChange.Action == ActionType.Delete)
                    {
                        
                    }

                    if (sessionChange.Action == ActionType.Modify)
                    {
                        
                    }

                    if (sessionChange.Action == ActionType.Add)
                    {
                        
                    }
                }
            }
        }

        private void ApplySpeakerChanges(IEnumerable<Change> speakerChanges)
        {
            foreach (var speakerChange in speakerChanges)
            {
                var session = SessionRepository.Get(speakerChange.EntityId);
                if (session != null)
                {
                    if (speakerChange.Action == ActionType.Delete)
                    {

                    }

                    if (speakerChange.Action == ActionType.Modify)
                    {

                    }

                    if (speakerChange.Action == ActionType.Add)
                    {

                    }
                }
            }
        }
    }
}
