namespace CodeProject.Business.Entities
{
    public class Person
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

    }
}
