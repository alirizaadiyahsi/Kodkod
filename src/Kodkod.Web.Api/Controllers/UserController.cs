using System.Threading.Tasks;
using Kodkod.Application.Users;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Utilities.PagedList;
using Kodkod.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    [Authorize(Policy = PermissionConsts.Admin_UserList_Name)]
    public class UserController : AuthorizedController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet("[action]")]
                public async Task<ActionResult<IPagedList<UserListDto>>> Users(UserListInput input)
        {
            return Ok(await _userAppService.GetUsersAsync(input));
        }
    }
}
