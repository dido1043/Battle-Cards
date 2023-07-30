using Program.Controllers;
using SIS.HTTP;
using SIS.MvcFramework;


namespace BattleCards
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable= new List<Route>();

           
            routeTable.Add(new Route("/", SIS.HTTP.HttpMethod.Get, new HomeController().HomePage));
            routeTable.Add(new Route("/about", SIS.HTTP.HttpMethod.Get, new HomeController().About));
            routeTable.Add(new Route("/users/login", SIS.HTTP.HttpMethod.Get, new UserController().Login));
            routeTable.Add(new Route("/users/register", SIS.HTTP.HttpMethod.Get, new UserController().Register));
            routeTable.Add(new Route("/cards/add", SIS.HTTP.HttpMethod.Get, new CardsController().Add));
            routeTable.Add(new Route("/cards/all", SIS.HTTP.HttpMethod.Get, new CardsController().All));
            Host.CreateHostAsync(routeTable, 8080);
        }
    }
}