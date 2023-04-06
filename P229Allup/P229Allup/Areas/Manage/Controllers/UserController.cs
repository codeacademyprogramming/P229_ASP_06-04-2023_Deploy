using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P229Allup.Areas.Manage.ViewModels.AccountViewModels;
using P229Allup.Models;
using P229Allup.ViewModels;
using System.Data;

namespace P229Allup.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            //List<UserVM> appUsers = await _userManager.Users
            //    .Where(u => u.UserName != User.Identity.Name)
            //    .Select(x => new UserVM
            //    {
            //        UserName = x.UserName,
            //        Email = x.Email,
            //        Id = x.Id,
            //        Name = x.Name,
            //        SurName = x.SurName,
            //        Role = (_userManager.GetRolesAsync(x))[0]
            //    })
            //    .ToListAsync();

            List<AppUser> appUsers = await _userManager.Users.Where(u=>u.UserName != User.Identity.Name).ToListAsync();

            List<UserVM> userVMs = new List<UserVM>();

            foreach (AppUser item in appUsers)
            {
                //string rol = (await _userManager.GetRolesAsync(item))[0];

                UserVM userVM = new UserVM
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    Id = item.Id,
                    Name = item.Name,
                    SurName = item.SurName,
                    Role = (await _userManager.GetRolesAsync(item))[0],
                    IsActive = item.IsActive
                };

                userVMs.Add(userVM);
            }

            return View(PageNatedList<UserVM>.Create(userVMs.AsQueryable(),pageIndex,3,5));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            List<RoleVM> roles = await _roleManager.Roles.Where(r => r.Name != "SuperAdmin")
                .Select(x=>new RoleVM
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            string roleName = (await _userManager.GetRolesAsync(appUser))[0];

            ChangeRoleVM changeRoleVM = new ChangeRoleVM
            {
                Id = id,
                RoleId = roles.FirstOrDefault(r=>r.Name == roleName).Id
            };

            ViewBag.Roles = roles;

            return View(changeRoleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, ChangeRoleVM changeRoleVM)
        {
            List<RoleVM> roles = await _roleManager.Roles.Where(r => r.Name != "SuperAdmin")
                .Select(x => new RoleVM
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();

            ViewBag.Roles = roles;

            if (!ModelState.IsValid)
            {
                return View(changeRoleVM);
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(changeRoleVM.RoleId))
            {
                return BadRequest();
            }

            if (id != changeRoleVM.Id)
            {
                return BadRequest();
            }

            if (!await _roleManager.Roles.AnyAsync(r=>r.Id == changeRoleVM.RoleId))
            {
                return NotFound();
            }

            AppUser appUser = await _userManager.FindByIdAsync(id);

            string roleName = (await _userManager.GetRolesAsync(appUser))[0];

            if (roles.FirstOrDefault(r=>r.Name == roleName).Id != changeRoleVM.RoleId)
            {
                await _userManager.RemoveFromRoleAsync(appUser, roleName);
                await _userManager.AddToRoleAsync(appUser, roles.FirstOrDefault(r => r.Id == changeRoleVM.RoleId).Name);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
