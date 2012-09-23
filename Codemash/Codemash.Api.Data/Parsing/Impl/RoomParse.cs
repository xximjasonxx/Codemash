
namespace Codemash.Api.Data.Parsing.Impl
{
    public class RoomParse : ParseBase<Room>
    {
        #region Implementation of IParse<Room>

        /// <summary>
        /// Parse the given string to an instance of T, return the default if the parse fails
        /// </summary>
        /// <param name="str">The string we will parse</param>
        /// <param name="defaultValue">The default value to be outputted</param>
        /// <returns></returns>
        public Room Parse(string str, Room defaultValue)
        {
            if (HasLookup(str.Trim())) return GetKeyValue(str.Trim());
            return TryEnumParse(str.Trim(), Room.Unknown);
        }

        #endregion
    }
}
