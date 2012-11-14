using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Client.Data.Repository.Impl
{
    public class JsonSpeakerRepository : RepositoryBase<Speaker>, ISpeakerRepository
    {
        #region Implementation of IRepository<Speaker>

        protected override string DownloadUrl
        {
            get { return "http://codemashdelta.azurewebsites.net/api/Speaker"; }
        }

        protected override Speaker CreateEntity(JToken ji)
        {
            var speaker = new Speaker
                              {
                                  Name = new StringWrapper(ji["Name"]).ToString(),
                                  Biography = new StringWrapper(ji["Biography"]).ToString(),
                                  BlogUrl = new StringWrapper(ji["BlogUrl"]).ToString(),
                                  Twitter = new StringWrapper(ji["Twitter"]).ToString(),
                                  SpeakerId = new StringWrapper(ji["SpeakerId"]).ToString().AsInt()
                              };

            return speaker;
        }

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
