using System;
using System.Collections.Generic;
using NPA.Business.Entities;

namespace NPA.Interfaces
{
    public interface IProductDataService : IDataRepository, IDisposable
    {
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        Product GetProduct(int productID);
        List<Product> GetProducts(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out int totalRows);
          
    }
}

