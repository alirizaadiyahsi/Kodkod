using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Core.Controllers
{
    [Authorize(Policy = PermissionConsts.AccessApi_Name)]
    public class AuthorizedController : BaseController
    {

    }
}