using Academy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Group> groups = new List<Group>
            {
                new Group{ Id = 1,No = "P229"},
                new Group{ Id = 2,No = "P133"},
                new Group{ Id = 3,No = "P228"},
            };

            return View(groups);
        }
    }
}
