using Program.Controllers;

namespace BattleCards
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    public class Program
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable= new List<Route>();

           
            routeTable.Add(new Route("/", SIS.HTTP.HttpMethod.Get, new HomeController().HomePage));
            routeTable.Add(new Route("/users/login", SIS.HTTP.HttpMethod.Get, new UserController().Login));
            routeTable.Add(new Route("/users/login", SIS.HTTP.HttpMethod.Post,new UserController().DoLogin));
            routeTable.Add(new Route("/users/register", SIS.HTTP.HttpMethod.Get, new UserController().Register));
            routeTable.Add(new Route("/cards/add", SIS.HTTP.HttpMethod.Get, new CardsController().Add));
            routeTable.Add(new Route("/cards/all", SIS.HTTP.HttpMethod.Get, new CardsController().All));

            routeTable.Add(new Route("/favicon.ico", SIS.HTTP.HttpMethod.Get, new StaticFilesController().Favicon));
            routeTable.Add(new Route("/css/main", SIS.HTTP.HttpMethod.Get, new StaticFilesController().MainCss));
            Host.CreateHostAsync(routeTable, 8080);
        }
    }
}