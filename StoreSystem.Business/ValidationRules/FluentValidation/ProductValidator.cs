using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotNull().WithMessage(" Product Name Bos Gecilemez");
            RuleFor(x => x.ProductName).Length(4, 20).WithMessage("4 ve 20 karakter uzunluğunda olmalı.");
            RuleFor(x => x.UnitPrice).NotNull().WithMessage("UnitPrice Bos Gecilemez");
            RuleFor(x => x.UnitsInStock).NotNull().WithMessage("UnitsInStock Bos Gecilemez");

        }
    }
    
}
