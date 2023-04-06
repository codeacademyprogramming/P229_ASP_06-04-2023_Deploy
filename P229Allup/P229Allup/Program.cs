using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P229Allup.DataAccessLayer;
using P229Allup.Extentions;
using P229Allup.Hubs;
using P229Allup.Interfaces;
using P229Allup.Models;
using P229Allup.Services;
using P229Allup.ViewModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    options.User.RequireUniqueEmail = true;

    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders().AddErrorDescriber<IdentityErrorDescriberAZ>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.Configure<SmtpSetting>(builder.Configuration.GetSection("SmtpSetting"));
builder.Services.AddScoped<ILayoutService, LayoutService>();


//AddSingleton AddTransient AddScoped
var app = builder.Build();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapHub<NotificationHub>("/notificationHub");


app.Run();
