using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using P229Allup.DataAccessLayer;
using P229Allup.Hubs;
using P229Allup.Models;
using P229Allup.ViewModels;

namespace P229Allup.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub> _hub;
        public OrdersController(AppDbContext context, IHubContext<NotificationHub> hub)
        {
            _context= context;
            _hub = hub;
        }

        public IActionResult Index(int pageIndex = 1)
        {
            IQueryable<Order> orders = _context.Orders
                .Include(o=>o.OrderItems)
                .Where(o=>o.IsDeleted == false);

            return View(PageNatedList<Order>.Create(orders,pageIndex,5,5));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Order order = await _context.Orders
                .Include(o=>o.OrderItems.Where(oi=>oi.IsDeleted == false)).ThenInclude(oi=>oi.Product)
                .FirstOrDefaultAsync(o=> o.Id == id && o.IsDeleted == false);

            if(order == null) return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeOrder(int? id,Order order)
        {
            if(id == null) return BadRequest();

            if(id != order.Id) return BadRequest();

            Order dbOrder = await _context.Orders
                .Include(o=>o.User)
                .Include(o => o.OrderItems.Where(oi => oi.IsDeleted == false)).ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id && o.IsDeleted == false);

            if (dbOrder == null) return NotFound();

            if ((int)order.Status < 0 || (int)order.Status > 4)
            {
                ModelState.AddModelError("Status", "Duzgun Secim Edin");
                return View("Detail", dbOrder);
            }

            dbOrder.Status = order.Status;
            dbOrder.Comment = order.Comment;

            await _context.SaveChangesAsync();

            if (dbOrder.User.ConnectionId != null)
            {
                await _hub.Clients.Client(dbOrder.User.ConnectionId).SendAsync("changedorder", $"{dbOrder.Status} {dbOrder.Comment}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
