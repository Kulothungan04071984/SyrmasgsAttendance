using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SyrmaSGS.Models;

var builder = WebApplication.CreateBuilder(args);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<iServicescs, DbServices>();
builder.Services.AddDbContext<SyrmasgsAttendanceContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
    

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
