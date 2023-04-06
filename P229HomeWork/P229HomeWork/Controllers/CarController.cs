using Microsoft.AspNetCore.Mvc;
using P229HomeWork.Models;

namespace P229HomeWork.Controllers
{
    public class CarController : Controller
    {
        private readonly List<Car> _cars;

        public CarController()
        {
            _cars = new List<Car>
            {
                new Car{Id = 1, Name = "N-1", Year = 2020, ModelId = 1},
                new Car{Id = 2, Name = "N-2", Year = 2020, ModelId = 1},
                new Car{Id = 3, Name = "N-3", Year = 2020, ModelId = 2},
                new Car{Id = 4, Name = "N-4", Year = 2020, ModelId = 2},
                new Car{Id = 5, Name = "N-5", Year = 2020, ModelId = 3},
                new Car{Id = 6, Name = "N-6", Year = 2020, ModelId = 3},
                new Car{Id = 7, Name = "N-7", Year = 2020, ModelId = 4},
                new Car{Id = 8, Name = "N-8", Year = 2020, ModelId = 4},
            };
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return View(_cars);
            }

            if (_cars.Exists(c=>c.ModelId == id))
            {
                return View(_cars.FindAll(c => c.ModelId == id));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return BadRequest();

            if (!_cars.Exists(c => c.Id == id)) return NotFound();

            return View(_cars.Find(c => c.Id == id));
        }
    }
}
