using System.Collections.Generic;
using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet("[action]")]
        [Authorize(Policy = PermissionConsts.ApiUser)]
        public IEnumerable<string> AuthorizedGet()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("[action]/{id}")]
        [Authorize(Policy = PermissionConsts.ApiUser)]
        public string AuthorizedGet(int id)
        {
            return "value";
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
