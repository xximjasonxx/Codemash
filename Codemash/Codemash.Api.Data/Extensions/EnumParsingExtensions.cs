using System;

namespace Codemash.Api.Data.Extensions
{
    public static class EnumParsingExtensions
    {
        /// <summary>
        /// Parse the given string instance into the desired enumation, return a default value if the parse fails
        /// </summary>
        /// <param name="str">The string instance to parse</param>
        /// <param name="defaultValue">The default value to return if the parse fails</param>
        /// <returns>An instance of the Level enum</returns>
        public static Level AsLevel(this string str, Level defaultValue)
        {
            if (Enum.IsDefined(typeof(Level), str))
                return (Level) Enum.Parse(typeof (Level), str);

            return defaultValue;
        }
    }
}
