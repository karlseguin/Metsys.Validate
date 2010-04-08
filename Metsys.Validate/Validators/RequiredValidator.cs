using System.Collections.Generic;

namespace Metsys.Validate.Validators
{
    internal sealed class RequiredValidator : IValidator, IDoJavascript
    {
        private readonly static KeyValuePair<string, string> _json = new KeyValuePair<string, string>("required", "true");

        public string Message { get; set; }
        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            yield return _json;
        }
        public bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is string)
            {
                return ((string) value).Length > 0;
            }
            return true;
        }
    }

}