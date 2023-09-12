namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.ComponentModel.DataAnnotations;

    public class UsersController : Controller
    {
        private UserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }

        public HttpResponse Register()
        {
            return this.View();
        }
        [HttpPost("/users/register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassword = this.Request.FormData["confirmPassword"];

            if (password != confirmPassword)
            {
                return this.Error(HttpErrorText.InvalidSamePasswords);
            }

            if (!this.userService.IsUsernameAvailable(username))
            {
                return this.Error(HttpErrorText.InvalidUsername);
            }

            if (!this.userService.IsEmailAvailable(email))
            {
                return this.Error(HttpErrorText.InvalidTakenEmail);
            }

            if (String.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return this.Error(HttpErrorText.InvalidEmail);
            }

            if (password == null || password.Length < 6 || password.Length > 20)
            {
                return this.Error(HttpErrorText.InvalidPassword);
            }

            var userId = this.userService.CreateUser(username,email,password);
            return this.Redirect("/users/login");
        }


        public HttpResponse Login()
        {

            return this.View();
        }

        [HttpPost("/users/login")]
        public HttpResponse DoLogin()
        {
            //read data
            var username = this.Request.FormData["username"];
            var password = this.Request.FormData["password"];
            var userId = this.userService.GetUserId(username, password);
            //check user
            if (userId == null)
            {
                return this.Error(HttpErrorText.InvalidUserError);
            }
            //log user 
            this.SignIn(userId);
            return this.Redirect("/");
        }

        [HttpGet("/logout")]
        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error(HttpErrorText.LogoutError);
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
