using System;
using System.Threading.Tasks;
using Kodkod.Application.Users;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.Utilities.PagedList;
using Kodkod.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    public class TestController : BaseController
    {
        private readonly IUserAppService _userAppService;

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
    }
}
