namespace Metsys.Validate.MvcSample.Validation
{
    public sealed class SharedRules : RuleSet
    {
        public SharedRules()
        {
            Create("PostTitle").Required().WithMessage("Please enter a title");
            
            Create("PostBody").Required().WithMessage("Please enter a body");
            Create("PostBody").Length(2, 4000).WithMessage("Post must be 2-4000 characters");
        }
    }
}