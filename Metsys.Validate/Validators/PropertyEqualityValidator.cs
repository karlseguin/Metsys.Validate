using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Metsys.Validate.Validators
{
    internal sealed class PropertyEqualityValidator<T> : IValidator, IDoJavascript
    {
        private readonly Expression<Func<T, object>> _property;

        public PropertyEqualityValidator(Expression<Func<T, object>> property)
        {
            _property = property;
        }

        public bool IsValid(object value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, string>> ToJson()
        {
            yield return new KeyValuePair<string, string>("eqTo", string.Concat('\'', _property.GetName(), '\''));
        }
    }
}