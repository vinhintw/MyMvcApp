using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
string DB_URL =
    Environment.GetEnvironmentVariable("DB_URL")
    ?? throw new InvalidOperationException("Connection string not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(DB_URL));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
