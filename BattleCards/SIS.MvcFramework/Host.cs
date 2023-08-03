namespace SIS.MvcFramework
{
    using SIS.HTTP;
    public class Host
    {
        public static async Task CreateHostAsync(List<Route> routeTable, int port = 8080)
        {
            IHttpServer server = new HttpServer(routeTable);


            await server.StartAsync(8080);
        }
    }
}