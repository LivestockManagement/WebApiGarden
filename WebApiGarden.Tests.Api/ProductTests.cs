using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiGarden.Business.Purchases;
using System.Linq;
using System.Text;
using System.Web.Http;
using WebApiGarden.Web.Api.Controllers;
using WebApiGarden.Business.Purchases.Services;
using WebApiGarden.Business.Purchases.Entities;
using WebApiGarden.Web.Api.Models;
using System.Security.Principal;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;

namespace WebApiGarden.Tests.Api
{
    [TestClass]
    public class ProductTests
    {
        private OrderRepository _OrderRepository;
        private IdentityService _IdentityService;
        private OrderController _OrderController;

        [TestInitialize]
        public void Init()
        {
            _OrderRepository = new OrderRepository();
            _IdentityService = new IdentityService();

            var principal = new GenericPrincipal(new GenericIdentity("Username1"), null);
            Thread.CurrentPrincipal = principal;

            _OrderController = new OrderController(_OrderRepository, _IdentityService);

        }

        [TestMethod]
        public void CheckSeedData()
        {
            Assert.IsTrue(_OrderRepository.Orders.Count > 0);
        }

        [TestMethod]
        public void GenerateSignature()
        {
            string appId = "1";
            string secret = "MySecret";
            // Simplistic implementation DO NOT USE
            var key = Convert.FromBase64String(secret);
            var provider = new System.Security.Cryptography.HMACSHA256(key);
            // Compute Hash from API Key (NOT SECURE)
            var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(appId));
            var signature = Convert.ToBase64String(hash);
            // signature = "YM6qwvfzDIn3Uvg3xD0Mg5xo98t0FT7qIQ8/M6D4UPU="
        }

        [TestMethod]
        public void GetOrders()
        {
            // Arrange

            // Act
            List<OrderModel> orders = _OrderController.Get();

            // Assert
            Assert.IsTrue(orders.Count() > 0);
        }


        [TestMethod]
        public void test_httpserver_controller()
        {

        }

    }
}
