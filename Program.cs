using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eventsphere.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
 // Import CoreAdmin namespace

namespace Eventsphere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get database connection string
            var connectionString = builder.Configuration.GetConnectionString("EventsphereDBContextConnection")
                ?? throw new InvalidOperationException("Connection string 'EventsphereDBContextConnection' not found.");

            // Configure Database Context
            builder.Services.AddDbContext<EventsphereDBContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure Identity
            builder.Services.AddDefaultIdentity<EventsphereUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                
                // Configure Two-Factor Authentication
                options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;

            }).AddEntityFrameworkStores<EventsphereDBContext>();

            // Add CoreAdmin Service
            builder.Services.AddCoreAdmin();

            // Add MVC and Razor Pages
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); // Ensure authentication middleware is enabled
            app.UseAuthorization();
       

           // Configure default routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}