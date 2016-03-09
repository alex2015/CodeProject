using FluentValidation;
using NPA.Business.Entities;

namespace NPA.Business
{
    public class ProductBusinessRules : AbstractValidator<Product>
    {
        public ProductBusinessRules()
        {          
             RuleFor(p => p.ProductName).NotEmpty().WithMessage("Product Name is required.");
        }
    }
}
