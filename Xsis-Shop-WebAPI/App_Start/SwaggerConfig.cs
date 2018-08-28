using System.Web.Http;
using WebActivatorEx;
using Xsis_Shop_WebAPI;
using Swashbuckle.Application;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Xsis_Shop_WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Xsis_Shop_WebAPI");
                        c.ResolveConflictingActions(apiDescription => apiDescription.First());
                    })
                .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}
