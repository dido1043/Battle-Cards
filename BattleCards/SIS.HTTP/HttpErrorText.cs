using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.HTTP
{
    public static class HttpErrorText
    {
        public const string InvalidTextLength = "Name cannot be less than 5 or bigger than 15 symbols.";
        public const string InvalidAttack = "Attack cannot be negative!";
        public const string InvalidHealth = "Health cannot be negative!";

        public const string TryAgain = "Try again!";

        public const string InvalidUserError = "Invalid user";
        public const string InvalidSamePasswords = "Passwords should be same!";
        public const string InvalidUsername = "Username already taken!";
        public const string InvalidTakenEmail = "Email already taken!";
        public const string InvalidPassword = "Password cannot be null, smaller than 6 symbols or bigger than 20 symbols!";

        public const string InvalidEmail = "Invalid email!";
        public const string LogoutError = "Only logged-in users can logout!";
    }
}
