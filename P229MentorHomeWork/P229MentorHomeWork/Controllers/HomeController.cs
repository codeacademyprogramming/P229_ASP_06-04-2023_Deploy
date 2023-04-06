using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229MentorHomeWork.DataAccessLayer;
using P229MentorHomeWork.Models;

namespace P229MentorHomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Feature> features = await _context.Features.ToListAsync();
            return View(features);
        }
    }
}
