using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApiGarden.Web.Api.Services
{
    // Overwrite the default controller selector defined by the routing config.
    public class ControllerSelector : DefaultHttpControllerSelector
    {
        private HttpConfiguration _HttpConfiguration;
        public ControllerSelector(HttpConfiguration httpConfiguration)
            : base(httpConfiguration)
        {
            _HttpConfiguration = httpConfiguration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();

            var routeData = request.GetRouteData();

            var controllerName = (string)routeData.Values["controller"];

            HttpControllerDescriptor descriptor;

            if (string.IsNullOrWhiteSpace(controllerName))
            {
                return base.SelectController(request);
            }
            else if (controllers.TryGetValue(controllerName, out descriptor))
            {
                //var version = GetVersionFromQueryString(request);
                var version = GetVersionFromHeader(request);
                //var version = GetVersionFromAcceptHeaderVersion(request);
                // var version = GetVersionFromMediaType(request);

                var newName = string.Concat(controllerName, "V", version);

                HttpControllerDescriptor versionedDescriptor;

                if (controllers.TryGetValue(newName, out versionedDescriptor))
                {
                    return versionedDescriptor;
                }

                return descriptor;
            }

            return null;
        }

        private string GetVersionFromHeader(HttpRequestMessage request)
        {
            const string HEADER_NAME = "X-Version";

            if (request.Headers.Contains(HEADER_NAME))
            {
                var header = request.Headers.GetValues(HEADER_NAME).FirstOrDefault();
                if (header != null)
                {
                    return header;
                }
            }

            return "1";
        }
    }
}