using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P229Allup.ViewModels.AccountViewsModels;
using P229Allup.DataAccessLayer;
using P229Allup.Models;
using P229Allup.ViewModels.AccountViewsModels;
using P229Allup.ViewModels.BasketViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using P229Allup.ViewModels;
using Microsoft.Extensions.Options;

namespace P229Allup.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _con;
        private readonly SmtpSetting _smtpSetting;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, AppDbContext context, IConfiguration con
            ,IOptions<SmtpSetting> smtpSetting)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _con = con;
            _smtpSetting = smtpSetting.Value;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            AppUser appUser = new AppUser
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Name = registerVM.Name,
                SurName = registerVM.SurName,
            };

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            string url = Url.Action("EmailConfirm", "Account", new { id = appUser.Id, token = token }, HttpContext.Request.Scheme, HttpContext.Request.Host.ToString());

            string templateKamilPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "_EmailConfirm.cshtml");
            string templateContent = await System.IO.File.ReadAllTextAsync(templateKamilPath);
            templateContent = templateContent.Replace("{{name}}", appUser.Name);
            templateContent = templateContent.Replace("{{surname}}", appUser.SurName);
            templateContent = templateContent.Replace("{{url}}", url);

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(MailboxAddress.Parse(_smtpSetting.Email));
            //mimeMessage.From.Add(MailboxAddress.Parse(_con.GetSection("SmtpSetting:Email").Value));
            //mimeMessage.From.Add(MailboxAddress.Parse("p229codeacademy@gmail.com"));
            mimeMessage.To.Add(MailboxAddress.Parse(appUser.Email));
            mimeMessage.Subject = "Email Confirmation";
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = templateContent
            };

            using (SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_smtpSetting.Host, _smtpSetting.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_smtpSetting.Email, _smtpSetting.Password);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
                smtpClient.Dispose();
            }

            //using (SmtpClient smtpClient = new SmtpClient())
            //{
            //    await smtpClient.ConnectAsync(_con.GetSection("SmtpSetting:Host").Value, int.Parse(_con.GetSection("SmtpSetting:Port").Value), MailKit.Security.SecureSocketOptions.StartTls);
            //    await smtpClient.AuthenticateAsync(_con.GetSection("SmtpSetting:Email").Value, _con.GetSection("SmtpSetting:Password").Value);
            //    await smtpClient.SendAsync(mimeMessage);
            //    await smtpClient.DisconnectAsync(true);
            //    smtpClient.Dispose();
            //}

            //using (SmtpClient smtpClient = new SmtpClient())
            //{
            //    await smtpClient.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //    await smtpClient.AuthenticateAsync("p229codeacademy@gmail.com", "tyguxjtqkyhvlvji");
            //    await smtpClient.SendAsync(mimeMessage);
            //    await smtpClient.DisconnectAsync(true);
            //    smtpClient.Dispose();
            //}

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            AppUser appUser = await _userManager.Users
                .Include(u=>u.Baskets.Where(b=>b.IsDeleted==false))
                .FirstOrDefaultAsync(u=>u.NormalizedEmail == loginVM.Email.Trim().ToUpperInvariant());

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View(loginVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = null;

            if (appUser.EmailConfirmed)
            {
                signInResult = await _signInManager
                .PasswordSignInAsync(appUser, loginVM.Password, loginVM.RemindMe, true);
            }
            else
            {
                ModelState.AddModelError("", "Emailnizi Tesdiqleyin");
                return View();
            }

            if (appUser.LockoutEnd > DateTime.UtcNow)
            {
                ModelState.AddModelError("", "Hesabiniz Bloklanib");
                return View(loginVM);
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View(loginVM);
            }

            string cookie = HttpContext.Request.Cookies["basket"];

            if (appUser.Baskets != null && appUser.Baskets.Count() > 0)
            {
                List<BasketVM> basketVMs = new List<BasketVM>();

                foreach (Basket basket in appUser.Baskets)
                {
                    BasketVM basketVM = new BasketVM
                    {
                        Id = (int)basket.ProductId,
                        Count = basket.Count,
                    };

                    basketVMs.Add(basketVM);
                }

                cookie = JsonConvert.SerializeObject(basketVMs);

                HttpContext.Response.Cookies.Append("basket", cookie);
            }
            else
            {
                HttpContext.Response.Cookies.Append("basket", "");
            }

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Profile()
        {
            AppUser appUser = await _userManager.Users
                .Include(u => u.Addresses.Where(a => a.IsDeleted == false))
                .Include(u=>u.Orders.Where(o=>o.IsDeleted == false)).ThenInclude(o=>o.OrderItems.Where(oi=>oi.IsDeleted == false)).ThenInclude(oi=>oi.Product)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ProfileVM profileVM = new ProfileVM
            {
                Addresses = appUser.Addresses,
                Address = new Address(),
                Orders = appUser.Orders,
                AccountVM = new AccountVM
                {
                    Name =appUser.Name,
                    SurName = appUser.SurName,
                    Email = appUser.Email,
                    UserName= appUser.UserName
                }
            };

            return View(profileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> AddAddress(Address address)
        {
            AppUser appUser = await _userManager.Users
                .Include(u => u.Addresses.Where(a => a.IsDeleted == false))
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            TempData["Tab"] = "Address";

            ProfileVM profileVM = new ProfileVM
            {
                Addresses = appUser.Addresses,
                Address = address
            };

            if (!ModelState.IsValid)
            {
                TempData["ModelError"] = "Error";
                return View("Profile",profileVM);
            }

            if (address.IsMain == false && (appUser.Addresses == null || appUser.Addresses.Count() <= 0))
            {
                address.IsMain = true;
            }

            if (address.IsMain && appUser.Addresses != null && appUser.Addresses.Count() > 0 && appUser.Addresses.Any(a=>a.IsMain && a.IsDeleted == false)) 
            {
                appUser.Addresses.FirstOrDefault(a => a.IsMain && a.IsDeleted == false).IsMain = false;
            }

            address.CreatedAt = DateTime.UtcNow.AddHours(4);
            address.CreatedBy = $"{appUser.Name} {appUser.SurName}";

            appUser.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Profile));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> UpdateProfile(AccountVM accountVM)
        {
            TempData["Tab"] = "Account";

            AppUser appUser = await _userManager.Users
                .Include(u => u.Addresses.Where(a => a.IsDeleted == false))
                .Include(u => u.Orders.Where(o => o.IsDeleted == false)).ThenInclude(o => o.OrderItems.Where(oi => oi.IsDeleted == false)).ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            ProfileVM profileVM = new ProfileVM
            {
                Addresses = appUser.Addresses,
                Address = new Address(),
                Orders = appUser.Orders,
                AccountVM = accountVM
            };

            if (!ModelState.IsValid)
            {
                return View("Profile", profileVM);
            }

            appUser.Name = accountVM.Name;
            appUser.SurName = accountVM.SurName;

            if (appUser.NormalizedUserName != accountVM.UserName.Trim().ToUpperInvariant())
            {
                if (await _userManager.Users.AnyAsync(u => u.NormalizedUserName == accountVM.UserName.Trim().ToUpperInvariant() && u.Id != appUser.Id))
                {
                    ModelState.AddModelError("UserName", $"User Name {accountVM.UserName} Already Taken");
                    return View("Profile", profileVM);
                }
                else
                {
                    appUser.UserName = accountVM.UserName;
                }
            }

            if (appUser.NormalizedEmail != accountVM.Email.Trim().ToUpperInvariant())
            {
                if (await _userManager.Users.AnyAsync(u => u.NormalizedEmail == accountVM.Email.Trim().ToUpperInvariant() && u.Id != appUser.Id))
                {
                    ModelState.AddModelError("Email", $"Email {accountVM.Email} Already Taken");
                    return View("Profile", profileVM);
                }
                else
                {
                    appUser.Email = accountVM.Email;
                }
            }

            IdentityResult identityResult = await _userManager.UpdateAsync(appUser);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View("Profile", profileVM);
            }

            if (!string.IsNullOrWhiteSpace(accountVM.CurrentPassword))
            {
                if (await _userManager.CheckPasswordAsync(appUser, accountVM.CurrentPassword))
                {
                    if (!string.IsNullOrWhiteSpace(accountVM.Password))
                    {
                        string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

                        identityResult = await _userManager.ResetPasswordAsync(appUser, token, accountVM.Password);

                        if (!identityResult.Succeeded)
                        {
                            foreach (IdentityError identityError in identityResult.Errors)
                            {
                                ModelState.AddModelError("", identityError.Description);
                            }

                            return View("Profile", profileVM);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", $"CurrentPassword Yanlisdir");
                    return View("Profile", profileVM);
                }

            }

            await _signInManager.SignInAsync(appUser, true);

            TempData["Success"] = "Hesabiniz Ugurla Yenilendi";

            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirm(string? id, string? token)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            if (string.IsNullOrWhiteSpace(token)) return BadRequest();

            AppUser appUser = await _userManager.FindByIdAsync(id);

            if(appUser == null) return NotFound();

            IdentityResult identityResult = await _userManager.ConfirmEmailAsync(appUser, token);

            if(!identityResult.Succeeded) return BadRequest();

            TempData["Success"] = $"{appUser.Email} Tesdiqlendi";

            return RedirectToAction(nameof(Login));
        }
    }
}
