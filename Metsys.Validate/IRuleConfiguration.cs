using System;
using System.Linq.Expressions;
using Metsys.Validate.Validators;
using System.Text.RegularExpressions;

namespace Metsys.Validate
{
    public interface IRuleConfiguration
    {
        IRuleConfiguration Required();
        IRuleConfiguration Length(int minimum);
        IRuleConfiguration Length(int? minimum, int? maximum);
        IRuleConfiguration Pattern(ValidationPattern pattern);
        IRuleConfiguration Pattern(string pattern);
        IRuleConfiguration Pattern(Regex pattern);
        IRuleConfiguration WithMessage(string message);
        IRuleConfiguration EqualTo<T>(Expression<Func<T, object>> property);
        
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

        public IRuleConfiguration Pattern(ValidationPattern pattern)
        {
            _data.Validators.Add(new CannedPatternValidator(pattern));
            return this;
        }

        public IRuleConfiguration Pattern(string pattern)
        {
            return Pattern(new Regex(pattern, RegexOptions.IgnoreCase));
        }

        public IRuleConfiguration Pattern(Regex pattern)
        {
            _data.Validators.Add(new RegularExpressionValidator(pattern));
            return this;
        }

        public IRuleConfiguration WithMessage(string message)
        {            
            _data.Message = message;
            return this;
        }

        public IRuleConfiguration EqualTo<T>(Expression<Func<T, object>> property)
        {
            _data.Validators.Add(new PropertyEqualityValidator<T>(property));
            return this;
        }
    }
}