using BattleCards.Controllers;

namespace BattleCards
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    public class Program
    {
        static async Task Main(string[] args)
        {
            //List<Route> routeTable= new List<Route>();

           
            await Host.CreateHostAsync(new StartUp(), 8080);
        }
    }
}