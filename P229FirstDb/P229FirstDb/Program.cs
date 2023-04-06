using Microsoft.EntityFrameworkCore;
using P229FirstDb.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();

app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");

app.Run();
