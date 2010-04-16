using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Metsys.Validate.Validators;

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

        protected IRuleConfiguration<T> RuleFor(Expression<Func<T, object>> expression)
        {            
            var rule = new PropertyValidatorData();
            AddRules(expression.GetName(), new[] { rule });
            return new RuleConfiguration<T>(rule);
        }
        protected ISharedRuleConfiguration SharedRuleFor(Expression<Func<T, object>> expression, string sharedRuleName)
        {            
            var sharedRules = RuleSet.GetRules(sharedRuleName);
            if (sharedRules == null)
            {
                throw new ArgumentException(string.Format("No shared rule with the name {0} exists", sharedRuleName));
            }
            var name = expression.GetName();
            AddRules(name, sharedRules);  
            return new SharedRuleConfiguration(s => SetSharedMessage(name, s));          
        }
        
        private void SetSharedMessage(string name, string message)
        {
            var rules = Data.Rules[name];
            for(var i = 0; i< rules.Count; ++i)
            {
                var validators = rules[i].Validators;
                rules[i] = new PropertyValidatorData{Message = message};                
                ((List<IValidator>)rules[i].Validators).AddRange(validators);
            }
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