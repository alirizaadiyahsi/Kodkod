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
        [Authorize(Policy = PermissionConsts.Name_UserList)]
        public async Task<ActionResult<IPagedList<UserListDto>>> Users()
        {
            return Ok(await _userAppService.GetUsersAsync(new UserListInput()));
        }

        [HttpGet("[action]/{username}")]
        [Authorize(Policy = PermissionConsts.Name_UserList)]
        public ActionResult<User> Users(string userName)
        {
            return Ok(new User
            {
                Id = Guid.NewGuid(),
                UserName = userName
            });
        }

        [HttpGet("[action]")]
        [Authorize(Policy = PermissionConsts.Name_ApiAccess)]
        public ActionResult<IEnumerable<WeatherForecast>> WeatherForecasts()
        {
            var rng = new Random();

            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }));
        }
    }
}
