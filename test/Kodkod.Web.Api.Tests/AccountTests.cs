using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kodkod.Application.Users.Dto;
using Kodkod.Utilities.PagedList;
using Kodkod.Web.Api.ViewModels;
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
            var responseLogin = await LoginAsApiUserAsync();
            Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
        }

        [Fact]
        public async Task TestGetToken()
        {
            var responseLogin = await LoginAsApiUserAsync();
            var loginResult = await responseLogin.Content.ReadAsAsync<LoginResult>();
            Assert.NotNull(loginResult.Token);
        }

        [Fact]
        public async Task TestAuthorizedAccess()
        {
            var responseLogin = await LoginAsApiUserAsync();
            var responseContent = await responseLogin.Content.ReadAsAsync<LoginResult>();
            var token = responseContent.Token;

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/test/WeatherForecasts/");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseGetUsers = await Client.SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var weatherForecasts = await responseGetUsers.Content.ReadAsAsync<IEnumerable<WeatherForecast>>();
            Assert.True(weatherForecasts.Any());
        }
    }
}