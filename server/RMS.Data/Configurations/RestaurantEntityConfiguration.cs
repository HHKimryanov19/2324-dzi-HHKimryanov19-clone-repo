using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RMS.Data.Models;

namespace RMS.Data.Configurations
{
    public class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        /// <summary>
        /// Configures main properties of restaurant entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder
                .Property(r => r.Name)
                .HasMaxLength(150)
                .IsRequired()
                .IsUnicode(true);

            builder
                .Property(r => r.ImageBytes)
                .IsRequired(false);

            builder
                .Property(r => r.Phone)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(r => r.DeliveryPrice)
                .IsRequired();

            builder
               .OwnsOne(r => r.Address);
        }
    }
}
