using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


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
                name: "Order",
                routeTemplate: "api/bulk/order/{orderId}",
                defaults: new { controller = "Orders", orderId = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "OrderItem",
                routeTemplate: "api/bulk/order/{orderId}/item/{orderItemId}",
                defaults: new { controller = "OrderItems", orderItemId = RouteParameter.Optional }
                );

            // JSON? Don't bother. The client can set that in the Header with the [Accept: application/json] attribute. 

            // Dependency Injection
            UnityConfig.RegisterComponents();
        }
    }
}
