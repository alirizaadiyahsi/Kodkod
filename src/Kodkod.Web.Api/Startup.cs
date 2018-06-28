using System;
using System.Text;
using Kodkod.Application;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
using Kodkod.Core.Users;
using Kodkod.EntityFramework;
using Kodkod.Web.Core.ActionFilters;
using Kodkod.Web.Core.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Kodkod.Web.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _signingKey;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            _signingKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]));

            _jwtTokenConfiguration = new JwtTokenConfiguration
            {
                Issuer = _configuration["Authentication:JwtBearer:Issuer"],
                Audience = _configuration["Authentication:JwtBearer:Audience"],
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(60),
            };
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KodkodDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<KodkodDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtTokenConfiguration>(config =>
            {
                config.Audience = _jwtTokenConfiguration.Audience;
                config.EndDate = _jwtTokenConfiguration.EndDate;
                config.Issuer = _jwtTokenConfiguration.Issuer;
                config.StartDate = _jwtTokenConfiguration.StartDate;
                config.SigningCredentials = _jwtTokenConfiguration.SigningCredentials;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtTokenConfiguration.Issuer,
                    ValidAudience = _jwtTokenConfiguration.Audience,
                    IssuerSigningKey = _signingKey
                };
            });

            services.AddAuthorization(options =>
            {
                foreach (var permission in PermissionConsts.AllPermissions())
                {
                    options.AddPolicy(permission.Name,
                        policy => policy.Requirements.Add(new PermissionRequirement(permission)));
                }
            });

            services.AddMvc(options => options.Filters.Add<KodkodDbContextActionFilter>());

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Kodkod API", Version = "v1" });
            });

            services.AddKodkodEntityFramework();
            services.AddKodkodApplication();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddScoped<KodkodDbContextActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kodkod API V1");
            });

            app.UseCors(builder =>
                builder.WithOrigins(_configuration["App:CorsOrigins"]
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)));

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
