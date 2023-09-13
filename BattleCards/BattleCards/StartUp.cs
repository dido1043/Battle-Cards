using Microsoft.EntityFrameworkCore;
using BattleCards.Controllers;
using BattleCards.Data;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleCards.Services;
using BattleCards.Services;

namespace BattleCards
{
    public class StartUp : IMvcApplication
    {

       // List<Route> routeTable = new List<Route>();
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserSevices,UserService>();
            serviceCollection.Add<ICardsService, CardService>();
                
        }
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

    }
}
