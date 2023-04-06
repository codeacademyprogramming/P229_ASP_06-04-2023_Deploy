using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using P229Allup.DataAccessLayer;
using P229Allup.Models;

namespace P229Allup.Hubs
{
    public class NotificationHub : Hub 
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public NotificationHub(AppDbContext context, IHttpContextAccessor contextAccessor,UserManager<AppUser> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                appUser.ConnectionId = Context.ConnectionId;
                if (_contextAccessor.HttpContext.User.IsInRole("Admin") || _contextAccessor.HttpContext.User.IsInRole("SuperAdmin"))
                {
                    await Groups.AddToGroupAsync(appUser.ConnectionId,"Admins");
                }else if (_contextAccessor.HttpContext.User.IsInRole("Member"))
                {
                    await Groups.AddToGroupAsync(appUser.ConnectionId,"Users");
                }

                await _userManager.UpdateAsync(appUser);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
               
                if (_contextAccessor.HttpContext.User.IsInRole("Admin") || _contextAccessor.HttpContext.User.IsInRole("SuperAdmin"))
                {
                    await Groups.RemoveFromGroupAsync(appUser.ConnectionId, "Admins");
                }
                else if (_contextAccessor.HttpContext.User.IsInRole("Member"))
                {
                    await Groups.RemoveFromGroupAsync(appUser.ConnectionId, "Users");
                }
                appUser.ConnectionId = null;
                await _userManager.UpdateAsync(appUser);
            }
        }
    }
}
