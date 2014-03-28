using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Products;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    public class OrdersController : ApiController
    {
        private OrderRepository _OrderRepository;
        private ModelFactory _ModelFactory;

        public OrdersController(OrderRepository orderRepository)
        {
            _OrderRepository = orderRepository;
            _ModelFactory = new ModelFactory();
        }

        // GET api/Orders
        public IEnumerable<OrderModel> GetOrders()
        {
            return _OrderRepository.Orders.Select(x => _ModelFactory.Create(x));
        }
    }
}
