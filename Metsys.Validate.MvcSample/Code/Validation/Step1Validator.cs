namespace Metsys.Validate.MvcSample.Validation
{
    public class Step1Validator : ClassValidator<Step1>
    {
        public Step1Validator()
        {
            RuleFor(u => u.Required).Required().WithMessage("Please fill this in!");
            RuleFor(u => u.Minimum).Required().Length(3);
            RuleFor(u => u.Maximum).Length(null, 10);
            RuleFor(u => u.MinMax).Required().Length(4, 9);           
            RuleFor(u => u.Email).Required().Pattern(ValidationPattern.Email);
            RuleFor(u => u.CreditCard).Required().Pattern(ValidationPattern.CreditCard);
            RuleFor(u => u.Url).Required().Pattern(ValidationPattern.Url);
            RuleFor(u => u.Number).Required().Pattern(ValidationPattern.Number);
            RuleFor(u => u.Digits).Required().Pattern(ValidationPattern.Digits);
        }
    }
}