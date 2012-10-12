using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Client.Data.Repository.Impl
{
    public class JsonSpeakerRepository : RepositoryBase<Speaker>, ISpeakerRepository
    {
        #region Implementation of IRepository<Speaker>

        /// <summary>
        /// Load the repository with data
        /// </summary>
        public async void Load()
        {
            const string downloadUrl = "http://dl.dropbox.com/u/13029365/DevLink2012_Speakers.json";
            using (var client = new HttpClient())
            {
                var responseString = await client.GetStringAsync(downloadUrl);
                var jsonArray = JArray.Parse(responseString);
                var speakers = (from ji in jsonArray.AsJEnumerable()
                                select new Speaker
                                           {
                                               FirstName = new StringWrapper(ji["FirstName"]).ToString(),
                                               LastName = new StringWrapper(ji["LastName"]).ToString(),
                                               Biography = new StringWrapper(ji["Bio"]).ToString(),
                                               BlogUrl = new StringWrapper(ji["Url"]).ToString(),
                                               Company = new StringWrapper(ji["Company"]).ToString(),
                                               PictureUrl = new StringWrapper(ji["PictureUrl"]).ToString(),
                                               Twitter = new StringWrapper(ji["Twitter"]).ToString(),
                                               SpeakerId = new StringWrapper(ji["SpeakerId"]).ToString().AsInt()
                                           }).ToList();

                AddRange(speakers);
                if (LoadCompleted != null)
                    LoadCompleted(this, new EventArgs());
            }
        }

        public event EventHandler LoadCompleted;

        /// <summary>
        /// Return an item from the repository by an ID value
        /// </summary>
        /// <param name="id">The ID value, should be unique</param>
        /// <returns></returns>
        public Speaker Get(int id)
        {
            return Repository.FirstOrDefault(s => s.SpeakerId == id);
        }

        /// <summary>
        /// return a list of all items in the repository
        /// </summary>
        /// <returns></returns>
        public IList<Speaker> GetAll()
        {
            return Repository;
        }

        #endregion
    }
}
