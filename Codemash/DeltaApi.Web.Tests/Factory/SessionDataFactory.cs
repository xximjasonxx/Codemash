using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Extensions;
using Codemash.Api.Data.Parsing.Impl;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;

namespace DeltaApi.Web.Tests.Factory
{
    public static class SessionDataFactory
    {
        public static IList<Session> GetStandardSessions()
        {
            using (var fileStream = File.OpenRead("Factory/StandardSession.json"))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    var jsonString = reader.ReadToEnd();
                    var jsonArray = JArray.Parse(jsonString);

                    return (from it in jsonArray.AsJEnumerable()
                            select new Session
                                       {
                                           SessionId = it["SessionId"].ToString().AsInt(),
                                           Title = it["Title"].ToString(),
                                           Abstract = it["Abstract"].ToString(),
                                           Level = it["Level"].ToString(),
                                           Track = it["Track"].ToString(),
                                           Room = it["Room"].ToString(),
                                           Start = it["StartTime"].ToString().AsDateTime(),
                                           End = it["EndTime"].ToString().AsDateTime(),
                                           SpeakerId = it["Speaker"]["SpeakerId"].ToString().AsInt()
                                       }).ToList();
                }
            }
        }
    }
}
