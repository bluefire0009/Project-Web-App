using System.ComponentModel.DataAnnotations;

namespace WebCalendaar.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        public required string UserName { get; set; }
        public required string Password { get; set; }

        public required string Email { get; set; }
    }
}