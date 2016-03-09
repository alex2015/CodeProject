using NPA.Business.Entities;
using System.Collections.Generic;

namespace NPA.WEB.Models
{
    public class ProductViewModel : TransactionalInformation
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
       
        public List<Product> Products { get; set; }
    }
}

