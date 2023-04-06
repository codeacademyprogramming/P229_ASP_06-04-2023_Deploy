using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229FirstDb.DataAccessLayer;
using P229FirstDb.Models;

namespace P229FirstDb.Controllers
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
            IEnumerable<Group> groups = await _context.Groups
                .Include(g => g.Students)
                .Include(g => g.GroupTeachers).ThenInclude(gt => gt.Teacher)
                .ToListAsync();

            IEnumerable<Group> groups2 = await _context.Groups
                .Include(g => g.Students)
                .Include(g => g.GroupTeachers).ThenInclude(gt => gt.Teacher)
                .Where(c=>c.Id == 1)
                .ToListAsync();

            Group group = await _context.Groups
                .Include(g => g.Students)
                .Include(g => g.GroupTeachers).ThenInclude(gt => gt.Teacher).FirstOrDefaultAsync(c=>c.StudentCount == 18);

            Group group = await _context.Groups.FirstOrDefaultAsync(c => c.Id == 1);

            IEnumerable<Group> groups1 = await _context.Groups.Where(g => g.StudentCount >= 15).ToListAsync();


            if (await _context.Groups.AnyAsync(c=>c.Id == 1))
            {

            }

            return View(/*groups*/);
        }
    }
}
