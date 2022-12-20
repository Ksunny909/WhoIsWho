using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WhoIsWho.Models.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
