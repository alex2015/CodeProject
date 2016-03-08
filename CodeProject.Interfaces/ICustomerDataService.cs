﻿using System;
using System.Collections.Generic;
using CodeProject.Business.Entities;

namespace CodeProject.Interfaces
{
    public interface IPersonDataService : IDataRepository, IDisposable
    {
        void CreatePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        void ActivatePerson(Person person, bool isActive);
        Person GetPerson(int personID);
        List<Person> GetPersons(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
        void InitializeData();
        void LoadData();  
    }
}
