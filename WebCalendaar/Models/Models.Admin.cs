namespace WebCalendaar.Models
{
    public class Admin
    {
        public Guid AdminId { get; set; }

        public required string UserName { get; set; }
        public required string Password { get; set; }

        public required string Email { get; set; }
    }
}