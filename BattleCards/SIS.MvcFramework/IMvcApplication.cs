namespace SIS.MvcFramework
{
    using SIS.HTTP;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IMvcApplication
    {
        void Configure(List<Route> routes);
        void ConfigureServices(IServiceCollection serviceCollection);

    }
}
