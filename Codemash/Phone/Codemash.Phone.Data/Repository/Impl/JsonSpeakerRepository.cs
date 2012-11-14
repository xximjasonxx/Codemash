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
            get { return "http://codemashdelta.azurewebsites.net/api/Speaker"; }
        }

        protected override Speaker CreateObject(JToken jToken)
        {
            return new Speaker
                       {
                           Biography = new StringWrapper(jToken["Biography"]).ToString(),
                           BlogUrl = new StringWrapper(jToken["BlogUrl"]).ToString(),
                           Name = new StringWrapper(jToken["Name"]).ToString(),
                           Twitter = new StringWrapper(jToken["Twitter"]).ToString(),
                           SpeakerId = new StringWrapper(jToken["SpeakerId"]).ToString().AsInt()
                       };
        }

        /// <summary>
        /// Instructs the repository to Save all dirty records
        /// </summary>
        // TODO: Refactor this to share with Session Repository
        public override void Save()
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
                                db.Speakers.DeleteOnSubmit(item);
                                break;
                            case EntityState.Modified:
                                db.Speakers.Attach(item, true);
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
        public Speaker Get(int id)
        {
            return Repository.First(s => s.SpeakerId == id);
        }

        #endregion
    }
}
