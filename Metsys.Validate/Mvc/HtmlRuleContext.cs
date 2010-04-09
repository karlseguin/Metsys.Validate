using System.Text;
using System.Collections.Generic;

namespace Metsys.Validate.Mvc
{
    public class HtmlRuleContext
    {
        public string Prefix { get; set; }
        public string Key{ get; set;}
        public StringBuilder RulesBuilder { get; set; }        
        public IList<PropertyValidatorData> Data{ get; set;}
    }
}