using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using WebApiGarden.Business.Purchases;
using WebApiGarden.Business.Purchases.Entities;

namespace WebApiGarden.Web.Api.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _UrlHelper = new UrlHelper(request);
        }



        // Order
        public OrderModel Create(Order order)
        {
            return new OrderModel()
            {
                Id = order.Id,
                User = Create(order.User),
                OrderItems = order.OrderItems.Select(x => Create(x)),
                Url = _UrlHelper.Link("Order", new { orderId = order.Id })
            };

        }

        // OrderDetail
        public OrderItemModel Create(OrderItem orderItem)
        {
            return new OrderItemModel()
            {
                Id = orderItem.Id,
                Product = Create(orderItem.Product),
                Quantity = orderItem.Quantity,
                Url = _UrlHelper.Link("OrderItem", new { orderId = orderItem.Order.Id, orderItemId = orderItem.Id })
            };
        }

        // Product
        public ProductModel Create(Product product)
        {
            return new ProductModel() { 
                Id = product.Id, 
                Name = product.Name,
                Url = _UrlHelper.Link("Product", new { productId = product.Id })
            };
        }

        // User
        public UserModel Create(User user)
        {
            return new UserModel() { Id = user.Id, Name = user.Name };
        }


        internal Product Parse(ProductModel productModel)
        {
            return new Product()
            {
                Name = productModel.Name
            };
        }

        internal AuthTokenModel Create(AuthToken authToken)
        {
            return new AuthTokenModel()
            {
                Token = authToken.Token,
                Expiration = authToken.Expiration
            };
        }
    }
}