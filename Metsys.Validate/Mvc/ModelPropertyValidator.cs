using System.Collections.Generic;
using System.Web.Mvc;
using Metsys.Validate.Validators;

namespace Metsys.Validate.Mvc
{  
    internal class ModelPropertyValidator : ModelValidator
    {
        private readonly IValidator _validator;
        private readonly string _message;

        public ModelPropertyValidator(string message, IValidator validator, ModelMetadata metadata, ControllerContext controllerContext)
            : base(metadata, controllerContext)
        {
            _validator = validator;
            _message = message;
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var value = Metadata.Model;
            if (!_validator.IsValid(value, container))
            {
                yield return new ModelValidationResult
                                 {                                     
                                     Message = Validator.MessageCallback(_message)
                                 };
            }
        }       
    }
}