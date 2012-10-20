
namespace Codemash.Phone7.Core
{
    public static class ParsingExtensionMethods
    {
        /// <summary>
        /// Takes a string and parses it a unique key value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsKey(this string str)
        {
            return str.GetHashCode();
        }

        /// <summary>
        /// Convert a string to its int representation, return MinValue if the parse fails
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsInt(this string str)
        {
            int intVal;
            return int.TryParse(str, out intVal) ? intVal : int.MinValue;
        }
    }
}
