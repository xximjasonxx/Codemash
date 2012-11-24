using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Core;
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
        public Change Get(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return all changes in the current repository
        /// </summary>
        /// <returns></returns>
        public IList<Change> GetAll()
        {
            return _repository;
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
