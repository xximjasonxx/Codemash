using System;
using System.Linq;
using Codemash.Server.Core.Attributes;

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

        /// <summary>
        /// Given an instance of the Level enum type, spit out the appropriate string representation
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string AsDisplayString(this Level level)
        {
            return level.ToString();
        }

        /// <summary>
        /// Given an instance of the Room enum type, spit out the appropriate string representation
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public static string AsDisplayString(this Room room)
        {
            return AsDisplayString<Room>(room);
        }

        /// <summary>
        /// Given an instance of the Track enum type, spit out the appropriate string representation
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public static string AsDisplayString(this Track track)
        {
            return AsDisplayString<Track>(track);
        }

        /// <summary>
        /// Given an instance of T (enum) and return the appropriate string representation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        private static string AsDisplayString<T>(T enumValue)
        {
            var attr = enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(false)
                .OfType<EnumKeyAttribute>().FirstOrDefault();
            return attr == null ? enumValue.ToString() : attr.AlternativeName;
        }
    }
}
