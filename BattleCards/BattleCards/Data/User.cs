using SIS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data
{
    public class User : UserIdentity
    {
        public User()
        {
            this.Id = new Guid().ToString();
        }

        

        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
