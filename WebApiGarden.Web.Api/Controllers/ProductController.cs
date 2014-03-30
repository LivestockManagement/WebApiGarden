using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Products.Services;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Business.Purchases.Entities;
using WebApiGarden.Web.Api.Filters;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    [Authorise(false)] // with a valid token anyone can access this controller.
    public class ProductController : BaseApiController
    {
        public ProductController(OrderRepository orderRepository)
            : base(orderRepository)
        {
        }

        // GET api/product
        public List<ProductModel> Get()
        {
            return _OrderRepository.Products
                .Select(x => _ModelFactory.Create(x))
                .ToList();
        }

        // GET api/product/1
        public ProductModel Get(int productId)
        {
            return _OrderRepository.Products
                .Where(x => x.Id == productId)
                .Select(x => _ModelFactory.Create(x))
                .Single();
        }

        // POST api/product 
        // Example json [ { "Name": "Pineapple" } ]
        public HttpResponseMessage Post([FromBody]ProductModel productModel)
        {

            try
            {
                Product product = _ModelFactory.Parse(productModel);
                _OrderRepository.Add(product);
                ProductModel newProductModel = _ModelFactory.Create(product);

                return Request.CreateResponse(HttpStatusCode.Created, newProductModel);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to save Product.");
        }
    }
}
