using System;
using Metsys.Validate.Validators;

namespace Metsys.Validate
{
    public interface IRuleConfiguration
    {
        IRuleConfiguration Required();
        IRuleConfiguration Length(int minimum);
        IRuleConfiguration Length(int? minimum, int? maximum);
        IRuleConfiguration WithMessage(string message);
    }
    
    public class RuleConfiguration : IRuleConfiguration
    {
        private readonly PropertyValidatorData _data;

        internal RuleConfiguration(PropertyValidatorData data)
        {
            _data = data;
        }

        public IRuleConfiguration Required()
        {
            _data.Validators.Add(new RequiredValidator());
            return this;
        }

        public IRuleConfiguration Length(int minimum)
        {
            return Length(minimum, null);
        }

        public IRuleConfiguration Length(int? minimum, int? maximum)
        {
            _data.Validators.Add(new StringLengthValidator(minimum, maximum));
            return this;
        }

        public IRuleConfiguration WithMessage(string message)
        {            
            _data.Message = message;
            return this;
        }
    }
}