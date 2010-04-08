using System.Collections.Generic;

namespace Metsys.Validate
{
    public class ClassValidatorData
    {
        private readonly IDictionary<string, PropertyValidatorData> _rules = new Dictionary<string, PropertyValidatorData>();
        public IDictionary<string, PropertyValidatorData> Rules
        {
            get { return _rules; }
        }
    }
}