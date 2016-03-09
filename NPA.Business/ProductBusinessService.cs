using System;
using System.Collections.Generic;
using NPA.Business.Entities;
using NPA.Interfaces;
using NPA.Business.Common;

namespace NPA.Business
{
    public class ProductBusinessService
    {
        private readonly IProductDataService _productDataService;

        public ProductBusinessService(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public Product CreateProduct(Product product, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                var productBusinessRules = new ProductBusinessRules();
                var results = productBusinessRules.Validate(product);

                bool validationSucceeded = results.IsValid;
                var failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return product;
                }

                _productDataService.CreateSession();
                _productDataService.BeginTransaction();
                _productDataService.CreateProduct(product);
                _productDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Product successfully created.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _productDataService.CloseSession();
            }

            return product;


        }

        public void UpdateProduct(Product product, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {

                var productBusinessRules = new ProductBusinessRules();
                var results = productBusinessRules.Validate(product);

                bool validationSucceeded = results.IsValid;
                var failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _productDataService.CreateSession();
                _productDataService.BeginTransaction();

                var existingProduct = _productDataService.GetProduct(product.ProductID);

                existingProduct.ProductName = product.ProductName;
                existingProduct.QuantityPerUnit = product.QuantityPerUnit;
                existingProduct.UnitPrice = product.UnitPrice;
             
                _productDataService.UpdateProduct(product);
                _productDataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Product was successfully updated.");

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _productDataService.CloseSession();
            }


        }

        public List<Product> GetProducts(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            var products = new List<Product>();

            try
            {
                int totalRows;

                _productDataService.CreateSession();
                products = _productDataService.GetProducts(currentPageNumber, pageSize, sortExpression, sortDirection, out totalRows);
                _productDataService.CloseSession();

                transaction.TotalPages = Utilities.CalculateTotalPages(totalRows, pageSize);
                transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _productDataService.CloseSession();
            }

            return products;

        }

        public Product GetProduct(int productID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            var product = new Product();

            try
            {

                _productDataService.CreateSession();
                product = _productDataService.GetProduct(productID);
                _productDataService.CloseSession();      
                transaction.ReturnStatus = true;

            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _productDataService.CloseSession();
            }

            return product;
        }
    }
}

