using Microsoft.AspNetCore.Identity;

namespace emirathes.Models
{
    public class ProgramUsers : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
    }
}
