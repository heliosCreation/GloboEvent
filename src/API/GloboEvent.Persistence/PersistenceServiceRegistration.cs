using GloboEvent.Application.Contrats.Persistence;
using GloboEvent.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GloboEvent.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GloboEventDbContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("GloboEventDataConnectionString"),
                b => b.MigrationsAssembly(typeof(GloboEventDbContext).Assembly.FullName))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            return services;

        }
    }
}
