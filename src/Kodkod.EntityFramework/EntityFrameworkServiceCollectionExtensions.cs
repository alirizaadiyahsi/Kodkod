using Kodkod.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Kodkod.EntityFramework
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddKodkodEntityFramework(this IServiceCollection services)
        {
            //todo: add conventional registrar
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<KodkodDbContext>();

            return services;
        }
    }
}