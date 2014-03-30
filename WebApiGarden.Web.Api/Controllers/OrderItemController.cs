﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGarden.Business.Purchases.Services;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Business.Purchases.Entities;
using WebApiGarden.Web.Api.Filters;
using WebApiGarden.Web.Api.Models;

namespace WebApiGarden.Web.Api.Controllers
{
    [Authorise] // Requires valid token & authenticated user.
    public class OrderItemController : BaseApiController
    {
        private IdentityService _IdentityService;

        public OrderItemController(OrderRepository orderRepository, IdentityService identityService) 
            : base(orderRepository)
        {
            _IdentityService = identityService;
        }

        public List<OrderItemModel> Get(int orderId)
        {
            Order order = _OrderRepository.GetOrder(orderId, _IdentityService.CurrentUser.Id);

            return order.Items.Select(x => _ModelFactory.Create(x)).ToList();
        }

        public OrderItemModel Get(int orderId, int orderItemId)
        {
            Order order = _OrderRepository.GetOrder(orderId, _IdentityService.CurrentUser.Id);
            OrderItem orderItem = order.Items.Where(x => x.Id == orderItemId).Single();

            return _ModelFactory.Create(orderItem);
        }
    }
}
