using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Products.Services;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Web.Api.Filters;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    public class OrdersController : BaseApiController
    {
        private IdentityService _IdentityService;

        public OrdersController(OrderRepository orderRepository, IdentityService identityService) 
            : base(orderRepository)
        {
            _IdentityService = identityService;
        }

        // GET api/order
        public List<OrderModel> GetOrders(int minItems = 0, int maxItems = 10)
        {
            var query = _OrderRepository.GetAll();

            return query
                .Where(x => x.OrderItems.Count >= minItems && x.OrderItems.Count <= maxItems)
                .Select(x => _ModelFactory.Create(x))
                .ToList();
        }

        // GET api/order/1
        public OrderModel Get(int orderId)
        {
            return _ModelFactory.Create(_OrderRepository.Get(orderId));
        }

        // POST api/Orders
    }
}
