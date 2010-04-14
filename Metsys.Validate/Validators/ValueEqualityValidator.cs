using System.Collections.Generic;

namespace Metsys.Validate.Validators
{
    public class ValueEqualityValidator : IValidator, IDoJavascript
    {
        private readonly object _value;
        public ValueEqualityValidator(object value)
        {
            _value = value;
        }

        public bool IsValid(object value, object container)
        {
            if (value == null && _value == null) { return true; }
            if (value == null) { return false; }
            return value.Equals(_value);
        }

        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            string compareTo;
            if (ValidationHelper.IsNumber(_value))
            {
                compareTo = _value.ToString();
            }
            else
            {
                compareTo = string.Concat('\'', _value.ToString(), '\'');
            }
            yield return new KeyValuePair<string, string>("eq", compareTo);
        }
    }
}