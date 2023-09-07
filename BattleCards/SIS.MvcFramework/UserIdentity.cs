using System.ComponentModel.DataAnnotations;

namespace SIS.MvcFramework
{
    public class UserIdentity
    {
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
    }
}
