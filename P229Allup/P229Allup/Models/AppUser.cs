using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace P229Allup.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? SurName { get; set; }
        public bool IsActive { get; set; }
        [StringLength(100)]
        public string? ConnectionId { get; set; }

        public List<Basket>? Baskets { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
