using P229Allup.Models;
using P229Allup.ViewModels.BasketViewModels;

namespace P229Allup.ViewModels.OrderViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public IEnumerable<Basket> Baskets { get; set; }
    }
}
