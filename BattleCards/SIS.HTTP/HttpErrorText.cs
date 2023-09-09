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
    }
}
