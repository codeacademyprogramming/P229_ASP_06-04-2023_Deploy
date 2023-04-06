using Microsoft.AspNetCore.Mvc;

namespace P229FirstFrontToBack.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
