using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdminSideEcoFridge.Areas.Identity.Data;
using AdminSideEcoFridge.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using EcoFridge;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EcoFridgeDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
    ).AddCookie(option => {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddScoped<IAuthorizationHandler, RolesInDBAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("admin"));

    options.AddPolicy("IndividualUserPolicy", policy =>
        policy.RequireRole("individual_user"));

    options.AddPolicy("FoodBusinessPolicy", policy =>
        policy.RequireRole("food_business"));

    options.AddPolicy("DoneeOrgPolicy", policy =>
        policy.RequireRole("donee_organization"));

    options.AddPolicy("UsersPolicy", policy =>
        policy.RequireRole("donee_organization", "individual_user", "food_business"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
