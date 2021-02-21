using System;
using System.Data;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.ShipCity).NotEmpty();
            RuleFor(o => o.ShipCity).Equal("New York");

            RuleFor(o => o.OrderDate).NotEmpty();
            RuleFor(o => o.OrderDate).GreaterThan(new DateTime(2021, 02, 15));
            RuleFor(o => o.OrderDate).LessThan(new DateTime(2021, 03, 15));

        }
    }
}