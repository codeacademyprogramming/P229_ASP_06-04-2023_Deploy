using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229Allup.DataAccessLayer;
using P229Allup.Models;

namespace P229Allup.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Category> categories= await _context.Categories.Where(c=>c.IsDeleted == false && c.IsMain).ToListAsync();

            return View(categories);
        }
    }
}
