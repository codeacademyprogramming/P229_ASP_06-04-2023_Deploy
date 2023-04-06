using P229Allup.Models;

namespace P229Allup.ViewModels.AccountViewsModels
{
    public class ProfileVM
    {
        public IEnumerable<Address>? Addresses { get; set; }
        public Address? Address { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public AccountVM AccountVM { get; set; }
    }
}
