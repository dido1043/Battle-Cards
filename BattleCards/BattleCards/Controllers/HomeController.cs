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
            return this.View(viewModel);
        }
        
 
    }
}
