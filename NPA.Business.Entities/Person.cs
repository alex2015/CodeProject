namespace NPA.Business.Entities
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
	    public string CompanyName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
	    public string Address { get; set; }
        public string MobileNumber { get; set; }
        public byte[] Image { get; set; }
        public bool IsActive { get; set; }
    }
}
