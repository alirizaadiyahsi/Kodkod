using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kodkod.EntityFramework;
using Kodkod.Tests.Shared;
using Kodkod.Utilities.Collections.Dictionary.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Kodkod.Web.Api.Tests
{
    public class ApiTestBase : TestBase
    {
        protected readonly HttpClient Client;
        protected readonly KodkodDbContext KodkodInMemoryContext = GetInitializedDbContext();
        private static Dictionary<string, string> _testUserFormData;

        public ApiTestBase()
        {
            _testUserFormData = new Dictionary<string, string>
            {
                {"email", "testuser@mail.com"},
                {"username", "testuser"},
                {"password", "123qwe"}
            };

            //if this is true, Automapper is throwing exception
            ServiceCollectionExtensions.UseStaticRegistration = false;

            Client = GetTestServer();
        }

        protected async Task<HttpResponseMessage> LoginAsTestUserAsync()
        {
            return await Client.PostAsync("/api/account/login",
                _testUserFormData.ToStringContent(Encoding.UTF8, "application/json"));
        }

        private static HttpClient GetTestServer()
        {
            var server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration(config =>
                    {
                        config.SetBasePath(Path.GetFullPath(@"../../.."));
                        config.AddJsonFile("appsettings.json", false);
                    })
            );

            return server.CreateClient();
        }
    }
}
