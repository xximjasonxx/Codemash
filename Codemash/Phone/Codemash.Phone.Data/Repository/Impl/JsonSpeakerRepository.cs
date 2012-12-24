using System.Linq;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Common;
using Codemash.Phone.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class JsonSpeakerRepository : RepositoryBase<Speaker>, ISpeakerRepository
    {
        #region Overrides of RepositoryBase<Speaker>

        protected override string DownloadUrl
        {
            get { return string.Format("{0}/Speaker", Config.DeltaApiRoot); }
        }

        protected override Speaker CreateObject(JToken jToken)
        {
            var speaker = new Speaker
                              {
                                  Biography = new StringWrapper(jToken["Biography"]).ToString(),
                                  BlogUrl = new StringWrapper(jToken["BlogUrl"]).ToString(),
                                  Name = new StringWrapper(jToken["Name"]).ToString(),
                                  Twitter = new StringWrapper(jToken["Twitter"]).ToString(),
                                  SpeakerId = new StringWrapper(jToken["SpeakerId"]).ToString().AsLong()
                              };

            return speaker;
        }

        /// <summary>
        /// Instructs the repository to Save all dirty records
        /// </summary>
        protected override void Save()
        {
            if (Repository.Count(it => it.IsDirty) > 0)
            {
                using (var db = GetContext())
                {
                    foreach (var item in Repository.Where(it => it.IsDirty))
                    {
                        switch (item.EntityState)
                        {
                            case EntityState.New:
                                db.Speakers.InsertOnSubmit(item);
                                break;
                            case EntityState.Removed:
                                var sp = db.Speakers.FirstOrDefault(s => s.SpeakerId == item.SpeakerId);
                                if (sp != null)
                                    db.Speakers.DeleteOnSubmit(sp);
                                break;
                            case EntityState.Modified:
                                var speaker = db.Speakers.FirstOrDefault(s => s.SpeakerId == item.SpeakerId);
                                if (speaker != null)
                                {
                                    speaker.Biography = item.Biography;
                                    speaker.BlogUrl = item.BlogUrl;
                                    speaker.Name = item.Name;
                                    speaker.Twitter = item.Twitter;
                                }
                                break;
                        }
                    }

                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region Implementation of IRepository<Speaker>

        /// <summary>
        /// Get an item from the Repository based on its int key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Speaker Get(long id)
        {
            return Repository.FirstOrDefault(s => s.SpeakerId == id);
        }

        public void Add(Speaker item)
        {
            Repository.Add(item);
        }

        #endregion
    }
}
