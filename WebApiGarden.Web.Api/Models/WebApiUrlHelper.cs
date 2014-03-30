using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebApiGarden.Web.Api.Models
{
    public class WebApiUrlHelper : UrlHelper
    {
        public WebApiUrlHelper()
        {
        }

        public WebApiUrlHelper(HttpRequestMessage request)
            : base(request)
        {
        }

        public override string Link(string routeName, IDictionary<string, object> routeValues)
        {
            string link = "";
            if (this.Request != null) {
                link = base.Link(routeName, routeValues);
            }
            return link;
        }

        public override string Link(string routeName, object routeValues)
        {
            string link = "";
            if (this.Request != null)
            {
                link = base.Link(routeName, routeValues);
            }
            return link;
        }
    }
}