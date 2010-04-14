using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Metsys.Validate.Mvc
{
    public class ValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metaData, ControllerContext context)
        {
            if (metaData.ContainerType != null && !string.IsNullOrEmpty(metaData.PropertyName))
            {
                var rules = Validator.RulesFor(metaData.ContainerType);
                if (rules == null) { return Enumerable.Empty<ModelValidator>(); }

                IList<PropertyValidatorData> data;
                if (!rules.Rules.TryGetValue(metaData.PropertyName, out data))
                {
                    return Enumerable.Empty<ModelValidator>();
                }
                return YieldValidators(data, metaData, context);
            }
            return Enumerable.Empty<ModelValidator>();
        }

        private static IEnumerable<ModelValidator> YieldValidators(IEnumerable<PropertyValidatorData> data, ModelMetadata metaData, ControllerContext context)
        {
            foreach(var d in data)
            {
                foreach (var validator in d.Validators)
                {
                    yield return new ModelPropertyValidator(d.Message, validator, metaData, context);                    
                }
            }
        }   
    }
}