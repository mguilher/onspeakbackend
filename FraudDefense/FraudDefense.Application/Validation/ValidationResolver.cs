using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDefense.Application.Validation
{ 
    public delegate IValidation ValidationResolver(ValidationType type);
}
