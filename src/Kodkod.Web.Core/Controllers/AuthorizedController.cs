using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Core.Controllers
{
    [Authorize(Policy = PermissionConsts.Name_ApiAccess)]
    public class AuthorizedController : BaseController
    {

    }
}