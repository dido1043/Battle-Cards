using BattleCards.Data;

namespace Program.Services
{
    public class UserService : IUserSevices
    {
        private ApplicationDbContext db;
        public UserService()
        {
            this.db = new ApplicationDbContext();
        }
        public void CreateUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailValid(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameValid(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool IsUserValid(string username, string password)
        {
            throw new NotImplementedException();
        }

        
        public static string SHA512(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                // Convert to text
                // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
