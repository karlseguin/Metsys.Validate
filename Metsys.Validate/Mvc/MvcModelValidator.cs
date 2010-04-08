using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Metsys.Validate.Mvc
{
    internal class MvcModelValidator : ModelValidator
    {           
        public MvcModelValidator(ModelMetadata metadata, ControllerContext controllerContext) : base(metadata, controllerContext){}
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            return Enumerable.Empty<ModelValidationResult>();
        }       
    }
}