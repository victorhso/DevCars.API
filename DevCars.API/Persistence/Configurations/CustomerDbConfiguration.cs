using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class CustomerDbConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasMany(p => p.Pedido)
                 .WithOne(c => c.Client)
                 .HasForeignKey(p => p.idCustomer)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
