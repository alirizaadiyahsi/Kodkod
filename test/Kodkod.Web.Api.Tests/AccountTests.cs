using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Kodkod.Web.Api.Tests
{
    public class AccountTests
    {
        private readonly HttpClient _client;
        public AccountTests()

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

            _client = server.CreateClient();
        }

        [Fact]
        public async Task TestUnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/test/AuthorizedGet");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task TestGetToken()
        {
            const string bodyString = @"{username: ""testuser"", password: ""123qwe""}";
            var response = await _client.PostAsync("/api/account/login", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            Assert.NotNull((string)responseJson["token"]);
        }

        [Fact]
        public async Task TestAuthorizedAccess()
        {
            const string bodyString = @"{username: ""testuser"", password: ""123qwe""}";
            var response = await _client.PostAsync("/api/account/login", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/test/AuthorizedGet/5");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var getValueResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, getValueResponse.StatusCode);

            var getValueResponseString = await getValueResponse.Content.ReadAsStringAsync();
            Assert.True(getValueResponseString == "value");
        }
    }
}
