using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Controllers
{
    public class CardsController :Controller
    {
        public HttpResponse All(HttpRequest request)
        {

            return this.View("View/Cards/All.html");
        }

        public HttpResponse Add(HttpRequest request)
        {

            return this.View("View/Cards/Add.html");
        }
        public HttpResponse Collection(HttpRequest request)
        {

            return this.View("");
        }
    }
}
