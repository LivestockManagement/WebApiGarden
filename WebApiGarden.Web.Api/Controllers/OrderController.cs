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
    [Authorise] // with a valid token & a valid user account you can access this controller.
    public class OrderController : BaseApiController
    {
        private IdentityService _IdentityService;

        public OrderController(OrderRepository orderRepository, IdentityService identityService) 
            : base(orderRepository)
        {
            _IdentityService = identityService;
        }

        // GET api/order
        public List<OrderModel> GetOrders(int minItems = 0, int maxItems = 10)
        {
            return _OrderRepository.GetOrders(_IdentityService.CurrentUser.Id)
                .Where(x => x.OrderItems.Count >= minItems && x.OrderItems.Count <= maxItems)
                .Select(x => _ModelFactory.Create(x))
                .ToList();
        }

        // GET api/order/1
        public OrderModel Get(int orderId)
        {
            Order order = _OrderRepository.GetOrder(_IdentityService.CurrentUser.Id, orderId);

            return _ModelFactory.Create(order);
        }

        // POST api/Orders
    }
}
