using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiGarden.Business.Purchases;
using System.Linq;
using System.Text;

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

    }
}
