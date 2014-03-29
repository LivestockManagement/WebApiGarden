using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiGarden.Web.Api.Filters;


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
                defaults: new { controller = "Products", productId = RouteParameter.Optional }
                );

            
            config.Routes.MapHttpRoute(
                name: "Order",
                routeTemplate: "api/order/{orderId}",
                defaults: new { controller = "Orders", orderId = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "OrderItem",
                routeTemplate: "api/order/{orderId}/item/{orderItemId}",
                defaults: new { controller = "OrderItems", orderItemId = RouteParameter.Optional }
                );



            // JSON? Don't bother. The client can set that in the Header with the [Accept: application/json] attribute. 

            // Dependency Injection
            UnityConfig.RegisterComponents();

#if !DEBUG
            // Force HTTPS / SSL Everywhere!
            config.Filters.Add(new RequireHttpsAttribute());
#endif

        }
    }
}
