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
            var name = expression.GetName();            
            if (!Data.Rules.ContainsKey(name))
            {
                Data.Rules.Add(name, new List<PropertyValidatorData>(1));
            }
            var rules = new PropertyValidatorData();
            Data.Rules[name].Add(rules);
            return new RuleConfiguration(rules);            
        }
    }
}