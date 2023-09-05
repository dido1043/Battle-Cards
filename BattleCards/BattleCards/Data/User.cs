using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data
{
    public class User
    {
        public User()
        {
            this.Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
