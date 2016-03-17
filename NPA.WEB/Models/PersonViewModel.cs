using System.Collections.Generic;
using NPA.Business.Entities;

namespace NPA.WEB.Models
{
    public class PersonViewModel : TransactionalInformation
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
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
