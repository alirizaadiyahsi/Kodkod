using AutoMapper;
using Kodkod.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Kodkod.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddKodkodApplication(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddTransient<IUserApplicationService, UserApplicationService>();

            return services;
        }
    }
}
