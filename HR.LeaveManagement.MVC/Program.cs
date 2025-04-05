using HR.LeaveManagement.MVC.Services;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using HR.LeaveManagement.MVC.Services.Base;
using Hanssens.Net;
using HR.LeaveManagement.MVC.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//llow http context accessor injectable to any other class
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/users/login");
});

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IClient, Client>(c=> c.BaseAddress = new Uri("http://localhost:5098"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
builder.Services.AddSingleton<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCookiePolicy();
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
