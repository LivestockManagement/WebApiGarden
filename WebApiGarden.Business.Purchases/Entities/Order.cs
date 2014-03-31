using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiGarden.Business.Purchases.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}
