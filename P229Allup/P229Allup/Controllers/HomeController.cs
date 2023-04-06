using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229Allup.DataAccessLayer;
using P229Allup.Models;
using P229Allup.ViewModels.HomeViewModels;
using System.Text;

namespace P229Allup.Controllers
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

            HomeVM homeVM = new HomeVM
            {
                Sliders = await _context.Sliders.Where(s => s.IsDeleted == false).ToListAsync(),
                Categories = await _context.Categories.Where(c => c.IsDeleted == false && c.IsMain).ToListAsync(),
                BestSeller = await _context.Products.Where(c => c.IsDeleted == false && c.IsBestSeller).ToListAsync(),
                Featured = await _context.Products.Where(c => c.IsDeleted == false && c.IsFeatured).ToListAsync(),
                NewArrival = await _context.Products.Where(c => c.IsDeleted == false && c.IsNewArrival).ToListAsync()
            };

            return View(homeVM);
        }

        //public IActionResult SetSession()
        //{
        //    HttpContext.Session.SetString("P229Session", "P229 First Session");

        //    return Content("Session Elave Olundu");
        //}

        //public IActionResult GetSession()
        //{
        //    string session = HttpContext.Session.GetString("P229Session");

        //    return Content(session);
        //}

        //public IActionResult SetCookie()
        //{
        //    HttpContext.Response.Cookies.Append("P229Cookie", "P229 First Cookie");

        //    return Content("Cookie Elave Olundu");
        //}

        //public IActionResult SetCookie1()
        //{
        //    HttpContext.Response.Cookies.Append("P229Cookie", "Secon Cookie");

        //    return Content("Cookie Elave Olundu");
        //}

        //public IActionResult GetCookie()
        //{
        //    string cookie = HttpContext.Request.Cookies["P229Cookie"];

        //    return Content(cookie);
        //}
    }
}