using System;
using Codemash.Phone.Data.Entities;
using Ninject;
using RestSharp;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class JsonChangeRepository : IChangeRepository
    {
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
                                                 return;
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
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
