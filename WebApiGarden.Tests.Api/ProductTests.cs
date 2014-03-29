using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiGarden.Business.Purchases;
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
            _OrderRepository = new OrderRepository();
        }

        [TestMethod]
        public void CheckSeedData()
        {
            Assert.IsTrue(_OrderRepository.Orders.Count > 0);
        }


    }
}
