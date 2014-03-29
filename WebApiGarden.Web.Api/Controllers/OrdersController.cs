using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    public class OrdersController : BaseApiController
    {
        public OrdersController(OrderRepository orderRepository) 
            : base(orderRepository)
        {
        }

        // GET api/Orders
        public List<OrderModel> GetOrders(int minItems = 0, int maxItems = 10)
        {
            var query = _OrderRepository.GetAll();

            return query
                .Where(x => x.OrderItems.Count >= minItems && x.OrderItems.Count <= maxItems)
                .Select(x => _ModelFactory.Create(x))
                .ToList();
        }

        // GET api/Orders
        public OrderModel Get(int orderId)
        {
            return _ModelFactory.Create(_OrderRepository.Get(orderId));
        }
    }
}
