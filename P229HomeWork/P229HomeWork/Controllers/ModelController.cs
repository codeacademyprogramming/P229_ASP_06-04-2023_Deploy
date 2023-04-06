using Microsoft.AspNetCore.Mvc;
using P229HomeWork.Models;

namespace P229HomeWork.Controllers
{
    public class ModelController : Controller
    {
        public IActionResult Index(int? id)
        {
            List<Model> models = new List<Model>
            {
                new Model{Id=1, Name = "X5",MarkaId = 2},
                new Model{Id=2, Name = "X7",MarkaId = 2},
                new Model{Id=3, Name = "E-Class",MarkaId = 1},
                new Model{Id=4, Name = "C-Class",MarkaId = 1},
            };

            if (id == null)
            {
                return View(models);
            }

            if (models.Exists(m=>m.MarkaId == id))
            {
                return View(models.FindAll(m => m.MarkaId == id));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
