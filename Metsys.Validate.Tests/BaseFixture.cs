using System;
using System.Reflection;
using System.Collections.Generic;

namespace Metsys.Validate.Tests
{    
    public abstract class BaseFixture
    {
        protected BaseFixture()
        {
            typeof(Validator).GetField("_validator", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, new Dictionary<Type, ClassValidatorData>());
            Validator.InitializeFromAssembly(Assembly.GetExecutingAssembly());
        }                
    }
}