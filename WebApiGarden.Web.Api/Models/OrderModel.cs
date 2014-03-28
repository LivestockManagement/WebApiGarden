using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGarden.Web.Api.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public IEnumerable<OrderDetailModel> OrderDetailModels { get; set; }
    }
}