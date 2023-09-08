using SIS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data
{
    public class User : UserIdentity<string>
    {
        public User()
        {
            this.Id = new Guid().ToString();
            this.Role = IdentityRole.User;
            this.Cards = new HashSet<UserCard>();
        }

        

        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
