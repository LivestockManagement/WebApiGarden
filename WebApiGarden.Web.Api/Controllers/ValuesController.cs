using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiGarden.Web.Api.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        { 
            return new string[] { "Values 1", "Values 2"};
        }
    }
}
