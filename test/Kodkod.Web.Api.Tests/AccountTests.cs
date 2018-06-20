using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kodkod.Application.Users.Dto;
using Kodkod.Utilities.PagedList;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Kodkod.Web.Api.Tests
{
    public class AccountTests : ApiTestBase
    {
        [Fact]
        public async Task TestUnAuthorizedAccess()
        {
            var responseGetUsers = await Client.GetAsync("/api/test/GetUsers");
            Assert.Equal(HttpStatusCode.Unauthorized, responseGetUsers.StatusCode);
        }

        [Fact]
        public async Task TestLogin()
        {
            var responseLogin = await LoginAsTestUserAsync();
            Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        }

        [Fact]
        public async Task TestGetToken()
        {
            var responseLogin = await LoginAsTestUserAsync();
            var okObjectResult = await responseLogin.Content.ReadAsAsync<OkObjectResult>();
            var jsonObject = JObject.Parse(okObjectResult.Value.ToString());
            Assert.NotNull((string)jsonObject["token"]);
        }

        [Fact]
        public async Task TestAuthorizedAccess()
        {
            var responseLogin = await LoginAsTestUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<OkObjectResult>();
            var responseJson = JObject.Parse(responseContent.Value.ToString());
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/test/GetUsers/");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await Client.SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListDto>>();
            Assert.True(users.Items.Count > 0);
        }
    }
}