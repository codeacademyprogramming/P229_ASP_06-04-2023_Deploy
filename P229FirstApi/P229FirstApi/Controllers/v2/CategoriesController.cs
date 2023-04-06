using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229FirstApi.DAL;
using P229FirstApi.DTOs.CategroryDTOs;
using P229FirstApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace P229FirstApi.Controllers.v2
{
    /// <summary>
    /// Categories Services
    /// </summary>
    [Route("api/v2/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin,Member")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("readtoken")]
        public async Task<IActionResult> ReadToken()
        {
            var token = Request.Headers.Authorization.ToString().Split(' ')[1];

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            var claims = (JwtSecurityToken)handler.ReadToken(token);

            string email = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            return Ok(email);
        }

        /// <summary>
        /// Creates an Category.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Employee
        ///     {        
        ///       "name": "Test"      
        ///     }
        /// </remarks>
        /// <param name="category"></param>
        /// <returns>A newly created category Id</returns>
        /// <response code="201">Returns the newly created Id</response>
        /// <response code="400">InCorrect Object</response>          
        /// <response code="409">Same Name Category</response>          
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> Post(CategoryPostDTo categoryPostDTo)
        {
            Category category = new Category
            {
                Name = categoryPostDTo.Name.Trim(),
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, category.Id);
        }

        /// <summary>
        /// Get List Of Category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //Old Mapping
            //List<CategoryListDto> categoryListDtos = await _context.Categories.Where(c => c.IsDeleted == false)
            //    .Select(x => new CategoryListDto
            //    {
            //        Id = x.Id,
            //        Name = x.Name
            //    })
            //    .ToListAsync();

            List<CategoryListDto> categoryListDtos = _mapper
                .Map<List<CategoryListDto>>(await _context.Categories.Include(c=>c.Products).Where(c => c.IsDeleted == false).ToListAsync());

            return Ok(categoryListDtos);
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (category == null) return NotFound($"Id: {id} Is InCorrect");

            CategoryGetDto categoryGetDto = new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryGetDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put(/*[FromForm]*/ /*CategoryListDto*/ CategoryPutDto categoryPutDto)
        {
            //Category category = _mapper.Map<Category>(categoryPutDto);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Category dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == categoryPutDto.Id);

            if (dbCategory == null) return NotFound($"Id: {categoryPutDto.Id} Is InCorrect");

            if (await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Name.ToLower() == categoryPutDto.Name.Trim().ToLower() && c.Id != dbCategory.Id))
            {
                return Conflict($"Daxil etdiyniz Categoriya adi: {categoryPutDto.Name.Trim()} artiq movcuddur");
            }

            dbCategory.Name = categoryPutDto.Name.Trim();
            dbCategory.UpdatedAt = DateTime.UtcNow.AddHours(4);
            dbCategory.UpdatedBy = "System";

            //dbCategory = _mapper.Map<Category>(categoryPutDto);

            //_context.Categories.Update(dbCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest("Id is Null");

            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (category == null) return NotFound($"Id: {id} Is InCorrect");

            category.DeletedAt = DateTime.UtcNow.AddHours(4);
            category.DeletedBy = "System";
            category.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
