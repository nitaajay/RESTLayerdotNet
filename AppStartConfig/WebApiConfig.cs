using System;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApiSelfHostApp.Formatters;
using WebApiSelfHostApp.Handlers;

namespace WebApiSelfHostApp.AppStartConfig
{
    public static class WebApiConfig
    {
        public static async Task Register(HttpSelfHostConfiguration httpSelfHostConfiguration)
        {
            var dateFormat = ConfigurationManager.AppSettings["DateFormat"];
            httpSelfHostConfiguration.MapHttpAttributeRoutes();
            httpSelfHostConfiguration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional, action = RouteParameter.Optional });
            httpSelfHostConfiguration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling =
                TypeNameHandling.Auto;
            var formatters = httpSelfHostConfiguration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Culture = new CultureInfo("en-US", true);

            var cultureInfo = new CultureInfo("en-US")
            {
                DateTimeFormat = new DateTimeFormatInfo
                {
                    ShortDatePattern = dateFormat
                }
            };
            settings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = dateFormat,
                Culture = cultureInfo
            });
            httpSelfHostConfiguration.EnableCors();
            httpSelfHostConfiguration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore;
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            httpSelfHostConfiguration.Formatters.Add(jsonFormatter);
            httpSelfHostConfiguration.MessageHandlers.Add(new CorsHandler());
            //httpSelfHostConfiguration.MessageHandlers.Add(new NullValueHandler());
            httpSelfHostConfiguration.Formatters.Add(new BrowserJsonFormatter());
            httpSelfHostConfiguration.MaxReceivedMessageSize = 2147483600;

            var httpSelfHostServer = new HttpSelfHostServer(httpSelfHostConfiguration);

            Console.WriteLine("========================================================================");
            Console.WriteLine(string.Concat("Opening server at: ", httpSelfHostConfiguration.BaseAddress.AbsoluteUri));
            Console.WriteLine("========================================================================");
            Console.WriteLine();
            Console.WriteLine("========================================================================");
            Console.WriteLine(Environment.NewLine + "API Started successfully on port " +
                              httpSelfHostConfiguration.BaseAddress.Port);
            Console.WriteLine(Environment.NewLine +
                              "========================================================================");
            await httpSelfHostServer.OpenAsync().ConfigureAwait(false);
            Console.WriteLine(Environment.NewLine + "Press Enter to quit.");

            Console.WriteLine(Environment.NewLine + "========================================================================");

            Console.WriteLine();
        }
    }
}
