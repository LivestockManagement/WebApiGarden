using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Business.Purchases.Entities;
using WebApiGarden.Web.Api.Filters;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    public class ProductV2Controller : BaseApiController
    {
        public ProductV2Controller(OrderRepository orderRepository)
            : base(orderRepository)
        {
        }

        // GET api/product
        public List<ProductV2Model> Get()
        {
            return _OrderRepository.Products
                .Select(x => _ModelFactory.CreateV2(x))
                .ToList();
        }

        // GET api/product/1
        public ProductV2Model Get(int productId)
        {
            return _OrderRepository.Products
                .Where(x => x.Id == productId)
                .Select(x => _ModelFactory.CreateV2(x))
                .Single();
        }

        // POST api/product 
        // Example json [ { "Name": "Pineapple" } ]
        public HttpResponseMessage Post([FromBody]ProductV2Model productModel)
        {

            try
            {
                Product product = _ModelFactory.Parse(productModel);
                _OrderRepository.AddProduct(product);
                ProductV2Model newProductModel = _ModelFactory.CreateV2(product);

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
