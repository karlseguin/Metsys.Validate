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
            assembly.GetExportedTypes().Where(t => typeof (IClassValidator).IsAssignableFrom(t))
                .ToList()
                .ForEach(t => _validator.Add(t.BaseType.GetGenericArguments()[0], ((IClassValidator)Activator.CreateInstance(t)).Data));            
        }
        
        public static ClassValidatorData RulesFor<T>()
        {           
            ClassValidatorData data;
            return !_validator.TryGetValue(typeof(T), out data) ? null : data;
        }
    }
}
