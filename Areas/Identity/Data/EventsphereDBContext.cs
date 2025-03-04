using Eventsphere.Areas.Identity.Data;
using Eventsphere.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eventsphere.Areas.Identity.Data;

public class EventsphereDBContext : IdentityDbContext<EventsphereUser>
{
    public EventsphereDBContext(DbContextOptions<EventsphereDBContext> options)
        : base(options)
    {
    }

    public DbSet<HelpCenter> HelpCenter { get; set; } //Adds help center table

    public DbSet<FormEvent> EventsFormed { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
