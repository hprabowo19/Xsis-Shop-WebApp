using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Xsis_Shop_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Method_1_Parameter",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Method_2_Parameter",
                routeTemplate: "api/{controller}/{action}/{id}/{id2}",
                defaults: new { action = "get", id = RouteParameter.Optional, id2 = RouteParameter.Optional }
            );
        }
    }
}
