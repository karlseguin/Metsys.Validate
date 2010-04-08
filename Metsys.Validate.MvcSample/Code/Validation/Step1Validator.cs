namespace Metsys.Validate.MvcSample.Validation
{
    public class Step1Validator : ClassValidator<Step1>
    {
        public Step1Validator()
        {
            RuleFor(u => u.Required).Required().WithMessage("Please fill this in!");
            RuleFor(u => u.Minimum).Length(3);
            RuleFor(u => u.Maximum).Length(null, 10);
            RuleFor(u => u.MinMax).Length(4, 9);
        }
    }
}