using DevCars.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DevCars.API.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.HasKey(c => c.Id);

            //modelBuilder.Entity<Carro>().ToTable("db_Carro");

            builder
                .Property(c => c.Marca)
                //.IsRequired()
                //.HasColumnName("Marca")
                .HasColumnType("VARCHAR(100)")
                .HasDefaultValue("Padrão")
                .HasMaxLength(100);

            builder
                .Property(c => c.DataProducao)
                .HasDefaultValueSql("getdate()");
        }
    }
}
