using System;
using System.Collections.Generic;

namespace Metsys.Validate.Validators
{
    internal sealed class CannedPatternValidator : IValidator, IDoJavascript
    {
        private static readonly IDictionary<ValidationPattern, KeyValuePair<string, string>> _jsonLookup = new Dictionary<ValidationPattern, KeyValuePair<string, string>>
                                                                                {
                                                                                    {ValidationPattern.Email, new KeyValuePair<string, string>("email", "true")},
                                                                                    {ValidationPattern.Digits, new KeyValuePair<string, string>("digits", "true")},
                                                                                    {ValidationPattern.Number, new KeyValuePair<string, string>("number", "true")},                                                                                    
                                                                                    {ValidationPattern.CreditCard, new KeyValuePair<string, string>("creditcard", "true")},
                                                                                    {ValidationPattern.Url, new KeyValuePair<string, string>("url", "true")},
                                                                                };
    
        private readonly ValidationPattern _pattern;
        
        public CannedPatternValidator(ValidationPattern pattern)
        {
            _pattern = pattern;
        }
        public bool IsValid(object value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            yield return _jsonLookup[_pattern];
        }
    }
}