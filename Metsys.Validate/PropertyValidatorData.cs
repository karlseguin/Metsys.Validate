using System.Collections.Generic;
using Metsys.Validate.Validators;

namespace Metsys.Validate
{   
    public class PropertyValidatorData
    {
        private readonly IList<IValidator> _validators = new List<IValidator>(5);

        public IList<IValidator> Validators
        {
            get
            {
                return _validators;
            }
        }
        public string Message{ get; set;}
    }
}