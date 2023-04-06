using Microsoft.AspNetCore.Mvc;
using P229HomeWork.Models;

namespace P229HomeWork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Marka> markas = new List<Marka>
            {
                new Marka{Id = 1, Name = "BMW"},
                new Marka{Id = 2, Name = "Mercedes"},
            };

            return View(markas);
        }
    }
}
