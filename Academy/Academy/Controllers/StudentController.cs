using Academy.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Controllers
{
    public class StudentController : Controller
    {
        private readonly List<Student> _students;

        public StudentController()
        {
            _students = new List<Student>
            {
                new Student{Id = 1, Name = "Vusal", SurName="Zeynalov", Age = 19, GroupId = 1},
                new Student{Id = 2, Name = "Kamil", SurName="Abdullayev", Age = 19, GroupId = 1},
                new Student{Id = 3, Name = "Leman", SurName="Hashimova", Age = 20, GroupId = 1},
                new Student{Id = 4, Name = "Aqil", SurName="Soltanli", Age = 19, GroupId = 2},
                new Student{Id = 5, Name = "Cavid", SurName="Aliyev", Age = 20, GroupId = 2},
                new Student{Id = 6, Name = "Faiq", SurName="Aliyev", Age = 27, GroupId = 3},
            };
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return View(_students);
            }

            if (_students.Exists(s=>s.GroupId == id))
            {
                return View(_students.FindAll(s => s.GroupId == id));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (_students.Exists(s => s.Id == id))
            {
                return View(_students.Find(s => s.Id == id));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
