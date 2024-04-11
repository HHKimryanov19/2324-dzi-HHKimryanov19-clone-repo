using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RMS.Data.Models;

namespace RMS.Data.Configurations
{
    internal class DishEntityConfiguration : IEntityTypeConfiguration<Dish>
    {
        /// <summary>
        /// Configures main properties of dish entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder
                .HasOne(d => d.Restaurant)
                .WithMany(r => r.Dishes)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(d => d.OrdersDishes)
                .WithOne(od => od.Dish)
                .HasForeignKey(od => od.DishId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(d => d.Title)
                .HasMaxLength(150)
                .IsRequired()
                .IsUnicode(true);

            builder
                .Property(d => d.Price)
                .IsRequired();

            builder
                .Property(d => d.Info)
                .HasMaxLength(300)
                .IsUnicode(true);

            builder
                .Property(d => d.Category)
                .IsRequired();

            builder
                .Property(d => d.ImageBytes)
                .IsRequired(false);
        }
    }
}
