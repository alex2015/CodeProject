using System.Collections.Generic;
using System.Linq;
using NPA.Business.Entities;
using System.Linq.Dynamic;

namespace NPA.Data.EntityFramework
{
    public class PersonDataService : EntityFrameworkService, IPersonDataService
    {
        public void UpdatePerson(Person person)
        {

        }

        public void CreatePerson(Person person)
        {
            dbConnection.Persons.Add(person);
        }

        public void ActivatePerson(Person person, bool isActive)
        {
            person.IsActive = isActive;
        }

        public Person GetPerson(int personID)
        {
            return dbConnection.Persons.FirstOrDefault(c => c.PersonID == personID);
        }

        public List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Persons.Count();

            return dbConnection.Persons.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public void DeletePerson(Person person)
        {
            dbConnection.Persons.Remove(person);
        }
    }
}

