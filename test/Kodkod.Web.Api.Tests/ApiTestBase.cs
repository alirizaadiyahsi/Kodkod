using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kodkod.Utilities.Collections.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Kodkod.Web.Api.Tests
{
   public class ApiTestBase
    {
        protected readonly HttpClient Client;

        private static readonly Dictionary<string, string> TestUserFormData = new Dictionary<string, string>
        {
            {"email", "testuser@mail.com"},
            {"username", "testuser"},
            {"password", "123qwe"}
        };

        public ApiTestBase()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            var server = new TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .ConfigureAppConfiguration(config =>
                    {
                        config.SetBasePath(Path.GetFullPath(@"../../.."));
                        config.AddJsonFile("appsettings.json", false);
                    })
            );

            Client = server.CreateClient();
        }

        protected async Task<HttpResponseMessage> LoginAsTestUserAsync()
        {
            return await Client.PostAsync("/api/account/login",
                TestUserFormData.ToStringContent(Encoding.UTF8, "application/json"));
        }
    }
}
