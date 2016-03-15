using FluentValidation;
using NPA.Business.Entities;

namespace NPA.Business
{
    public class PersonBusinessRules : AbstractValidator<Person>
    {
      
        public PersonBusinessRules()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");      
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage("Company Name is required.");
        }

    }
}
