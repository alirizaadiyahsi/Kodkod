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
        private static Dictionary<string, string> _testUserFormData;

        protected readonly IMapper Mapper;
        protected readonly KodkodDbContext KodkodInMemoryContext;
        protected readonly ClaimsPrincipal ContextUser;
        protected readonly HttpClient Client;
        protected async Task<HttpResponseMessage> LoginAsTestUserAsync()
        {
            return await Client.PostAsync("/api/account/login",
                _testUserFormData.ToStringContent(Encoding.UTF8, "application/json"));
        }

        public TestBase()
        {
            //disable automapper static registration
            ServiceCollectionExtensions.UseStaticRegistration = false;

            Mapper = new Mapper(
                new MapperConfiguration(
                    configure => { configure.AddProfile<ApplicationMappingProfile>(); }
                )
            );
            Client = GetTestServer();

            _testUserFormData = new Dictionary<string, string>
            {
                {"email", "testuser@mail.com"},
                {"username", "testuser"},
                {"password", "123qwe"}
            };

            KodkodInMemoryContext = GetInitializedDbContext();
            ContextUser = GetContextUser();
        }

        public static readonly User AdminUser = new User
        {
            Id = new Guid("C41A7761-6645-4E2C-B99D-F9E767B9AC77"),
            UserName = "admin",
            Email = "admin@mail.com"
        };

        public static readonly User TestUser = new User
        {
            Id = new Guid("065E903E-6F7B-42B8-B807-0C4197F9D1BC"),
            UserName = "testuser",
            Email = "testuser@mail.com"
        };

        public static readonly Role AdminRole = new Role
        {
            Id = new Guid("F22BCE18-06EC-474A-B9AF-A9DE2A7B8263"),
            Name = "Admin"
        };

        public static readonly Role MemberRole = new Role
        {
            Id = new Guid("11D14A89-3A93-4D39-A94F-82B823F0D4CE"),
            Name = "Member"
        };

        public static readonly Permission ApiUserPermission = new Permission
        {
            Id = new Guid("41F04B93-8C0E-4AC2-B6BA-63C052A2F02A"),
            Name = "ApiUser",
            DisplayName = "Api user"
        };

        public static readonly UserRole AdminUserRole = new UserRole
        {
            RoleId = AdminRole.Id,
            UserId = AdminUser.Id
        };

        public static readonly UserRole TestUserRole = new UserRole
        {
            RoleId = MemberRole.Id,
            UserId = TestUser.Id
        };

        public static readonly RolePermission AdminRolePermission = new RolePermission
        {
            PermissionId = ApiUserPermission.Id,
            RoleId = AdminRole.Id
        };

        public static readonly RolePermission MemberRolePermission = new RolePermission
        {
            PermissionId = ApiUserPermission.Id,
            RoleId = MemberRole.Id
        };

        public static readonly List<User> AllTestUsers = new List<User>
        {
            new User {UserName = "A",Email="a@mail.com"},
            new User {UserName = "B",Email="b@mail.com"},
            new User {UserName = "C",Email="c@mail.com"},
            new User {UserName = "D",Email="d@mail.com"},
            AdminUser,
            TestUser
        };

        public static KodkodDbContext GetEmptyDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KodkodDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.UseLazyLoadingProxies();

            var inMemoryContext = new KodkodDbContext(optionsBuilder.Options);

            return inMemoryContext;
        }

        public static KodkodDbContext GetInitializedDbContext()
        {
            var inMemoryContext = GetEmptyDbContext();

            inMemoryContext.AddRange(ApiUserPermission);
            inMemoryContext.AddRange(AdminRole, MemberRole);
            inMemoryContext.AddRange(AllTestUsers);
            inMemoryContext.AddRange(AdminUserRole, TestUserRole);
            inMemoryContext.AddRange(AdminRolePermission, MemberRolePermission);

            inMemoryContext.SaveChanges();

            return inMemoryContext;
        }

        public static ClaimsPrincipal GetContextUser()
        {
            return new ClaimsPrincipal(
                  new ClaimsIdentity(
                      new List<Claim>
                      {
                        new Claim(ClaimTypes.Name, TestUser.UserName)
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
