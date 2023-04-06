using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace P229Allup.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
