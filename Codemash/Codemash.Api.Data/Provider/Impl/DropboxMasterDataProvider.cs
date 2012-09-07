using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Parsing.Impl;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;
using Ninject;

namespace Codemash.Api.Data.Provider.Impl
{
    public class DropboxMasterDataProvider : IMasterDataProvider
    {
        [Inject]
        public RoomParse RoomParser { get; set; }

        [Inject]
        public TrackParse TrackParser { get; set; }

        #region Implementation of IMasterDataProvider

        /// <summary>
        /// Return all sessions from the Master Datasource
        /// </summary>
        public IList<Session> GetAllSessions()
        {
            var downloadUrl = "http://dl.dropbox.com/u/13029365/DevLink2012_Sessions.json";
            var client = new WebClient();
            var jsonString = client.DownloadString(downloadUrl);
            var jsonArray = JArray.Parse(jsonString);

            return (from it in jsonArray.AsJEnumerable()
                    select new Session
                        {
                            SessionId = it["SessionId"].ToString().AsInt(),
                            Track = TrackParser.Parse(it["Track"].ToString(), Track.Unknown)
                        }).ToList();
        }

        /// <summary>
        /// Return all speakers from the Master Datasource
        /// </summary>
        public IList<Speaker> GetAllSpeakers()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
