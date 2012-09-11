using System.Linq;
using Codemash.Api.Data.Entities;
using Codemash.Api.Data.Ex;

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
    }
}
