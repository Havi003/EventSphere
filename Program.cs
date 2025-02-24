using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eventsphere.Areas.Identity.Data;
namespace Eventsphere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("EventsphereDBContextConnection") ?? throw new InvalidOperationException("Connection string 'EventsphereDBContextConnection' not found.");

            object value = builder.Services.AddDbContext<EventsphereDBContext>(options => options.UseSqlServer(connectionString));

            object value1 = builder.Services.AddDefaultIdentity<EventsphereUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<EventsphereDBContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
           
        }
    }
}
