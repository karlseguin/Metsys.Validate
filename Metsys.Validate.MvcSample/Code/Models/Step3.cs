namespace Metsys.Validate.MvcSample
{
    public class Step3
    {
        public string Title{ get; set;}
        public string Body{ get; set;}
        public string MessageGroup{ get; set;}
    }

    public class Step3Validator : ClassValidator<Step3>
    {
        public Step3Validator()
        {
            RuleFor(s => s.MessageGroup).Required().WithMessage("This field is required");
            RuleFor(s => s.MessageGroup).Length(5, 5).Pattern(ValidationPattern.Number).WithMessage("Please enter a 5 digit number");

            SharedRuleFor(s => s.Title, "PostTitle");
            SharedRuleFor(s => s.Body, "PostBody");
        }
    }
}