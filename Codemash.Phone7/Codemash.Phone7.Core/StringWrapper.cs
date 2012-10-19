namespace Codemash.Phone7.Core
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
            if (_value == null)
                return string.Empty;

            return _value.ToString();
        }
    }
}
