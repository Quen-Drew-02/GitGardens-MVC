using GitGardens.Data;
using GitGardens.Interface;
using GitGardens.Models;
using GitGardens.Repository;
using GitGardens.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GitGardens
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Database context
            builder.Services.AddDbContext<GitGardensDBContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DatabaseConnection1"))
                );


            // Authentication + Authorization
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                });

            builder.Services.AddAuthorization();

            // Password hasher
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Garden
            builder.Services.AddScoped<IGardenService, GardenService>();
            builder.Services.AddScoped<IGardenRepository, GardenRepository>();

            //Garden Metrics
            builder.Services.AddScoped<IGardenMetricsRepository, GardenMetricsRepository>();
            builder.Services.AddScoped<IGardenMetricsService, GardenMetricsService>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            // MVC routig
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
