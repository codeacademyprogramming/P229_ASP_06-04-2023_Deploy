using Microsoft.AspNetCore.Mvc;

namespace P229FirstFrontToBack.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
