using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Codemash.Client.Data.Extensions;
using Newtonsoft.Json.Linq;

namespace Codemash.Client.Data.Repository.Impl
{
    public class JsonChangeRepository : IChangeRepository
    {
        private IList<Change> _changeListing;

        public ISettingsRepository SettingsRepository { get; private set; }

        public JsonChangeRepository(ISettingsRepository settingsRepository)
        {
            SettingsRepository = settingsRepository;
        }

        public async Task<int> LoadAsync()
        {
            var requestUri = string.Format("{0}/Change?clientId={1}", Config.DeltaApiRoot,
                                           SettingsRepository.RegisteredClientId);

            var request = (HttpWebRequest) WebRequest.Create(requestUri);
            var response = await request.GetResponseAsync();
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var responseString = stream.ReadToEnd();
                var jsonArray = JArray.Parse(responseString);

                _changeListing = (from ch in jsonArray
                                  select new Change
                                             {
                                                 Action = new StringWrapper(ch["Action"]).ToString().AsActionTypeEnum(),
                                                 Changeset = new StringWrapper(ch["Changeset"]).ToString().AsInt(),
                                                 EntityId = new StringWrapper(ch["EntityId"]).ToString().AsLong(),
                                                 EntityType = new StringWrapper(ch["EntityType"]).ToString(),
                                                 Key = new StringWrapper(ch["Key"]).ToString(),
                                                 Value = new StringWrapper(ch["Value"]).ToString()
                                             }).ToList();
            }

            return 0;
        }

        public Change Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Change> GetAll()
        {
            return _changeListing ?? new List<Change>();
        }
    }
}
