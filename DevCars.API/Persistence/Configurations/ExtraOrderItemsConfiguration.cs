using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.API.Persistence.Configurations
{
    public class ExtraOrderItemsConfiguration : IEntityTypeConfiguration<ItensPedidoExtra>
    {
        public void Configure(EntityTypeBuilder<ItensPedidoExtra> builder)
        {
            builder.HasKey(i => i.Id);
        }
    }
}
