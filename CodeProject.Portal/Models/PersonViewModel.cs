using System.Collections.Generic;
using CodeProject.Business.Entities;

namespace CodeProject.Portal.Models
{
    public class PersonViewModel : TransactionalInformation
    {
        public int PersonID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public byte[] Image { get; set; }
        public string ImageUrl { get; set; }

        public List<Person> Persons { get; set; }

    }

    public class PersonActivateModel : TransactionalInformation
    {
        public int PersonID { get; set; }
        public bool IsActive { get; set; }
    }

}
