﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using P229Allup.DataAccessLayer;
using P229Allup.Hubs;
using P229Allup.Models;
using P229Allup.ViewModels.BasketViewModels;
using P229Allup.ViewModels.OrderViewModels;

namespace P229Allup.Controllers
{
    [Authorize(Roles = "Member")]
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hub;

        public OrderController(UserManager<AppUser> userManager, AppDbContext context, IHubContext<NotificationHub> hub)
        {
            _userManager = userManager;
            _context = context;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            AppUser appUser = await _userManager.Users
                .Include(u => u.Baskets.Where(b => b.IsDeleted == false)).ThenInclude(b => b.Product)
                .Include(u => u.Addresses.Where(a => a.IsMain && a.IsDeleted == false))
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (appUser.Baskets == null || appUser.Baskets.Count <= 0)
            {
                return RedirectToAction("index", "home");
            }

            if (appUser.Addresses == null || appUser.Addresses.Count <= 0)
            {
                return RedirectToAction("profile", "account");
            }

            OrderVM orderVM = new OrderVM
            {
                Order = new Order
                {
                    Name = appUser.Name,
                    SurName = appUser.SurName,
                    Email = appUser.Email,
                    Country = appUser.Addresses.First().Country,
                    City = appUser.Addresses.First().City,
                    State = appUser.Addresses.First().State,
                    PostalCode = appUser.Addresses.First().PostalCode,
                },
                Baskets = appUser.Baskets
            };

            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            AppUser appUser = await _userManager.Users
                .Include(u => u.Baskets.Where(b => b.IsDeleted == false)).ThenInclude(b => b.Product)
                .Include(u => u.Addresses.Where(a => a.IsMain && a.IsDeleted == false))
                .Include(u=>u.Orders)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            OrderVM orderVM = new OrderVM
            {
                Order = order,
                Baskets = appUser.Baskets
            };

            if (!ModelState.IsValid)
            {
                return View(orderVM);
            }

            order.CreatedAt= DateTime.UtcNow.AddHours(4);
            order.CreatedBy = $"{appUser.Name} {appUser.SurName}";
            order.No = appUser.Orders != null && appUser.Orders.Count > 0 ? appUser.Orders.Last().No + 1 : 1;

            appUser.Orders.Add(order);
            order.OrderItems = new List<OrderItem>();
            foreach (Basket basket in appUser.Baskets)
            {
                basket.IsDeleted = true;

                OrderItem orderItem = new OrderItem
                {
                    CreatedAt = DateTime.UtcNow.AddHours(4),
                    CreatedBy = $"{appUser.Name} {appUser.SurName}",
                    Count = basket.Count,
                    ProductId = basket.ProductId,
                    Price = basket.Product.DiscountedPrice > 0 ? basket.Product.DiscountedPrice : basket.Product.Price
                };

                order.OrderItems.Add(orderItem);
            }
            HttpContext.Response.Cookies.Append("basket", "");

            await _context.SaveChangesAsync();

            TempData["Errorr"] = $"{order.No} Sifarisiniz Ugurla Gonderildi";

            await _hub.Clients.Group("Admins").SendAsync("checkOrder", $"{order.Name} {order.SurName} {order.No} Sifaris Elave etdi. Cemi: {order.OrderItems.Sum(oi => oi.Count * oi.Price)}");

            return RedirectToAction("index","home");
        }
    }
}
