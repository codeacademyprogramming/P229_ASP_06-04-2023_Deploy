using Microsoft.AspNetCore.Identity;

namespace P229FirstApi.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
