namespace Metsys.Validate.MvcSample
{
    public class Step4
    {
        public string Original{ get; set;}
        public string Compare{ get; set;}        
    }

    public class Step4Validator : ClassValidator<Step4>
    {
        public Step4Validator()
        {
            RuleFor(u => u.Original).Required().WithMessage("Please fill this in!");
            RuleFor(u => u.Compare).EqualTo(s => s.Original).WithMessage("Does not match Original");
        }
    }
}