﻿using System;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
namespace WebApiGarden.Web.Api.Filters
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            var request = actionContext.Request;
            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "<p>Https is required.</p>";
                if (request.Method.Method == "GET")
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                    UriBuilder uriBuilder = new UriBuilder(request.RequestUri);
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 443;
                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }
            }
        }
    }
}