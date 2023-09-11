using BattleCards.Data;
using SIS.MvcFramework;
using System.Security.Cryptography;
using System.Text;

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
            var user = new User() 
            {
                Username = username,
                Email = email,
                Role = IdentityRole.User,
                Password = ComputeHash(password)
            };
            this.db.Users.Add(user);
            this.db.SaveChanges();  
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
            var user = this.db.Users.FirstOrDefault(x => x.Username == username);
            return user.Password == ComputeHash(password);
        }

        
        public static string ComputeHash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            
            var hashedInputBytes = hash.ComputeHash(bytes);

            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();      
        }
    }
}
