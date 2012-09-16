using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Extensions;
using Newtonsoft.Json.Linq;

namespace Server.CoreTests.Factory
{
    internal static class MoqSpeakerDataFactory
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
                                       SpeakerID = sp["SpeakerId"].ToString().AsInt(),
                                       FirstName = sp["FirstName"].ToString(),
                                       LastName = sp["LastName"].ToString(),
                                       Biography = sp["Bio"].ToString(),
                                       BlogUrl = sp["Url"].ToString(),
                                       Company = sp["Company"].ToString(),
                                       EmailAddress = string.Empty,
                                       Twitter = sp["Twitter"].ToString()
                                   }).Where(sp => sp.Biography != string.Empty).ToList();
            }
        }
    }
}
