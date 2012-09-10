using System;

namespace Codemash.Api.Data.Ex
{
    public class PropertyNotFoundException : Exception
    {
        public string ItemType { get; private set; }
        public string PropertyName { get; private set; }

        public PropertyNotFoundException(string itemType, string propertyName)
            : base(string.Format("Property {0} not found on item of type {1}", propertyName, itemType))
        {
            
        }
    }
}
