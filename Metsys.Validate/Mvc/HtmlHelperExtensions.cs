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
            return html.RuleFor<T>(null);
        }
        public static string RuleFor<T>(this HtmlHelper html, string prefix)
        {
            var validations = Validator.RulesFor<T>();
            if (validations == null || validations.Rules.Count == 0) { return "{}"; }

            var context = new HtmlRuleContext
                              {
                                  RulesBuilder = new StringBuilder(),                                  
                                  Prefix = prefix,
                              };

            foreach (var kvp in validations.Rules)
            {
                context.Key = kvp.Key;
                context.Data = kvp.Value;
                RuleFor(context);                
                context.RulesBuilder.Append(',');                
            }
            if (context.RulesBuilder.Length > 1)
            {
                context.RulesBuilder.Insert(0, "rules:{");
                context.RulesBuilder.Remove(context.RulesBuilder.Length - 1, 1);
                context.RulesBuilder.Append('}');
            }
            return string.Concat('{', context.RulesBuilder.ToString(), '}');
        }

 
        private static void RuleFor(HtmlRuleContext context)
        {
            var sb = context.RulesBuilder;
            var startPosition = sb.Length;
            foreach(var data in context.Data)
            {
                var found = false;
                var start = sb.Length;
                foreach (var validator in data.Validators)
                {
                    if (!(validator is IDoJavascript)) { continue; }
                    foreach (var property in ((IDoJavascript) validator).ToJson())
                    {
                        sb.AppendFormat("{0}:{1}", property.Key, property.Value);
                        sb.Append(",");
                        found = true;
                    }
                }
                if (found)
                {
                    if (!string.IsNullOrEmpty(data.Message))
                    {
                        sb.AppendFormat("message:'{0}',", JsHelper.Escape(Validator.MessageCallback(data.Message)));
                    }
                    sb.Insert(start, '{');                   
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("},");
                }                
            }
            if (sb.Length > startPosition)
            { 
                sb.Insert(startPosition, string.Concat(SafeKey(context), ":["));
                sb.Remove(sb.Length - 1, 1);
                sb.Append(']');
            }
        
        }        
        private static string SafeKey(HtmlRuleContext context)
        {
            return string.IsNullOrEmpty(context.Prefix) ? context.Key : string.Format("\"{0}.{1}\"", context.Prefix, context.Key);            
        }        

    }
}