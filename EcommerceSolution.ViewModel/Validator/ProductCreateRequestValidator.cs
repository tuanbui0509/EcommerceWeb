using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.ViewModels.Catolog.Products;
using FluentValidation;

namespace EcommerceSolution.ViewModel.Validator
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(x => x.Name).Length(0, 5);
            RuleFor(x => x.Stock).ExclusiveBetween(0, Int32.MaxValue);
            RuleFor(x => x.Description).Length(0, 5);
            RuleFor(x => x.Price).ExclusiveBetween(0, Int32.MaxValue);
            RuleFor(x => x.OriginalPrice).ExclusiveBetween(0, Int32.MaxValue);
        }
    }
}
