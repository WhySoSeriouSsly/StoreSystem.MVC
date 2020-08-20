using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace StoreSystem.Business.ValidationRules
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)//IEntity entity yaparsak dtolara uygulaymayız
        {
            var result = validator.Validate((IValidationContext) entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
