using Microsoft.AspNetCore.Mvc;
using P229Allup.Models;

namespace P229Allup.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            return Ok(blog);
        }
    }
}
