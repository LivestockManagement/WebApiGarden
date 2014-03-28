using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGarden.Web.Api.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public ProductModel ProductModel { get; set; }
    }
}