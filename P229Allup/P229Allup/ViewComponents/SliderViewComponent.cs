using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229Allup.DataAccessLayer;
using P229Allup.Models;

namespace P229Allup.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(s=>s.IsDeleted == false).ToListAsync();

            return View(sliders);
        }
    }
}
