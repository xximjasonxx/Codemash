using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Common;
using Codemash.Phone.Data.Entities;
using Codemash.Phone.Data.Extensions;
using Newtonsoft.Json.Linq;
using Ninject;
using RestSharp;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class JsonChangeRepository : IChangeRepository
    {
        private ChangeList _repository = new ChangeList();

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        #region Implementation of IRepository<Change>

        /// <summary>
        /// Loads the repository from whatever backing data store is most appropriate
        /// </summary>
        public void Load()
        {
            var client = new RestClient(Config.DeltaApiRoot);
            var request = new RestRequest("Change", Method.GET);
            request.AddParameter("channel", SettingsRepository.PushChannelUri);

            client.ExecuteAsync(request, resp =>
                                             {
                                                 JArray jsonArray = JArray.Parse(resp.Content);
                                                 _repository = new ChangeList();
                                                 _repository.AddRange(from ch in jsonArray.AsJEnumerable()
                                                                      select CreateChangeEntity(ch));

                                                 if (LoadCompleted != null)
                                                     LoadCompleted(this, new EventArgs());
                                             });
        }

        public event EventHandler LoadCompleted;

        public IList<Change> SpeakerChanges
        {
            get { return _repository.SpeakerChanges; }
        }

        public IList<Change> SessionChanges
        {
            get { return _repository.SessionChanges; }
        }

        public ChangeList AllChanges
        {
            get { return _repository ?? (_repository = new ChangeList()); }
        }

        #endregion

        private Change CreateChangeEntity(JToken ch)
        {
            var change = new Change
                       {
                           Action = new StringWrapper(ch["Action"]).ToString().AsActionTypeEnum(),
                           Changeset = new StringWrapper(ch["Changeset"]).ToString().AsInt(),
                           EntityId = new StringWrapper(ch["EntityId"]).ToString().AsLong(),
                           EntityType = new StringWrapper(ch["EntityType"]).ToString(),
                           Key = new StringWrapper(ch["Key"]).ToString(),
                           Value = new StringWrapper(ch["Value"]).ToString()
                       };

            change.MarkAsClean();
            return change;
        }
    }
}
