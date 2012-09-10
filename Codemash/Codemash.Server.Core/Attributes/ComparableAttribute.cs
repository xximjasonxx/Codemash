using System;

namespace Codemash.Server.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ComparableAttribute : Attribute
    {
    }
}
