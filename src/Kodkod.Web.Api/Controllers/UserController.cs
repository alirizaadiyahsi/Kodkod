using System.Threading.Tasks;
using Kodkod.Application.Users;
using Kodkod.Application.Users.Dto;
using Kodkod.Utilities.PagedList;
using Kodkod.Web.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    //todo: comment out AuthorizedController after implementation of vue login
    public class UserController : BaseController//AuthorizedController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        //todo: comment out [Authorize(Policy = PermissionConsts.ApiUserPermissionName)] after implementation of vue login
        [HttpGet("[action]")]
        //[Authorize(Policy = PermissionConsts.ApiUserPermissionName)]
        public async Task<IPagedList<UserListDto>> Users(UserListInput input)
        {
            return await _userAppService.GetUsersAsync(input);
        }   
    }
}
