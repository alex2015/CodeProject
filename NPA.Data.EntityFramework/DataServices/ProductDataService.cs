using System.Collections.Generic;
using System.Linq;
using NPA.Business.Entities;
using System.Linq.Dynamic;

namespace NPA.Data.EntityFramework
{
    public class ProductDataService : EntityFrameworkService, IProductDataService
    {
        public void UpdateProduct(Product product)
        {
            
        }

        public void CreateProduct(Product product)
        {
            dbConnection.Products.Add(product);
        }

        public Product GetProduct(int productID)
        {
            var product = dbConnection.Products.FirstOrDefault(c => c.ProductID == productID);
            return product;
        }

        public List<Product> GetProducts(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows)
        {
            sortExpression = sortExpression + " " + sortDirection;

            totalRows = dbConnection.Products.Count();

            return dbConnection.Products.OrderBy(sortExpression).Skip((currentPageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}


