using System;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using Metsys.Validate.Validators;

namespace Metsys.Validate.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static string RuleFor<T>(this HtmlHelper html)
        {
            var validations = Validator.RulesFor<T>();
            if (validations == null || validations.Rules.Count == 0) { return "{}"; }
            
            var rulesBuilder = new StringBuilder();
            var messagesBuilder = new StringBuilder();

            foreach (var kvp in validations.Rules)
            {
                RuleFor(rulesBuilder, kvp.Key, kvp.Value.Validators);
                MessagesFor(messagesBuilder, kvp.Key, kvp.Value.Message);
                rulesBuilder.Append(',');                
            }
            if (rulesBuilder.Length > 1)
            {
                rulesBuilder.Insert(0, "rules:{");
                rulesBuilder.Remove(rulesBuilder.Length - 1, 1);
                rulesBuilder.Append('}');
            }
            if (messagesBuilder.Length > 1)
            {
                messagesBuilder.Insert(0, ", messages:{");
                messagesBuilder.Remove(messagesBuilder.Length - 1, 1);
                messagesBuilder.Append('}');
            }
            return string.Concat('{', rulesBuilder.ToString(), messagesBuilder.ToString(), '}');
        }

 
        private static void RuleFor(StringBuilder sb , string name, IEnumerable<IValidator> validators)
        {
            var startPosition = sb.Length;
            foreach(var validator in validators)
            {
                if (!(validator is IDoJavascript)) { continue; }
                foreach (var property in ((IDoJavascript)validator).ToJson())
                {
                    sb.AppendFormat("{0}:{1}", property.Key, property.Value);
                    sb.Append(",");
                }                                
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Insert(startPosition, string.Concat(name, ":{"));                
                sb.Append('}');
            }         
        }
        private static void MessagesFor(StringBuilder sb, string key, string message)
        {
            if (string.IsNullOrEmpty(message)) { return; }
            sb.AppendFormat("{0}: '{1}',", key, Escape(message));
        }


        private static string Escape(string @string)
        {
            return string.IsNullOrEmpty(@string) ? string.Empty : @string.Replace("'", "\\'");
        }

    }
}