using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceSolution.ViewModels.Catolog.Products;
using FluentValidation;

namespace EcommerceSolution.ViewModel.Validator
{
    public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 5);
            RuleFor(x => x.Description).Length(0, 5);
            RuleFor(x => x.IsFeatured);
        }
    }
}
