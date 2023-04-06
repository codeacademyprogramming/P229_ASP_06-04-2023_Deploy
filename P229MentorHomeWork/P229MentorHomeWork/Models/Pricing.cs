using Microsoft.AspNetCore.Routing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P229MentorHomeWork.Models
{
    public class Pricing
    {
        public int Id { get; set; }
        [StringLength(100,MinimumLength =5)]
        [Required]
        public string Name { get; set; }
        [Column(TypeName ="money")]
        public double Price { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActivated { get; set; }

        public IEnumerable<PricingOffer> PricingOffers { get; set; }
    }
}
