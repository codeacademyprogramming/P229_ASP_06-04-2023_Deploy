using P229Allup.Models;
using P229Allup.ViewModels.BasketViewModels;

namespace P229Allup.ViewModels.ComponentViewModels.HeaderViewComponent
{
    public class HeaderVM
    {
        public IDictionary<string,string> Settings { get; set; }
        public List<BasketVM> BasketVMs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
