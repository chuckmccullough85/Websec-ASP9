
using AcmeLib;
using AcmeWeb.Areas.Identity.Data;
using AcmeWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'acmedb connection' not found.");
var identityconnection = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'identityconnection' not found.");
builder.Services.AddDbContext<AcmeWebContext>(options => 
	options.UseSqlite(identityconnection));

builder.Services.AddDefaultIdentity<AcmeWebUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AcmeWebContext>();

builder.Services.AddDbContext<BankContext>(options =>
      options.UseSqlite(connectionString)
             .EnableSensitiveDataLogging());

builder.Services.AddScoped<BankService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddRazorPages();
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
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
