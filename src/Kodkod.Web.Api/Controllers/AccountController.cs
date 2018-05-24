using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Kodkod.Core.AppConsts;
using Kodkod.Core.Entities;
using Kodkod.Web.Api.Authentication;
using Kodkod.Web.Api.Helpers;
using Kodkod.Web.Api.ViewModels;
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
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userToVerify = await GetClaimsIdentity(loginViewModel.UserName, loginViewModel.Password);
            if (userToVerify == null)
            {
                return Unauthorized();
            }

            var token = new JwtSecurityToken
            (
                issuer: _jwtTokenConfiguration.Issuer,
                audience: _jwtTokenConfiguration.Audience,
                claims: userToVerify.Claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: _jwtTokenConfiguration.SigningCredentials
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(KodkodClaimTypes.ApiUserRole, KodkodClaimValues.ApiAccess)
                });
            }

            return null;
        }
    }
}