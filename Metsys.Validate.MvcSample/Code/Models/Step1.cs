namespace Metsys.Validate.MvcSample
{
    public class Step1
    {
        public string Required{ get; set;}
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        public string MinMax { get; set; }
        public string Email { get; set; }        
        public string CreditCard { get; set; }
        public string Url { get; set; }
        public string Number { get; set; }
        public string Digits { get; set; }
        public string Regex { get; set;}
    }

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
            RuleFor(u => u.Regex).Required().Pattern("^(cat|dog)$").WithMessage("Must be 'cat' or 'dog'");
        }
    }
}