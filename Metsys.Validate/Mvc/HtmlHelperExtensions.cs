using System.Text;
using System.Web.Mvc;
using Metsys.Validate.Validators;

namespace Metsys.Validate.Mvc
{
    using System;
    using System.Collections.Generic;

    public static class HtmlHelperExtensions
    {
        public static string RuleFor<T>(this HtmlHelper html)
        {
            return html.RuleFor<T>(null, false);
        }
        public static string RuleFor<T>(this HtmlHelper html, bool includeServerSide)
        {
            return html.RuleFor<T>(null, includeServerSide);
        }
        public static string RuleFor<T>(this HtmlHelper html, string prefix)
        {
            return RuleFor<T>(html, prefix, false);
        }
        public static string RuleFor<T>(this HtmlHelper html, string prefix, bool includeServerSide)
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
            if (includeServerSide)
            {
                IncludeModelStateError(html.ViewData.ModelState, context);
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
        
        private static void IncludeModelStateError(IEnumerable<KeyValuePair<string, ModelState>> dictionary, HtmlRuleContext context)
        {
            var sb = context.RulesBuilder;
            var start = sb.Length;
            foreach(var kvp in dictionary)
            {
                if (kvp.Value.Errors.Count == 0){ continue; }
                context.Key = kvp.Key;
                sb.AppendFormat("{0}: '{1}',", SafeKey(context), JsHelper.Escape(Validator.MessageCallback(kvp.Value.Errors[0].ErrorMessage)));
            }
            if (sb.Length > start)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Insert(start, ", init:{");
                sb.Append('}');
            }
        }
        private static string SafeKey(HtmlRuleContext context)
        {
            return string.IsNullOrEmpty(context.Prefix) ? context.Key : string.Format("\"{0}.{1}\"", context.Prefix, context.Key);
        }

    }
}