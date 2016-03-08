using FluentValidation;
using NPA.Business.Entities;

namespace NPA.Business
{
    public class PersonBusinessRules : AbstractValidator<Person>
    {
      
        public PersonBusinessRules()
        {
            RuleFor(c => c.ContactName).NotEmpty().WithMessage("ContactName is required.");      
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage("Company Name is required.");
        }

    }
}
