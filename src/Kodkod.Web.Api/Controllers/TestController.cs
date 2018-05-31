using System;
using System.Collections.Generic;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodkod.Web.Api.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet("[action]")]
        [Authorize(Policy = PermissionConsts.ApiUser)]
        public ActionResult<List<User>> GetUsers()
        {
            var userList = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "test_user_1"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "test_user_2"
                }
            };

            return userList;
        }

        [HttpGet("[action]/{username}")]
        [Authorize(Policy = PermissionConsts.ApiUser)]
        public ActionResult<User> GetUser(string userName)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                UserName = userName
            };
        }
    }
}
