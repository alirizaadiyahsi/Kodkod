using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Kodkod.Web.Api.Tests
{
    public class AccountTests : ApiTestBase
    {
        [Fact]
        public async Task TestUnAuthorizedAccessAsync()
        {
            var response = await Client.GetAsync("/api/test/AuthorizedGet");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task TestLoginAsync()
        {
            var response = await LoginAsTestUserAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task TestGetTokenAsync()
        {
            var response = await LoginAsTestUserAsync();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            Assert.NotNull((string)responseJson["token"]);
        }

        [Fact]
        public async Task TestAuthorizedAccessAsync()
        {
            var response = await LoginAsTestUserAsync();
            var responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/test/AuthorizedGet/5");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var getValueResponse = await Client.SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, getValueResponse.StatusCode);

            var getValueResponseString = await getValueResponse.Content.ReadAsStringAsync();
            Assert.True(getValueResponseString == "value");
        }
    }
}