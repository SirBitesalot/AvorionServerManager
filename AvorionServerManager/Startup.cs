using Microsoft.Owin.Security.ApiKey;
using Microsoft.Owin.Security.ApiKey.Contexts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace AvorionServerManager
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var defaultSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                        {
                            new StringEnumConverter{ CamelCaseText = true },
                        }
            };

            JsonConvert.DefaultSettings = () => { return defaultSettings; };

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = defaultSettings;
            config.Formatters.Add( new TextMediaTypeFormatter());
            appBuilder.UseApiKeyAuthentication(new ApiKeyAuthenticationOptions()
            {
                Provider = new ApiKeyAuthenticationProvider()
                {
                    OnValidateIdentity = ValidateIdentity
                }
            });
            appBuilder.UseWebApi(config);
        }
        private async Task ValidateIdentity(ApiKeyValidateIdentityContext context)
        {
            if (ApiKeyController.IsValidKey(context.ApiKey))
            {
                context.Validate();
            }
        }
    }
}
