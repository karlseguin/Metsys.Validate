using System;
using System.Linq.Expressions;

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
            var rules = new PropertyValidatorData();
            Data.Rules.Add(expression.GetName(), rules);
            return new RuleConfiguration(rules);            
        }
    }
}