using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Kodkod.Core.Users;
using Kodkod.Web.Api.ViewModels;
using Kodkod.Web.Core.Authentication;
using Kodkod.Web.Core.Controllers;
using Kodkod.Web.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kodkod.Web.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;

        public AccountController(
            UserManager<User> userManager,
            IOptions<JwtTokenConfiguration> jwtTokenConfiguration)
        {
            _userManager = userManager;
            _jwtTokenConfiguration = jwtTokenConfiguration.Value;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<LoginResult>> Login([FromBody]LoginViewModel loginViewModel)
        {
            var userToVerify = await GetClaimsIdentity(loginViewModel.UserName, loginViewModel.Password);
            if (userToVerify == null)
            {
                return BadRequest(new
                {
                    ErrorMessage = "The user name or password is incorrect. Try again."
                });
            }

            var token = new JwtSecurityToken
            (
                issuer: _jwtTokenConfiguration.Issuer,
                audience: _jwtTokenConfiguration.Audience,
                claims: userToVerify.Claims,
                expires: _jwtTokenConfiguration.EndDate,
                notBefore: _jwtTokenConfiguration.StartDate,
                signingCredentials: _jwtTokenConfiguration.SigningCredentials
            );

            return Ok(new LoginResult { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<OkObjectResult>> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return new BadRequestObjectResult(ErrorHelper.AddErrorToModelState("EmailAlreadyExist", "This email already exists!", ModelState));
            }

            var applicationUser = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(ErrorHelper.AddErrorsToModelState(result, ModelState));
            }

            return new OkObjectResult("Account created");
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });
            }

            return null;
        }
    }
}