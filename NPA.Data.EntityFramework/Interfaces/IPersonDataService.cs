using System;
using System.Collections.Generic;
using NPA.Business.Entities;

namespace NPA.Data.EntityFramework
{
    public interface IPersonDataService : IDataRepository, IDisposable
    {
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        void ActivatePerson(Person person, bool isActive);
        Person GetPerson(int personID);
        List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
    }
}
