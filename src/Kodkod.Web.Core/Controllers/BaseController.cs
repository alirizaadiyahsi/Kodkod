using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Core.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
    }
}