using System.Collections.Generic;
using NPA.Business.Entities;
using FluentValidation.Results;

namespace NPA.Utilities
{
    public static class ValidationErrors
    {
        public static TransactionalInformation PopulateValidationErrors(IList<ValidationFailure> failures)
        {
            var transaction = new TransactionalInformation {ReturnStatus = false};

            foreach (var error in failures)
            {
                if (transaction.ValidationErrors.ContainsKey(error.PropertyName) == false)
                {
                    transaction.ValidationErrors.Add(error.PropertyName, error.ErrorMessage);
                }

                transaction.ReturnMessage.Add(error.ErrorMessage);                       
            }

            return transaction;
        }
    }
}
