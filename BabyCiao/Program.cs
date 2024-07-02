using BabyCiao;
using BabyCiao.Data;
using BabyCiao.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<BabyciaoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Babyciao")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<UserInfoServer>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = new PathString("/Home/NoLogin");
    option.AccessDeniedPath = new PathString("/Home/NoRole");
});
////設定session狀態
//builder.Services.AddSession(option => {
//    option.Cookie.Name = "andy_cookie";
//    option.IdleTimeout=TimeSpan.FromMicroseconds(10);
//    option.Cookie.IsEssential = true;
//    option.Cookie.HttpOnly = true;
//    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;

//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

////使用session功能
//app.UseSession();


app.Run();
