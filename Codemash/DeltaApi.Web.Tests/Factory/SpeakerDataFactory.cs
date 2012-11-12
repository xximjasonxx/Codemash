using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;

namespace DeltaApi.Web.Tests.Factory
{
    public static class SpeakerDataFactory
    {
        public static IList<Speaker> GetSpeakersFromFile()
        {
            using (var reader = new StreamReader("Factory/StandardSpeaker.json"))
            {
                var jsonString = reader.ReadToEnd();
                var jsonArray = JArray.Parse(jsonString);

                return (from sp in jsonArray.AsJEnumerable()
                        select new Speaker
                        {
                            SpeakerId = sp["SpeakerId"].ToString().AsInt(),
                            Name = sp["FirstName"].ToString(),
                            Biography = sp["Bio"].ToString(),
                            BlogUrl = sp["Url"].ToString(),
                            EmailAddress = string.Empty,
                            Twitter = sp["Twitter"].ToString()
                        }).Where(sp => sp.Biography != string.Empty).ToList();
            }
        }
    }
}
