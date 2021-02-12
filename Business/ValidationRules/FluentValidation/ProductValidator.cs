using System;
using System.Data;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Product name must start with 'A'");
            RuleFor(up => up.UnitPrice).NotEmpty();
            RuleFor(up => up.UnitPrice).GreaterThan(0);
            RuleFor(u => u.UnitsInStock).NotEmpty();
        }

        private bool StartWithA(string arg)
        {
            arg.StartsWith("A");
            return true;
        }
    }
}