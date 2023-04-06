using Microsoft.AspNetCore.Mvc;

namespace P229FirstFrontToBack.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
