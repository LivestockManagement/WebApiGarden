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
        private WebApiUrlHelper _WebApiUrlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            if (request == null)
            {
                _WebApiUrlHelper = new WebApiUrlHelper();
            }
            else
            {
                _WebApiUrlHelper = new WebApiUrlHelper(request);
            }
            
        }

        // Order
        public OrderModel Create(Order order)
        {
            return new OrderModel()
            {
                Id = order.Id,
                User = Create(order.User),
                Items = order.Items.Select(x => Create(x)),
                Url = _WebApiUrlHelper.Link("Order", new { orderId = order.Id })
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
                Url = _WebApiUrlHelper.Link("OrderItem", new { orderItemId = orderItem.Id })
            };
        }

        // Product
        public ProductModel Create(Product product)
        {
            return new ProductModel() { 
                Id = product.Id, 
                Name = product.Name,
                Url = _WebApiUrlHelper.Link("Product", new { productId = product.Id })
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