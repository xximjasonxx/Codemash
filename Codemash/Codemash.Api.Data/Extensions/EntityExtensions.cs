using System.Collections.Generic;
using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Extensions
{
    /// <summary>
    /// Extension method for Entities
    /// </summary>
    /// <remarks>Generally these methods would be defined within the custom types,
    /// however, the functionality cannot be abstracted to a logical area</remarks>
    public static class EntityExtensions
    {
        /// <summary>
        /// Apply a change to the properties of the extended object
        /// </summary>
        /// <typeparam name="T">The type of the extended object</typeparam>
        /// <param name="theObject">The object instance being extended</param>
        /// <param name="propertyName">The name of the property to modify</param>
        /// <param name="value">The new value for the property</param>
        public static void ApplyChange<T>(this T theObject, string propertyName, string value) where T : EntityBase
        {
            // get the properties for T
            var properties = typeof (T).GetProperties();
            if (properties.Count(p => p.Name.ToLower() == propertyName.ToLower()) == 0)
                throw new PropertyNotFoundException(typeof(T).ToString(), propertyName);

            properties.First(p => p.Name.ToLower() == propertyName.ToLower()).SetValue(theObject, value, null);
        }

        /// <summary>
        /// Analyze a Sesssion object and return a key value pair with Property/Value differences
        /// </summary>
        /// <param name="first">The session being extended (new)</param>
        /// <param name="second">The session being compared against (old)</param>
        /// <returns></returns>
        public static IDictionary<string, string> CompareTo<T>(this T first, T second) where T : EntityBase
        {
            // begin looping through the values of thisSession
            // only properties marked for comparison
            var masterProperties = first.GetType().GetProperties()
                .Where(p => p.GetCustomAttributes(false).OfType<ComparableAttribute>().Any())
                .ToList();

            var returnResults = new Dictionary<string, string>();
            foreach (var property in masterProperties)
            {
                var masterValue = property.GetValue(first, null).ToString();
                var childValue = second.GetType().GetProperty(property.Name).GetValue(second, null).ToString();

                if (masterValue.CompareTo(childValue) != 0)
                {
                    returnResults.Add(property.Name, masterValue);
                }
            }

            return returnResults;
        }
    }
}
