using Microsoft.AspNetCore.Identity;

namespace emirathes.Models
{
    public class ProgramUsers : IdentityUser
    {
        //public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
