namespace Metsys.Validate.MvcSample
{
    public class Step5
    {
        public string Original{ get; set;}
        public string New { get; set; }        
    }

    public class Step5Validator : ClassValidator<Step5>
    {
        public Step5Validator()
        {
            SharedRuleFor(s => s.Original, "Password");
            SharedRuleFor(s => s.New, "Password").WithMessage("Password should be at least 6 characters");         
        }
    }
}