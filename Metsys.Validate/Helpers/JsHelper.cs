namespace Metsys.Validate
{
    public static class JsHelper
    {
        public static string Escape(string @string)
        {
            return string.IsNullOrEmpty(@string) ? string.Empty : @string.Replace("'", "\\'");
        }
    }
}