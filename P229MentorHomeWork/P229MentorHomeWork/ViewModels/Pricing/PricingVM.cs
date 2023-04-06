using P229MentorHomeWork.Models;

namespace P229MentorHomeWork.ViewModels.Pricing
{
    public class PricingVM
    {
        public IEnumerable<P229MentorHomeWork.Models.Pricing> Pricings { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
    }
}
