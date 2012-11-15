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
        #region Implementation of IEntityParser<Session>

        /// <summary>
        /// Given some "contents" attempt to parse to an instance of T
        /// </summary>
        /// <param name="contents">The contents which will be parsed</param>
        /// <returns></returns>
        public Session Parse(string contents)
        {
            JObject jToken = JObject.Parse(contents);
            var session = new Session
                       {
                           Abstract = new StringWrapper(jToken["Abstract"]).ToString(),
                           Level = new StringWrapper(jToken["Difficulty"]).ToString(),
                           SessionId = new StringWrapper(jToken["Title"]).ToString().GetHashCode(),
                           SpeakerId = new StringWrapper(jToken["SpeakerName"]).ToString().GetHashCode(),
                           Track = new StringWrapper(jToken["Technology"]).ToString(),
                           Room = new StringWrapper(jToken["Room"]).ToString(),
                           Title = new StringWrapper(jToken["Title"]).ToString(),
                           Start = new StringWrapper(jToken["Start"]).ToString().AsDateTime(),
                           End = new StringWrapper(jToken["End"]).ToString().AsDateTime()
                       };

            return session;
        }

        #endregion
    }
}
