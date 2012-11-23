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
        private IList<Change> _repository;

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

        #region Implementation of IRepository<Change>

        /// <summary>
        /// Loads the repository from whatever backing data store is most appropriate
        /// </summary>
        public void Load()
        {
            var client = new RestClient("http://192.168.1.4/DeltaApi/api/");
            var request = new RestRequest("Change", Method.GET);
            request.AddParameter("channel", SettingsRepository.PushChannelUri);

            client.ExecuteAsync(request, resp =>
                                             {
                                                 JArray jsonArray = JArray.Parse(resp.Content);
                                                 _repository = (from ch in jsonArray.AsJEnumerable()
                                                                select CreateChangeEntity(ch)).ToList();

                                                 if (LoadCompleted != null)
                                                     LoadCompleted(this, new EventArgs());
                                             });
        }

        public event EventHandler LoadCompleted;

        /// <summary>
        /// Get an item from the Repository based on its int key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Change Get(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IChangeRepository

        /// <summary>
        /// Return whether there are changes in the repository or not
        /// </summary>
        public bool HasChanges
        {
            get { return _repository.Count > 0; }
        }

        #endregion

        private Change CreateChangeEntity(JToken ch)
        {
            var change = new Change
                       {
                           Action = new StringWrapper(ch["Action"]).ToString().AsActionTypeEnum(),
                           ChangeId = int.MinValue,
                           Changeset = new StringWrapper(ch["Changeset"]).ToString().AsInt(),
                           EntityId = new StringWrapper(ch["EntityId"]).ToString().AsInt(),
                           Key = new StringWrapper(ch["Key"]).ToString(),
                           Value = new StringWrapper(ch["Value"]).ToString()
                       };

            change.MarkAsClean();
            return change;
        }
    }
}
