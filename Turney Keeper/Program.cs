using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Turney_Keeper.Data;

namespace Turney_Keeper
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/User/Create";

            });
            builder.Services.AddDbContext<Turney_KeeperContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Turney_KeeperContext") ?? throw new InvalidOperationException("Connection string 'Turney_KeeperContext' not found.")));

            builder.Services.AddDbContext<Turney_KeeperContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Turney_KeeperContext")).EnableSensitiveDataLogging());

            builder.Services.AddRazorPages();
            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.MapRazorPages();

            app.Run();
        }
    }
}