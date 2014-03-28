using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiGarden.Business.Products;

namespace WebApiGarden.Web.Api.Models
{
    public class ModelFactory
    {
        // Product
        public ProductModel Create(Product product)
        {
            return new ProductModel() { Id = product.Id, Name = product.Name };
        }

        // Order
        public OrderModel Create(Order order)
        {
            return new OrderModel() { 
                Id = order.Id, 
                Customer = order.Customer, 
                OrderDetailModels = order.OrderDetails.Select(x => Create(x))
            };

        }

        // OrderDetail
        public OrderDetailModel Create(OrderDetail orderDetail)
        {
            return new OrderDetailModel() { 
                Id = orderDetail.Id,
                ProductModel = Create(orderDetail.Product),
                Quantity = orderDetail.Quantity
            };
        }

    }
}