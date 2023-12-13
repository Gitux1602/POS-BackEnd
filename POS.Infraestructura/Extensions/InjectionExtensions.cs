using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infraestructura.Persistences.Contexts;
using POS.Infraestructura.Persistences.Interfaces;
using POS.Infraestructura.Persistences.Repositories;

namespace POS.Infraestructura.Extensions
{
    public static class InjectionExtensions
    {
        public static object AddInjectionInfraestructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var assembly = typeof(POSContext).Assembly.FullName;

            //Uso de los servicios iyectar DbContext
            services.AddDbContext<POSContext>
                (
                   options => options.UseSqlServer(
                   configuration.GetConnectionString("POSConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}