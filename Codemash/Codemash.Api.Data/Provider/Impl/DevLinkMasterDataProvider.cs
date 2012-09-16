﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Parsing.Impl;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;
using Ninject;

namespace Codemash.Api.Data.Provider.Impl
{
    public class DevLinkMasterDataProvider : IMasterDataProvider
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
                            SessionID = it["SessionID"].ToString().AsInt(),
                            Title = it["Title"].ToString(),
                            Abstract = it["Abstract"].ToString(),
                            Level = it["Level"].ToString().AsLevel(Level.Unknown),
                            Track = TrackParser.Parse(it["Track"].ToString(), Track.Unknown),
                            Room = RoomParser.Parse(it["Room"].ToString(), Room.Unknown),
                            Start = it["StartTime"].ToString().AsDateTime(),
                            End = it["EndTime"].ToString().AsDateTime(),
                            SpeakerID = it["Speaker"]["SpeakerID"].ToString().AsInt()
                        }).ToList();
        }

        /// <summary>
        /// Return all speakers from the Master Datasource
        /// </summary>
        public IList<Speaker> GetAllSpeakers()
        {
            var downloadUrl = "http://dl.dropbox.com/u/13029365/DevLink2012_Speakers.json";
            var client = new WebClient();
            var jsonString = client.DownloadString(downloadUrl);
            var jsonArray = JArray.Parse(jsonString);

            return (from it in jsonArray.AsJEnumerable()
                    select new Speaker
                        {
                            SpeakerID = it["SpeakerID"].ToString().AsInt(),
                            FirstName = it["FirstName"].ToString(),
                            LastName = it["LastName"].ToString(),
                            Company = it["Company"].ToString(),
                            Twitter = it["Twitter"].ToString(),
                            Biography = it["Bio"].ToString(),
                            BlogUrl = it["Url"].ToString()
                        }).ToList();
        }

        #endregion
    }
}
