using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Parsing;
using Newtonsoft.Json.Linq;
using Ninject;

namespace Server.CoreTests.Factory
{
    public class TestDataFactory
    {
        [Inject]
        public ISessionEntityParser SessionEntityParser { get; set; }

        [Inject]
        public ISpeakerEntityParser SpeakerEntityParser { get; set; }

        public IList<Session> GetSessions()
        {
            using (var reader = new StreamReader("Factory/StandardSession.json"))
            {
                var jsonString = reader.ReadToEnd();
                var array = JArray.Parse(jsonString);

                return (from it in array.AsJEnumerable()
                        select SessionEntityParser.Parse(it.ToString())).ToList();
            }
        }

        public IList<Speaker> GetSpeakers()
        {
            using (var reader = new StreamReader("Factory/StandardSpeaker.json"))
            {
                var jsonString = reader.ReadToEnd();
                var jsonArray = JArray.Parse(jsonString);

                return (from sp in jsonArray.AsJEnumerable()
                        select SpeakerEntityParser.Parse(sp.ToString())).Where(sp => sp.Biography != string.Empty).ToList();
            }
        }
    }
}
