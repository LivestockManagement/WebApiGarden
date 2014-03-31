using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiGarden.Business.Purchases.Entities
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public Developer Developer { get; set; }
    }
}
