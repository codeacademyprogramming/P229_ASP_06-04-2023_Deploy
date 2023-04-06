using System.ComponentModel.DataAnnotations;

namespace P229MentorHomeWork.Models
{
    public class Offer
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        public IEnumerable<PricingOffer> PricingOffers { get; set; }
    }
}
