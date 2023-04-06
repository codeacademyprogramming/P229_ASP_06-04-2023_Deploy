using P229Allup.Models;
using P229Allup.ViewModels.BasketViewModels;

namespace P229Allup.Interfaces
{
    public interface ILayoutService
    {
        Task<IDictionary<string,string>> GetSettings();
        Task<IEnumerable<Category>> GetCategories();
        Task<List<BasketVM>> GetBaskets();
    }
}
