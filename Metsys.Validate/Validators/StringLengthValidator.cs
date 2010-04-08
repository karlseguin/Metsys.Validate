using System.Collections.Generic;

namespace Metsys.Validate.Validators
{
    internal sealed class StringLengthValidator : IValidator, IDoJavascript
    {
        public int? _minimumLength;
        internal int? _maximumLength;

        public StringLengthValidator(int? minimumLength, int? maximumLength)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            if (_minimumLength != null && _minimumLength != 0)
            {
                yield return new KeyValuePair<string, string>("minlength", _minimumLength.ToString());
            }
            if (_maximumLength != null)
            {
                yield return new KeyValuePair<string, string>("maxlength", _maximumLength.ToString());
            }            
        }

        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (!(value is string))
            {
                return false;
            }
            var length = ((string)value).Length;
            if (_minimumLength != null && length < _minimumLength)
            {
                return false;
            }
            if (_maximumLength != null && length > _maximumLength)
            {
                return false;
            }
            return true;
        }


    }
}