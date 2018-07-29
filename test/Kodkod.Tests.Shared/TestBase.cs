using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kodkod.Application;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
using Kodkod.Core.Users;
using Kodkod.EntityFramework;
using Kodkod.Utilities.Collections.Dictionary.Extensions;
using Kodkod.Web.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kodkod.Tests.Shared
{
    public class TestBase
    {
        private static Dictionary<string, string> _apiUserFormData;

        protected readonly IMapper Mapper;
        protected readonly KodkodDbContext KodkodInMemoryContext;
        protected readonly ClaimsPrincipal ContextUser;
        protected readonly HttpClient Client;
        protected async Task<HttpResponseMessage> LoginAsApiUserAsync()
        {
            return await Client.PostAsync("/api/account/login",
                _apiUserFormData.ToStringContent(Encoding.UTF8, "application/json"));
        }

        public TestBase()
        {
            Mapper = new Mapper(
                new MapperConfiguration(
                    configure => { configure.AddProfile<ApplicationMappingProfile>(); }
                )
            );
            Client = GetTestServer();

            _apiUserFormData = new Dictionary<string, string>
            {
                {"email",  UserConsts.ApiEmail},
                {"username",  UserConsts.ApiUserName},
                {"password", "123qwe"}
            };

            KodkodInMemoryContext = GetInitializedDbContext();
            ContextUser = GetContextUser();
        }

        public static readonly UserRole AdminUserRole = new UserRole
        {
            RoleId = RoleConsts.AdminRole.Id,
            UserId = UserConsts.AdminUser.Id
        };

        public static readonly UserRole TestUserRole = new UserRole
        {
            RoleId = RoleConsts.ApiUserRole.Id,
            UserId = UserConsts.ApiUser.Id
        };

        public static readonly RolePermission AdminRolePermission = new RolePermission
        {
            PermissionId = PermissionConsts.Permission_ApiAccess.Id,
            RoleId =RoleConsts.AdminRole.Id
        };

        public static readonly RolePermission ApiUserRolePermission = new RolePermission
        {
            PermissionId = PermissionConsts.Permission_ApiAccess.Id,
            RoleId = RoleConsts.ApiUserRole.Id
        };

        public static readonly List<User> AllTestUsers = new List<User>
        {
            new User {UserName = "A",Email="a@mail.com"},
            new User {UserName = "B",Email="b@mail.com"},
            new User {UserName = "C",Email="c@mail.com"},
            new User {UserName = "D",Email="d@mail.com"},
            UserConsts.AdminUser,
            UserConsts.ApiUser
        };

        public static KodkodDbContext GetEmptyDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KodkodDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();

            var inMemoryContext = new KodkodDbContext(optionsBuilder.Options);

            return inMemoryContext;
        }

        public static KodkodDbContext GetInitializedDbContext()
        {
            var inMemoryContext = GetEmptyDbContext();

            inMemoryContext.AddRange(PermissionConsts.AllPermissions());
            inMemoryContext.AddRange(RoleConsts.AdminRole, RoleConsts.ApiUserRole);
            inMemoryContext.AddRange(AllTestUsers);
            inMemoryContext.AddRange(AdminUserRole, TestUserRole);
            inMemoryContext.AddRange(AdminRolePermission, ApiUserRolePermission);

            inMemoryContext.SaveChanges();

            return inMemoryContext;
        }

        public static ClaimsPrincipal GetContextUser()
        {
            return new ClaimsPrincipal(
                  new ClaimsIdentity(
                      new List<Claim>
                      {
                        new Claim(ClaimTypes.Name,  UserConsts.ApiUser.UserName)
                      },
                      "TestAuthenticationType"
                  )
              );
        }

        private static HttpClient GetTestServer()
        {
            var server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration(config =>
                    {
                        config.SetBasePath(Path.Combine(Path.GetFullPath(@"../../../.."), "Kodkod.Tests.Shared"));
                        config.AddJsonFile("appsettings.json", false);
                    })
            );

            return server.CreateClient();
        }
    }
}
