using AutoMapper;
using Kodkod.Application.Permissions;
using Kodkod.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Kodkod.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddKodkodApplication(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IPermissionAppService, PermissionAppService>();

            return services;
        }
    }
}
