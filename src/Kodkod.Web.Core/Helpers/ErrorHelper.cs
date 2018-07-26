using System.Collections.Generic;
using System.Linq;
using Kodkod.Application.Dto;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Web.Core.Helpers
{
    public static class ErrorHelper
    {
        public static List<NameValueDto> AddErrorsToModelState(IdentityResult identityResult)
        {
            return identityResult.Errors.Select(e => new NameValueDto {Name = e.Code, Value = e.Description}).ToList();
        }

        public static List<NameValueDto> AddErrorToModelState(string key, string description)
        {
            return new List<NameValueDto> { new NameValueDto { Name = key, Value = description } };
        }
    }
}
