using Microsoft.Extensions.DependencyInjection;

namespace Kodkod.EntityFramework
{
    public static class EntityFrameworkServiceExtensions
    {
        public static IServiceCollection AddSphinxEntityFramework(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<KodkodDbContext>();

            return services;
        }
    }
}