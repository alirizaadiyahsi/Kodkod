using System.Collections.Generic;
using Kodkod.Application.Dto;

namespace Kodkod.Web.Api.ViewModels
{
    public class ErrorResult
    {
        public List<NameValueDto> Errors { get; set; }
    }
}
