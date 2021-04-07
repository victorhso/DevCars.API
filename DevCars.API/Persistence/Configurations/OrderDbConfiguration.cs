using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class OrderDbConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasMany(i => i.ItensExtras)
                .WithOne()
                .HasForeignKey(e => e.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Car)
                .WithOne()
                .HasForeignKey<Pedido>(p => p.IdCar)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
