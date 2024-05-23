using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ebooks.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ebooksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ebooksContext") ?? throw new InvalidOperationException("Connection string 'ebooksContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//sesstion creation 
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
