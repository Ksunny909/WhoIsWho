using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhoIsWho.Models.Entities;

namespace WhoIsWho.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public virtual DbSet<Booking> Bookings { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
