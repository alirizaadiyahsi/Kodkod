using Kodkod.Web.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/test")]
    public class Test2Controller : BaseController
    {
        [HttpGet]
        public string Get() => "Hello world v2!";

        [HttpGet, MapToApiVersion("3.0")]
        public string GetV3() => "Hello world v3!";
    }
}