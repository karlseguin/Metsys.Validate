namespace Metsys.Validate.Validators
{
    internal class ValidationHelper
    {
        public static bool IsNumber(object o)
        {
            return o is int || o is double || o is decimal || o is float || o is long || o is short;
        }
    }
}