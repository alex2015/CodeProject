using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NPA.WEB.Models;
using NPA.Business.Entities;
using NPA.Business;
using Ninject;
using NPA.Data.EntityFramework;

namespace NPA.WEB.WebApiControllers
{
    [RoutePrefix("api/PersonService")]
    public class PersonServiceController : ApiController
    {

        [Inject]
        public IPersonDataService _personDataService { get; set; }

        [Route("CreatePerson")]     
        [HttpPost]
        public HttpResponseMessage CreatePerson(HttpRequestMessage request, [FromBody] PersonViewModel personViewModel)
        {
            TransactionalInformation transaction;

            var person = new Person
            {
                CompanyName = personViewModel.CompanyName,
                Name = personViewModel.Name,
                Country = personViewModel.Country,
                City = personViewModel.City,
                Address = personViewModel.Address,
                MobileNumber = personViewModel.MobileNumber
            };

            if (personViewModel.ImageUrl != null)
            {
                person.Image = Convert.FromBase64String(personViewModel.ImageUrl.Replace("data:image/jpeg;base64,", ""));
            }

            var personBusinessService = new PersonBusinessService(_personDataService);
            personBusinessService.CreatePerson(person, out transaction);
            if (transaction.ReturnStatus == false)
            {                
                personViewModel.ReturnStatus = false;
                personViewModel.ReturnMessage = transaction.ReturnMessage;
                personViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse<PersonViewModel>(HttpStatusCode.BadRequest, personViewModel);
                return responseError;
              
            }

            personViewModel.PersonID = person.PersonID;
            personViewModel.ReturnStatus = true;
            personViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse<PersonViewModel>(HttpStatusCode.OK, personViewModel);
            return response;

        }

        [Route("UpdatePerson")]
        [HttpPost]
        public HttpResponseMessage UpdatePerson(HttpRequestMessage request, [FromBody] PersonViewModel personViewModel)
        {
            TransactionalInformation transaction;

            var person = new Person
            {
                PersonID = personViewModel.PersonID,
                CompanyName = personViewModel.CompanyName,
                Name = personViewModel.Name,
                Country = personViewModel.Country,
                City = personViewModel.City,
                Address = personViewModel.Address,
                MobileNumber = personViewModel.MobileNumber
            };

            if (personViewModel.ImageUrl != null)
            {
                person.Image = Convert.FromBase64String(personViewModel.ImageUrl.Replace("data:image/jpeg;base64,", ""));
            }

            var personBusinessService = new PersonBusinessService(_personDataService);
            personBusinessService.UpdatePerson(person, out transaction);
            if (transaction.ReturnStatus == false)
            {
                personViewModel.ReturnStatus = false;
                personViewModel.ReturnMessage = transaction.ReturnMessage;
                personViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, personViewModel);
                return responseError;

            }

            personViewModel.ReturnStatus = true;
            personViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, personViewModel);
            return response;

        }

        [Route("GetPersons")]
        [HttpPost]
        public HttpResponseMessage GetPersons(HttpRequestMessage request, [FromBody] PersonViewModel personViewModel)
        {

            TransactionalInformation transaction;

            int currentPageNumber = personViewModel.CurrentPageNumber;
            int pageSize = personViewModel.PageSize;
            string sortExpression = personViewModel.SortExpression;
            string sortDirection = personViewModel.SortDirection;

            var personBusinessService = new PersonBusinessService(_personDataService);
            var persons = personBusinessService.GetPersons(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {                
                personViewModel.ReturnStatus = false;
                personViewModel.ReturnMessage = transaction.ReturnMessage;
                personViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, personViewModel);
                return responseError;

            }

            personViewModel.TotalPages = transaction.TotalPages;
            personViewModel.TotalRows = transaction.TotalRows;
            personViewModel.Persons = persons;
            personViewModel.ReturnStatus = true;
            personViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, personViewModel);
            return response;

        }

        [Route("GetPerson")]
        [HttpPost]
        public HttpResponseMessage GetPerson(HttpRequestMessage request, [FromBody] PersonViewModel personViewModel)
        {

            TransactionalInformation transaction;

            int personID = personViewModel.PersonID;
          
            var personBusinessService = new PersonBusinessService(_personDataService);
            var person = personBusinessService.GetPerson(personID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                personViewModel.ReturnStatus = false;
                personViewModel.ReturnMessage = transaction.ReturnMessage;
                personViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, personViewModel);
                return responseError;

            }

            personViewModel.PersonID = person.PersonID;
            personViewModel.CompanyName = person.CompanyName;
            personViewModel.Name = person.Name;
            personViewModel.Country = person.Country;
            personViewModel.City = person.City;
            personViewModel.Address = person.Address;
            personViewModel.MobileNumber = person.MobileNumber;

            if (person.Image != null)
            {
                personViewModel.ImageUrl = string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(person.Image));
            }

            personViewModel.ReturnStatus = true;
            personViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, personViewModel);
            return response;

        }

        [Route("DeletePerson")]
        [HttpPost]
        public HttpResponseMessage DeletePerson(HttpRequestMessage request, [FromBody] PersonViewModel personViewModel)
        {
            TransactionalInformation transaction;

            var personBusinessService = new PersonBusinessService(_personDataService);

            personBusinessService.DeletePerson(personViewModel.PersonID, out transaction);

            if (transaction.ReturnStatus == false)
            {
                personViewModel.ReturnStatus = false;
                personViewModel.ReturnMessage = transaction.ReturnMessage;
                personViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, personViewModel);
                return responseError;
            }

            personViewModel.ReturnStatus = true;
            personViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, personViewModel);
            return response;
        }

        [Route("ActivatePerson")]
        [HttpPost]
        public HttpResponseMessage ActivatePerson(HttpRequestMessage request, [FromBody] PersonActivateModel personActivateModel)
        {
            TransactionalInformation transaction;

            var personBusinessService = new PersonBusinessService(_personDataService);

            personBusinessService.ActivatePerson(personActivateModel.PersonID, personActivateModel.IsActive, out transaction);

            if (transaction.ReturnStatus == false)
            {
                personActivateModel.ReturnStatus = false;
                personActivateModel.ReturnMessage = transaction.ReturnMessage;
                personActivateModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, personActivateModel);
                return responseError;
            }

            personActivateModel.ReturnStatus = true;
            personActivateModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, personActivateModel);
            return response;
        }
    }
}