using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Metsys.Validate
{
    public static class Validator
    {
        private static readonly IDictionary<Type, ClassValidatorData> _validator = new Dictionary<Type, ClassValidatorData>();
        
        public static void InitializeFromAssembly(Assembly assembly)
        {            
            var types = assembly.GetExportedTypes();

            types.Where(t => typeof (RuleSet).IsAssignableFrom(t))
                .ToList()
                .ForEach(t => Activator.CreateInstance(t));
            
            types.Where(t => typeof (IClassValidator).IsAssignableFrom(t))
                .ToList()
                .ForEach(t => _validator.Add(t.BaseType.GetGenericArguments()[0], ((IClassValidator)Activator.CreateInstance(t)).Data));            
        }
        
        public static ClassValidatorData RulesFor<T>()
        {
            return RulesFor(typeof (T));
        }
        public static ClassValidatorData RulesFor(Type type)
        {
            ClassValidatorData data;
            return !_validator.TryGetValue(type, out data) ? null : data;
        }
    }
}
