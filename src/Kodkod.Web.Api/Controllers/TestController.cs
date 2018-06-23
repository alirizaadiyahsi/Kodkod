using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kodkod.Application.Users;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.Utilities.PagedList;
using Kodkod.Web.Api.ViewModels;
using Kodkod.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    public class TestController : BaseController
    {
        private readonly IUserAppService _userAppService;
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public TestController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("[action]")]
        [Authorize(Policy = PermissionConsts.ApiUser)]
        public async Task<IPagedList<UserListDto>> GetUsers()
        {
            return await _userAppService.GetUsersAsync(new GetUsersInput());
        }

        [HttpGet("[action]/{username}")]
        [Authorize(Policy = PermissionConsts.ApiUser)]
        public ActionResult<User> GetUser(string userName)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = userName
            };
        }

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }       
    }
}
