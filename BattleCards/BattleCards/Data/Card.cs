namespace BattleCards.Data
{
using System.ComponentModel.DataAnnotations;
    public class Card
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Keyword { get; set; }

        public int Attack { get; set; }
        public int Health { get; set; }

        public virtual ICollection<UserCard> Users { get; set; }
    }
}
