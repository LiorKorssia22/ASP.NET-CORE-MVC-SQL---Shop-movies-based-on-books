using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationbookstore.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebApplicationbookstoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplicationbookstoreContext") ?? throw new InvalidOperationException("Connection string 'WebApplicationbookstoreContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usersaccounts}/{action=Login}/{Id?}");

app.Run();
