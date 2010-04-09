using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Metsys.Validate
{
    internal interface IClassValidator
    {
        ClassValidatorData Data{ get;}
    } 
    
    public abstract class ClassValidator<T> : IClassValidator
    {
        private readonly ClassValidatorData _data = new ClassValidatorData();

        public ClassValidatorData Data
        {
            get { return _data; }
        }

        protected IRuleConfiguration RuleFor(Expression<Func<T, object>> expression)
        {            
            var rule = new PropertyValidatorData();
            AddRules(expression.GetName(), new[] { rule });
            return new RuleConfiguration(rule);
        }
        protected void SharedRuleFor(Expression<Func<T, object>> expression, string sharedRuleName)
        {            
            var sharedRules = RuleSet.GetRules(sharedRuleName);
            if (sharedRules == null)
            {
                throw new ArgumentException(string.Format("No shared rule with the name {0} exists", sharedRuleName));
            }
            AddRules(expression.GetName(), sharedRules);            
        }   
        
        private void AddRules(string name, IEnumerable<PropertyValidatorData> rules)
        {
            if (!Data.Rules.ContainsKey(name))
            {
                Data.Rules.Add(name, new List<PropertyValidatorData>(1));
            }
            foreach(var rule in rules)
            {
                Data.Rules[name].Add(rule);
            }
        }
    }
}