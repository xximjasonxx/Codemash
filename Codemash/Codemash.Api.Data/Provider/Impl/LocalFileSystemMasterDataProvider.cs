using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Parsing;
using Newtonsoft.Json.Linq;
using Ninject;

namespace Codemash.Api.Data.Provider.Impl
{
    public class LocalFileSystemMasterDataProvider : IMasterDataProvider
    {
        [Inject]
        public ISessionEntityParser SessionEntityParser { get; set; }

        [Inject]
        public ISpeakerEntityParser SpeakerEntityParser { get; set; }

        #region Implementation of IMasterDataProvider

        /// <summary>
        /// Return all sessions from the Master Datasource
        /// </summary>
        public IList<Session> GetAllSessions()
        {
            const string filepath = @"C:\Users\jason\Dropbox\Public\codemash_sessions.json";
            using (var streamReader = new StreamReader(filepath))
            {
                string fileContents = streamReader.ReadToEnd();
                JArray jsonArray = JArray.Parse(fileContents);
                return (from it in jsonArray.AsEnumerable()
                        select SessionEntityParser.Parse(it.ToString())).ToList();
            }
        }

        /// <summary>
        /// Return all speakers from the Master Datasource
        /// </summary>
        public IList<Speaker> GetAllSpeakers()
        {
            const string filepath = @"C:\Users\jason\Dropbox\Public\codemash_speakers.json";
            using (var streamReader = new StreamReader(filepath))
            {
                string fileContents = streamReader.ReadToEnd();
                JArray jsonArray = JArray.Parse(fileContents);
                return (from it in jsonArray.AsEnumerable()
                        select SpeakerEntityParser.Parse(it.ToString())).ToList();
            }
        }

        #endregion
    }
}
