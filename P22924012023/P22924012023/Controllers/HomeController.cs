using Microsoft.AspNetCore.Mvc;
using P22924012023.Models;

namespace P22924012023.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index(int? id, string name)
        {
            //ViewData["Name"] = "Hamid";
            //ViewData["Age"] = 32;

            //ViewBag.Name = "Mammadov";



            //TempData["Name"] = "Code";


            Student student = new Student
            {
                Name = "Vusal",
                SurName = "Zeynalov"
            };

            //TempData["stu"] = student;

            return View(student);

            //return RedirectToAction("test");
        }

        public IActionResult Test()
        {
            return View();
        }

        //public IActionResult Index()
        //{
        //    //return Content("P229 Content Method");
        //    //return Json("{name:'Hamid',surname:'Mammadov'}");
        //    return View();
        //}
        //public IActionResult Index(int id)
        //{
        //    JsonResult jsonResult = new JsonResult("{name:'Hamid',surname:'Mammadov'}");

        //    if (true)
        //    {
        //        return NotFound();
        //    }

        //    ViewResult viewResult = new ViewResult();
        //    viewResult.ViewName = "testview";

        //    return viewResult;
        //}

        //public JsonResult Index()
        //{
        //    JsonResult jsonResult = new JsonResult("{name:'Hamid',surname:'Mammadov'}");

        //    return jsonResult;
        //}

        //public ContentResult Index()
        //{
        //    ContentResult contentResult = new ContentResult();
        //    contentResult.ContentType = "application/json";
        //    contentResult.StatusCode = 409229;
        //    contentResult.Content = "P229 First Content result";

        //    return contentResult;
        //}
    }
}
