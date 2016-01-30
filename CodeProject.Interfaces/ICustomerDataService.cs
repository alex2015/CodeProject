using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    /// <summary>
    /// Person Data Service
    /// </summary>
    public interface IPersonDataService : IDataRepository, IDisposable
    {
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        Person GetPerson(int personID);
        List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        void InitializeData();
        void LoadData();  
    }
}
