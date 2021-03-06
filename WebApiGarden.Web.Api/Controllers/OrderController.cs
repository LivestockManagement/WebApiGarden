﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Business.Purchases.Entities;
using WebApiGarden.Web.Api.Filters;
using WebApiGarden.Web.Api.Models;
using WebApiGarden.Web.Api.Services;

namespace WebApiGarden.Web.Api.Controllers
{
    [Authorise(false)] // Requires token.
    public class OrderController : BaseApiController
    {
        private IdentityService _IdentityService;

        public OrderController(OrderRepository orderRepository, IdentityService identityService) 
            : base(orderRepository)
        {
            _IdentityService = identityService;
        }

        // GET api/order
        public List<OrderModel> Get()
        {
            return _OrderRepository.GetOrders(_IdentityService.CurrentUser)
                .Select(x => _ModelFactory.Create(x))
                .ToList();
        }

        // GET api/order/1
        public OrderModel Get(int orderId)
        {
            Order order = _OrderRepository.GetOrder(orderId, _IdentityService.CurrentUser);

            return _ModelFactory.Create(order);
        }

        // POST api/Orders
    }
}
