using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RMS.Data.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        /// <summary>
        /// Configures main properties of user entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(u => u.FirstName)
                .HasMaxLength(150)
                .IsRequired()
                .IsUnicode(true);

            builder
                .Property(u => u.LastName)
                .HasMaxLength(150)
                .IsRequired()
                .IsUnicode(true);

            builder
                .OwnsOne(u => u.Address);

            builder
                .HasOne(u => u.Restaurant)
                .WithMany(r => r.Employees)
                .HasForeignKey(u => u.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
