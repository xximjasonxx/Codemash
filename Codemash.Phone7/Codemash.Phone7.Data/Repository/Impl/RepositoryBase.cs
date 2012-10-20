using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Phone7.Data.Entities;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Codemash.Phone7.Data.Repository.Impl
{
    public abstract class RepositoryBase<T> where T : EntityBase
    {
        private IList<T> _repository;
        protected IList<T> Repository
        {
            get { return _repository ?? (_repository = new List<T>()); }
        }

        public event EventHandler LoadCompleted;

        public void Load()
        {
            var client = new RestClient();
            var request = new RestRequest(DownloadUrl, Method.GET);

            client.ExecuteAsync(request, LoadCompleteCallback);
        }

        private void LoadCompleteCallback(IRestResponse restResponse)
        {
            var jsonString = restResponse.Content;
            var jsonArray = JArray.Parse(jsonString);
            _repository = (from ja in jsonArray.AsJEnumerable()
                           select CreateObject(ja)).ToList();

            if (LoadCompleted != null)
                LoadCompleted(this, new EventArgs());

        }

        // abstract properties
        protected abstract string DownloadUrl { get; }

        // abstract methods
        protected abstract T CreateObject(JToken jToken);
    }
}
