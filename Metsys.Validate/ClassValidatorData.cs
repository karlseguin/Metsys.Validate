using System.Collections.Generic;

namespace Metsys.Validate
{
    public class ClassValidatorData
    {
        private readonly IDictionary<string, IList<PropertyValidatorData>> _rules = new Dictionary<string, IList<PropertyValidatorData>>();
        public IDictionary<string, IList<PropertyValidatorData>> Rules
        {
            get { return _rules; }
        }
    }
}