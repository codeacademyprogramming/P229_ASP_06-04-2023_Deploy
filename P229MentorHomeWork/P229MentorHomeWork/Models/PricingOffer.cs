using System.ComponentModel.DataAnnotations.Schema;

namespace P229MentorHomeWork.Models
{
    public class PricingOffer
    {
        public int Id { get; set; }
        public int PricingId { get; set; }
        public Pricing Pricing { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
