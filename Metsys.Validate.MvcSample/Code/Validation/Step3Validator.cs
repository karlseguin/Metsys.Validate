namespace Metsys.Validate.MvcSample.Validation
{
    public class Step3Validator : ClassValidator<Step3>
    {
        public Step3Validator()
        {
            RuleFor(s => s.MessageGroup).Required().WithMessage("This field is required");
            RuleFor(s => s.MessageGroup).Length(5, 5).Pattern(ValidationPattern.Number).WithMessage("Please enter a 5 digit number"); 
        }
    }
}