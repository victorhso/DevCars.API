using DevCars.API.Entities;
using DevCars.API.Persistence.Configurations;
using DevCars.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace DevCars.API.Persistence
{
    public class DevCarsDbContext : DbContext
    {
        public DevCarsDbContext(DbContextOptions<DevCarsDbContext> options) : base(options)
        {

        }

        internal object Select(Func<object, CarItemViewModel> p)
        {
            throw new NotImplementedException();
        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItensPedidoExtra> ItensPedidosExtras { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CarDbConfiguration());

            //modelBuilder.ApplyConfiguration(new CustomerDbConfiguration());

            //modelBuilder.ApplyConfiguration(new OrderDbConfiguration());

            //modelBuilder.ApplyConfiguration(new ExtraOrderItemsConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
