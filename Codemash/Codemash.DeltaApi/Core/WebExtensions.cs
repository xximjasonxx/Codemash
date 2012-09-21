using System.Text.RegularExpressions;

namespace Codemash.DeltaApi.Core
{
    public static class WebExtensions
    {
        /// <summary>
        /// Given a type name return the controller name or empty if one cannot be found
        /// </summary>
        /// <param name="controllerTypeName">The full type name of the Controller class</param>
        /// <returns></returns>
        public static string AsControllerName(this string controllerTypeName)
        {
            Regex regex = new Regex("controller", RegexOptions.IgnoreCase);
            return regex.IsMatch(controllerTypeName) ? regex.Replace(controllerTypeName, string.Empty) : string.Empty;
        }
    }
}