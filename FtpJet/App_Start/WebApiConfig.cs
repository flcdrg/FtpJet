using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NodaTime.Serialization.JsonNet;

namespace FtpJet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            NodaTime.IDateTimeZoneProvider provider = NodaTime.DateTimeZoneProviders.Tzdb;
            config.Formatters.JsonFormatter.SerializerSettings.ConfigureForNodaTime(provider);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
