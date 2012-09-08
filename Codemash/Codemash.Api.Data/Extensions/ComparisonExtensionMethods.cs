using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Extensions
{
    public static class ComparisonExtensionMethods
    {
        /// <summary>
        /// Analyze a Sesssion object and return a key value pair with Property/Value differences
        /// </summary>
        /// <param name="thisSession">The session being extended (new)</param>
        /// <param name="thatSession">The session being compared against (old)</param>
        /// <returns></returns>
        public static IDictionary<string, string> CompareTo(this Session thisSession, Session thatSession)
        {
            // begin looping through the values of thisSession
            // only properties marked for comparison
            var masterProperties = thisSession.GetType().GetProperties()
                .Where(p => p.GetCustomAttributes(false).OfType<ComparablePropertyAttribute>().Any())
                .ToList();

            var returnResults = new Dictionary<string, string>();
            foreach (var property in masterProperties)
            {
                var masterValue = property.GetValue(thisSession, null).ToString();
                var childValue = thatSession.GetType().GetProperty(property.Name).GetValue(thatSession, null).ToString();

                if (masterValue.CompareTo(childValue) != 0)
                {
                    returnResults.Add(property.Name, masterValue);
                }
            }

            return returnResults;
        }
    }
}
