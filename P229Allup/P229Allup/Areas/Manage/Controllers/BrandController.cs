using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using P229Allup.DataAccessLayer;
using P229Allup.Models;
using P229Allup.ViewModels;

namespace P229Allup.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        //[AllowAnonymous]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            IQueryable<Brand> query = _context.Brands
                .Include(b=>b.Products)
                .Where(b => b.IsDeleted == false);

            return View(PageNatedList<Brand>.Create(query,pageIndex, 3,3));
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Brand brand = await _context.Brands
                .Include(b=>b.Products.Where(p=>p.IsDeleted == false))
                .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (brand == null) return NotFound();

            return View(brand);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }

            if (await _context.Brands.AnyAsync(b=>b.IsDeleted == false && b.Name.ToLower() == brand.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"Bu Adda {brand.Name} Brand Artiq Movcuddur");
                return View(brand);
            }

            brand.Name = brand.Name.Trim();
            brand.CreatedBy = "System";
            brand.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null) return BadRequest();

            Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (brand == null) return NotFound();

            return View(brand);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(int? id, Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);

            if (id == null) return BadRequest();

            if(id != brand.Id) return BadRequest();

            Brand dbBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbBrand == null) return NotFound();

            if (await _context.Brands.AnyAsync(b => b.IsDeleted == false && b.Name.ToLower() == brand.Name.Trim().ToLower() && b.Id != id))
            {
                ModelState.AddModelError("Name", $"Bu Adda {brand.Name} Brand Artiq Movcuddur");
                return View(brand);
            }

            dbBrand.Name = brand.Name.Trim();
            dbBrand.UpdatedAt = DateTime.UtcNow.AddHours(4);
            dbBrand.UpdatedBy = "System";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null) return BadRequest();

            //Brand brand = await _context.Brands
            //    .Include(b => b.Products.Where(p => p.IsDeleted == false))
            //    .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            //if (brand == null) return NotFound();

            //return View(brand);

            if (id == null) return BadRequest();

            Brand brand = await _context.Brands
                .Include(b => b.Products.Where(p => p.IsDeleted == false))
                .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (brand == null) return NotFound();

            brand.IsDeleted = true;
            brand.DeletedBy = "System";
            brand.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            IEnumerable<Brand> brands = await _context.Brands.Include(b => b.Products).Where(b => b.IsDeleted == false).ToListAsync();

            return PartialView("_BrandIndexPartial", brands);
        }

        //[HttpGet]
        //public async Task<IActionResult> DeleteBrand(int? id)
        //{
        //    if (id == null) return BadRequest();

        //    Brand brand = await _context.Brands
        //        .Include(b => b.Products.Where(p => p.IsDeleted == false))
        //        .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

        //    if (brand == null) return NotFound();

        //    brand.IsDeleted = true;
        //    brand.DeletedBy = "System";
        //    brand.DeletedAt = DateTime.UtcNow.AddHours(4);

        //    //foreach (var item in brand.Products)
        //    //{
        //    //    item.BrandId = null;
        //    //}

        //    //_context.Brands.Remove(brand);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
    }
}