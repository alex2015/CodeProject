using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;
using CodeProject.Interfaces;
using CodeProject.Business.Common;

using FluentValidation;
using FluentValidation.Results;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net.Mail;

namespace CodeProject.Business
{
    public class PersonBusinessService
    {
        private IPersonDataService _personDataService;

        /// <summary>
        /// Constructor
        /// </summary>
        public PersonBusinessService(IPersonDataService personDataService)
        {
            _personDataService = personDataService;
        }

        /// <summary>
        /// Create Person
        /// </summary>
        /// <param name="person"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Person CreatePerson(Person person, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                PersonBusinessRules personBusinessRules = new PersonBusinessRules();
                ValidationResult results = personBusinessRules.Validate(person);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

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

        /// <summary>
        /// Update Person
        /// </summary>
        /// <param name="person"></param>
        /// <param name="transaction"></param>
        public void UpdatePerson(Person person, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                PersonBusinessRules personBusinessRules = new PersonBusinessRules();
                ValidationResult results = personBusinessRules.Validate(person);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _personDataService.CreateSession();
                _personDataService.BeginTransaction();

                Person existingPerson = _personDataService.GetPerson(person.PersonID);

                existingPerson.PersonCode = person.PersonCode;
                existingPerson.CompanyName = person.CompanyName;
                existingPerson.ContactName = person.ContactName;
                existingPerson.ContactTitle = person.ContactTitle;
                existingPerson.Address = person.Address;
                existingPerson.City = person.City;
                existingPerson.Region = person.Region;
                existingPerson.PostalCode = person.PostalCode;
                existingPerson.Country = person.Country;
                existingPerson.PhoneNumber = person.PhoneNumber;
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

        /// <summary>
        /// Get Persons
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Person> persons = new List<Person>();

            try
            {
                int totalRows;

                _personDataService.CreateSession();
                persons = _personDataService.GetPersons(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _personDataService.CloseSession();

                transaction.TotalPages = CodeProject.Business.Common.Utilities.CalculateTotalPages(totalRows, pageSize);
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

        /// <summary>
        /// Get Person
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Person GetPerson(int personID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Person person = new Person();

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

        /// <summary>
        /// Delete Person
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Initialize Data
        /// </summary>
        /// <param name="transaction"></param>
        public void InitializeData(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Person person = new Person();

            try
            {

                _personDataService.CreateSession();
                _personDataService.BeginTransaction();
                _personDataService.InitializeData();
                _personDataService.CommitTransaction(true);
                _personDataService.CloseSession();

                _personDataService.CreateSession();
                _personDataService.BeginTransaction();
                _personDataService.LoadData();
                _personDataService.CommitTransaction(true);
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

        }




    }
}
