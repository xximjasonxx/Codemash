namespace Codemash.Server.Core
{
    public class StringWrapper
    {
        private readonly object _value;

        public StringWrapper(object value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value == null ? string.Empty : _value.ToString();
        }
    }
}
