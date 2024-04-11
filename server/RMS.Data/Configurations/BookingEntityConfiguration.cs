using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RMS.Data.Models;

namespace RMS.Data.Configurations
{
    public class BookingEntityConfiguration : IEntityTypeConfiguration<Booking>
    {
        /// <summary>
        /// Configures main properties of booking entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            builder
               .HasOne(b => b.Restaurant)
               .WithMany(r => r.Bookings)
               .HasForeignKey(b => b.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(b => b.IsInside)
                .IsRequired();

            builder
               .Property(b => b.NumberOfPeople)
               .IsRequired();

            builder
               .Property(b => b.Date)
               .IsRequired();
        }
    }
}
