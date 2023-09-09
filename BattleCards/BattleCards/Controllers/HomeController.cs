namespace BattleCards.Controllers
{
    using BattleCards.ViewModels;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Text;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Home()
        {
            var viewModel = new HomeViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Battle Cards";
            if (this.IsUserSignedIn())
            {
                viewModel.Message += " Hello user!";
            }
            return this.View(viewModel);
        }
       
        public HttpResponse About()
        {
            
            this.SignIn("Dido");
            //if (this.IsUserSignedIn())
            //{
            //    return this.View();
            //}
            return this.View();
        }
 
    }
}
