using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Parsing.Impl
{
    public class TrackEnumParse : EnumParseBase<Track>
    {
        #region Implementation of IParse<Track>

        /// <summary>
        /// Parse the given string to an instance of T, return the default if the parse fails
        /// </summary>
        /// <param name="str">The string we will parse</param>
        /// <param name="defaultValue">The default value to be outputted</param>
        /// <returns></returns>
        public Track Parse(string str, Track defaultValue)
        {
            // return the lookup value if there is one
            if (HasLookup(str)) return GetKeyValue(str);

            // attempt a straight parse, return the default if it fails
            return TryEnumParse(str, defaultValue);
        }

        #endregion
    }
}
