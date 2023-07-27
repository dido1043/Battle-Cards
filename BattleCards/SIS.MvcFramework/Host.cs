using SIS.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public class Host
    {
        public static async Task CreateHostAsync(List<Route> routeTable, int port = 8080)
        {
            IHttpServer server = new HttpServer(routeTable);

            
            await server.StartAsync(8080);
        }
    }
}
