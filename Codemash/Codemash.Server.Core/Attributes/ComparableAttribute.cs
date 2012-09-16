using System;

namespace Codemash.Server.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ComparableAttribute : Attribute
    {
        public string KeyName { get; private set; }

        public ComparableAttribute()
        {
            KeyName = null;
        }

        public ComparableAttribute(string keyName)
        {
            KeyName = keyName;
        }
    }
}
