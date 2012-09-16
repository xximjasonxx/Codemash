
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Parsing.Impl;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;

namespace Server.CoreTests.Factory
{
    public static class MoqSessionCompareTestFactory
    {
        public static IList<Session> GetStandardSessionList()
        {
            using (var reader = new StreamReader("Factory/StandardSession.json"))
            {
                var jsonString = reader.ReadToEnd();
                var array = JArray.Parse(jsonString);
                var trackParser = new TrackParse();
                var roomParser = new RoomParse();

                return (from it in array.AsJEnumerable()
                        select new Session()
                        {
                            SessionId = it["SessionId"].ToString().AsInt(),
                            Title = it["Title"].ToString(),
                            Abstract = it["Abstract"].ToString(),
                            LevelType = it["Level"].ToString().AsLevel(Level.Unknown),
                            TrackType = trackParser.Parse(it["Track"].ToString(), Track.Unknown),
                            RoomType = roomParser.Parse(it["Room"].ToString(), Room.Unknown),
                            Start = it["StartTime"].ToString().AsDateTime(),
                            End = it["EndTime"].ToString().AsDateTime(),
                            SpeakerId = it["Speaker"]["SpeakerId"].ToString().AsInt()
                        }).ToList();
            }
        }
    }
}
