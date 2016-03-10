using System;
using System.Collections.Generic;
using System.Collections;

namespace NPA.Business.Entities
{
    public class TransactionalInformation
    {
        public bool ReturnStatus { get; set; }
        public List<string> ReturnMessage { get; set; }
        public Hashtable ValidationErrors { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public bool IsAuthenicated { get; set; }
        public string SortExpression { get; set; }
        public string SortDirection { get; set; }
        public int CurrentPageNumber { get; set; }

        public TransactionalInformation()
        {
            ReturnMessage = new List<string>();
            ReturnStatus = true;
            ValidationErrors = new Hashtable();
            TotalPages = 0;
            TotalPages = 0;
            PageSize = 0;
            IsAuthenicated = false;
        }
    }
}
