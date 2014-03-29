using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGarden.Web.Api.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public string Url { get; set; }

        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}