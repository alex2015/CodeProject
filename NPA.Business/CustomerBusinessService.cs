using System;
using System.Collections.Generic;
using NPA.Business.Entities;
using NPA.Business.Common;
using NPA.Data.EntityFramework;

namespace NPA.Business
{
    public class PersonBusinessService
    {
        private readonly IPersonDataService _personDataService;

        public PersonBusinessService(IPersonDataService personDataService)
        {
            _personDataService = personDataService;
        }

        public Person CreatePerson(Person person, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                var personBusinessRules = new PersonBusinessRules();
                var results = personBusinessRules.Validate(person);

                bool validationSucceeded = results.IsValid;
                var failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return person;
                }

                _personDataService.CreateSession();
                _personDataService.BeginTransaction();
                _personDataService.CreatePerson(person);
                _personDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Person successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _personDataService.CloseSession();
            }

            return person;


        }

        public void UpdatePerson(Person person, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                var personBusinessRules = new PersonBusinessRules();
                var results = personBusinessRules.Validate(person);

                bool validationSucceeded = results.IsValid;
                var failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _personDataService.CreateSession();
                _personDataService.BeginTransaction();

                var existingPerson = _personDataService.GetPerson(person.PersonID);

                existingPerson.CompanyName = person.CompanyName;
                existingPerson.Name = person.Name;
                existingPerson.ContactTitle = person.ContactTitle;
                existingPerson.Address = person.Address;
                existingPerson.City = person.City;
                existingPerson.Region = person.Region;
                existingPerson.Country = person.Country;
                existingPerson.MobileNumber = person.MobileNumber;
                existingPerson.Image = person.Image;

                _personDataService.UpdatePerson(person);
                _personDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Person was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _personDataService.CloseSession();
            }


        }

        public List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            var persons = new List<Person>();

            try
            {
                int totalRows;

                _personDataService.CreateSession();
                persons = _personDataService.GetPersons(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _personDataService.CloseSession();

                transaction.TotalPages = Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _personDataService.CloseSession();
            }

            return persons;

        }

        public Person GetPerson(int personID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            var person = new Person();

            try
            {

                _personDataService.CreateSession();
                person = _personDataService.GetPerson(personID);
                _personDataService.CloseSession();      
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _personDataService.CloseSession();
            }

            return person;

        }

        public void DeletePerson(int personID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                _personDataService.CreateSession();
                _personDataService.BeginTransaction();
                
                _personDataService.DeletePerson(_personDataService.GetPerson(personID));
                _personDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Person was successfully deleted.");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _personDataService.CloseSession();
            }
        }

        public void ActivatePerson(int personID, bool isActive, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                _personDataService.CreateSession();
                _personDataService.BeginTransaction();

                _personDataService.ActivatePerson(_personDataService.GetPerson(personID), isActive);
                _personDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add(string.Format("Person was successfully {0}.", isActive ? "activate" : "deactivate"));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _personDataService.CloseSession();
            }
        }
    }
}
