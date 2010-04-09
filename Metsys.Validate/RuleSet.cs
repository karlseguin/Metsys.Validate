using System.Collections.Generic;

namespace Metsys.Validate
{
    public abstract class RuleSet
    {
        private static readonly IDictionary<string, IList<PropertyValidatorData>> _rules = new Dictionary<string, IList<PropertyValidatorData>>();
        
        protected IRuleConfiguration Create(string name)
        {
            if (!_rules.ContainsKey(name))
            {
                _rules.Add(name, new List<PropertyValidatorData>(1));
            }
            var rules = new PropertyValidatorData();
            _rules[name].Add(rules);
            return new RuleConfiguration(rules);     
        }
        
        internal static IList<PropertyValidatorData> GetRules(string name)
        {
            return _rules.ContainsKey(name) ? _rules[name] : null;
        }
    }
}