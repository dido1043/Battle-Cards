using Microsoft.EntityFrameworkCore;
using Program.Controllers;
using Program.Data;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCards
{
    public class StartUp : IMvcApplication
    {

       // List<Route> routeTable = new List<Route>();
        public void ConfigureServices()
        {

        }
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

    }
}
