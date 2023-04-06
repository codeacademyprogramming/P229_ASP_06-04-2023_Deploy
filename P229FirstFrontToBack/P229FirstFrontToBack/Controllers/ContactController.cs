using Microsoft.AspNetCore.Mvc;

namespace P229FirstFrontToBack.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
