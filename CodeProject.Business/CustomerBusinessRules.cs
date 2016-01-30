using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using CodeProject.Business.Entities;
using System.Configuration;
using CodeProject.Interfaces;

namespace CodeProject.Business
{
    public class PersonBusinessRules : AbstractValidator<Person>
    {
      
        public PersonBusinessRules()
        {          
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage("Company Name is required.");
            RuleFor(c => c.PersonCode).NotEmpty().WithMessage("Person Code is required.");      
        }

    }
}
