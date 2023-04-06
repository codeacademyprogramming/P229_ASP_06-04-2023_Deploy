using Microsoft.AspNetCore.Mvc;

namespace P229FirstApi.Controllers.v1
{
    [ApiController]
    //[Route("api/test")]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return StatusCode(StatusCodes.Status201Created);
        }


        //[HttpGet("1")]

        //public IActionResult asdasd()
        //{
        //    return Ok("Get Action");
        //}

        //[HttpGet("2")]
        //public IActionResult asd()
        //{
        //    return Ok("Get Action");
        //}

        //[HttpGet]
        //[Route("testroute/{name}/{surname}")]
        //public IActionResult testaction(string name,string surname)
        //{
        //    return Ok(surname+" Test Api "+name);
        //}

        //[HttpGet("testroute")]
        //public IActionResult testaction(string name, string surname)
        //{
        //    return Ok(surname + " Test Api " + name);
        //}

        //[HttpGet]
        //[Route("testroute")]
        //public IActionResult testaction(string name, string surname)
        //{
        //    return Ok(surname + " Test Api " + name);
        //}
    }
}
