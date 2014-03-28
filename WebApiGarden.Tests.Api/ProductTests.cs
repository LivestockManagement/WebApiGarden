using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiGarden.Business.Products;
using System.Linq;

namespace WebApiGarden.Tests.Api
{
    [TestClass]
    public class ProductTests
    {
        private OrderRepository _OrderRepository;
        
        [TestInitialize]
        public void Init()
        {
            Order order = new Order() { Customer = "John" };

            Product product = new Product() { Name = "Apples", Price = 66.33M };

            OrderDetail orderDetail = new OrderDetail() { Order = order, Product = product, Quantity = 934 };

            _OrderRepository.Orders.Add(order);
        }

        [TestMethod]
        public void CheckSeedData()
        {
            Assert.IsTrue(_OrderRepository.Orders.Count > 0);
        }


    }
}
