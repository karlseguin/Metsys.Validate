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
    }
    public interface IRuleConfiguration<T>
    {
        IRuleConfiguration<T> EqualTo(Expression<Func<T, object>> property);
        IRuleConfiguration<T> Required();
        IRuleConfiguration<T> Length(int minimum);
        IRuleConfiguration<T> Length(int? minimum, int? maximum);
        IRuleConfiguration<T> Pattern(ValidationPattern pattern);
        IRuleConfiguration<T> Pattern(string pattern);
        IRuleConfiguration<T> Pattern(Regex pattern);
        IRuleConfiguration<T> WithMessage(string message);        
    }
    
    public class RuleConfiguration : IRuleConfiguration
    {
        protected readonly PropertyValidatorData _data;

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
    }
    
    public sealed class RuleConfiguration<T> : RuleConfiguration, IRuleConfiguration<T>
    {
        internal RuleConfiguration(PropertyValidatorData data) : base(data){}
        
        public IRuleConfiguration<T> EqualTo(Expression<Func<T, object>> property)
        {
            _data.Validators.Add(new PropertyEqualityValidator<T>(property));
            return this;
        }

        public new IRuleConfiguration<T> Required()
        {
            base.Required();
            return this;
        }

        public new IRuleConfiguration<T> Length(int minimum)
        {            
            return Length(minimum, null);
        }

        public new IRuleConfiguration<T> Length(int? minimum, int? maximum)
        {
            base.Length(minimum, maximum);            
            return this;
        }

        public new IRuleConfiguration<T> Pattern(ValidationPattern pattern)
        {
            base.Pattern(pattern);
            return this;
        }

        public new IRuleConfiguration<T> Pattern(string pattern)
        {
            base.Pattern(pattern);
            return this;
        }

        public new IRuleConfiguration<T> Pattern(Regex pattern)
        {
            base.Pattern(pattern);            
            return this;
        }

        public new IRuleConfiguration<T> WithMessage(string message)
        {
            base.WithMessage(message);
            return this;
        }                       
    }
}