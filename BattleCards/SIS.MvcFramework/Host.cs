namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using System.Reflection.Metadata;

    public class Host 
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port = 8080)
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection serviceCollection = new ServiceCollection();



            application.ConfigureServices(serviceCollection);
            application.Configure(routeTable);

            AutoRegisterStaticFiles(routeTable);
            AutoRegisterRoutes(routeTable, application,serviceCollection);
            Console.WriteLine("All registered routes");
            foreach (var route in routeTable)
            {
                Console.WriteLine($"{route.Method} -> {route.Path}");
            }
            IHttpServer server = new HttpServer(routeTable);


            await server.StartAsync(port);
        }

        private static void AutoRegisterRoutes(List<Route> routeTable, IMvcApplication application , IServiceCollection serviceCollection)
        {
            var controllerTypes = application.GetType().Assembly.GetTypes()
                 .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));
            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods().Where(x => x.IsPublic && !x.IsStatic
                && x.DeclaringType == controllerType && !x.IsAbstract && !x.IsConstructor && !x.IsSpecialName);

                foreach (var method in methods)
                {
                    var url = "/" + controllerType.Name.Replace("Controller", String.Empty).ToLower()
                        + "/" + method.Name.ToLower();
                    if (method.Name.ToLower() == "home")
                    {
                        url = "/";
                    }
                    var attribute = method.GetCustomAttributes(false)
                        .Where( x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute)))
                        .FirstOrDefault() as BaseHttpAttribute;
                    
                    var httpMethod = HttpMethod.Get;
                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }
                    if (!String.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }
                    routeTable.Add(new Route(url, httpMethod,(request) =>
                    {
                        var instance = serviceCollection.CreateInstance(controllerType) as Controller;
                        instance.Request = request;
                        var response = method.Invoke(instance, new object [] {  }) as HttpResponse;
                        return response;
                    }));
                }
            }
        }

        private static void AutoRegisterStaticFiles(List<Route> routeTable)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);
            foreach (var staticFile in staticFiles)
            {
                var url = staticFile.Replace("wwwroot", String.Empty)
                    .Replace("\\", "/");

                routeTable.Add(new Route(url, HttpMethod.Get, (request) =>
                {
                    var fileContent = File.ReadAllBytes(staticFile);
                    var fileExt = new FileInfo(staticFile).Extension;
                    var contentType = fileExt switch
                    {
                        ".css" => "text/css",
                        ".html" => "text/html",
                        ".js" => "text/javascript",
                        ".ico" => "image/x-icon",
                        ".jpg" => "image/jpeg",
                        ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        _ => "text/plain",
                    };

                    return new HttpResponse(contentType, fileContent, HttpStatus.OK);
                }));
            }
        }
    }
}