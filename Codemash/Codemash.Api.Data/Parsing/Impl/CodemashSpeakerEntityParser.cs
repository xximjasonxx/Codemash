using Codemash.Api.Data.Entities;
using Codemash.Server.Core;
using Newtonsoft.Json.Linq;

namespace Codemash.Api.Data.Parsing.Impl
{
    public class CodemashSpeakerEntityParser : ISpeakerEntityParser
    {
        #region Implementation of IEntityParser<Speaker>

        /// <summary>
        /// Given some "contents" attempt to parse to an instance of T
        /// </summary>
        /// <param name="contents">The contents which will be parsed</param>
        /// <returns></returns>
        public Speaker Parse(string contents)
        {
            var it = JObject.Parse(contents);
            return new Speaker
                       {
                           SpeakerId = new StringWrapper(it["Name"]).ToString().GetHashCode(),
                           Name = new StringWrapper(it["Name"]).ToString(),
                           Biography = new StringWrapper(it["Biography"]).ToString(),
                           BlogUrl = new StringWrapper(it["BlogURL"]).ToString(),
                           Twitter = new StringWrapper(it["TwitterHandle"]).ToString()
                       };
        }

        #endregion
    }
}
