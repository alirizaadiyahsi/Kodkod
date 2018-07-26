using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Core.Controllers
{
    [Authorize(Policy = PermissionConsts.ApiUser_Name)]
    public class AuthorizedController : BaseController
    {

    }
}