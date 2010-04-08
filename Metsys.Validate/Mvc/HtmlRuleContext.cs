using System.Text;

namespace Metsys.Validate.Mvc
{
    public class HtmlRuleContext
    {
        public string Prefix { get; set; }
        public string Key{ get; set;}
        public StringBuilder RulesBuilder { get; set; }
        public StringBuilder MessageBuilder { get; set; } 
        public PropertyValidatorData Data{ get; set;}
    }
}