using Program.Controller;
using SIS.HTTP;

namespace BattleCards
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();
            server.AddRoute("/", new HomeController().HomePage);
            server.AddRoute("/about", new HomeController().About);
            server.AddRoute("/users/login", new UserController().Login);
            server.AddRoute("/users/register", new UserController().Register);
            server.StartAsync(8080);
        }
    }
}