using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Parsing
{
    public abstract class ParseBase<T>
    {
        private readonly IDictionary<string, T> _lookupDictionary;
        
        protected ParseBase()
        {
            // instantiate the underlying member
            _lookupDictionary = new Dictionary<string, T>();

            // Build the lookup table
            InitializeLookupTable();
        }

        /// <summary>
        /// Initialize the Lookup table based on T and create the key value pairs
        /// </summary>
        private void InitializeLookupTable()
        {
            var applicableEnumFields = typeof(T).GetFields().Where(fi => fi.GetCustomAttributes(false)
                .OfType<EnumKeyAttribute>().Any()).ToList();

            applicableEnumFields.ForEach(fi =>
            {
                var attribute = fi.GetCustomAttributes(false).OfType<EnumKeyAttribute>().First();
                _lookupDictionary.Add(attribute.AlternativeName, (T)fi.GetValue(null));
            });
        }

        /// <summary>
        /// Return whether the lookup table contains the key
        /// </summary>
        /// <param name="key">The key value to use in the lookup</param>
        /// <returns></returns>
        protected bool HasLookup(string key)
        {
            return _lookupDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Return the value for the given key
        /// </summary>
        /// <param name="key">The key to perform the lookup with</param>
        /// <returns>The instance of type T that shall be returned from the lookup table</returns>
        protected T GetKeyValue(string key)
        {
            return _lookupDictionary[key];
        }

        /// <summary>
        /// Try to parse a string value to an enum
        /// </summary>
        /// <param name="strValue">The string value to be parsed to the enum instance</param>
        /// <param name="defaultValue">The default value to return if the value cannot be parsed</param>
        /// <returns></returns>
        protected T TryEnumParse(string strValue, T defaultValue)
        {
            if (Enum.IsDefined(typeof(T), strValue))
                return (T)Enum.Parse(typeof(Track), strValue);

            return defaultValue;
        }
    }
}
