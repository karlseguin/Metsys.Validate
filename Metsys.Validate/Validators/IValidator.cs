namespace Metsys.Validate.Validators
{
    public interface IValidator
    {
        bool IsValid(object value, object container);  
    }
}