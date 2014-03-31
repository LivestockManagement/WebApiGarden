using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiGarden.Business.Purchases.Entities;

namespace WebApiGarden.Business.Purchases
{
    public class OrderRepository : IRepository
    {
        public List<Product> Products { get; set; }
        public List<User> Users { get; set; }
        public List<Developer> Developers { get; set; }
        public List<Order> Orders { get; set; }
        public List<AuthToken> AuthTokens { get; set; }

        public OrderRepository()
        {
            Products = new List<Product>();
            Users = new List<User>();
            Developers = new List<Developer>();
            Orders = new List<Order>();
            AuthTokens = new List<AuthToken>();

            Random rand = new Random();

            // Products
            for (int x = 1; x <= 10; x++ )
            {
                AddProduct(new Product() { Name = string.Format("Product {0}", x), Price = x * 1.5M });
            }

            // Users
            for (int x = 1; x <= 10; x++)
            {
                Users.Add(new User() { Id = x, Name = string.Format("Name{0}", x), Username = string.Format("Username{0}", x), Password = string.Format("Password{0}", x) });
            }

            // Developers
            for (int x = 1; x <= 10; x++)
            {
                Developers.Add(new Developer() { Id = x, 
                    AppId = string.Format("{0}", x), 
                    Name = string.Format("Name{0}", x),
                    Secret = "MySecret"
                });
            }

            // Orders
            int orderItemId = 1;
            for (int x = 1; x <= 1000; x++)
            {
                int userId = rand.Next(Users.Count() - 1);
                Order order = new Order() { Id = x, User = Users[userId] };
                for (int y = 1; y <= 10; y++)
                { 
                    int productId = rand.Next(Products.Count() - 1);
                    int quantity = rand.Next(100);
                    order.Items.Add(new OrderItem() { Id = orderItemId, Order = order, Product = Products[productId], Quantity = quantity });
                    orderItemId++;
                }
                Orders.Add(order);
            }
        }

        public Order GetOrder(int orderId, string username)
        {
            return Orders.Where(x => x.Id == orderId && x.User.Username == username).FirstOrDefault();
        }

        public List<Order> GetOrders(string username)
        {
            return Orders.Where(x => x.User.Username == username).ToList();
        }

        public void AddProduct(Product product)
        {
            product.Id = Products.Count() + 1;
            Products.Add(product);
        }

        public AuthToken GetAuthToken(string token)
        {
            return AuthTokens.Where(x => x.Token == token && x.Expiration >= DateTime.Now).FirstOrDefault();
        }
    }
}
