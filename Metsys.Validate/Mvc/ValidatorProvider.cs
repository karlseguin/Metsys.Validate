using System.Collections.Generic;
using System.Web.Mvc;

namespace Metsys.Validate.Mvc
{
    public class ValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metaData, ControllerContext context)
        {
            if (metaData.ContainerType != null && !string.IsNullOrEmpty(metaData.PropertyName))
            {
                return GetPropertyValidators(metaData, context);
            }
            return new[] { new MvcModelValidator(metaData, context) };
        }

        private static IEnumerable<ModelValidator> GetPropertyValidators(ModelMetadata metaData, ControllerContext context)
        {
            var rules = Validator.RulesFor(metaData.ContainerType);            
            if (rules == null) { return null; }

            PropertyValidatorData data;
            if (!rules.Rules.TryGetValue(metaData.PropertyName, out data))
            {
                return null;                
            }            
            return YieldValidators(data, metaData, context);
        }

        private static IEnumerable<ModelValidator> YieldValidators(PropertyValidatorData data, ModelMetadata metaData , ControllerContext context)
        {
            foreach(var validator in data.Validators)
            {
                yield return new MvcPropertyValidator(data.Message, validator, metaData, context);
            }
        }
    }
}