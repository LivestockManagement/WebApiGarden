using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApiGarden.Web.Api.Filters;
using WebApiContrib.Formatting.Jsonp;

namespace WebApiGarden.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Product",
                routeTemplate: "api/product/{productId}",
                defaults: new { controller = "Product", productId = RouteParameter.Optional }
                );

            
            config.Routes.MapHttpRoute(
                name: "Order",
                routeTemplate: "api/order/{orderId}",
                defaults: new { controller = "Order", orderId = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "OrderItem",
                routeTemplate: "api/order/{orderId}/item/{orderItemId}",
                defaults: new { controller = "OrderItem", orderItemId = RouteParameter.Optional }
                );

            // Add support for JSONP.
            config.AddJsonpFormatter();
            
            // Dependency Injection
            UnityConfig.RegisterComponents();

#if !DEBUG
            // Force HTTPS / SSL Everywhere!
            config.Filters.Add(new RequireHttpsAttribute());
#endif

        }
    }
}
