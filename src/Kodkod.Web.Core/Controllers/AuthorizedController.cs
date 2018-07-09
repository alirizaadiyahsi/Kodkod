using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Core.Controllers
{
    [Authorize(Policy = PermissionConsts.ApiUserPermissionName)]
    public class AuthorizedController : BaseController
    {

    }
}