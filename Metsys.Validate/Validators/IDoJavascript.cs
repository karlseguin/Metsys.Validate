using System.Collections.Generic;

namespace Metsys.Validate.Validators
{
    public interface IDoJavascript
    {
        IEnumerable<KeyValuePair<string, string>> ToJson();
    }
}