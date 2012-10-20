using System.Linq;
using Codemash.Phone7.Core;
using Codemash.Phone7.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Phone7.Data.Repository.Impl
{
    public class JsonSpeakerRepository : RepositoryBase<Speaker>, ISpeakerRepository
    {
        #region Overrides of RepositoryBase<Speaker>

        protected override string DownloadUrl
        {
            get { return "http://dl.dropbox.com/u/13029365/codemash_speakers.json"; }
        }

        protected override Speaker CreateObject(JToken jToken)
        {
            return new Speaker
                       {
                           Biography = new StringWrapper(jToken["Biography"]).ToString(),
                           BlogUrl = new StringWrapper(jToken["BlogURL"]).ToString(),
                           Name = new StringWrapper(jToken["Name"]).ToString(),
                           Twitter = new StringWrapper(jToken["TwitterHandle"]).ToString(),
                           SpeakerId = new StringWrapper(jToken["SpeakerURI"]).ToString().AsKey()
                       };
        }

        /// <summary>
        /// Instructs the repository to Save all dirty records
        /// </summary>
        public override void Save()
        {
            //throw new System.NotImplementedException();
        }

        #endregion

        #region Implementation of IRepository<Speaker>

        /// <summary>
        /// Get an item from the Repository based on its int key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Speaker Get(int id)
        {
            return Repository.First(s => s.SpeakerId == id);
        }

        #endregion
    }
}
