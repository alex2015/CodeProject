using System.Net;
using System.Net.Http;
using System.Web.Http;
using NPA.WEB.Models;
using NPA.Business.Entities;
using NPA.Business;
using NPA.Interfaces;
using Ninject;

namespace NPA.WEB.WebApiControllers
{
    [RoutePrefix("api/ProductService")]
    public class ProductServiceController : ApiController
    {

        [Inject]
        public IProductDataService _productDataService { get; set; }

        [Route("CreateProduct")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(HttpRequestMessage request, [FromBody] ProductViewModel productViewModel)
        {
            TransactionalInformation transaction;

            var product = new Product
            {
                ProductName = productViewModel.ProductName,
                QuantityPerUnit = productViewModel.QuantityPerUnit,
                UnitPrice = productViewModel.UnitPrice
            };


            var productBusinessService = new ProductBusinessService(_productDataService);
            productBusinessService.CreateProduct(product, out transaction);
            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;

            }

            productViewModel.ProductID = product.ProductID;
            productViewModel.ReturnStatus = true;
            productViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, productViewModel);
            return response;

        }

        [Route("UpdateProduct")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(HttpRequestMessage request, [FromBody] ProductViewModel productViewModel)
        {
            TransactionalInformation transaction;

            var product = new Product
            {
                ProductID = productViewModel.ProductID,
                ProductName = productViewModel.ProductName,
                QuantityPerUnit = productViewModel.QuantityPerUnit,
                UnitPrice = productViewModel.UnitPrice
            };


            var productBusinessService = new ProductBusinessService(_productDataService);
            productBusinessService.UpdateProduct(product, out transaction);
            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;

            }

            productViewModel.ReturnStatus = true;
            productViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, productViewModel);
            return response;

        }

        [Route("GetProducts")]
        [HttpPost]
        public HttpResponseMessage GetProducts(HttpRequestMessage request, [FromBody] ProductViewModel productViewModel)
        {
            TransactionalInformation transaction;

            var currentPageNumber = productViewModel.CurrentPageNumber;
            var pageSize = productViewModel.PageSize;
            var sortExpression = productViewModel.SortExpression;
            var sortDirection = productViewModel.SortDirection;

            var productBusinessService = new ProductBusinessService(_productDataService);
            var products = productBusinessService.GetProducts(currentPageNumber, pageSize, sortExpression, sortDirection, out transaction);
            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }

            productViewModel.TotalPages = transaction.TotalPages;
            productViewModel.TotalRows = transaction.TotalRows;
            productViewModel.Products = products;
            productViewModel.ReturnStatus = true;
            productViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, productViewModel);
            return response;

        }

        [Route("GetProduct")]
        [HttpPost]
        public HttpResponseMessage GetProduct(HttpRequestMessage request, [FromBody] ProductViewModel productViewModel)
        {
            TransactionalInformation transaction;

            var productID = productViewModel.ProductID;

            var productBusinessService = new ProductBusinessService(_productDataService);
            var product = productBusinessService.GetProduct(productID, out transaction);
            if (transaction.ReturnStatus == false)
            {
                productViewModel.ReturnStatus = false;
                productViewModel.ReturnMessage = transaction.ReturnMessage;
                productViewModel.ValidationErrors = transaction.ValidationErrors;

                var responseError = Request.CreateResponse(HttpStatusCode.BadRequest, productViewModel);
                return responseError;
            }

            productViewModel.ProductID = product.ProductID;
            productViewModel.ProductName = product.ProductName;
            productViewModel.QuantityPerUnit = product.QuantityPerUnit;
            productViewModel.UnitPrice = product.UnitPrice;
          
            productViewModel.ReturnStatus = true;
            productViewModel.ReturnMessage = transaction.ReturnMessage;

            var response = Request.CreateResponse(HttpStatusCode.OK, productViewModel);
            return response;
        }
    }
}