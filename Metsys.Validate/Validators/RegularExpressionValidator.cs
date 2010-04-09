using System.Collections.Generic;
using System.Text.RegularExpressions;
    
namespace Metsys.Validate.Validators
{
    public class RegularExpressionValidator : IValidator, IDoJavascript
    {
        private readonly Regex _expression;
        public RegularExpressionValidator(Regex expression)
        {
            _expression = expression;
        }

        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }            
            return _expression.IsMatch(value.ToString());
        }

        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            var options = (_expression.Options & RegexOptions.IgnoreCase) == RegexOptions.IgnoreCase ? "i" : string.Empty;
            options += (_expression.Options & RegexOptions.Multiline) == RegexOptions.Multiline ? "m" : string.Empty;
            var expression = string.Format("/{0}/{1}", _expression, options);
            yield return new KeyValuePair<string, string>("regex", expression);
        }
    }
}