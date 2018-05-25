using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
    }

    [Authorize(Policy = PermissionsConsts.ApiUser)]
    public class AuthorizedController : BaseController
    {

    }
}