using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Server.Core;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;
using Ninject;

namespace Codemash.Api.Data.Parsing.Impl
{
    public class CodemashSessionEntityParser : ISessionEntityParser
    {
        [Inject]
        public TrackEnumParse TrackParser { get; set; }

        [Inject]
        public RoomEnumParse RoomParser { get; set; }

        #region Implementation of IEntityParser<Session>

        /// <summary>
        /// Given some "contents" attempt to parse to an instance of T
        /// </summary>
        /// <param name="contents">The contents which will be parsed</param>
        /// <returns></returns>
        public Session Parse(string contents)
        {
            JObject jToken = JObject.Parse(contents);
            return new Session
                       {
                           Abstract = new StringWrapper(jToken["Abstract"]).ToString(),
                           LevelType = new StringWrapper(jToken["Difficulty"]).ToString().AsLevel(Level.Unknown),
                           SessionId = new StringWrapper(jToken["Title"]).ToString().GetHashCode(),
                           SpeakerId = new StringWrapper(jToken["SpeakerName"]).ToString().GetHashCode(),
                           TrackType = TrackParser.Parse(new StringWrapper(jToken["Technology"]).ToString(), Track.Unknown),
                           RoomType = RoomParser.Parse(new StringWrapper(jToken["Room"]).ToString(), Room.Unknown),
                           Title = new StringWrapper(jToken["Title"]).ToString(),
                           Start = new StringWrapper(jToken["Start"]).ToString().AsDateTime(true),
                           End = new StringWrapper(jToken["End"]).ToString().AsDateTime(true)
                       };
        }

        #endregion
    }
}
